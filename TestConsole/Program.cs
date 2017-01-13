using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //NavBar: https://www2.its-stuttgart.de/intranet/schueler/untis/frames/navbar.htm
            //Functions: https://www2.its-stuttgart.de/intranet/schueler/untis/untisscripts.js
            //TimeTables: https://www2.its-stuttgart.de/intranet/schueler/untis/c/02/c00024.htm
            //Die Time Tables sind folgender masen nach /untis aufgebaut: untis/abfragetyp/Woche/typSelectedindex.htm
            //Die Informationen lassen sich aus der NavBar entnehmen.

            var request = WebRequest.Create("https://www2.its-stuttgart.de/intranet/schueler/untis/c/02/c00024.htm");
            request.ContentType = "text/html; charset=UTF-8";
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes("vorname.name:Password"))}");

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException)
            {
                return;
            }


            Deserializer.Deserailze(response.GetResponseStream());


            
        }
    }
}
