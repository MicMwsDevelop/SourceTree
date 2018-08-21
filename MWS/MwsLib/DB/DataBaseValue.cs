//
// DataBaseValue.cs
// 
// DataBase値 変換クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using MwsLib.Common;
using System;

namespace MwsLib.DB
{
	/// <summary>
	/// DataBase値 変換クラス
	/// </summary>
	/// <remarks>
	/// ※※※※※※※※※※※※※※※※※※
	/// ※※※※※　使用上の注意　※※※※※
	/// ※※※※※※※※※※※※※※※※※※
	/// ・主キーフィールドは、(int)(string)などでキャストしてください。
	/// 　（処理効率のため本クラスは使用しない）
	/// ・主キー以外の文字列フィールド(DBNullの可能性がある)は、変換クラスに該当するメソッドは作成しませんので「ToString()」を使用してください。
	/// 　（DBNullの場合、文字数0の文字列データとして扱われます。また、DB_Null格納フィールドを「as string」でキャストしてしまうととnullが返ります。
	/// 　　(string)でキャストしてしまうと実行時に例外が発生します。）
	/// </remarks>
	public static class DataBaseValue
    {
		////////////////////////////////////////////////////////////////////////////////////////
		// DataBase値→クラスのフィールド、プロパティ値
		// ※Get用のControllerで使用する
		////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// 数値日付型(YYYYMMDD)からDate型に変換
		/// </summary>
		/// <param name="source">日付に該当する数値</param>
		/// <returns>日付</returns>
		public static Date ConvObjectToDate(object source)
		{
			if (DBNull.Value == source)
			{
				return Date.MinValue;
			}

			var data = source as long?;

			if (!data.HasValue || data.Value == 0)
			{
				return Date.MinValue;
			}
			else
			{
				return Date.Parse((int)data.Value);
			}
		}


		/// <summary>
		/// 数値を解析して開始日付を生成する(DBNull or 0は、日付クラス最少値)
		/// </summary>
		/// <remarks>
		/// ※DB上で数値8桁で日付データを保持するint型フィールドからのデータ取得用
		/// ※「開始日付」に限らず、DBNullを保持しない保証があるフィールド（例.主キーフィールド）でも使用してください。
		/// </remarks>
		/// <param name="source">日付に該当する数値</param>
		/// <returns>数値に該当する日付</returns>
		public static Date ConvObjectToStartDate(object source)
        {
            if (DBNull.Value == source)
            {
                return Date.MinValue;
            }

            var data = source as int?;

            if (!data.HasValue || data.Value == 0)
            {
                return Date.MinValue;
            }
            else
            {
                return Date.Parse(data.Value);
            }
        }

        /// <summary>
        /// 数値を解析して終了日付を生成する(DBNull or 0は、日付クラス最大値)
        /// </summary>
        /// <remarks>
        /// ※DB上で数値8桁で日付データを保持するint型フィールドからのデータ取得用
        /// </remarks>
        /// <param name="source">日付に該当する数値</param>
        /// <returns>数値に該当する日付</returns>
        public static Date ConvObjectToEndDate(object source)
        {
            var data = source as int?;

            if (!data.HasValue || data.Value == 0)
            {
                return Date.MaxValue;
            }
            else
            {
                return Date.Parse(data.Value);
            }
        }

        /// <summary>
        /// 数値を解析して日付を生成する(DBNull or 0は、null)
        /// </summary>
        /// <remarks>
        /// ※DB上で数値8桁で日付データを保持するint型フィールドからのデータ取得用
        /// </remarks>
        /// <param name="source">日付に該当する数値</param>
        /// <returns>数値に該当する日付</returns>
        public static Date? ConvObjectToDateNull(object source)
        {
            var data = source as int?;

            if (!data.HasValue || data.Value == 0)
            {
                return null;
            }
            else
            {
                return Date.Parse(data.Value);
            }
        }

        /// <summary>
        /// 【旧システム用関数】数値を解析して日付を生成する(DBNull or 0は、null)
        /// </summary>
        /// <remarks>
        /// ※DB上で数値8桁で日付データを保持するint型フィールドからのデータ取得用
        /// ※月日しかデータが存在しない場合は、年を1として取得する
        /// </remarks>
        /// <param name="source">日付に該当する数値</param>
        /// <returns>数値に該当する日付</returns>
        public static Date? ConvOldSystemObjectToDateNull(object source)
        {
            var data = source as int?;

            if (!data.HasValue || data.Value == 0)
            {
                return null;
            }
            else
            {
                try
                {
                    int dateNum = data.Value;
                    if (dateNum < 10000)
                    {
                        // 日付が月日しかない古いデータは、年に仮値(1)を入れる
                        dateNum += 10000;
                    }
                    return Date.Parse(dateNum);
                }
                catch (ArgumentOutOfRangeException)
                {
                    // 不正値が入っていた場合はエラーにせず、nullを返す
                    return null;
                }
            }
        }

        /// <summary>
        /// DB上のDate型の値を解析して日付情報を生成する(DBNullは、日付クラス最少値)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>日付</returns>
        public static Date ConvObjectToDateByDate(object source)
        {
            if (DBNull.Value == source)
            {
                return Date.MinValue;
            }
            else
            {
                // 値が存在しDateTime型の場合のみ変換
                // ※DB上のDate型はC#上ではDateTime型として解釈される
                if (source is DateTime)
                {
                    DateTime dateTime = (DateTime)source;
                    Date date = new Date(dateTime);
                    return date;
                }
                else
                {
                    return Date.MinValue;
                }
            }
        }

        /// <summary>
        /// DB上のDate型の値を解析して日付情報を生成する(DBNullは、null)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>日付</returns>
        public static Date? ConvObjectToDateNullByDate(object source)
        {
            if (DBNull.Value == source)
            {
                return null;
            }
            else
            {
                // 値が存在しDateTime型の場合のみ変換
                // ※DB上のDate型はC#上ではDateTime型として解釈される
                if (source is DateTime)
                {
                    DateTime dateTime = (DateTime)source;
                    Date date = new Date(dateTime.Year, dateTime.Month, dateTime.Day);
                    return date;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 数値を解析して年月を生成する(DBNull or 0は、null)
        /// </summary>
        /// <param name="source">日付に該当する数値</param>
        /// <returns>数値に該当する日付</returns>
        public static YearMonth? ConvObjectToYearMonthNull(object source)
        {
            var data = source as int?;

            if (!data.HasValue || data.Value == 0)
            {
                return null;
            }
            else
            {
                return YearMonth.Parse(data.Value);
            }
        }

        /// <summary>
        /// 値を解析してintに変換する(DBNullは、0)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>int値</returns>
        public static int ConvObjectToInt(object source)
        {
            if (DBNull.Value == source)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(source);
            }
        }

        /// <summary>
        /// 値を解析してintに変換する(DBNullは、デフォルト値)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>int値</returns>
        public static int ConvObjectToInt(object source, int defaultNum)
        {
            if (DBNull.Value == source)
            {
                return defaultNum;
            }
            else
            {
                return Convert.ToInt32(source);
            }
        }

        // $パフォーマンス改善$
        /// <summary>
        /// 値を解析してshortに変換する(DBNullは、0)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>int値</returns>
        public static int ConvObjectToShort(object source)
        {
            if (DBNull.Value == source)
            {
                return 0;
            }
            else
            {
                return (short)source;
            }
        }

        /// <summary>
        /// 値を解析してuintに変換する(DBNullは、0)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>int値</returns>
        public static uint ConvObjectToUint(object source)
        {
            if (DBNull.Value == source)
            {
                return 0;
            }
            else
            {
                return uint.Parse(source.ToString());
            }
        }

        /// <summary>
        /// 値を解析してdecimalに変換する(DBNullは、0)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>decimal値</returns>
        public static decimal ConvObjectToDecimal(object source)
        {
            if (DBNull.Value == source)
            {
                return 0;
            }
            else
            {
                return decimal.Parse(source.ToString());
            }
        }

        /// <summary>
        /// 値を解析してboolに変換する(DBNullは、false)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>bool値</returns>
        public static bool ConvObjectToBool(object source)
        {
            if (DBNull.Value == source)
            {
                return false;
            }
            else
            {
                if (source is int)
                {
                    return (int)source == 0 ? false : true;
                }
                else if (source is short)
                {
                    return (short)source == 0 ? false : true;
                }
                else
                {
                    return bool.Parse(source.ToString());
                }
            }
        }

        /// <summary>
        /// 値を解析してTimeに変換する
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>Time値</returns>
        public static Time ConvObjectToTime(object source)
        {
            var data = source as int?;

            if (!data.HasValue)
            {
                return new Time(0, 0, 0);
            }
            else
            {
                int hour = data.Value / 10000;
                int minute = (data.Value % 10000) / 100;
                int second = data.Value % 100;
                return new Time(hour, minute, second);
            }
        }

        /// <summary>
        /// 値を解析してTimeに変換する(DBNullは、null)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>Time値</returns>
        public static Time? ConvObjectToTimeNull(object source)
        {
            var data = source as int?;

            if (!data.HasValue)
            {
                return null;
            }
            else
            {
                int hour = data.Value / 10000;
                int minute = (data.Value % 10000) / 100;
                int second = data.Value % 100;
                return new Time(hour, minute, second);
            }
        }

        /// <summary>
        /// 値を解析してTimeに変換する(DBNull or -1は、null)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>Time値</returns>
        public static Time? ConvObjectToValidTimeNull(object source)
        {
            var data = source as int?;

            if (!data.HasValue
                || data.Value == -1)
            {
                return null;
            }
            else
            {
                int hour = data.Value / 10000;
                int minute = (data.Value % 10000) / 100;
                int second = data.Value % 100;
                return new Time(hour, minute, second);
            }
        }

        /// <summary>
        /// 値を解析してDateTimeに変換する
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>DateTime値</returns>
        public static DateTime ConvObjectToDateTime(object source)
        {
            if (DBNull.Value == source)
            {
                return DateTimeDef.Default;
            }
            else
            {
                return DateTime.Parse(source.ToString());
            }
        }

        /// <summary>
        /// 値を解析してDateTimeに変換する(DBNullは、null)
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>DateTime値</returns>
        public static DateTime? ConvObjectToDateTimeNull(object source)
        {
            if (DBNull.Value == source)
            {
                return null;
            }
            else
            {
                DateTime dateTime = DateTime.Parse(source.ToString());
                if (dateTime.Equals(DateTimeDef.Default))
                {
                    return null;
                }
                else
                {
                    return dateTime;
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////
        // クラスのフィールド、プロパティ値→DataBase値
        // ※Set用のControllerで使用する
        ////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 日付を表す数値に変換する(日付クラス最少値は、0に変換)
        /// </summary>
        /// <param name="source">数値に該当する日付</param>
        /// <returns>日付に該当する数値</returns>
        public static int ConvStartDateToInt(Date source)
        {
            //  最小値の場合は0に変換
            if (source == Date.MinValue)
            {
                return 0;
            }
            return int.Parse(source.GetNumeralString(true));
        }

        /// <summary>
        /// 日付を表す数値に変換する(日付クラス最大値は、0に変換)
        /// </summary>
        /// <param name="source">数値に該当する日付</param>
        /// <returns>日付に該当する数値</returns>
        public static int ConvEndDateToInt(Date source)
        {
            //  最大値の場合は0に変換
            if (source == Date.MaxValue)
            {
                return 0;
            }
            return int.Parse(source.GetNumeralString(true));
        }

        /// <summary>
        /// 日付を表す数値に変換する(nullは、0に変換)
        /// </summary>
        /// <param name="source">数値に該当する日付</param>
        /// <returns>日付に該当する数値</returns>
        public static int ConvDateNullToInt(Date? source)
        {
            //  nullの場合は0に変換
            if (source == null)
            {
                return 0;
            }
            return int.Parse(source.Value.GetNumeralString(true));
        }

        /// <summary>
        /// 日付を表す数値に変換する(nullは、0に変換)
        /// </summary>
        /// <param name="source">数値に該当する日付</param>
        /// <returns>日付に該当する数値</returns>
        public static int ConvYearMonthNullToInt(YearMonth? source)
        {
            //  nullの場合は0に変換
            if (source == null)
            {
                return 0;
            }
            return int.Parse(source.Value.GetNumeralString());
        }

        /// <summary>
        /// 時刻を表す数値に変換する(nullは、0に変換)
        /// </summary>
        /// <param name="source">数値に該当する時刻</param>
        /// <returns>時刻に該当する数値</returns>
        public static int ConvTimeNullToInt(Time? source)
        {
            int result = 0;
            var data = source as Time?;

            //  nullの場合は0に変換
            if (data == null)
            {
                return result;
            }

            result += data.Value.Hour * 10000;
            result += data.Value.Minute * 100;
            result += data.Value.Second;

            return result;
        }

        /// <summary>
        /// 時刻を表す数値に変換する(nullは、-1に変換)
        /// </summary>
        /// <param name="source">数値に該当する時刻</param>
        /// <returns>時刻に該当する数値</returns>
        public static int ConvValidTimeNullToInt(Time? source)
        {
            //  nullの場合は-1に変換
            if (!source.HasValue)
            {
                return -1;
            }

            int result = 0;
            result += source.Value.Hour * 10000;
            result += source.Value.Minute * 100;
            result += source.Value.Second;

            return result;
        }

        /// <summary>
        /// 日時クラス最小値を表す日時デフォルトに変換する(nullは、1899-12-30 00:00:00.000に変換)
        /// </summary>
        /// <param name="source">数値に該当する日時</param>
        /// <returns>日時に該当する数値</returns>
        public static DateTime ConvDateTimeNullToDateTime(DateTime? source)
        {
            //  nullの場合は0に変換
            if (source == null)
            {
                return DateTimeDef.Default;
            }
            else
            {
                return source.Value;
            }
        }

        /// <summary>
        /// ブール値を数値に変換する
        /// </summary>
        /// <param name="source">ブール値</param>
        /// <returns>ブール値に該当する数値（true：1、false：0）</returns>
        public static int ConvBoolToInt(bool source)
        {
            //  tureは1、falseは0に変換
            if (source)
            {
                return 1;
            }
            else
            {

            }
            return 0;
        }

        /// <summary>
        /// ブール値を数値に変換する（旧システム互換用）<br/>
        /// ※旧システムでtrueを-1としてDBに保存している箇所で使用する
        /// </summary>
        /// <param name="source">ブール値</param>
        /// <returns>ブール値に該当する数値（true：-1、false：0）</returns>
        public static int ConvOldSystemBoolToInt(bool source)
        {
            //  tureは-1、falseは0に変換
            if (source)
            {
                return -1;
            }
            else
            {

            }
            return 0;
        }

        /// <summary>
        /// DBのintをuintに変換する(DBNullは、0)
        /// 【血液化学検査】の検査値データ取得のため作成
        /// プログラムではuintで保持しているが、DBではintで保持しているため
        /// </summary>
        /// <param name="source">object値</param>
        /// <returns>int値</returns>
        public static uint ConvIntObjectToUint(object source)
        {
            if (DBNull.Value == source)
            {
                return 0;
            }
            else
            {
                int obj = int.Parse(source.ToString());
                return (uint)obj;
            }
        }
    }
}
