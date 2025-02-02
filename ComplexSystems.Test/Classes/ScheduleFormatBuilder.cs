﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexSystems.Classes.Templates;
using ComplexSystems.Classes.Templates.TemplateElements;

namespace ComplexSystems.Classes
{
	/// <summary>
	/// Вспомогательный класс для парсинга формата расписания
	/// </summary>
	public class ScheduleFormatParser
	{
		/// <summary> Типовой парсер </summary>
		public static ScheduleFormatParser Default =>
			new ScheduleFormatParser()
				.RegisterElement(new Separator()).RegisterElement(new YearElement()).RegisterElement(new MonthElement())
				.RegisterElement(new DayWeekElement()).RegisterElement(new DayElement()).RegisterElement(new HourElement())
				.RegisterElement(new MinuteElement()).RegisterElement(new SecondElement()).RegisterElement(new MillisecondElement());

		readonly List<TemplateElement> _registerElements = new List<TemplateElement>();

		public ScheduleFormatParser RegisterElement(TemplateElement element)
		{
			_registerElements.Add(element);
			return this;
		}

		/// <summary>
		/// Разбор строки формата для расписания
		/// </summary>
		public IEnumerable<TemplateElement> Parse(string formatString)
		{
			var usedSymbols = string.Join("", _registerElements.OfType<DatePartTemplateElement>().SelectMany(el => el.Template)).Distinct().ToArray();

			var result = new List<TemplateElement>();
			var counter = 0;
			var buffer = new StringBuilder();

			foreach (var ch in formatString)
			{
				var isSeparator = !usedSymbols.Contains(ch);

				if (!isSeparator) buffer.Append(ch);

				if (isSeparator || counter == formatString.Length - 1)
				{
					if (buffer.Length > 0)
					{
						var currentTemplate = _registerElements.OfType<DatePartTemplateElement>()
							.FirstOrDefault(templ => templ.Template == buffer.ToString());
						if (currentTemplate == null)
							throw new Exception($"Ошибка разбора формата \"{formatString}\"");
						result.Add(currentTemplate);
						buffer.Clear();
					}

					if (isSeparator)
						result.Add(new Separator { Value = ch });
				}

				counter++;
			}
			if (result.OfType<DatePartTemplateElement>().GroupBy(elem => elem.DatePart).Any(g => g.Count() > 1))
				throw new Exception($"Ошибка разбора формата \"{formatString}\"");

			return result;
		}

	}

}
