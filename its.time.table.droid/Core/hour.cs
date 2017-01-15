using System;

namespace its.time.table.droid.Core
{
    public class Hour
    {
        private int a;
        public DateTime endTime;
        public DateTime startTime;

        public Hour(int a)
        {
            this.a = a;
        }

        public Hour(int a, DateTime startTime, DateTime endTime) : this(a)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public string Room { get; internal set; }
        public string Subject { get; internal set; }
        public string Teacher { get; internal set; }
        public string Week { get; internal set; }
    }
}