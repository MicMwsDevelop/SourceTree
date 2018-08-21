//
// DateConversion.cs
// 
// 日付,月,期間関連変換ヘルパークラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
namespace MwsLib.Common
{
	public static class DateConversion
    {
        /// <summary>
        /// 日付の属する月を取得
        /// </summary>
        public static YearMonth ToYearMonth(this Date date)
        {
            return new YearMonth(date.Year, date.Month);
        }

        // Ver1.001 Dateの拡張メソッドToDate()に、特定日ではなく最終日指定の方法を追加
        /// <summary>
        /// 月のいずれかの日を表す日付を取得
        /// </summary>
        /// <param name="day">取得する日付の日を指定。その月の最終日を指定する場合は-1を指定する。</param>
        public static Date ToDate(this YearMonth month, int day)
        {
            if (day == -1)
            {
                // その月の最終日
                return new Date(month.Year, month.Month, month.GetDays());
            }
            else
            {
                return new Date(month.Year, month.Month, day);
            }
        }

        /// <summary>
        /// 指定日１日を表す期間を取得
        /// </summary>
        public static Span ToSpan(this Date date)
        {
            return new Span(date, date);
        }

        /// <summary>
        /// 指定月１か月あらわす期間を取得
        /// </summary>
        public static Span ToSpan(this YearMonth month)
        {
            return new Span(month.ToDate(1), month.ToDate(month.GetDays()));
        }

        /// <summary>
        /// 標準文字列からの変換
        /// </summary>
        public static Date ToDate(this string str)
        {
            return Date.Parse(str);
        }

        /// <summary>
        /// 標準文字列からの変換
        /// </summary>
        public static YearMonth ToYearMonth(this string str)
        {
            return YearMonth.Parse(str);
        }
        
        public static Span ToSpan(this string str)
        {
            return Span.Parse(str);
        }

        /// <summary>
        /// 日付からYYYYMMDD形式の整数値に変換
        /// </summary>
        public static int ToIntYMD(this Date date)
        {
            return date.Year * 10000 + date.Month * 100 + date.Day;
        }

        /// <summary>
        /// YYYYMMDD形式の整数値から日付への変換
        /// </summary>
        public static Date YMDToDate(this int ymd)
        {
            int year = ymd / 10000;
            int month = (ymd % 10000) / 100;
            int day = ymd % 100;

            return new Date(year, month, day);
        }



        /// <summary>
        /// YYYYMMDD形式の整数値から日付への変換
        /// </summary>
        /// <returns>
        /// 日付。ymdが0の場合はnull
        /// </returns>
        public static Date? YMDToDateOrNull(this int ymd)
        {
            if (ymd == 0)
            {
                return null;
            }
            else
            {
                int year = ymd / 10000;
                int month = (ymd % 10000) / 100;
                int day = ymd % 100;

                return new Date(year, month, day);
            }
        }


        /// <summary>
        /// 日付からYYYYMMDD形式の整数値に変換
        /// </summary>
        /// <returns>
        /// YYYYMMDD形式のint値。dateがnullの場合は0;
        /// </returns>
        public static int ToIntYMD(this Date ? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToIntYMD();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 年月からYYYYMM形式の整数値に変換
        /// </summary>
        public static int ToIntYM(this YearMonth month)
        {
            return month.Year * 100 + month.Month;
        }

        /// <summary>
        /// YYYYMM形式の整数値から年月への変換
        /// </summary>
        public static YearMonth YMToYearMonth(this int ym)
        {
            int year = ym / 100;
            int month = ym % 100;

            return new YearMonth(year, month);
        }

        /// <summary>
        /// YYYYMM形式の整数値から年月への変換
        /// </summary>
        /// <returns>
        /// 年月。ymが0の場合はnull
        /// </returns>
        public static YearMonth? YMToYearMonthOrNull(this int ym)
        {
            if (ym == 0)
            {
                return null;
            }
            else
            {
                int year = ym / 100;
                int month = ym % 100;

                return new YearMonth(year, month);
            }
        }


        /// <summary>
        /// 年月からYYYYMM形式の整数値に変換
        /// </summary>
        /// <returns>
        /// YYYYMM形式のint値。dateがnullの場合は0;
        /// </returns>
        public static int ToIntYM(this YearMonth? yearMonth)
        {
            if (yearMonth.HasValue)
            {
                return yearMonth.Value.ToIntYM();
            }
            else
            {
                return 0;
            }
        }
    }

}
