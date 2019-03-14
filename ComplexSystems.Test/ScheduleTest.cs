using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems
{

	/// <summary>
	/// Вспомогательный класс для тестирования
	/// </summary>
	public class ScheduleTest
	{
		private readonly Schedule _schedule;

		private readonly bool _isValid = true;

		public string ScheduleString { get; }

		public List<string> ResultList = new List<string>();

		public ScheduleTest(string scheduleString)
		{
			ScheduleString = scheduleString;
			try
			{
				_schedule = new Schedule(ScheduleString);
			}
			catch (Exception)
			{
				_isValid = false;
			}
		}

		public ScheduleTest TestNearestEvent(string srcTime, string nextTime)
		{
			if (!_isValid) return this;

			var result = _schedule.NearestEvent(DateTime.Parse(srcTime)) == DateTime.Parse(nextTime);
			ResultList.Add($" NearestEvent:     {srcTime} ==> {nextTime}\t\t{(result ? "OK" : "Fail")}");

			return this;
		}

		public ScheduleTest TestNextEvent(string srcTime, string nextTime)
		{
			if (!_isValid) return this;

			var result = _schedule.NextEvent(DateTime.Parse(srcTime)) == DateTime.Parse(nextTime);
			ResultList.Add($" NextEvent:        {srcTime} ==> {nextTime}\t\t{(result ? "OK" : "Fail")}");

			return this;
		}

		public ScheduleTest TestNearestPrevEvent(string srcTime, string prevTime)
		{
			if (!_isValid) return this;

			var result = _schedule.NearestPrevEvent(DateTime.Parse(srcTime)) == DateTime.Parse(prevTime);
			ResultList.Add($" NearestPrevEvent: {srcTime} ==> {prevTime}\t\t{(result ? "OK" : "Fail")}");

			return this;

		}
		public ScheduleTest TestPrevEvent(string srcTime, string prevTime)
		{
			if (!_isValid) return this;

			var result = _schedule.PrevEvent(DateTime.Parse(srcTime)) == DateTime.Parse(prevTime);
			ResultList.Add($" PrevEvent:        {srcTime} ==> {prevTime}\t\t{(result ? "OK" : "Fail")}");

			return this;

		}
		

		public void PrintResult()
		{
			Console.WriteLine($"\r\n{ScheduleString}{(_isValid ? "" : "(Invalid)")}");
			Console.WriteLine("------------------------");
			Console.WriteLine(string.Join("\r\n", ResultList));
			Console.WriteLine("========================");
		}

	}
}
