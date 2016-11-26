using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.XPath;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = WebRequest.Create("https://www2.its-stuttgart.de/intranet/schueler/untis/c/47/c00024.htm");
            request.ContentType = "text/html; charset=UTF-8";
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(""))}");

            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var a = WebUtility.HtmlDecode(reader.ReadToEnd());
                }
            }
        }
    }
}
