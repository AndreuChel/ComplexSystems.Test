using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public enum DateParts { Year, Month, Day, DayWeek, Hour, Minute, Second, Millisecond }

    public abstract class TemplateElement { }

    public class Separator : TemplateElement
    {
        public string Value { get; set; }
    }

    public abstract class DatePartTemplateElement : TemplateElement
    {
        public abstract string Template { get; }
        public abstract DateParts DatePart { get; }
    }

    [RangeValue(2000,2100)]
    public class YearElement : DatePartTemplateElement
    {
        public override DateParts DatePart => DateParts.Year;
        public override string Template => "yyyy";
    }

    [RangeValue(1, 12)]
    public class MonthElement : DatePartTemplateElement
    {
        public override DateParts DatePart => DateParts.Month;
        public override string Template => "MM";
    }


    public abstract class ValidationAttribute : Attribute
    {
        public abstract bool isValid(int value);
    }

    public class RangeValueAttribute : ValidationAttribute
    {
        private int _minValue;
        private int _maxValue;

        public RangeValueAttribute(int minValue, int maxValue)
        {
            this._minValue = minValue;
            this._maxValue = maxValue;
        }
        public override bool isValid(int value) => value >= _minValue && value <= _maxValue;
    }

    
    

}
