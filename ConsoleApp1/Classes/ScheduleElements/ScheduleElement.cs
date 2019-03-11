using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.ScheduleElements
{
	public abstract class ScheduleElement
	{
		public static ScheduleElement GetElement(string valueString)
		{
			/*
			private static Regex Pattern => new Regex(@"^(?<min>\d+)-(?<max>\d+)(/(?<step>\d+))?$");
			var matches = Pattern.Matches(inputString);
			MinValue = int.Parse(matches[0].Groups["min"].Value),
			MaxValue = int.Parse(matches[0].Groups["max"].Value),
			Step = matches[0].Groups["step"].Length > 0 ? int.Parse(matches[0].Groups["step"].Value) : 1
			*/

			/*
			 private static Regex Pattern => new Regex(@"^\d+$");
			 public override bool IsMatch(string inputString) => Pattern.IsMatch(inputString);
			 Value = int.Parse(inputString)
			*/
			
			/*
			private const char Delimiter = ',';
			public override bool IsMatch(string inputString) => inputString.Any(ch => ch == Delimiter);
			var parts = inputString.Split(Delimiter);
			return new ListScheduleElementValue
			{
				Values = parts.Select(p => p.GetScheduleElement()).ToList()
			};
			*/
			throw new NotImplementedException();
		}

	}

	public class IntListScheduleElement : ScheduleElement
	{
		
		
	}

	public class IntRangeScheduleElement : ScheduleElement { }


}
