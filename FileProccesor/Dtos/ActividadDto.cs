using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Actividades")]
    public class ActividadDto
    {
        [PrimaryKey(PrimaryKeyType.Assigned,Column = "CodActividad")]
        public int Id{ get; set;}
        
        [Property("DescActividad",Length = 50)]
        public string DescActividad { get; set;}
    }
}