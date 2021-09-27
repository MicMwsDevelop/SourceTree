//
// SqlServerHelper.cs
// 
// SQL Server ヘルパーメソッド
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/06 勝呂)
//
using CommonLib.Common;

namespace CommonLib.DB.SqlServer
{
	public static class SqlServerHelper
	{
		/// <summary>
		/// 指定日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		private static string ToDate(Date? date)
		{
			if (date.HasValue)
			{
				return string.Format("DATEFROMPARTS({0}, {1}, {2})", date.Value.Year, date.Value.Month, date.Value.Day);
			}
			return "getdate()";
		}

		/// <summary>
		/// 先月初日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		public static string FirstDayOfLasMonth(Date? date)
		{
			return string.Format("DATEADD(dd, 1, EOMONTH({0}, -2))", ToDate(date));
		}

		/// <summary>
		/// 先月末日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		public static string LastDayOfLasMonth(Date? date)
		{
			return string.Format("EOMONTH({0}, -1)", ToDate(date));
		}

		/// <summary>
		/// 当月初日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		public static string FirstDayOfTheMonth(Date? date)
		{
			return string.Format("DATEADD(dd, 1, EOMONTH({0} , -1))", ToDate(date));
		}

		/// <summary>
		/// 当月末日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		public static string LastDayOfTheMonth(Date? date)
		{
			return string.Format("EOMONTH({0})", ToDate(date));
		}

		/// <summary>
		/// 翌月初日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		public static string FirstDayOfNextMonth(Date? date)
		{
			return string.Format("DATEADD(dd, 1, EOMONTH({0})", ToDate(date));
		}

		/// <summary>
		/// 翌月末日のSQL文字列の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>SQL文字列</returns>
		public static string LastDayOfNextMonth(Date? date)
		{
			return string.Format("EOMONTH({0}, 1) ", ToDate(date));
		}

		/// <summary>
		/// 先月初日のYYYYMMDD形式の整数値の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>YYYYMMDD形式の整数値</returns>
		public static int FirstDayOfLasMonthToIntYMD(Date? date)
		{
			if (date.HasValue)
			{
				return date.Value.FirstDayOfLasMonth().ToIntYMD();
			}
			return Date.Today.FirstDayOfLasMonth().ToIntYMD();
		}

		/// <summary>
		/// 先月末日のYYYYMMDD形式の整数値の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>YYYYMMDD形式の整数値</returns>
		public static int LastDayOfLasMonthToIntYMD(Date? date)
		{
			if (date.HasValue)
			{
				return date.Value.LastDayOfLasMonth().ToIntYMD();
			}
			return Date.Today.LastDayOfLasMonth().ToIntYMD();
		}

		/// <summary>
		/// 当月初日のYYYYMMDD形式の整数値の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>YYYYMMDD形式の整数値</returns>
		public static int FirstDayOfTheMonthToIntYMD(Date? date)
		{
			if (date.HasValue)
			{
				return date.Value.FirstDayOfTheMonth().ToIntYMD();
			}
			return Date.Today.FirstDayOfTheMonth().ToIntYMD();
		}

		/// <summary>
		/// 当月末日のYYYYMMDD形式の整数値の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>YYYYMMDD形式の整数値</returns>
		public static int LastDayOfTheMonthToIntYMD(Date? date)
		{
			if (date.HasValue)
			{
				return date.Value.LastDayOfTheMonth().ToIntYMD();
			}
			return Date.Today.LastDayOfTheMonth().ToIntYMD();
		}

		/// <summary>
		/// 翌月初日のYYYYMMDD形式の整数値の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>YYYYMMDD形式の整数値</returns>
		public static int FirstDayOfNextMonthToIntYMD(Date? date)
		{
			if (date.HasValue)
			{
				return date.Value.FirstDayOfNextMonth().ToIntYMD();
			}
			return Date.Today.FirstDayOfNextMonth().ToIntYMD();
		}

		/// <summary>
		/// 翌月末日のYYYYMMDD形式の整数値の取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <returns>YYYYMMDD形式の整数値</returns>
		public static int LastDayOfNextMonthToIntYMD(Date? date)
		{
			if (date.HasValue)
			{
				return date.Value.LastDayOfNextMonth().ToIntYMD();
			}
			return Date.Today.LastDayOfNextMonth().ToIntYMD();
		}
	}
}
