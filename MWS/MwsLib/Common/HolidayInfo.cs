//
// HolidayInfo.cs
//
// MIC休日情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MwsLib.Settings;

namespace MwsLib.Common
{
	/// <summary>
	/// 休日情報
	/// </summary>
	public class HolidayInfo
	{
		/// <summary>
		/// 休年
		/// </summary>
		public int Year { get; set; }

		/// <summary>
		/// 日付リスト
		/// </summary>
		public List<Date> DateList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public HolidayInfo()
		{
			Year = 0;
			DateList = new List<Date>();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Empty()
		{
			Year = 0;
			DateList.Clear();
		}

		/// <summary>
		/// 休日の設定
		/// </summary>
		/// <param name="str">休日文字列</param>
		/// ex.2018,0101,0211,0429,0503,0504,0505,0923,1103,1123,1223
		/// ※先頭フィールドは年度
		public void SetHoliday(string str)
		{
			this.Empty();

			string[] split = str.Split(',');
			if (1 < split.Length)
			{
				Year = int.Parse(split[0]);
				if (1 < split.Length)
				{
					for (int i = 1; i < split.Length; i++)
					{
						DateList.Add(Date.Parse(string.Format("{0:D4}/{1}/{2}", Year, split[i].Substring(0, 2), split[i].Substring(2))));
					}
				}
			}
		}
	}

	/// <summary>
	/// MIC休日判定クラス
	/// </summary>
	public static class CompanyHoliday
	{
		/// <summary>
		/// 日曜日
		/// </summary>
		public static bool Sunday = false;

		/// <summary>
		/// 月曜日
		/// </summary>
		public static bool Monday = false;

		/// <summary>
		/// 火曜日
		/// </summary>
		public static bool Tuesday = false;

		/// <summary>
		/// 水曜日
		/// </summary>
		public static bool Wednesday = false;

		/// <summary>
		/// 木曜日
		/// </summary>
		public static bool Thursday = false;

		/// <summary>
		/// 金曜日
		/// </summary>
		public static bool Friday = false;

		/// <summary>
		/// 土曜日
		/// </summary>
		public static bool Saturday = false;

		/// <summary>
		/// 祝日
		/// </summary>
		public static List<HolidayInfo> NationalHoliday = null;

		/// <summary>
		/// ハッピーマンデー
		/// </summary>
		public static List<HolidayInfo> HappyMonday = null;

		/// <summary>
		/// 臨時休日
		/// </summary>
		public static List<HolidayInfo> SpecialHolday = null;

		/// <summary>
		/// クリア
		/// </summary>
		public static void Empty()
		{
			Sunday = false;
			Monday = false;
			Tuesday = false;
			Wednesday = false;
			Thursday = false;
			Friday = false;
			Saturday = false;
			NationalHoliday = new List<HolidayInfo>();
			HappyMonday = new List<HolidayInfo>();
			SpecialHolday = new List<HolidayInfo>();
		}

		/// <summary>
		/// 休日の設定
		/// </summary>
		/// <param name="settings">MIC休日環境設定</param>
		public static void SetHoliday(MicHolidaySettings settings)
		{
			Empty();

			string[] split = settings.WeeklyHoliday.Split(',');
			if (7 == split.Length)
			{
				Sunday = ("1" == split[0]) ? true : false;
				Monday = ("1" == split[1]) ? true : false;
				Tuesday = ("1" == split[2]) ? true : false;
				Wednesday = ("1" == split[3]) ? true : false;
				Thursday = ("1" == split[4]) ? true : false;
				Friday = ("1" == split[5]) ? true : false;
				Saturday = ("1" == split[6]) ? true : false;
			}
			foreach (string str in settings.NationalHoliday)
			{
				HolidayInfo holiday = new HolidayInfo();
				holiday.SetHoliday(str);
				NationalHoliday.Add(holiday);
			}
			// ソート(昇順)
			NationalHoliday.Sort(delegate (HolidayInfo hi1, HolidayInfo hi2) { return hi1.Year - hi2.Year; });

			foreach (string str in settings.HappyMonday)
			{
				HolidayInfo holiday = new HolidayInfo();
				holiday.SetHoliday(str);
				HappyMonday.Add(holiday);
			}
			// ソート(昇順)
			HappyMonday.Sort(delegate (HolidayInfo hi1, HolidayInfo hi2) { return hi1.Year - hi2.Year; });

			foreach (string str in settings.SpecialHoliday)
			{
				HolidayInfo holiday = new HolidayInfo();
				holiday.SetHoliday(str);
				SpecialHolday.Add(holiday);
			}
			// ソート(昇順)
			SpecialHolday.Sort(delegate (HolidayInfo hi1, HolidayInfo hi2) { return hi1.Year - hi2.Year; });
		}

		/// <summary>
		/// 指定日付が休日かどうかを返す
		/// </summary>
		/// <param name="day">指定日付</param>
		/// <returns>true:指定日付が休日, false:指定日付は休日ではない</returns>
		public static bool IsHoliday(Date day)
		{
			// 休日
			if (IsWeeklyHoliday(day.DayOfWeek))
			{
				return true;
			}
			// 国民の休日
			if (IsNationalHoliday(day))
			{
				return true;
			}
			// 臨時休日
			if (IsSpecialHoliday(day))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 祝日の取得
		/// </summary>
		/// <remarks>
		/// 該当する年の休日が設定されていないときには前年の休日を引き継ぐ
		/// 
		/// 　※HappyHolidayの場合には使用禁止
		/// 　※HappyHolidayの場合は、環境設定の内容が異なる為
		/// </remarks>
		/// <param name="year">年</param>
		/// <returns>休日リスト</returns>
		private static List<Date> GetNationalHoliday(int year)
		{
			List<Date> dateList = new List<Date>();
			for (int i = NationalHoliday.Count - 1; i >= 0; i--)
			{
				HolidayInfo info = NationalHoliday[i];
				if (info.Year == year)
				{
					dateList = info.DateList;
					break;
				}
				else if (info.Year < year)
				{
					for (int n = 0; n < info.DateList.Count; n++)
					{
						dateList.Add(new Date(year, info.DateList[n].Month, info.DateList[n].Day));
					}
					break;
				}
			}
			return dateList;
		}

		/// <summary>
		/// HappyHolidayの取得
		/// </summary>
		/// <remarks>
		/// 該当する年の休日が設定されていないときには前年の休日を引き継ぐ
		/// 
		/// 　※通常の祝日の場合には使用禁止
		/// 　※通常の祝日の場合は、環境設定の内容が異なる為
		/// </remarks>
		/// <param name="year"></param>
		/// <returns></returns>
		private static List<Date> GetHappyMonday(int year)
		{
			// 環境設定の内容取得
			List<Date> iniDateList = new List<Date>();
			for (int i = HappyMonday.Count - 1; i >= 0; i--)
			{
				HolidayInfo info = HappyMonday[i];
				if (info.Year == year)
				{
					iniDateList = info.DateList;
					break;
				}
				else if (info.Year < year)
				{
					for (int n = 0; n < info.DateList.Count; n++)
					{
						iniDateList.Add(new Date(year, info.DateList[n].Month, info.DateList[n].Day));
					}
					break;
				}
			}

			// 日付に変換
			// 　※環境設定は●月の第△週という形式で保存されているため
			List<Date> dateList = new List<Date>();
			foreach (Date date in iniDateList)
			{
				int count = 1;
				int days = DateTime.DaysInMonth(date.Year, date.Month);
				for (int i = 1; i <= days; i++)
				{
					DateTime today = new DateTime(date.Year, date.Month, i);
					if (today.DayOfWeek == DayOfWeek.Monday)
					{
						if (count == date.Day)
						{
							dateList.Add(new Date(today));
						}
						count++;
					}
				}
			}
			return dateList;
		}

		/// <summary>
		/// 祝日の取得
		/// </summary>
		/// <remarks>
		/// 該当する年の休日が設定されていないときには前年の休日を引き継ぐ
		/// 
		/// 　※HappyHolidayの場合には使用禁止
		/// 　※HappyHolidayの場合は、環境設定の内容が異なる為
		/// </remarks>
		/// <param name="year">年</param>
		/// <returns>休日リスト</returns>
		private static List<Date> GetSpecialHoliday(int year)
		{
			List<Date> dateList = new List<Date>();
			for (int i = SpecialHolday.Count - 1; i >= 0; i--)
			{
				HolidayInfo info = SpecialHolday[i];
				if (info.Year == year)
				{
					dateList = info.DateList;
					break;
				}
				else if (info.Year < year)
				{
					for (int n = 0; n < info.DateList.Count; n++)
					{
						dateList.Add(new Date(year, info.DateList[n].Month, info.DateList[n].Day));
					}
					break;
				}
			}
			return dateList;
		}

		/// <summary>
		/// 休日かどうかを返す
		/// </summary>
		/// <param name="dayOfweek">曜日</param>
		/// <returns>true:指定曜日が休日, false:指定曜日は休日ではない</returns>
		private static bool IsWeeklyHoliday(DayOfWeek dayOfweek)
		{
			switch (dayOfweek)
			{
				case DayOfWeek.Sunday:
					return Sunday;
				case DayOfWeek.Monday:
					return Monday;
				case DayOfWeek.Tuesday:
					return Tuesday;
				case DayOfWeek.Wednesday:
					return Wednesday;
				case DayOfWeek.Thursday:
					return Thursday;
				case DayOfWeek.Friday:
					return Friday;
				case DayOfWeek.Saturday:
					return Saturday;
			}
			return false;
		}

		/// <summary>
		/// 臨時休日かどうかを返す
		/// </summary>
		/// <param name="date">日付</param>
		/// <returns>true：休日、false：休日以外</returns>
		private static bool IsSpecialHoliday(Date date)
		{
			List<Date> specialHoldayList = GetSpecialHoliday(date.Year);
			if (specialHoldayList.Exists(x => x == date))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 国民の休日かどうかを返す
		/// </summary>
		/// <param name="date">指定日付</param>
		/// <returns>true：休日、false：休日以外</returns>
		public static bool IsNationalHoliday(Date date)
		{
			// 春分の日
			if (date.IsVernalEquinoxDay())
			{
				return true;
			}
			// 秋分の日
			if (date.IsAutumnEquinoxDay())
			{
				return true;
			}
			// 年間の休日取得
			List<Date> nationHolidayList = GetNationalHoliday(date.Year);

			// 休日が存在するか
			if (nationHolidayList.Exists(x => x == date))
			{
				return true;
			}
			// 振替休日
			if (DayOfWeek.Monday == date.DayOfWeek)
			{
				// 検索日が月曜日
				if (IsWeeklyHoliday(DayOfWeek.Sunday))
				{
					// 日曜日が休日で国民の休日の時には振替休日
					Date yesterday = date - 1;
					if (yesterday.IsVernalEquinoxDay())
					{
						return true;
					}
					// 秋分の日
					if (yesterday.IsAutumnEquinoxDay())
					{
						return true;
					}
					// 国民の休日
					if (nationHolidayList.Exists(x => x == yesterday))
					{
						return true;
					}
				}
			}
			// HappyMondayの休日取得
			List<Date> happyMondayList = GetHappyMonday(date.Year);

			// HappyMondayが存在するか
			if (happyMondayList.Exists(x => x == date))
			{
				return true;
			}
			return false;
		}
	}
}
