using System;
using System.Linq;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;

namespace FileProccesor.Services.Helpers
{
    public static class  HelperPersona
    {
        public static string 
            GetPersona(string cuit,string tipo,string razon,string apellidoynombre,string dni,int empresa)
        {
            var documento=GetCorporativo(cuit, empresa, razon);

            return tipo == "Corporativo" 
                ? documento 
                : GetParticular(cuit, empresa, apellidoynombre, dni);
        }

        private static string GetParticular(string cuit, int empresa, string apellidoynombre, string dni)
        {
            var persona = ActiveRecordBase<PersonaDto>.TryFind(new KeyPersona(empresa, CuitToDni(dni), TipoDocumento.DocumentoUnico));

            if (persona == null)
            {
                var tipoDoc = TipoDocumento.DocumentoUnico;
                var componentes = apellidoynombre.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var apellido = componentes.First();
                var nombre = componentes.Last();

                if (dni.Length > 9)
                {
                    tipoDoc = TipoDocumento.Cuit;
                    apellido = apellidoynombre;
                    nombre = cuit;
                }

                CrearParticular(cuit,dni,apellido,nombre,empresa,tipoDoc);
            }
            return CuitToDni(dni);
        }

        private static string GetCorporativo(string cuit,int empresa,string razon)
        {
            var documento = CuitToDni(cuit);
            var corporativo = ActiveRecordBase<PersonaDto>.TryFind(new KeyPersona(empresa, documento, TipoDocumento.Cuit));
            if (corporativo == null)
                CrearCorporativo(cuit,razon,empresa);
            return documento;

        }

        private static void CrearCorporativo(string cuit,string razon,int empresa)
        {
            var nuevoCorporativo = new PersonaDto
            {
                Apellido = razon,
                Nombre = cuit,
                Key = new KeyPersona(empresa, CuitToDni(cuit), TipoDocumento.Cuit),
                CodPostal = GetCp(),
                ActividadPersona = GetActividad()
            };
            nuevoCorporativo.Save();
            nuevoCorporativo.Cuenta = HelperCuenta.GetCuenta(cuit, CuitToDni(cuit), empresa).ToString();
            nuevoCorporativo.Save();
        }

        private static void CrearParticular(string cuit,string dni,string apellido,string nombre,int empresa,string tipoDoc)
        {
            var nuevaPersona = new PersonaDto
            {
                Apellido = apellido,
                Nombre = nombre,
                Key = new KeyPersona(empresa, CuitToDni(dni), tipoDoc),
               CodPostal = GetCp(),
                ActividadPersona = GetActividad()
            };
            nuevaPersona.Save();
            nuevaPersona.Cuenta = HelperCuenta.GetCuenta(cuit, dni, empresa).ToString();
            nuevaPersona.Save();
        }

        public static string CuitToDni(string dni)
        {
            if (string.IsNullOrEmpty(dni))
                return "000000000";

            if (dni.Length <= 9) 
                return RemoveZeroLeft(dni);
            
            return dni.Contains("-") 
                       ? RemoveZeroLeft(dni.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries)[1]) 
                       : RemoveZeroLeft(dni.Substring(2, 8));
        }

        private static string RemoveZeroLeft(string dni)
        {
            return int.Parse(dni).ToString();
        }

        private static CodigoPostalDto GetCp()
        {
           var cp= ActiveRecordBase<CodigoPostalDto>.TryFind("2000");
            if (cp != null) return cp;
            var cpDefault = new CodigoPostalDto("2000", "Rosario", "Santa Fe");
            cpDefault.Save();
            return cpDefault;
        }

        private static string  GetActividad()
        {
            return "Sin Especificar";
        }
    }
}