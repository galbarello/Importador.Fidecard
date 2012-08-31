using System;
using Castle.ActiveRecord;
using FileProccesor.Keys;
using FileProccesor.Schemes;
using FileProccesor.Services.Helpers;

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
                Programa = "",
                Error=string.Empty
            };
        }

        [Property]
        public string Error { get; set; }

        internal bool Validate()
        {
            try
            {
                var documento = HelperPersona.GetPersona(
                        Cuit, TipoCliente,
                        RazonSocial, NombrePersona,
                        NroDocumento, Empresa);

                var cliente = HelperCuenta.GetCuenta(
                    Cuit, NroDocumento, Empresa);


                var puntos = HelperPuntos.GetPuntos(Empresa, FechaHoraComprobante,
                                                ImportePesosNetoImpuestos);

                double acelerador = Double.Parse(Coeficiente) / 100;
                puntos = acelerador > 0 ? acelerador * puntos : puntos;

                var cuenta = new CuentaCorrienteDto
                {
                    FechaCompra = FechaHoraComprobante.Date,
                    HoraCompra = DateTime.Now,
                    Key = new KeyCuenta
                    {
                        CodEmpresa = Empresa,
                        NumeroComprobante = NroComprobante
                    },
                    MontoCompra = ImportePesosNetoImpuestos,
                    Movimiento = puntos >= 0 ? HelperMovimiento.FindMovimiento("Suma De Puntos") : HelperMovimiento.FindMovimiento("Anulación Carga"),
                    NumeroDocumento = documento,
                    NumeroCuenta = cliente,
                    Puntos = puntos,
                    Sucursal = HelperSucursal.GetSucursal(),
                    Usuario = "web",
                    Programa = Programa,
                    Secretaria = Secretaria,
                    Coeficiente = Coeficiente
                };
                return true;
            }
            catch { return false; }
        }
        
    }
}

   