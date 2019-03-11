using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComplexSystems.Classes.ScheduleElements.Elements;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes.ScheduleElements.ValueParsers
{
    class IntRangeParser : ScheduleElementValueParser
    {
        private static Regex Pattern => new Regex(@"^(?<min>\d+)-(?<max>\d+)(/(?<step>\d+))?$");

        public override bool IsMatch(string valueString) => Pattern.IsMatch(valueString);

        public override ScheduleElement GetValue(DatePartTemplateElement template, string valueString)
        {
            var matches = Pattern.Matches(valueString);

            var minValue = template.GetValue(matches[0].Groups["min"].Value);
            var maxValue = template.GetValue(matches[0].Groups["max"].Value);
            if (minValue > maxValue)
            {
                var tmp = minValue; minValue = maxValue; maxValue = tmp;
            }

            var step = matches[0].Groups["step"].Length > 0 ? int.Parse(matches[0].Groups["step"].Value) : 1;

            return new IntRangeScheduleElement(template, minValue, maxValue, step);
        }
    }
}
