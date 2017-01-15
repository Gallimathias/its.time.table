using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using HtmlAgilityPack;
using System.Globalization;

namespace its.time.table.droid.Core
{
    internal class Service
    {
        public Dictionary<int, string> Classes { get; private set; }
        public TimeTable TimeTable { get { return getTimeTable(); } }
                
        public Service()
        {
            Classes = new Dictionary<int, string>();

            initialize();
        }

        private void initialize()
        {
            var request = WebRequest.Create("https://www2.its-stuttgart.de/intranet/schueler/untis/frames/navbar.htm");
            request.ContentType = "text/html; charset=UTF-8";
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes("vorname.nachname:Password"))}");

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException)
            {
                return;
            }

            var doc = new HtmlDocument();
            doc.Load(response.GetResponseStream());
            var navBar = doc.DocumentNode.Element("html").Element("head").Elements("script").ToArray()[1].InnerText.Split(
                new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var classes = navBar.FirstOrDefault(v => v.StartsWith(" var classes")).Substring(15).TrimStart('[').TrimEnd(']').Split(',');

            for (int i = 0; i < classes.Length; i++)
            {
                Classes.Add(i + 1, classes[i]);
            }
        }

        private TimeTable getTimeTable()
        {
            Calendar calendar = new GregorianCalendar();
            var week = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            var request = WebRequest.Create($"https://www2.its-stuttgart.de/intranet/schueler/untis/c/{week.ToString("00")}/c00024.htm");
            request.ContentType = "text/html; charset=UTF-8";
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes("vorname.nachname:Password"))}");

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException)
            {
                return null;
            }


            return Deserializer.Deserailze(response.GetResponseStream());
        }
    }
}