using System;
using System.Collections.Generic;

namespace its.time.table.droid.Core
{
    public class TimeTable
    {
        public string className;
        public string classTeacher;
        public DateTime endDate;
        public DateTime startDate;
        public Dictionary<int, List<Hour>> hours;

        public TimeTable(string className, string classTeacher, DateTime startDate, DateTime endDate)
        {
            this.className = className;
            this.classTeacher = classTeacher;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        internal void AddHours(Dictionary<int, List<Hour>> hours)
        {
            this.hours = hours;
        }
    }
}
