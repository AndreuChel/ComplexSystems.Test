using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.Templates.TemplateElements
{
	public abstract class DatePartTemplateElement : TemplateElement
	{
		public abstract string Template { get; }
		public abstract DateParts DatePart { get; }
		public abstract int MinValue { get; }
		public abstract int MaxValue { get; }

		public virtual int GetValue(string valueString)
		{
			var digits = "0123456789".ToCharArray();
			if (valueString.Any(ch => !digits.Contains(ch)))
				throw new ArgumentOutOfRangeException("Invalid date element value");

			var result = int.Parse(valueString);
			if (result < MinValue || result > MaxValue)
				throw new ArgumentOutOfRangeException("Date element out of range");

			return result;
		}
	}
}
