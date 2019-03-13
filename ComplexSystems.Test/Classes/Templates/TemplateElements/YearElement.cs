using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComplexSystems.Classes.Templates.TemplateElements
{
	public class YearElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Year;
		public override string Name => "Год";
		public override string Template => "yyyy";
		public override int MinValue  => 2000;
		public override int MaxValue => 2100;
	}

}
