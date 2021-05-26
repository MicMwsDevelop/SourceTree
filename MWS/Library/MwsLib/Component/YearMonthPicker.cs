using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.Component
{
	public class YearMonthPicker : System.Windows.Forms.DateTimePicker
	{
		public YearMonthPicker()
		{
			ValueChanged += (sender, e) => { Value = Value.AddDays(Value.Day * -1 + 1); };
			Value = DateTime.Now;
			CustomFormat = "yyyy年MM月";
			Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		}
	}
}
