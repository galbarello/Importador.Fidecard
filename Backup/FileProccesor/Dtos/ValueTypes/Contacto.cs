using Castle.ActiveRecord;

namespace FileProccesor.Dtos.ValueTypes
{
    public class Contacto{
        [Property("NombreContacto",Length = 50)]
        public string Nombre{ get; set;}

        [Property("TelefonoContacto",Length = 16)]
        public string Telefono{ get; set;}

        [Property("email",Length = 50)]
        public string Email{ get; set;} 
    }
}