using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.Templates.TemplateElements
{
	public class MillisecondElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Millisecond;
		public override string Name => "Миллисекунда";
		public override string Template => "fff";
		public override int MinValue => 0;
		public override int MaxValue => 999;
		public override string DefaultValue => "0";

	}
}
