using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Parentesco")]
    public class ParentescoDto:ActiveRecordBase<ParentescoDto>
    {
        [PrimaryKey("CodParentesco")]
        public int Id{ get; set;}

        [Property("DescParentesco")]
        public string Descripccion { get; set;}
        
    }
}