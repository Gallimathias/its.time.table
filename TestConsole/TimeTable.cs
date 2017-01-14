using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class TimeTable
    {
        private string className;
        private string classTeacher;
        private DateTime endDate;
        private DateTime startDate;
        private Dictionary<int, List<Hour>> hours;

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
