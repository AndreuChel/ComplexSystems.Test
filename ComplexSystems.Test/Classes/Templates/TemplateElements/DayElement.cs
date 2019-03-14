using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.Templates.TemplateElements
{
	public class DayElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Day;
		public override string Name => "День";
		public override string Template => "dd";
		public override int MinValue => 1;
		public override int MaxValue => 32;

	}
}
