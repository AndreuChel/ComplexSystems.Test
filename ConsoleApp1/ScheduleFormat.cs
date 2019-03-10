using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ScheduleFormat
    {
        public string FormatString { get; }
        public List<TemplateElement> SheduleItems { get; }
        
        public ScheduleFormat(string formatString, ScheduleFormatBuilder builder = null)
        {
            if (string.IsNullOrEmpty(formatString))
                throw new ArgumentNullException("formatString cannot be empty!");

            builder = builder ?? ScheduleFormatBuilder.Default;

            FormatString = formatString;
            SheduleItems = builder.Parse(FormatString)?.ToList() ?? new List<TemplateElement>();
        }

        public bool IsMatch(string scheduleString) => throw new NotImplementedException();
        
    }

    public class ScheduleFormatBuilder
    {
        public static ScheduleFormatBuilder Default => 
            new ScheduleFormatBuilder()
            .RegisterElement(new Separator())
            .RegisterElement(new YearElement())
            .RegisterElement(new MonthElement());

        private List<TemplateElement> _registerElements = new List<TemplateElement>();

        public ScheduleFormatBuilder RegisterElement(TemplateElement element) { return this; }

        public IEnumerable<TemplateElement> Parse(string formatString) { throw new NotImplementedException(); }
    }
}
