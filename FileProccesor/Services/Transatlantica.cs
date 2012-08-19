using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using FileProccesor.Dtos;
using FileProccesor.Schemes;
using FileProccesor.Services.Helpers;
using log4net;

namespace FileProccesor.Services
{
    public class Transatlantica : Importar
    {
        private static readonly NameValueCollection Section =
        (NameValueCollection)ConfigurationManager.GetSection("Empresas");
        private static readonly ILog Log
           = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly int Empresa = Convert.ToInt32(Section["Ola"]);

        public override void Persistir(string file)
        {
            try
            {
                foreach (var archivo in HelperAggregator.Agrupar(FileProcessor<TransatlanticaFile>.GetData(file)))
                {
                    var consumoDb = new ConsumoDto
                                        {
                                            Cuit = archivo.Consumo[0].Cuit,
                                            FechaHoraComprobante = archivo.Consumo[0].FechaHoraComprobante,
                                            ImportePesosNetoImpuestos = archivo.ImportePesosNetoImpuestos,
                                            NombrePersona = archivo.Consumo[0].NombrePersona,
                                            NroComprobante = archivo.NroComprobante,
                                            NroDocumento = archivo.Consumo[0].NroDocumento,
                                            RazonSocial = archivo.Consumo[0].RazonSocial,
                                            TipoCliente = archivo.Consumo[0].TipoCliente,
                                            Archivo = file,
                                            Empresa = Empresa,
                                            Coeficiente = archivo.Consumo[0].Coeficiente,
                                            Secretaria = archivo.Consumo[0].Secretaria,
                                            Programa = archivo.Consumo[0].Programa
                                        };
                    consumoDb.Save();
                }
                base.Persistir(file);
            }
            catch (Exception ex)
            {            
                Log.Fatal(ex);
            }
        }
    }
}