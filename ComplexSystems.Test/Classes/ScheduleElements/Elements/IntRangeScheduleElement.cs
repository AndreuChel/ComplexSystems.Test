using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes.ScheduleElements.Elements
{
	public class IntRangeScheduleElement : ScheduleElement
	{
		private int MinValue { get; }
		private int MaxValue { get; }
		private int Step { get; }

		public IntRangeScheduleElement(DatePartTemplateElement template, int minValue, int maxValue, int step)
			 : base(template)
		{
			MinValue = minValue;
			MaxValue = maxValue;
			Step = step;
		}

		public override int Next(int value)
		{
			var result = Enumerable.Range(0, int.MaxValue).Select(i => MinValue + Step * i).First(nv => nv >= value);
			if (result < MinValue || result > MaxValue)
				result = MinValue;

			return result;
		}

		
		public override int Prev(int value)
		{
			var maxRangeValue = MaxValue - (MaxValue - MinValue) % Step;

			var result = maxRangeValue;
			if (value >= MinValue && value <= maxRangeValue)
			{
				result = Enumerable.Range(0, Step).Select(s => value - s).First(val => val % Step == 0);
			}

			return result;
		}

	}
}
