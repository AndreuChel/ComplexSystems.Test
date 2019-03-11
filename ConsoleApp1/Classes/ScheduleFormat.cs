using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes.Templates;
using ConsoleApp1.Classes.Templates.TemplateElements;

namespace ConsoleApp1.Classes
{
	public class ScheduleFormat
	{
		public string FormatString { get; }
		public List<TemplateElement> ScheduleItems { get; }
        
		public ScheduleFormat(string formatString, ScheduleFormatBuilder builder = null)
		{
			if (string.IsNullOrEmpty(formatString))
				throw new ArgumentException("Строка, задающая формат не может быть пустой");

			builder = builder ?? ScheduleFormatBuilder.Default;

			FormatString = formatString;
			ScheduleItems = builder.Parse(FormatString)?.ToList() ?? new List<TemplateElement>();
		}

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
