using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.Templates.TemplateElements
{
	public class DayElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Day;
		public override string Template => "dd";
		public override int MinValue  => 1;
		public override int MaxValue => 32;
	}
}
