using System;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;
using FileProccesor.Services.Helpers;

namespace FileProccesor.Services
{
    public static class WorkflowFidecard
        {
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
                                Movimiento = EnumMovimientos.SumaPuntos,
                                NumeroDocumento = documento,
                                NumeroCuenta = cliente,
                                Puntos = HelperPuntos.GetPuntos(archivo.FechaHoraComprobante, archivo.ImportePesosNetoImpuestos),
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
                            transac.VoteRollBack();
                        }
                }
            }
        }
    }
