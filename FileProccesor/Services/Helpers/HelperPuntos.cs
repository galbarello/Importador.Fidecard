using System;
using System.Linq;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using System.Configuration;
using System.Collections.Specialized;

namespace FileProccesor.Services.Helpers
{
    public static class HelperPuntos
    {
        private static readonly NameValueCollection Section =
        (NameValueCollection)ConfigurationManager.GetSection("Empresas");

        public static double GetPuntos(int empresa,DateTime comprobante, double monto)
        {
            var puntos = 0d;

            var config = 0d;

            if (empresa == int.Parse(Section["Ola"]))
                config=Double.Parse(ConfigurationManager.AppSettings["puntosOla"]);
            else
                config = Double.Parse(ConfigurationManager.AppSettings["puntosCambio"]);


            var reglas=ActiveRecordBase<ReglasDto>.FindAll()
                .Where(x => x.Key.CodEmpresa == empresa)
                .Where(x => x.Key.Sucursal == HelperSucursal.GetSucursal())
                .Where(x=>x.Key.FechaInicio<=comprobante)
                .Where(x=>x.FechaFin==null || x.FechaFin> comprobante )
                .ToList();

            foreach (var regla in reglas)
            {
                var modificador = monto * Convert.ToDouble(regla.Multiplicador);
                puntos = puntos+modificador;
            }



            return puntos>0? puntos : (monto * (config/100));
        }
    }
}