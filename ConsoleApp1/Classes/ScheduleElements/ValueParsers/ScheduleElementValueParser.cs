using ConsoleApp1.Classes.Templates.TemplateElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.ScheduleElements.ValueParsers
{
    public abstract class ScheduleElementValueParser
    {
        public abstract bool IsMatch(string valueString);

        public abstract ScheduleElement GetValue(DatePartTemplateElement template, string valueString);
    }
}
