using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Parentesco")]
    public class ParentescoDto:ActiveRecordBase<ParentescoDto>
    {
        /// <summary>
        /// Solo para usar castle activerecord
        /// </summary>
        public ParentescoDto()
        {}

        public ParentescoDto(string desc)
        {
            Descripccion = desc;
        }

        [PrimaryKey("CodParentesco")]
        public int Id{ get; set;}

        [Property("DescParentesco")]
        public string Descripccion { get; set;}
        
    }
}