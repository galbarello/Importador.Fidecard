using System;
using System.Configuration;
using System.Collections.Specialized;
using FileProccesor.Dtos;
using FileProccesor.Schemes;
using FileProccesor.Services.Helpers;
using log4net;
using System.Reflection;


namespace FileProccesor.Services
{
    public class Cambio : Importar
    {
        private static readonly NameValueCollection Section =
        (NameValueCollection)ConfigurationManager.GetSection("Empresas");

        public static readonly int Empresa = Convert.ToInt32(Section["Cambio"]);

        private static readonly ILog Log
           = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void Persistir(string file)
        {
            try
            {
                foreach (var archivo in HelperAggregator.Agrupar(FileProcessor<CambioFile>.GetData(file)))
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
                        Coeficiente = "0100",
                        Secretaria = string.Empty,
                        Programa = ""
                    };
                    consumoDb.Save();
                }
                base.Persistir(file);
            }
            catch (Exception ex)
            { 
            }
        }
    }
}