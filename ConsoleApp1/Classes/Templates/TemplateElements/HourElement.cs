using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.Templates.TemplateElements
{
	public class HourElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Hour;
		public override string Template => "HH";
		public override int MinValue  => 0;
		public override int MaxValue => 23;
	}
}
