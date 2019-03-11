using ConsoleApp1.Classes.Templates.TemplateElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes.ScheduleElements.Elements
{
    public class IntScheduleElement : ScheduleElement
    {
        private int Value { get; set; }
        public IntScheduleElement(DatePartTemplateElement template, int value) 
            : base(template)
        {
            Value = value;
        }
        
    }
}
