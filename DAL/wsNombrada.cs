using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BOL;
namespace DAL
{
    public class wsNombrada
    {
        public static String Execute(CredencialesWS cr,Nombrada nom)
        {
            String resp;
            StringBuilder xmlenvia = new StringBuilder("");
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            xmlenvia.Append(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:scc=""https://sccnlp.com/"">");
            xmlenvia.Append(@"<soap:Header>");
            xmlenvia.Append(@"<scc:UserCredentials>");
            xmlenvia.Append(@"<scc:userName>");
            xmlenvia.Append(cr.user);
            xmlenvia.Append(@"</scc:userName>");
            xmlenvia.Append(@"<scc:password>");
            xmlenvia.Append(cr.pass);
            xmlenvia.Append("</scc:password>");
            xmlenvia.Append(@"</scc:UserCredentials>");
            xmlenvia.Append(@"</soap:Header>");
            xmlenvia.Append(@"<soap:Body>");
            xmlenvia.Append(@" <scc:consultarNombradaByConcesionaria>");
            xmlenvia.Append(@"<scc:rutEmpresa>");
            xmlenvia.Append(cr.rutEmpr);
            xmlenvia.Append(@"</scc:rutEmpresa>");
            xmlenvia.Append(@"<scc:filtro>");
            xmlenvia.Append(@"<scc:fechaInicio>");
            xmlenvia.Append(nom.fechaInicioNombrada);
            xmlenvia.Append(@"</scc:fechaInicio>");
            xmlenvia.Append(@"</scc:filtro>");
            xmlenvia.Append(@"</scc:consultarNombradaByConcesionaria>");
            xmlenvia.Append(@"</soap:Body>");
            xmlenvia.Append(@"</soap:Envelope>");
            soapEnvelopeXml.LoadXml(xmlenvia.ToString());



            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
             
                    resp = soapResult;
                }
            }


            return resp;

        }
        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest()
        {

            string ruta_WS = Parameter.LeerRutawsNombrada();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ruta_WS);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

    }
}
