using System;
using System.Configuration;
using System.IO;
using Retlang.Channels;
using Retlang.Fibers;

namespace FileProccesor.Services
{
    public abstract class Importar
    {
        private static readonly string PathBackUp = ConfigurationManager.AppSettings["backUpPath"];
        private readonly IFiber _cola;
        private readonly IChannel<string> _channel;

        protected Importar()
        {
            _cola = new PoolFiber();
            _channel=new Channel<string>();
            _cola.Start();
            _channel.Subscribe(_cola, MoverArchivo);
        }


        private static void MoverArchivo(string file)
        {
            var lista = file.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            var archivoBack=string.Format(@"{0}\{1}", PathBackUp, lista[lista.GetUpperBound(0)]);
            File.Move(file, archivoBack);
        }

        public virtual void Persistir(string filepath)
        {
            _channel.Publish(filepath);
        }

    
    }
}