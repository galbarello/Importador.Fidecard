using System;
using FileHelpers;
using FileProccesor.Dtos;

namespace FileProccesor.Schemes
{
    [DelimitedRecord(";")]
    [IgnoreFirst]
    public class TransatlanticaFile:IFileSpec
    {

        /// <summary>
        /// CUIT_Empresa;Nombre_Empresa;Nro_Documento;Persona;Clave_Referencia;Fecha_Comprobante;Origen;Tipo_Cliente;Importe_Neto
        /// </summary>
        public string Cuit;

        public string RazonSocial;

        [FieldNullValue("")]
        public string NroDocumento;

        [FieldNullValue("")]
        public string NombrePersona;

        public string NroComprobante;

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime FechaHoraComprobante;

        public string Origen;

        public string TipoCliente;

        public string Secretaria;

        public string Coeficiente;

        [FieldConverter(typeof(MoneyConverter))] 
        public double ImportePesosNetoImpuestos;

        public string Programa;      

        
        public void Save()
        {
           ((ConsumoDto)(this)).Save();
        }
    }
}