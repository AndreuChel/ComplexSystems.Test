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
    /// <summary>
	/// Класс для задания и расчета времени по расписанию.
	/// </summary>
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

        /// <summary>
		/// Создает пустой экземпляр, который будет соответствовать
		/// расписанию типа "*.*.* * *:*:*.*" (раз в 1 мс).
		/// </summary>
        public Schedule(): this("*.*.* * *:*:*.*") { }

        /// <summary>
		/// Создает экземпляр из строки с представлением расписания.
		/// </summary>
		/// <param name="scheduleString">Строка расписания.
		/// Формат строки:
		///     yyyy.MM.dd w HH:mm:ss.fff
		///     yyyy.MM.dd HH:mm:ss.fff
		///     HH:mm:ss.fff
		///     yyyy.MM.dd w HH:mm:ss
		///     yyyy.MM.dd HH:mm:ss
		///     HH:mm:ss
		/// Где yyyy - год (2000-2100)
		///     MM - месяц (1-12)
		///     dd - число месяца (1-31 или 32). 32 означает последнее число месяца
		///     w - день недели (0-6). 0 - воскресенье, 6 - суббота
		///     HH - часы (0-23)
		///     mm - минуты (0-59)
		///     ss - секунды (0-59)
		///     fff - миллисекунды (0-999). Если не указаны, то 0
		/// Каждую часть даты/времени можно задавать в виде списков и диапазонов.
		/// Например:
		///     1,2,3-5,10-20/3
		///     означает список 1,2,3,4,5,10,13,16,19
		/// Дробью задается шаг в списке.
		/// Звездочка означает любое возможное значение.
		/// Например (для часов):
		///     */4
		///     означает 0,4,8,12,16,20
		/// Вместо списка чисел месяца можно указать 32. Это означает последнее
		/// число любого месяца.
		/// Пример:
		///     *.9.*/2 1-5 10:00:00.000
		///     означает 10:00 во все дни с пн. по пт. по нечетным числам в сентябре
		///     *:00:00
		///     означает начало любого часа
		///     *.*.01 01:30:00
		///     означает 01:30 по первым числам каждого месяца
		/// </param>
        public Schedule(string scheduleString)
        {
            scheduleString = scheduleString.Trim();
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
			        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0)
                        .AddSeconds(1);
			        continue;
		        }
		        
		        //секунды
		        var s = result.Second;
		        var sNew = _schedule[DateParts.Second].Next(s);
		        if (s != sNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, s > sNew ? 0 : sNew, 0)
                        .AddMinutes(s > sNew ? 1 : 0);
			        continue;
		        }

		        //минуты
		        var m = result.Minute;
		        var mNew = _schedule[DateParts.Minute].Next(m);
		        if (m != mNew)
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, result.Hour, m > mNew ? 0 : mNew, 0, 0)
                        .AddHours(m > mNew ? 1 : 0);
			        continue;
		        }

		        //часы
		        var h = result.Hour;
		        var hNew = _schedule[DateParts.Hour].Next(h);
		        if (h != hNew )
		        {
			        result = new DateTime(result.Year, result.Month, result.Day, h > hNew ? 0 : hNew, 0, 0, 0)
                        .AddDays(h > hNew ? 1 : 0);
			        continue;
		        } 

		        //дни
		        var d = result.Day;
		        var dNew = _schedule[DateParts.Day].Next(d);

                var lastMonthDay = DateTime.DaysInMonth(result.Year, result.Month);
                dNew = dNew == 32 ? lastMonthDay : dNew;
                dNew = dNew > lastMonthDay ? 1 : dNew;

                if (d != dNew)
		        {
			        result = new DateTime(result.Year, result.Month, d > dNew ? 1 : dNew, 0, 0, 0, 0)
                        .AddMonths(d > dNew ? 1 : 0);
			        continue;
		        }

                //дни недели
                var dn = (int)result.DayOfWeek;
                var dnNew = _schedule[DateParts.DayWeek].Next(dn);
                if (dn != dnNew) {
                    var dayDiff = dn < dnNew ? dnNew - dn : 7 - dn + dnNew ;
                    result = new DateTime(result.Year, result.Month, result.Day).AddDays(dayDiff);
                    continue;
                }

                //месяцы
                var month = result.Month;
		        var monthNew = _schedule[DateParts.Month].Next(month);
		        if (month != monthNew)
		        {
			        result = new DateTime(result.Year, month > monthNew ? 1 : monthNew, 1, 0, 0, 0, 0)
                        .AddYears(month > monthNew ? 1 : 0);
			        continue;
		        }

                //годы
                var year = result.Year;
                var yearNew = _schedule[DateParts.Year].Next(year);

                if (yearNew < year)
                    return DateTime.MinValue;

                if (yearNew > year)
                {
                    result = new DateTime(yearNew,1,1,0,0,0,0);
                    continue;
                }

		        done = true;
	        }

	        return result;
        }

        /// <summary>
        /// Возвращает следующий момент времени в расписании.
        /// </summary>
        /// <param name="t1">Время, от которого нужно отступить</param>
        /// <returns>Следующий момент времени в расписании</returns>
        public DateTime NextEvent(DateTime t1)
            => NearestEvent(t1.AddMilliseconds(1));



    }
}
