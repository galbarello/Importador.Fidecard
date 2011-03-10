using System;
using System.Configuration;
using Castle.ActiveRecord;
using FileProccesor.Keys;
using FileProccesor.Schemes;
using FileProccesor.Services;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Cuentas_Corrientes")]
    public class CuentaCorrienteDto:ActiveRecordBase<CuentaCorrienteDto>
    {
        private static readonly int Empresa = int.Parse(ConfigurationManager.AppSettings["CodigoEmpresa"]);

        [CompositeKey]
        public KeyCuenta Key { get; set; }

        [Property("Cuenta")]
        public int NumeroCuenta { get; set; }

        [Property("fecha_compra")]
        public DateTime FechaCompra { get; set; }

        [Property("hora_compra")]
        public DateTime HoraCompra { get; set; }

        [Property("NroDoc", Length = 9)]
        public string NumeroDocumento { get; set; }

        [Property("Movimiento")]
        public int Movimiento { get; set; }

        [Property("codSucursal")]
        public int Sucursal { get; set; }

        [Property("codUsuario", Length = 9)]
        public string Usuario { get; set; }

        [Property("monto")]
        public double MontoCompra { get; set; }

        [Property("cantidadPuntos")]
        public int Puntos { get; set; }
    }
}