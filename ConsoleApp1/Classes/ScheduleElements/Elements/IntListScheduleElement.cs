using ConsoleApp1.Classes.ScheduleElements.ValueParsers;
using ConsoleApp1.Classes.Templates.TemplateElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.ScheduleElements.Elements
{
    public class IntListScheduleElement : ScheduleElement
    {
        private List<ScheduleElement> IntListItems;
        public IntListScheduleElement(DatePartTemplateElement template, IEnumerable<ScheduleElement> elements)
            :base (template)
        {
            IntListItems = elements.ToList();
        }
    }
}
