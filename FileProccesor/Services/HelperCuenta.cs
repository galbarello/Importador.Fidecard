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
           var cuentas = ActiveRecordBase<ClienteDto>.FindAll();

           if (cuentas.Count()<=0)
           {
               var cuentaNueva = new ClienteDto
                   (new KeyCliente(empresa,HelperPersona.CuitToDni(cuit),1),parentesco,1);
               cuentaNueva.Save();

               return cuentaNueva.Key.NroCuenta;
           }
           return 999;
       }

        public static int GetNumeroNuevo()
        {
           return ActiveRecordBase<ClienteDto>.FindAll().Max(x => x.Key.NroCuenta);
        }
    }
}