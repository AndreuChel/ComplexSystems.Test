using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes;
using ComplexSystems.Classes.ScheduleElements;
using ComplexSystems.Classes.Templates.TemplateElements;

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

        private readonly Dictionary<DateParts, ScheduleElement> _scheduleDict;

        public Schedule(): this("*.*.* * *:*:*.*") { }

        public Schedule(string scheduleString)
        {
            try
            {
                var supportFormatList = SupportFormats.Select(format => new ScheduleFormat(format)).ToArray();

                var allSeparators = supportFormatList.SelectMany(el => el.ScheduleItems).OfType<Separator>()
                    .Select(sep => sep.Value).Distinct().ToArray();

                var usedTemplate = supportFormatList.FirstOrDefault(format => format.IsMatch(scheduleString, allSeparators));

                if (usedTemplate == null)
                    throw new ArgumentException($"Не определен формат");
			
                var usedTemplateElements = usedTemplate.ScheduleItems.OfType<DatePartTemplateElement>().ToArray();

                _scheduleDict = scheduleString.Split(allSeparators)
                    .Select((te, index) => ScheduleElement.GetValue(usedTemplateElements[index], te)).ToArray()
                    .ToDictionary(se => se.ParentTemplate.DatePart, se => se);
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
	        throw new NotImplementedException();
        }

    }
}
