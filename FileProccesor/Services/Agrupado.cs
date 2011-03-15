using System.Collections.Generic;
using FileProccesor.Schemes;

namespace FileProccesor.Services
{
    public class AgrupadoTransa
    {
        public string NroComprobante;
        public IList<TransatlanticaFile> Consumo;
        public double ImportePesosNetoImpuestos;
    }

    public class AgrupadoCambio
    {
        public string NroComprobante;
        public IList<CambioFile> Consumo;
        public double ImportePesosNetoImpuestos;
    }
}