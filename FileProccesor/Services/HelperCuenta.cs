using System;
using System.Linq;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;

namespace FileProccesor.Services
{
    public static class HelperCuenta
    {
       public static int GetCuenta(string cuit,string dni, int empresa,Parentesco parentesco)
       {
          var cuentas = parentesco == Parentesco.Titular
                               ? ActiveRecordBase<ClienteDto>.FindAll()
                                     .Where(x => x.Key.CodEmpresa == empresa)
                                     .Where(x => x.Key.NumeroDocumento == HelperPersona.CuitToDni(cuit))
                                     .ToList()
                               : ActiveRecordBase<ClienteDto>.FindAll()
                                     .Where(x => x.Key.CodEmpresa == empresa)
                                     .Where(x => x.Key.NumeroDocumento == HelperPersona.CuitToDni(dni))
                                     .ToList();

           if (cuentas.Count<=0)
           {
               var cuentaNueva = new ClienteDto
                   (new KeyCliente(empresa,HelperPersona.CuitToDni(cuit),GetNumeroNuevo()),parentesco,GetExtension(cuit,empresa));
               cuentaNueva.Save();

               return cuentaNueva.Key.NroCuenta;
           }
           return cuentas.Max(x=>x.Key.NroCuenta);
       }

        private static int GetExtension(string cuit, int empresa)
        {
            var extensiones = ActiveRecordBase<ClienteDto>.FindAll()
                .Where(x => x.Key.CodEmpresa == empresa)
                .Where(x => x.Key.NumeroDocumento == HelperPersona.CuitToDni(cuit));

            return extensiones.Count() > 0 ? extensiones.Max(x => x.CodExtension) + 1 : 1;
        }

        public static int GetNumeroNuevo()
        {
            var cuentas= ActiveRecordBase<ClienteDto>.FindAll();
            return cuentas.Count() > 0 ? cuentas.Max(x => x.Key.NroCuenta) + 1 : 1;
        }
    }
}