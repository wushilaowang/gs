using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace gswmgzback
{
    class HttpGet
    {
        public static string HttpGetFunc(string url)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            //无论网站证书是否有效,一律通过,
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.ServerCertificateValidationCallback = new
                    RemoteCertificateValidationCallback
                    (
                    delegate { return true; }
                    );
            //try
            //{

                request.UserAgent = "Code Sample Web Client";
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            //}
            //catch (System.Net.WebException)
            //{
            //    MessageBox.Show("未知异常");
            //    return null;
            //}

        }
    }
}
