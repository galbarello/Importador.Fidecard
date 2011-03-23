using System;
using Castle.ActiveRecord;
using FileProccesor.Keys;

namespace FileProccesor.Dtos
{
    [ActiveRecord("ReglasNegocio")]
    public class ReglasDto:ActiveRecordValidationBase<ReglasDto>
    {
        [CompositeKey]
        public KeyRegla Key { get; set; }

        [Property("Basicos")]
        public int Multiplicador { get; set; }

        [Property("ProductoPromo")]
        public int MultiplicadorEspecial { get; set; }

        [Property("FechaFin")]
        public DateTime? FechaFin { get; set; }
    }
}