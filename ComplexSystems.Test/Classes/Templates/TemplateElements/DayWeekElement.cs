using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.Templates.TemplateElements
{
	public class DayWeekElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.DayWeek;
		public override string Name => "День недели";
		public override string Template => "w";
		public override int MinValue  => 0;
		public override int MaxValue => 6;

	}
}
