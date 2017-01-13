using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    static class Deserializer
    {
        public static string[] Days { get; set; }
        
        private static int count;

        public static TimeTable Deserailze(Stream stream)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(stream);

            var center = htmlDoc.DocumentNode.Element("html").Element("body").Element("center");

            var fonts = center.Elements("font").ToArray();

            var className = fonts[1].InnerText.Trim().Replace("&nbsp;", "").Trim();
            var classTeacher = fonts[2].InnerText.Trim();
            var dtString = fonts[3].InnerText.Trim().Split('-');
            var startDate = DateTime.Parse(dtString[0]);
            var endDate = DateTime.Parse(dtString[1]);

            var tableElemtents = center.Element("table").Elements("tr").Where(e => e.ChildNodes.Count > 0).ToList();
            var first = true;
            var hours = new Dictionary<int, List<Hour>>();

            var timeTable = new TimeTable(className, classTeacher, startDate, endDate);

            foreach (var item in tableElemtents)
            {
                if (first)
                {
                    first = false;
                    Days = item.InnerText.Split(new[] { '\n', ' ', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    continue;
                }

                var header = true;
                var headerId = 0;
                var tmpDate = startDate;
                var startTime = startDate;
                var endTime = startDate;
                var tmpHour = new List<Hour>();

                foreach (var node in item.Elements("td"))
                {
                    var count = node.Element("table").Elements("tr").Count();
                    var text = node.InnerText.Split(new[] { '\n', ' ', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    if (text.Length < 3)
                        continue;

                    if (header)
                    {
                        headerId = int.Parse(text[0]);
                        startTime = tmpDate.AddHours(double.Parse(text[1].Split(':')[0])).AddMinutes(double.Parse(text[1].Split(':')[1]));
                        endTime = tmpDate.AddHours(double.Parse(text[2].Split(':')[0])).AddMinutes(double.Parse(text[2].Split(':')[1]));
                        header = false;
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            var gap = i * 4;
                            var hour = new Hour(headerId, startTime, endTime);

                            hour.Teacher = text[gap];
                            hour.Room = text[gap + 1];
                            hour.Subject = text[gap + 2];
                            hour.Week = text[gap + 3];
                            tmpHour.Add(hour);
                        }

                        startTime = startTime.AddDays(count);
                        endTime = endTime.AddDays(count);
                        Next();
                    }

                }

                var tmpCount = item.Elements("td").Count();
                if (tmpCount == 1)
                {
                    var tmpH = new List<Hour>();
                    foreach (var h in hours[headerId - 1])
                    {
                        var t = h;
                        t.startTime = new DateTime(h.startTime.Year, h.startTime.Month, h.startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
                        t.endTime = new DateTime(h.endTime.Year, h.endTime.Month, h.endTime.Day, endTime.Hour, endTime.Minute, endTime.Second);
                        tmpH.Add(t);
                    }
                    hours.Add(headerId, tmpH);
                }
                else if (tmpCount > 1)
                    hours.Add(headerId, tmpHour);
            }

            timeTable.AddHours(hours);
            return timeTable;
        }

        public static string Next()
        {
            var tmp = count;
            count++;
            if (count > 0)
                count = 0;
            return Days[tmp];
        }
    }
}
