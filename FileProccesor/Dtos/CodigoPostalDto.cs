using System;
using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("CodigosPostales")]
    public class CodigoPostalDto:ActiveRecordBase<CodigoPostalDto>
    {
        /// <summary>
        /// solo para usar castle activerecord
        /// </summary>
        public CodigoPostalDto()
        {}

        public CodigoPostalDto(string codigo, string ciudad, string provincia)
        {
            CodPostal = codigo;
            Localidad = ciudad;
            Provinica = provincia;
        }

        [PrimaryKey(Length = 8)]
        public string CodPostal { get; set; }

        [Property(Length = 50)]
        public string Localidad { get; set; }

        [Property(Length = 50)]
        public string Provinica { get; set; }

        [Property]
        public DateTime? FechaBaja { get; set; }

        [Property("CodMotivoBaja")]
        public int? MotivoBaja { get; set; }
    }
}