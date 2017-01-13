using its.time.table.Xamarin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace its.time.table.Services
{
    class GetTimeTable_Service
    {
        public GetTimeTable_Service()
        {
            var request = WebRequest.Create("https://www2.its-stuttgart.de/intranet/schueler/untis/c/47/c00024.htm");
            request.ContentType = "text/html; charset=UTF-8";
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes("firstname.lastname:Password"))}");

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException)
            {
                return;
            }


            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var a = WebUtility.HtmlDecode(reader.ReadToEnd());
            }



        }
    }
}
