using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes.ScheduleElements.Elements;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes.ScheduleElements.ValueParsers
{
	public class IntParser : ScheduleElementValueParser
	{
		public override bool IsMatch(string valueString)
		{
			var digits = "0123456789".ToCharArray();
			return valueString.All(ch => digits.Contains(ch));
		}

		public override ScheduleElement GetValue(DatePartTemplateElement template, string valueString)
			 => new IntScheduleElement(template, template.GetValue(valueString));

	}
}
