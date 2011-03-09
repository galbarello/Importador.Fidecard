using System;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;

namespace FileProccesor.Services
{
    public static class  HelperPersona
    {
        public static string GetPersona(string dni,string nombre,string apellido,int empresa)
        {
            var documento = CuitToDni(dni);
            var persona=ActiveRecordBase<PersonaDto>.TryFind(new KeyPersona {EmpresaPersona = empresa, NroDocumento = documento});
            if (persona != null)
                return documento;
            var nuevaPersona = new PersonaDto
                                   {
                                       Apellido = apellido,
                                       Nombre = nombre,
                                       Key = new KeyPersona
                                                 {
                                                     EmpresaPersona = empresa,
                                                     NroDocumento = documento,
                                                     TipoDocumento=TipoDocumento.Cuit
                                                 }
                                   };
            nuevaPersona.Save();
            return documento;
        }

        private static string CuitToDni(string dni)
        {
            if (dni.Length <= 9) 
                return dni;
            return dni.Contains("-") 
                ? dni.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries)[1] 
                : dni.Substring(2, 8);
        }
    }
}