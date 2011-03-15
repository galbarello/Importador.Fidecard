using System.Collections.Generic;
using System.Linq;
using FileProccesor.Schemes;

namespace FileProccesor.Services.Helpers
{
    public static class HelperAggregator
    {
        public static IEnumerable<AgrupadoTransa> Agrupar(IEnumerable<TransatlanticaFile> listado)
        {
            return from consumo in listado
                   group consumo by consumo.NroComprobante
                   into unico
                       select new AgrupadoTransa
                                  {
                                      NroComprobante = unico.Key,
                                      Consumo = unico.ToList(),
                                      ImportePesosNetoImpuestos = unico.Sum(x => x.ImportePesosNetoImpuestos)
                                  };
            
        }

        public static IEnumerable<AgrupadoCambio> Agrupar(IEnumerable<CambioFile> listado)
        {
            return from consumo in listado
                   group consumo by consumo.NroComprobante
                       into unico
                       select new AgrupadoCambio
                       {
                           NroComprobante = unico.Key,
                           Consumo = unico.ToList(),
                           ImportePesosNetoImpuestos = unico.Sum(x => x.ImportePesosNetoImpuestos)
                       };
        }
    }
}