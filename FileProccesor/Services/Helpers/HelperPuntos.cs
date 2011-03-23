using System;
using System.Linq;
using Castle.ActiveRecord;
using FileProccesor.Dtos;

namespace FileProccesor.Services.Helpers
{
    public static class HelperPuntos
    {
        public static int GetPuntos(int empresa,DateTime comprobante, double monto)
        {
            var puntos = 0d;

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

            return (int) puntos;
        }
    }
}