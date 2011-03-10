using System;
using System.Linq;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;

namespace FileProccesor.Services
{
    public static class  HelperPersona
    {
        public static string 
            GetPersona(string cuit,string tipo,string razon,string apellidoynombre,string dni,int empresa)
        {
            if (tipo == "Corporativo")
            {
                var documento = CuitToDni(cuit);
                var corporativo = ActiveRecordBase<PersonaDto>.TryFind(new KeyPersona(empresa, documento,TipoDocumento.Cuit));
                if (corporativo == null)
                {
                    var nuevoCorporativo = new PersonaDto
                                               {
                                                   Apellido = razon,
                                                   Nombre = "",
                                                   Key = new KeyPersona(empresa, documento, TipoDocumento.Cuit),
                                                   Cuenta = HelperCuenta.GetCuenta(cuit,dni, empresa,Parentesco.Titular).ToString()
                                               };
                    nuevoCorporativo.Save();
                }
                return documento;
            }
            
            var persona = ActiveRecordBase<PersonaDto>.TryFind(new KeyPersona(empresa, dni, TipoDocumento.Cuit));

            if (persona == null)
            {
                var nuevaPersona = new PersonaDto
                                       {
                                           Apellido =
                                               apellidoynombre.Split(new[] {""}, StringSplitOptions.RemoveEmptyEntries).
                                               Last(),
                                           Nombre = apellidoynombre,
                                           Key = new KeyPersona(empresa, dni, TipoDocumento.Cuit),
                                           Cuenta = HelperCuenta.GetCuenta(cuit, dni, empresa, Parentesco.Empleado).ToString()
                                       };
                nuevaPersona.SaveAndFlush();
            }
            return dni;
        }

        public static string CuitToDni(string dni)
        {
            if (dni.Length <= 9) 
                return dni;
            return dni.Contains("-") 
                ? dni.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries)[1] 
                : dni.Substring(2, 8);
        }
    }
}