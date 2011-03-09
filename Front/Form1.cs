using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using FileProccesor.Services;
using Growl.Connector;
using Retlang.Channels;
using Retlang.Fibers;
using Application = Growl.Connector.Application;


namespace Front
{
    public partial class Form1 : Form
    {

        private readonly string _logFile = string.Format("{0}{1}", ConfigurationManager.AppSettings["PathToLog"], ConfigurationManager.AppSettings["logfile"]);
        private readonly StreamWriter _fileSaver;
        private bool _savefile, _showmess;
        private static GrowlConnector _growl;
        private static NotificationType _notificationType;
        private static Application _application;
        private const string SampleNotificationType = "Información General";
        private const string ApplicationName = "ImportadorArchivos";
        private static IFiber _workerFiber;
        private static IChannel<string> _importar;

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = ConfigurationManager.AppSettings["Path"];
            _application = new Application(ApplicationName);

            _workerFiber = new PoolFiber();
            _workerFiber.Start();

            _importar = new Channel<string>();
            _importar.Subscribe(_workerFiber, Work);

            _fileSaver = new StreamWriter(_logFile, true);

            _notificationType = new NotificationType(SampleNotificationType, "Sample Notification");

            _growl = new GrowlConnector { EncryptionAlgorithm = Cryptography.SymmetricAlgorithmType.AES };
            _growl.NotificationCallback += GrowlNotificationCallback;
            _growl.Register(_application, new[] { _notificationType });

            folderWatch.Created += FolderWatchCreated;
            folderWatch.Renamed += FolderWatchRenamed;
            folderWatch.Deleted += FolderWatchDeleted;

            InitDatabase();
        }

        private void Work(string filepath)
        {
            var archivo = filepath.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            ShowMessageBox(string.Format("importando {0}", archivo[archivo.GetUpperBound(0)]), "Importando");

            var importador = ImportadorFactory.ReturnImportador(filepath);

            importador.Persistir(filepath);

            ShowMessageBox(string.Format("importado {0}", archivo[archivo.GetUpperBound(0)]), "Finalizado");
        }

        private void GrowlNotificationCallback(Response response, CallbackData callbackdata)
        {
            SaveChangesToFile(String.Format("Response Type: {0}\r\nNotification ID: {1}\r\nCallback Data: {2}\r\nCallback Data Type: {3}\r\n", callbackdata.Result, callbackdata.NotificationID, callbackdata.Data, callbackdata.Type));
        }

        private void SaveChangesToFile(string content)
        {
            if (_savefile != true) return;
            _fileSaver.WriteLine(content);
            _fileSaver.Flush();
        }

        private void FolderWatchDeleted(object sender, FileSystemEventArgs e)
        {
            var contents = string.Format("Un Archivo '{0}' en '{1}' ha sido eliminado el {2} {3}", e.Name, folderWatch.Path,
                           DateTime.Now.ToString("ddd , dd-MMM-yyyy"), DateTime.Now.ToShortTimeString());
            Visible = true;
            ShowMessageBox(contents, "Archivo Eliminado");
            Visible = false;
            SaveChangesToFile(contents);
        }

        private void ShowMessageBox(string caption, string message)
        {
            if (!_showmess) return;
            var callbackContext = new CallbackContext(caption, message);

            var notification = new Notification(_application.Name, _notificationType.Name, DateTime.Now.Ticks.ToString(), caption, message);
            _growl.Notify(notification, callbackContext);
        }

        private void FolderWatchRenamed(object sender, RenamedEventArgs e)
        {
            var contents = string.Format("Un Archivo '{0}' en '{1}' ha sido renombrado a {4} el {2} {3}", e.OldName, folderWatch.Path,
                          DateTime.Now.ToString("ddd , dd-MMM-yyyy"), DateTime.Now.ToShortTimeString(), e.Name);
            Visible = true;
            ShowMessageBox(contents, "Archivo Renombrado");
            Visible = false;
            SaveChangesToFile(contents);
        }

        private void FolderWatchCreated(object sender, FileSystemEventArgs e)
        {
            _importar.Publish(e.FullPath);
            var contents = string.Format("Un Archivo '{0}' en '{1}' ha sido creado el {2} {3}", e.Name, folderWatch.Path,
                         DateTime.Now.ToString("ddd , dd-MMM-yyyy"), DateTime.Now.ToShortTimeString());
            Visible = true;
            ShowMessageBox(contents, "Nuevo Archivo Creado");
            Visible = false;
            SaveChangesToFile(contents);
        }

        private static void InitDatabase()
        {
            var config = ActiveRecordSectionHandler.Instance;
            var assemblyLibrary = Assembly.Load("FileProccesor");

            ActiveRecordStarter.Initialize(assemblyLibrary, config);

            switch (ConfigurationManager.AppSettings["Enviroment"])
            {
                case "Development":
                    ActiveRecordStarter.DropSchema();
                    ActiveRecordStarter.CreateSchema();
                    break;
                case "Testing":
                    ActiveRecordStarter.UpdateSchema();
                    break;
                default:
                    break;
            }
           

            var callbackContext = new CallbackContext("Conectado a la Base de Datos", ActiveRecordStarter.ConfigurationSource.ToString());

            var notification = new Notification(_application.Name, _notificationType.Name, DateTime.Now.Ticks.ToString(), "InitDatabase", "OK");
            _growl.Notify(notification, callbackContext);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog { Description = "Selecione un directorio a observar" };
            if (fbd.ShowDialog() == DialogResult.OK)
                textBox1.Text = fbd.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            _savefile = true;
            _showmess = false;
            if (checkBox1.Checked)
                _showmess = true;
            if (checkBox2.Checked)
                _savefile = false;
            if (checkBox3.Checked)
            {
                MessageBox.Show("El formulario se minimizara en el area de tareas", "El formulario sera escondido");
                WindowState = FormWindowState.Minimized;
                Visible = false;
            }

            folderWatch.Path = textBox1.Text;

            notifyIcon1.ShowBalloonTip(20, "Importador de Archivos Minimizado", "Double Click para mostrar Opciones", ToolTipIcon.Info);
        }
    }
}
