using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Movimientos")]
    public class MovimientoDto:ActiveRecordBase<MovimientoDto>
    {
        /// <summary>
        /// Solo para usar castle activerecord
        /// </summary>
        public MovimientoDto(){}

        public MovimientoDto(string desc)
        {
            Descripccion = desc;
        }

        [PrimaryKey(PrimaryKeyType.Increment,Column = "CodMovimiento")]
        public int Id { get; set; }

        [Property("DescMovimiento")]
        public string Descripccion { get; set; }
    }
}