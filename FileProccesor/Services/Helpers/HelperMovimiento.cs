using Castle.ActiveRecord;
using FileProccesor.Dtos;

namespace FileProccesor.Services.Helpers
{
    public static class HelperMovimiento
    {
        public static MovimientoDto FindMovimiento(string desc)
        {
            var movimiento = ActiveRecordBase<MovimientoDto>
                .FindAllByProperty("Descripccion", desc);

            if (movimiento.Length > 0)
                return movimiento[0];

            var nuevo = new MovimientoDto(desc);
            nuevo.Save();
            return nuevo;
        }
    }
}