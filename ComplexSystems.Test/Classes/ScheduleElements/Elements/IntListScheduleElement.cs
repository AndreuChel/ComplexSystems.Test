using ComplexSystems.Classes.ScheduleElements.ValueParsers;
using ComplexSystems.Classes.Templates.TemplateElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.ScheduleElements.Elements
{
    public class IntListScheduleElement : ScheduleElement
    {
        private List<ScheduleElement> IntListItems;
        public IntListScheduleElement(DatePartTemplateElement template, IEnumerable<ScheduleElement> elements)
            :base (template)
        {
            IntListItems = elements.ToList();
        }

        public override int Next(int value)
        {

	        var allNext =  IntListItems.Select(item => item.Next(value)).Where(val => val >= value).OrderBy(val=>val).ToArray();

	        if (!allNext.Any())
		        return ParentTemplate.MinValue;
			
	        return allNext.DefaultIfEmpty(allNext[0]).FirstOrDefault(val => val >= value);
        }

    }
}
