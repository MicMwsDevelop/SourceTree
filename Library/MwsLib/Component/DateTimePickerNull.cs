//
// DateTimePickerNull.cs
// 
// DateTimePicker Null許容型クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/24 勝呂):新規作成
//
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MwsLib.Component
{
	/// <summary>
	/// null許容型のDateTimePicker
	/// IDEのプロパティ ウィンドウでコントロールのShowCheckBoxプロパティをTrueにする必要あり
	/// </summary>
	public class DateTimePickerNull : DateTimePicker
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DateTimePickerNull()
		{
		}

		[Bindable(true), Browsable(false)]
		public new object Value
		{
			get
			{
				if (base.Checked)
					return base.Value;
				else
					return DBNull.Value;
			}
			set
			{
				try
				{
					if (Convert.IsDBNull(value))
					{
						base.Checked = false;
					}
					else
					{
						base.Value = Convert.ToDateTime(value);
						base.Checked = true;
					}
				}
				catch
				{
					base.Value = Convert.ToDateTime(value);
					base.Checked = true;
				}
			}
		}

		/// <summary>
		/// DateTime? を取得
		/// チェックボックスがOFFの時にはnullを返す
		/// </summary>
		/// <returns></returns>
		public DateTime? ToDateTime()
		{
			if (base.Checked)
				return (DateTime)base.Value;
			else
				return null;
		}

		/// <summary>
		/// DateTimeの設定
		/// </summary>
		/// <param name="value"></param>
		public void SetDateTime(DateTime? tm)
		{
			if (tm.HasValue)
				this.Value = tm.Value;
			else
				this.Value = DBNull.Value;
		}
	}
}
