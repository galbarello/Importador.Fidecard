using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FileProccesor.Dtos;
using RestSharp;

namespace FileProccesor.Services
{
    public class MailService
    {
        public static IRestResponse SendError(ConsumoDto record)  
        {
        
           var client = new RestClient();
           client.BaseUrl = "https://api.mailgun.net/v2";
           client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-6mal5axl-we7hzmlc2f5s66uqv7-ps98");
           RestRequest request = new RestRequest();
           request.AddParameter("domain",
                                "fidecard.mailgun.org", ParameterType.UrlSegment);
           request.Resource = "{domain}/messages";
           request.AddParameter("from", "Importador Fidecard <importador@fidecard.com.ar>");
           //request.AddParameter("to", "cabarcia@gruposigno.com.ar");
           //request.AddParameter("to", "carlosditzel@yahoo.com.ar");
           request.AddParameter("to", "galbarello@gmail.com");
           request.AddParameter("subject", "Error en importador");

           var errorFile = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14}", 
                record.Archivo, 
                record.Coeficiente, 
                record.Cuit, 
                record.Empresa, 
                record.FechaHoraComprobante, 
                record.ImportePesosNetoImpuestos, 
                record.LastOperation, 
                record.NombrePersona, 
                record.NroComprobante, 
                record.NroDocumento, 
                record.Programa, 
                record.RazonSocial, 
                record.Secretaria, 
                record.TipoCliente);
           request.AddParameter("html", "<html>Revise el proceso</html>");
           request.AddParameter("text", "Registro" + errorFile);
           request.Method = Method.POST;
           return client.Execute(request);
        }

        public static IRestResponse SendOk(string file)
        {
            var client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               "key-6mal5axl-we7hzmlc2f5s66uqv7-ps98");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "fidecard.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Importador Fidecard <importador@fidecard.com.ar>");
            //request.AddParameter("to", "cabarcia@gruposigno.com.ar");
            //request.AddParameter("to", "carlosditzel@yahoo.com.ar");
            request.AddParameter("to", "galbarello@gmail.com");
            request.AddParameter("subject", "Se ha importado " + file);
            request.AddParameter("text", "El archivo " + file + " se ha importado exitosamente");
            
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
