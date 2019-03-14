using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes.ScheduleElements.Elements;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes.ScheduleElements.ValueParsers
{
	public class IntListParser : ScheduleElementValueParser
	{
		private const char Delimiter = ',';

		public override bool IsMatch(string valueString) => valueString.Any(ch => ch == Delimiter);

		public override ScheduleElement GetValue(DatePartTemplateElement template, string valueString)
		{
			var parts = valueString.Split(Delimiter)
				 .Select(part => ScheduleElement.GetValue(template, part)).ToArray();

			return new IntListScheduleElement(template, parts);

		}
	}
}
