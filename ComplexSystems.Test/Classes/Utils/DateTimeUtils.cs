using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexSystems.Classes.Utils
{
	public static class DateTimeUtils
	{
		public static Dictionary<DateParts, int> ToDatePartsDictionary(this DateTime datetime)
			=> new Dictionary<DateParts, int>
			{
				{DateParts.Millisecond, datetime.Millisecond},
				{DateParts.Second, datetime.Second},
				{DateParts.Minute, datetime.Minute},
				{DateParts.Hour, datetime.Hour},
				{DateParts.Day, datetime.Day},
				{DateParts.DayWeek, (int) datetime.DayOfWeek},
				{DateParts.Month, datetime.Month},
				{DateParts.Year, datetime.Year}
			};

		public static DateTime ToDateTime(this Dictionary<DateParts, int> dateDict)
			=> new DateTime(
				dateDict.ContainsKey(DateParts.Year) ? dateDict[DateParts.Year] : DateTime.Now.Year,
				dateDict.ContainsKey(DateParts.Month) ? dateDict[DateParts.Month] : DateTime.Now.Month,
				dateDict.ContainsKey(DateParts.Day) ? dateDict[DateParts.Day] : DateTime.Now.Day,
				dateDict.ContainsKey(DateParts.Hour) ? dateDict[DateParts.Hour] : DateTime.Now.Hour,
				dateDict.ContainsKey(DateParts.Minute) ? dateDict[DateParts.Minute] : DateTime.Now.Minute,
				dateDict.ContainsKey(DateParts.Second) ? dateDict[DateParts.Second] : DateTime.Now.Second,
				dateDict.ContainsKey(DateParts.Millisecond) ? dateDict[DateParts.Millisecond] : DateTime.Now.Millisecond
			);
	}
}
