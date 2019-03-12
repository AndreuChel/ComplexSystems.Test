using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes.ScheduleElements.ValueParsers;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes.ScheduleElements
{

    public abstract class ScheduleElement
    {
        private static readonly List<ScheduleElementValueParser> SupportParsers  = 
            new List<ScheduleElementValueParser> {
                new IntParser(), new IntListParser(), new AnyIntParser(), new IntRangeParser()
            };

        public static ScheduleElement GetValue(DatePartTemplateElement template, string valueString) {

            var valueParser = SupportParsers.FirstOrDefault(parser => parser.IsMatch(valueString));
            if (valueParser == null)
                throw new ArgumentException($"Неверный формат параметра \"{template.Name}\"");
            
            return valueParser.GetValue(template, valueString);
        }

        protected ScheduleElement(DatePartTemplateElement template)
        {
            ParentTemplate = template;
        }

        public DatePartTemplateElement ParentTemplate { get; private set; }

        public abstract int Next(int value);

    }


}
