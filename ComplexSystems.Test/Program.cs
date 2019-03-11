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
                new Schedule("*.*.* 1-5 8-17:00:00");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

	        Console.ReadKey();
        }
    }
    

}
