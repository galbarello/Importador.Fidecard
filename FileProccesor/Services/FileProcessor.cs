using System.Collections.Generic;
using System.IO;
using FileHelpers;
using FileProccesor.Schemes;

namespace FileProccesor.Services
{
    public static class FileProcessor<T> where T : IFileSpec
    {
        public static IEnumerable<T> GetData(string file)
        {
            var i = 0;
            while (FileInUse(file))
                i++;
            return new FileHelperEngine<T>().ReadFile(file);
        }

        static bool FileInUse(string path)
        {
            try
            {
                //Just opening the file as open/create
                using (new FileStream(path, FileMode.OpenOrCreate))
                {
                    //If required we can check for read/write by using fs.CanRead or fs.CanWrite
                }
                return false;
            }
            catch (IOException ex)
            {
                //check if message is for a File IO
                var message = ex.Message;
                if (message.Contains("El proceso no puede obtener acceso al archivo"))
                    return true;
                throw;
            }
        }
    }
}