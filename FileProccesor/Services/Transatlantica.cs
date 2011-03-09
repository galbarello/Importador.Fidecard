using System;
using System.Configuration;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Schemes;
using System.Linq;

namespace FileProccesor.Services
{
    public class Transatlantica : Importar
    {
        public override void Persistir(string file)
        {
            var consumos = from consumo in FileProcessor<TransatlanticaFile>.GetData(file)
                           group consumo by consumo.NroComprobante
                           into unico
                               select new {
                                              NroComprobante = unico.Key,
                                              Consumo=unico.ToList(),
                                              ImportePesosNetoImpuestos = unico.Sum(x => x.ImportePesosNetoImpuestos)
                                          };

           
                foreach (var archivo in consumos)
                    {
                        using (var transac = new TransactionScope())
                            try
                            {
                                if (archivo.Consumo[0].TipoCliente == "Corporativo")
                                {
                                    var empresa = HelperPersona.GetPersona(archivo.Consumo[0].Cuit,
                                                                           archivo.Consumo[0].RazonSocial, "",
                                                                           Convert.ToInt32(
                                                                               ConfigurationManager.AppSettings[
                                                                                   "CodigoEmpresa"]));
                                }
                                else
                                {
                                    var nombre = archivo.Consumo[0].NombrePersona.Split(new[] {" "},
                                                                                        StringSplitOptions.
                                                                                            RemoveEmptyEntries);
                                    var apellido = nombre.Last();
                                    var nombrePila = nombre[0];
                                    var persona = HelperPersona.GetPersona(archivo.Consumo[0].Cuit, nombrePila, apellido,
                                                                           Convert.ToInt32(
                                                                               ConfigurationManager.AppSettings[
                                                                                   "CodigoEmpresa"]));
                                }

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
                            finally
                            {
                                base.Persistir(file);
                            }
                    }

        }
    }
}