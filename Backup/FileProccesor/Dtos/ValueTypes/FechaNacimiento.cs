using Castle.ActiveRecord;

namespace FileProccesor.Dtos.ValueTypes
{
    public class FechaNacimiento
    {
        [Property("DiaNacimiento", Length = 2)]
        public string Dia { get; set; }

        [Property("MesNacimiento", Length = 2)]
        public string Mes { get; set; }

        [Property("AnoNacimiento", Length = 4)]
        public string Año { get; set; }
    }
}