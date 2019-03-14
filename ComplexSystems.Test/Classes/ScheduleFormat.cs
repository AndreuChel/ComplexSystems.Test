using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes.Templates;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes
{
	/// <summary>
	/// Класс формата для расписания
	/// </summary>
	public class ScheduleFormat
	{
		public string FormatString { get; }
		public List<TemplateElement> ScheduleItems { get; }

		public ScheduleFormat(string formatString, ScheduleFormatParser builder = null)
		{
			if (string.IsNullOrEmpty(formatString))
				throw new ArgumentException("Строка, задающая формат не может быть пустой");

			builder = builder ?? ScheduleFormatParser.Default;

			FormatString = formatString;
			ScheduleItems = builder.Parse(FormatString)?.ToList() ?? new List<TemplateElement>();
		}

		/// <summary>
		/// Проверка соответствия строки расписания текущему шаблону
		/// </summary>
		/// <param name="scheduleString">строка расписания</param>
		/// <param name="allSeparators">общий массив символов-разделителей</param>
		/// <returns></returns>
		public bool IsMatch(string scheduleString, IEnumerable<char> allSeparators)
		{
			var tempalteSeparators = ScheduleItems.OfType<Separator>().Select(s => s.Value).ToArray();
			var scheduleSeparators = scheduleString.Where(ch => allSeparators.ToArray().Contains(ch)).ToArray();

			return tempalteSeparators.Length == scheduleSeparators.Length &&
					 tempalteSeparators.Select((el, i) => el == scheduleSeparators[i])
						 .Aggregate(true, (res, next) => res && next);

		}

	}
}
