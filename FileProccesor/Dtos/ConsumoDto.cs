using System;
using Castle.ActiveRecord;
using FileProccesor.Schemes;

namespace FileProccesor.Dtos
{
    [ActiveRecord("registros_archivos")]
    public class ConsumoDto : ActiveRecordBase<ConsumoDto>
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Property]
        public string Cuit { get; set; }
       
        [Property]
        public string RazonSocial { get; set; }
       
        [Property]
        public string NroDocumento { get; set; }
        
        [Property]
        public string NombrePersona { get; set; }
        
        [Property]
        public string TipoCliente { get; set; }
        
        [Property]
        public string NroComprobante { get; set; }
        
        [Property]
        public DateTime FechaHoraComprobante { get; set; }
        
        [Property]
        public double ImportePesosNetoImpuestos { get; set; }

        [Property]
        public bool Procesado { get; set; }

        [Timestamp]
        public DateTime LastOperation { get; set; }

        [Property]
        public string Archivo { get; set; }

        [Property]
        public int Empresa{ get; set; }

        [Property]
        public string Secretaria { get; set; }

        [Property]
        public string Coeficiente { get; set; }

        [Property]
        public string Programa { get; set; }



        public static implicit operator ConsumoDto(TransatlanticaFile file)
        {

            return new ConsumoDto
                       {
                           Cuit = file.Cuit,
                           FechaHoraComprobante = file.FechaHoraComprobante,
                           ImportePesosNetoImpuestos = file.ImportePesosNetoImpuestos,
                           NombrePersona = file.NombrePersona,
                           NroComprobante = file.NroComprobante,
                           NroDocumento = file.NroDocumento,
                           RazonSocial = file.RazonSocial,
                           TipoCliente = file.TipoCliente,
                           Procesado = false,
                           Archivo = "",
                           Coeficiente=file.Coeficiente,
                           Secretaria=file.Secretaria,
                           Programa=file.Programa
                       };
        }

        public static implicit operator ConsumoDto(CambioFile file)
        {

            return new ConsumoDto
            {
                Cuit = file.Cuit,
                FechaHoraComprobante = file.FechaHoraComprobante,
                ImportePesosNetoImpuestos = file.ImportePesosNetoImpuestos,
                NombrePersona = file.NombrePersona,
                NroComprobante = file.NroComprobante,
                NroDocumento = file.NroDocumento,
                RazonSocial = file.RazonSocial,
                TipoCliente = file.TipoCliente,
                Procesado = false,
                Archivo = "",
                Coeficiente = "100",
                Secretaria = "",
                Programa = ""
            };
        }
    }
}

    