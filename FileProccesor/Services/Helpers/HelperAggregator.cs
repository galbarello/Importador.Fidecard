using System.Collections.Generic;
using System.Linq;
using FileProccesor.Schemes;

namespace FileProccesor.Services.Helpers
{
    public class HelperAggregator
    {
        public static IEnumerable<Agrupado> Agrupar(IEnumerable<TransatlanticaFile> listado)
        {
            return from consumo in listado
                   group consumo by consumo.NroComprobante
                   into unico
                       select new Agrupado
                                  {
                                      NroComprobante = unico.Key,
                                      Consumo = unico.ToList(),
                                      ImportePesosNetoImpuestos = unico.Sum(x => x.ImportePesosNetoImpuestos)
                                  };
            
        }
    }
}