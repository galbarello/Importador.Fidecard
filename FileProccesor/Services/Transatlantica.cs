using System;
using System.Configuration;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;
using FileProccesor.Schemes;

namespace FileProccesor.Services
{
    public class Transatlantica : Importar
    {
        public static readonly int Empresa = 
            Convert.ToInt32(ConfigurationManager.AppSettings["CodigoEmpresa"]);

        public override void Persistir(string file)
        {
            foreach (var archivo in HelperAggregator.Agrupar(FileProcessor<TransatlanticaFile>.GetData(file)))
            {
                using (var transac = new TransactionScope())
                    try
                    {
                        var parentesco = archivo.Consumo[0].TipoCliente == "Corporativo" ? Parentesco.Titular : Parentesco.Empleado;
                        var cuenta = new CuentaCorrienteDto
                        {
                            FechaCompra = archivo.Consumo[0].FechaHoraComprobante.Date,
                            HoraCompra = DateTime.Now,
                            Key = new KeyCuenta
                            {
                                CodEmpresa = Empresa,
                                NumeroComprobante = archivo.Consumo[0].NroComprobante
                            },
                            MontoCompra = archivo.Consumo[0].ImportePesosNetoImpuestos,
                            Movimiento = EnumMovimientos.SumaPuntos,
                            NumeroDocumento =
                                HelperPersona.GetPersona(archivo.Consumo[0].Cuit, archivo.Consumo[0].TipoCliente, archivo.Consumo[0].RazonSocial, archivo.Consumo[0].NombrePersona, archivo.Consumo[0].NroDocumento, Empresa),
                            NumeroCuenta = HelperCuenta.GetCuenta(archivo.Consumo[0].Cuit, archivo.Consumo[0].NroDocumento, Empresa, parentesco),
                            Puntos = HelperPuntos.GetPuntos(archivo.Consumo[0].FechaHoraComprobante, archivo.ImportePesosNetoImpuestos),
                            Sucursal = HelperSucursal.GetSucursal(),
                            
                                             Usuario = "web"
                                         };
                        cuenta.Save();
                        
                        var consumoDb = new ConsumoDto
                        {
                            Cuit = archivo.Consumo[0].Cuit,
                            FechaHoraComprobante = archivo.Consumo[0].FechaHoraComprobante,
                            ImportePesosNetoImpuestos = archivo.ImportePesosNetoImpuestos,
                            NombrePersona = archivo.Consumo[0].NombrePersona,
                            NroComprobante = archivo.NroComprobante,
                            NroDocumento = archivo.Consumo[0].NroDocumento,
                            RazonSocial = archivo.Consumo[0].RazonSocial,
                            TipoCliente = archivo.Consumo[0].TipoCliente
                        };
                        consumoDb.Save();
                        transac.VoteCommit();
                    }
                    catch (Exception ex)
                    {
                        transac.VoteRollBack();
                    }
            }
            base.Persistir(file);
        }
    }
}