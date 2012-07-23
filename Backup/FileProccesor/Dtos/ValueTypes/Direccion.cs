using Castle.ActiveRecord;

namespace FileProccesor.Dtos.ValueTypes
{
    public class Direccion
    {
        [Property("Domicilio", Length = 120)]
        public string Domicilio { get; set; }

        [Property("CodPostal", Length = 8)]
        public string CodigoPostal { get; set; }
    }
}