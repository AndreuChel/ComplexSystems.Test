using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems
{
	class Program
	{
		static void Main()
		{
			try
			{
				var tests = new List<ScheduleTest> {
					  new ScheduleTest("*:*:00")
							.TestNearestEvent("01.01.2019 19:45:39","01.01.2019 19:46:00")
							.TestNextEvent("01.01.2019 19:45:39","01.01.2019 19:46:00")
							.TestNearestPrevEvent("01.01.2019 19:45:39","01.01.2019 19:45:00")
							.TestNearestPrevEvent("01.01.2019 19:45:00","01.01.2019 19:45:00")
							.TestNextEvent("01.01.2019 19:46:00","01.01.2019 19:47:00")
							.TestPrevEvent("01.01.2019 19:45:00","01.01.2019 19:44:00")
							.TestNearestEvent("01.01.2019 19:59:39","01.01.2019 20:00:00")
							.TestNearestEvent("01.01.2019 23:59:39","02.01.2019 00:00:00")
							.TestPrevEvent("01.01.2019 00:00:00","31.12.2018 23:59:00")

					, new ScheduleTest("2020-2022.1.* 0,6 8-17/2:00:00")
							.TestNearestEvent("01.01.2019 19:45:39","04.01.2020 08:00:00")
							.TestNextEvent("04.01.2020 08:00:00","04.01.2020 10:00:00")
							.TestNextEvent("05.01.2020 16:00:00","11.01.2020 08:00:00")
							.TestPrevEvent("05.01.2020 16:00:00","05.01.2020 14:00:00")
							.TestPrevEvent("05.01.2020 08:00:00","04.01.2020 16:00:00")
							.TestPrevEvent("02.01.2021 08:00:00","26.01.2020 16:00:00")

					, new ScheduleTest("*.*.* * *:*:*.*")
							.TestNearestEvent("01.01.2019 19:45:39.555","01.01.2019 19:45:39.555")
							.TestNextEvent("01.01.2019 19:45:39.555","01.01.2019 19:45:39.556")
							.TestNextEvent("01.01.2019 23:59:59.999","02.01.2019 00:00:00.000")
							.TestPrevEvent("01.01.2019 23:59:59.999","01.01.2019 23:59:59.998")
							.TestPrevEvent("01.01.2019 00:00:00.000","31.12.2018 23:59:59.999")
							.TestPrevEvent("01.03.2019 00:00:00.000","28.02.2019 23:59:59.999")
							.TestPrevEvent("01.03.2020 00:00:00.000","29.02.2020 23:59:59.999")

					, new ScheduleTest("*.*.32 00:00:00")
							.TestNearestEvent("01.02.2019 19:45:39","28.02.2019 0:00:00")
							.TestPrevEvent("01.03.2019 19:45:39","28.02.2019 0:00:00")
							.TestPrevEvent("01.04.2019 19:45:39","31.03.2019 0:00:00")

					, new ScheduleTest("*.*.31 00:00:00")
							.TestNearestEvent("01.02.2019 19:45:39","31.03.2019 0:00:00")
							.TestPrevEvent("15.03.2019 19:45:39","31.01.2019 0:00:00")

					, new ScheduleTest("*.1,2,7-12.* 8-17/4:*:00")
							.TestNearestEvent("28.2.2019 19:45:39","01.07.2019 8:00:00")
							.TestNextEvent("28.2.2019 19:45:39","01.07.2019 8:00:00")
							.TestNextEvent("01.07.2019 8:00:00","01.07.2019 08:01:00")
							.TestNextEvent("01.07.2019 8:59:00","01.07.2019 12:00:00")
							.TestNearestEvent("28.1.2019 10:45:39","28.01.2019 12:00:00")
							.TestNearestEvent("28.1.2019 12:45:39","28.01.2019 12:46:00")
							.TestNearestEvent("31.12.2019 16:59:00","31.12.2019 16:59:00")
							.TestNextEvent("31.12.2019 16:59:00","01.01.2020 08:00:00")
							.TestPrevEvent("31.12.2019 16:59:00","31.12.2019 16:58:00")
							.TestPrevEvent("31.12.2019 16:00:00","31.12.2019 12:59:00")
							.TestPrevEvent("01.07.2019 08:00:00","28.02.2019 16:59:00")

					, new ScheduleTest("*.9.*/2 1-5 10:00:00.000")
						.TestNextEvent("28.2.2019 19:45:39","03.09.2019 10:00:00")
				};

				tests.ForEach(t => t.PrintResult());

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}
	}


}
