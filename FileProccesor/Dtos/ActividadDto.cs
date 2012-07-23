using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Actividades")]
    public class ActividadDto:ActiveRecordBase<ActividadDto>
    {
        /// <summary>
        /// Solo para usar castle activerecord
        /// </summary>
        public ActividadDto()
        {
        }

        public ActividadDto(string actividad)
        {
            DescActividad = actividad;
        }

        [PrimaryKey(Column = "CodActividad")]
        public int Id{ get; set;}
        
        [Property("DescActividad",Length = 50)]
        public string DescActividad { get; set;}
    }
}