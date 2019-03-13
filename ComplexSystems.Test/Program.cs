using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
					var scheduleString = "*.1,2,7-12.* 8-17/4:*:00";
					//(2000-2100).(6-8).(1-32) (8-17):(30-59):00
					//  2019			 9       11    4		 25	 00
					var time = "28.2.2021 19:45:39";

					var res = new Schedule(scheduleString).NearestEvent(DateTime.Parse(time));
					Console.WriteLine(res);

					var d = new DateTime();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

	        Console.ReadKey();
        }
    }
    

}
