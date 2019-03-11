using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.Templates.TemplateElements
{
	public class MinuteElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Minute;
        public override string Name => "Минута";
        public override string Template => "mm";
		public override int MinValue  => 0;
		public override int MaxValue => 59;
	}
}
