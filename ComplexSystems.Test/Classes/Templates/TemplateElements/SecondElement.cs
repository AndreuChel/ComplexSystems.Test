using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComplexSystems.Classes.Templates.TemplateElements
{

	public class SecondElement : DatePartTemplateElement
	{
		public override DateParts DatePart => DateParts.Second;
        public override string Name => "Секунда";
        public override string Template => "ss";
		public override int MinValue  => 0;
		public override int MaxValue => 59;
	}
}
