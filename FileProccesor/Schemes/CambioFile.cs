using System;
using FileHelpers;
using FileProccesor.Dtos;

namespace FileProccesor.Schemes
{
    [DelimitedRecord(";")]
    [IgnoreFirst]
    public class CambioFile : IFileSpec
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

        public DateTime FechaHoraComprobante;

        public string Origen;

        public string TipoCliente;

        [FieldConverter(typeof(MoneyConverter))]
        public double ImportePesosNetoImpuestos;


        public void Save()
        {
            ((ConsumoDto)(this)).Save();
        }
    }
}