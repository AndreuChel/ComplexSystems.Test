using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Schedule
    {
        private static List<ScheduleFormat> _supportFormatList = new List<ScheduleFormat>
        {
            new ScheduleFormat("yyyy.MM.dd w HH:mm:ss.fff"),
            new ScheduleFormat("yyyy.MM.dd HH:mm:ss.fff")
        };
        
        public Schedule(): this("*.*.* **:*:*.* ") { }
        public Schedule(string scheduleString)
        {
            var formats = _supportFormatList.Where(format => format.IsMatch(scheduleString)).ToList();
        }
    }
}
