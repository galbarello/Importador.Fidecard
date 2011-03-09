namespace FileProccesor.Services
{
    public class ImportadorFactory
    {
        public static Importar ReturnImportador(string archivo)
        {
           return new Transatlantica();
        }
    }
}