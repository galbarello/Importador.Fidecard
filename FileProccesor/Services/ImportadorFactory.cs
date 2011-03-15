namespace FileProccesor.Services
{
    public class ImportadorFactory
    {
        public static Importar ReturnImportador(string archivo)
        {
            return archivo.Contains("OLA")
                       ? new Transatlantica()
                       : (archivo.Contains("CAMBIO")
                              ? (Importar) new Cambio()
                              : new NullFile());
        }
    }
}