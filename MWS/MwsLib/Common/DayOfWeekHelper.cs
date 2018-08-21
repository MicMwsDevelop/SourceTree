//
// DayOfWeekHelper.cs
// 
// 曜日(enum System.DayOfWeek)に関するヘルパークラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Linq;

namespace MwsLib.Common
{
	public static class DayOfWeekHelper
    {
        /// <summary>
        /// 内部テーブルデータの定義
        /// </summary>
        private class Definition
        {
            /// <summary>曜日</summary>
            public DayOfWeek Day { get; set; }
            /// <summary>日本語文字列</summary>
            public string String { get; set; }
            /// <summary>英数(半角)文字列</summary>
            public string ANString { get; set; }
        }

        /// <summary>
        /// 内部テーブル
        /// </summary>
        private static readonly Definition[] m_DayOfWeekDefinitions = 
        {
            new Definition { Day = DayOfWeek.Sunday, String = "日", ANString = "SUN" },
            new Definition { Day = DayOfWeek.Monday, String = "月", ANString = "MON" },
            new Definition { Day = DayOfWeek.Tuesday, String = "火", ANString = "TUE" },
            new Definition { Day = DayOfWeek.Wednesday, String = "水", ANString = "WED" },
            new Definition { Day = DayOfWeek.Thursday, String = "木", ANString = "THU" },
            new Definition { Day = DayOfWeek.Friday, String = "金", ANString = "FRI" },
            new Definition { Day = DayOfWeek.Saturday, String = "土", ANString = "SAT" },
        };

        /// <summary>
        /// System.DayOfWeekに対する文字列(漢字 ex."日")を取得する
        /// </summary>
        public static string GetString(this DayOfWeek day)
        {
            return (from x in m_DayOfWeekDefinitions where x.Day == day select x.String).Single();
        }

        /// <summary>
        /// System.DayOfWeekに対する文字列(英３文字 ex."SUN")を取得する
        /// </summary>
        public static string GetANString(this DayOfWeek day)
        {
            return (from x in m_DayOfWeekDefinitions where x.Day == day select x.ANString).Single();
        }
    }
}
