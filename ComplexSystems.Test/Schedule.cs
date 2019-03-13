using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes;
using ComplexSystems.Classes.ScheduleElements;
using ComplexSystems.Classes.Templates.TemplateElements;
using ComplexSystems.Classes.Utils;

namespace ComplexSystems
{
    public class Schedule
    {
        private static readonly List<string> SupportFormats = new List<string>
        {
            "yyyy.MM.dd w HH:mm:ss.fff",
            "yyyy.MM.dd HH:mm:ss.fff",
            "HH:mm:ss.fff",
            "yyyy.MM.dd w HH:mm:ss",
            "yyyy.MM.dd HH:mm:ss",
            "HH:mm:ss"
        };

        private readonly Dictionary<DateParts, ScheduleElement> _schedule;

        public Schedule(): this("*.*.* * *:*:*.*") { }

        public Schedule(string scheduleString)
        {
            try
            {
                var supportFormatList = SupportFormats.Select(format => new ScheduleFormat(format)).ToArray();

                var allSeparators = supportFormatList.SelectMany(el => el.ScheduleItems).OfType<Separator>()
                    .Select(sep => sep.Value).Distinct().ToArray();
                var allTemplateElements = supportFormatList.SelectMany(el => el.ScheduleItems).OfType<DatePartTemplateElement>().ToArray();

                var usedTemplate = supportFormatList.FirstOrDefault(format => format.IsMatch(scheduleString, allSeparators));

                if (usedTemplate == null)
                    throw new ArgumentException($"Не определен формат");
			
                var usedTemplateElements = usedTemplate.ScheduleItems.OfType<DatePartTemplateElement>().ToArray();

                _schedule = scheduleString.Split(allSeparators)
                    .Select((te, index) => ScheduleElement.GetValue(usedTemplateElements[index], te)).ToArray()
                    .ToDictionary(se => se.ParentTemplate.DatePart, se => se);

					//Для не определенных в шаблоне элементов добавляем значения по умолчанию	
					((int[])Enum.GetValues(typeof(DateParts)))
						.Where(dp => !_schedule.ContainsKey((DateParts)dp) && allTemplateElements.Any(te => te.DatePart == (DateParts)dp))
						.Select(dp => allTemplateElements.First(te => te.DatePart == (DateParts)dp))
						.ToList()
						.ForEach(te => _schedule.Add(te.DatePart, ScheduleElement.GetDefaultValue(te)));
			}
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Ошибка создания расписания \"{scheduleString}\". {ex.Message}");
            }
        }

        /// <summary>
        /// Возвращает следующий ближайший к заданному времени момент в расписании или
        /// само заданное время, если оно есть в расписании.
        /// </summary>
        /// <param name="t1">Заданное время</param>
        /// <returns>Ближайший момент времени в расписании</returns>
        public DateTime NearestEvent(DateTime t1)
        {
	        var result = new DateTime(t1.Year, t1.Month, t1.Day, t1.Hour, t1.Minute, t1.Second, t1.Millisecond);
	        var done = false;
	        while (!done)
	        {
				  //миллисекунды
		        var ms = result.Millisecond;
		        var msNew = _schedule[DateParts.Millisecond].Next(ms);
		        if (ms > msNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0).AddSeconds(1);
			        continue;
		        }
		        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, msNew);
		        
		        //секунды
		        var s = result.Second;
		        var sNew = _schedule[DateParts.Second].Next(s);
		        if (s > sNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, 0, 0).AddMinutes(1);
			        continue;
		        }
		        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, sNew, result.Millisecond);

		        //минуты
		        var m = result.Minute;
		        var mNew = _schedule[DateParts.Minute].Next(m);
		        if (m > mNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, 0, 0, 0).AddHours(1);
			        continue;
		        }
		        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, mNew, result.Second, result.Millisecond);

		        //часы
		        var h = result.Hour;
		        var hNew = _schedule[DateParts.Hour].Next(h);
		        if (h > hNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, 0, 0, 0, 0).AddDays(1);
			        continue;
		        } else if (h < hNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, hNew, 0, 0, 0);
			        continue;
		        } else
			        result = new DateTime(result.Year, result.Month, result.Day, hNew, result.Minute, result.Second, result.Millisecond);

		        //дни
		        var d = result.Day;
		        var dNew = _schedule[DateParts.Day].Next(d);
		        if (d > dNew)
		        {
			        result = new DateTime(result.Year, result.Month, 0, 0, 0, 0, 0).AddMonths(1);
			        continue;
		        }
		        result = new DateTime(result.Year, result.Month, dNew, result.Hour, result.Minute, result.Second, result.Millisecond);

		        //месяцы
		        var month = result.Month;
		        var monthNew = _schedule[DateParts.Month].Next(month);
		        if (month > monthNew)
		        {
			        result = new DateTime(result.Year, 0, 0, 0, 0, 0, 0).AddYears(1);
			        continue;
		        }
		        result = new DateTime(result.Year, monthNew, result.Day, result.Hour, result.Minute, result.Second, result.Millisecond);

		        //годы
		        result = new DateTime(_schedule[DateParts.Year].Next(result.Year), monthNew, result.Day, result.Hour, result.Minute, result.Second, result.Millisecond);

		        done = true;
	        }

	        return result;
        }

    }
}
