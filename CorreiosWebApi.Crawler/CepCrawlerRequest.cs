using System;
using System.IO;
using System.Net;

namespace CorreiosWebApi.Crawler
{
    public class CepCrawlerRequest
    {
        public String readHtmlPage()
        {
            var url = "http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaCepEndereco.cfm?t";

            String cep = "76493000";

            //setup some variables end

            String result = "";
            String strPost = "relaxation=" + cep + "&Metodo=listaLogradouro&TipoConsulta=relaxation&StartRow=1&EndRow=10";
            StreamWriter myWriter = null;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr =
               new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();

                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }
    }
}