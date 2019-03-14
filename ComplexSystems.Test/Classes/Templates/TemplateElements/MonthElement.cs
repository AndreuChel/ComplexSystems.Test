using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.Templates.TemplateElements
{
	public class MonthElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Month;
		public override string Name => "Месяц";
		public override string Template => "MM";
		public override int MinValue => 1;
		public override int MaxValue => 12;

	}

}
