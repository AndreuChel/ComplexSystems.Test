using ComplexSystems.Classes.ScheduleElements.Elements;
using ComplexSystems.Classes.Templates.TemplateElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.ScheduleElements.ValueParsers
{
	public class AnyIntParser : ScheduleElementValueParser
	{
		private static Regex Pattern => new Regex(@"^(?<all>\*)(/(?<step>\d+))?$");

		public override bool IsMatch(string valueString) => Pattern.IsMatch(valueString);

		public override ScheduleElement GetValue(DatePartTemplateElement template, string valueString)
		{
			var matches = Pattern.Matches(valueString);
			var step = matches[0].Groups["step"].Length > 0 ? int.Parse(matches[0].Groups["step"].Value) : 1;

			return new IntRangeScheduleElement(template, template.MinValue, template.MaxValue, step);
		}
	}
}
