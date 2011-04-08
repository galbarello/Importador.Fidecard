using System;
using System.Reflection;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;
using FileProccesor.Services.Helpers;
using log4net;

namespace FileProccesor.Services
{
    public static class WorkflowFidecard
    {
        private static readonly ILog Log
           = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void Registro()
        {
            foreach (var archivo in ActiveRecordBase<ConsumoDto>.FindAllByProperty("Procesado", false))
            {
                var documento = HelperPersona.GetPersona(
                    archivo.Cuit, archivo.TipoCliente,
                    archivo.RazonSocial, archivo.NombrePersona,
                    archivo.NroDocumento, archivo.Empresa);

                    var cliente = HelperCuenta.GetCuenta(
                        archivo.Cuit, archivo.NroDocumento, archivo.Empresa);

                    using (var transac = new TransactionScope())
                        try
                        {
                            var puntos = HelperPuntos.GetPuntos(archivo.Empresa, archivo.FechaHoraComprobante,
                                                            archivo.ImportePesosNetoImpuestos);
                           
                            var cuenta = new CuentaCorrienteDto
                            {
                                FechaCompra = archivo.FechaHoraComprobante.Date,
                                HoraCompra = DateTime.Now,
                                Key = new KeyCuenta
                                {
                                    CodEmpresa = archivo.Empresa,
                                    NumeroComprobante = archivo.NroComprobante
                                },
                                MontoCompra = archivo.ImportePesosNetoImpuestos,
                                Movimiento = puntos>=0 ? EnumMovimientos.SumaPuntos : EnumMovimientos.AnulaPuntos,
                                NumeroDocumento = documento,
                                NumeroCuenta = cliente,
                                Puntos = Math.Abs(puntos),
                                Sucursal = HelperSucursal.GetSucursal(),
                                Usuario = "web"
                            };
                            cuenta.Save();
                            archivo.Procesado = true;
                            archivo.Save();
                            transac.VoteCommit();
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            transac.VoteRollBack();
                        }
                }
            }
        }
    }
