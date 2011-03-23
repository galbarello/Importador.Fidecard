using System.Linq;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;

namespace FileProccesor.Services.Helpers
{
    public static class HelperCuenta
    {
        public static int GetCuenta(string cuit,string dni, int empresa)
        {
            var cuentas = ActiveRecordBase<ClienteDto>.FindAll()
                .Where(x => x.Key.CodEmpresa == empresa)
                .Where(x => x.Key.NumeroDocumento == HelperPersona.CuitToDni(cuit))
                .ToList();

            var micuenta = cuentas.Count <= 0 
                               ? CrearNuevaCuenta(cuit,empresa) 
                               : cuentas.Max(x => x.Key.NroCuenta);

           if (!string.IsNullOrEmpty(dni))
               AgregarPersonaCuenta(dni, empresa,micuenta);

            return micuenta;
        }

        private static void AgregarPersonaCuenta(string dni, int empresa, int cuenta)
        {
            var cuentas = ActiveRecordBase<ClienteDto>.FindAll()
                .Where(x => x.Key.CodEmpresa == empresa)
                .Where(x => x.Key.NumeroDocumento == HelperPersona.CuitToDni(dni))
                .ToList();
            if (cuentas.Count<=0)
                new ClienteDto(new KeyCliente(
                    empresa, HelperPersona.CuitToDni(dni), cuenta), 
                    Parentesco.Empleado, GetExtension(cuenta, empresa))
                .Save();
        }

        private static int GetExtension(int cuenta, int empresa)
        {
            var extensiones = ActiveRecordBase<ClienteDto>.FindAll()
                .Where(x => x.Key.CodEmpresa == empresa)
                .Where(x => x.Key.NroCuenta == cuenta);

            var numero= extensiones.Count() > 0 
                ? extensiones.Max(x => x.CodExtension) + 1 : 1;
            
            return numero;
        }

        private static int CrearNuevaCuenta(string cuit,int empresa)
        {
            var cuentaNueva = new ClienteDto(
                    new KeyCliente(empresa, HelperPersona.CuitToDni(cuit), GetNumeroNuevo(empresa)),
                        Parentesco.Titular, 1);
            cuentaNueva.Save();

            return cuentaNueva.Key.NroCuenta;
        }

        public static int GetNumeroNuevo(int empresa)
        {
            var cuentas= ActiveRecordBase<ClienteDto>.FindAll()
                .Where(x=>x.Key.CodEmpresa==empresa);

            return cuentas.Count() > 0 ? cuentas.Max(x => x.Key.NroCuenta) + 1 : 1;
        }
    }
}