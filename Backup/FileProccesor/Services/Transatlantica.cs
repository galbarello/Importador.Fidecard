using System;
using System.Collections.Specialized;
using System.Configuration;
using FileProccesor.Dtos;
using FileProccesor.Schemes;
using FileProccesor.Services.Helpers;

namespace FileProccesor.Services
{
    public class Transatlantica : Importar
    {
        private static readonly NameValueCollection Section =
        (NameValueCollection)ConfigurationManager.GetSection("Empresas");

        public static readonly int Empresa = Convert.ToInt32(Section["Ola"]);

        public override void Persistir(string file)
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
                                        Empresa=Empresa
                                    };
                consumoDb.Save();
            }
            base.Persistir(file);
        }
    }
}