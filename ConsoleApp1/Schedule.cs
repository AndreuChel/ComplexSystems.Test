using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes;
using ConsoleApp1.Classes.Templates.TemplateElements;

namespace ConsoleApp1
{
    public class Schedule
    {
        private static readonly List<ScheduleFormat> SupportFormatList = new List<ScheduleFormat>
        {
            new ScheduleFormat("yyyy.MM.dd w HH:mm:ss.fff"),
            new ScheduleFormat("yyyy.MM.dd HH:mm:ss.fff"),
            new ScheduleFormat("HH:mm:ss.fff"),
            new ScheduleFormat("yyyy.MM.dd w HH:mm:ss"),
            new ScheduleFormat("yyyy.MM.dd HH:mm:ss"),
            new ScheduleFormat("HH:mm:ss")
        };
        
        public Schedule(): this("*.*.* * *:*:*.*") { }
        public Schedule(string scheduleString)
        {
	        var allSeparators = SupportFormatList.SelectMany(el => el.ScheduleItems).OfType<Separator>()
		        .Select(sep => sep.Value).Distinct().ToArray();

            var usedTemplate = SupportFormatList.FirstOrDefault(format => format.IsMatch(scheduleString, allSeparators));

            if (usedTemplate == null)
	            throw new ArgumentException("Invalid scheduleString!");

				//в результате парсинга заполнить dictionary:
				//Dictionary<DateParts, ScheduleValue>
				//список "эл,эл,эл"(единичный элемент тоже список), диапазон "эл-эл/шаг" (min , max, step=1), * - тоже диапазон
				//список может содержать диапазон

        }
    }
}
