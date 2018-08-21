//
// Date.cs
// 
// 日付クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Diagnostics;

namespace MwsLib.Common
{
	/// <summary>
	/// 日付 - 共通データ型(値型)
	/// </summary>
	[Serializable]
    public struct Date : IComparable<Date>, IEquatable<Date>
    {
        // 内部データ
        /// <summary>
        /// DateTime型で保持するDateTime構造体(64→128bit※下記参照)(Dateインスタンス初期化後は絶対に変更しない)
        /// </summary>
        /// <remarks>
        /// このクラスは主にDateTime型の機能を利用して実装している。
        /// 　　×おり、DateTimeと同じ64bitのサイズを持つ。
        /// </remarks>
        private DateTime DateValue;

        // 20150408林変更
        // DateTimeからの年月日の取得がボトルネックになっているようなので、冗長化する。
        // Dateのサイズは、64bitから、64+16+8+8=96(実際には128bit)に増える
        // 年月日の値(全て0origin)のコピー

        // 200150803林変更
        //   Dateが引数無しで初期化された時、DateValueは1年1月1日1日を示すが、YearValue, MonthValue, DayValueは0で初期化されている。
        //   上記の場合の問題を解消する為、YearValue,MonthValue,DayValueの値が0の場合は1とするように、つまり、年月日の表現は0originで
        //   表す方式に変更した。

        // 下記3つの内部変数の値が全て-1されたものとなり紛らわしいので、デバッガに表示されないようにした。
        // ただし、直接ウォッチ式に追加すれば値を見ることは出来る。

        /// <summary>
        /// Yearの内部表現(西暦1年=0, 西暦2年=1,…,西暦x年=x-1)
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short YearValue;

        /// <summary>
        /// Monthの内部表現(0～11→1月=0,2月=1,…,12月=11)
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private sbyte MonthValue;

        /// <summary>
        /// Dayの内部表現(0～30→1日=0,2日=1,…,31月=30)
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private sbyte DayValue;

        // 定義済みインスタンス

        /// <summary>本クラスで表現可能な最も過去の日付</summary>
        /// <remarks>
        /// MinValueは日付未定義値も兼ねている為、実際の日付としては使用できない。
        /// </remarks>
        public static readonly Date MinValue = new Date { DateValue = new DateTime(1, 1, 1), YearValue = (short)0, MonthValue = (sbyte)0, DayValue = (sbyte)0 };

        /// <summary>本クラスで表現可能な最も未来の日付</summary>
        public static readonly Date MaxValue = new Date { DateValue = new DateTime(9999, 12, 31),  YearValue = (short)9998, MonthValue = (sbyte)11, DayValue = (sbyte)30 };

        /*
         * MaxValueにDateTime.MaxValueを代入するやり方だと、DateValueの時分秒が23:59:59になってしまう。
         * Dateクラス内部実装上、初期化方法によらず同じ日付なら同じ値が入っていなければならない。
         * その為、MaxValueの初期化方法を変更し、int year, month, dayを指定するコンストラクタ以外の
         * 経路での初期化を排除した。
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <exception cref="ArgumentException">日付として異常な値がパラメータとして指定された。または表現可能範囲外の日付が指定された。</exception>
        public Date(int year, int month, int day)
        {
            DateValue = new DateTime(year, month, day);
            YearValue = (short)(year - 1);
            MonthValue = (sbyte)(month - 1);
            DayValue = (sbyte)(day - 1);
        }

        /// <summary>
        /// DateTime型による初期化
        /// </summary>
        /// <param name="date">DateTime値</param>
        public Date(DateTime date)
        {
            DateValue = new DateTime(date.Year, date.Month, date.Day);
            YearValue = (short)(date.Year - 1);
            MonthValue = (sbyte)(date.Month - 1);
            DayValue = (sbyte)(date.Day - 1);
        }

        /// <summary>年を取得</summary>
        public int Year
        {
            get
            {
                return YearValue + 1;
            }
        }

        /// <summary>月を取得</summary>
        public int Month
        {
            get
            {
                return MonthValue + 1;
            }
        }

        /// <summary>日を取得</summary>
        public int Day
        {
            get
            {
                return DayValue + 1;
            }
        }

        /// <summary>日付に該当する曜日を取得</summary>
        public DayOfWeek DayOfWeek
        {
            get
            {
                return DateValue.DayOfWeek;
            }
        }

        /// <summary>
        /// 曜日文字列(月,火,水,木,金,土,日)の取得
        /// </summary>
        public string GetDayOfWeekString()
        {
            return DayOfWeek.GetString();
        }

        /// <summary>
        /// 曜日英数文字列(SUN,MON,TUE,WED,THU,FRI,SAT)の取得
        /// </summary>
        public string GetDayOfWeekANString()
        {
            return DayOfWeek.GetANString();
        }

        /// <summary>DateTime値を取得</summary>
        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        /// <summary>
        /// 日付を表す文字列表現を取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0:D}/{1:D2}/{2:D2}", Year, Month, Day);
        }

        /// <summary>
        /// 文字列を解析して日付インスタンスを生成する
        /// </summary>
        /// <param name="source"></param>
        /// <returns>文字列に該当する日付</returns>
        /// <exception cref="ArgumentException">文字列を日付として解析できなかった。または表現可能な範囲外。</exception>
        public static Date Parse(string source)
        {
            string[] separated = source.Split(new[] { '/' });
            if (separated.Length < 3)
            {
                throw new ArgumentException("日付文字列(yyyy/mm/dd形式)として解析できない({0})", source);
            }
            else
            {
                int year = int.Parse(separated[0]);
                int month = int.Parse(separated[1]);
                int day = int.Parse(separated[2]);

                if (year == 0 && month == 0 && day == 0)
                {
                    return MinValue;
                }
                else
                {
                    return new Date(year, month, day);
                }
            }
        }

        /// <summary>
        /// 数値を解析して日付インスタンスを生成する
        /// </summary>
        /// <remarks>削除予定→今後はclass DateConversionのToIntYMD, YMDToDateを使ってください。</remarks>
        /// <param name="source"></param>
        /// <returns>数値に該当する日付</returns>
        public static Date Parse(int source)
        {
            int year = source / 10000;
            int month = (source % 10000) / 100;
            int day = source % 100;

            if (year == 0 && month == 0 && day == 0)
            {
                return MinValue;
            }
            else
            {
                return new Date(year, month, day);
            }

        }

        /// <summary>
        /// 文字列を解析して日付インスタンスを生成し、引数retValにセット。解析に成功したかどうかを返す。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="retVal"></param>
        /// <returns></returns>
        public static bool TryParse(string source, out Date retVal)
        {
            bool result = false;
            retVal = MinValue;
            try
            {
                retVal = Parse(source);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 当日（マシン日付）の日付を取得
        /// </summary>
        public static Date Today
        {
            get
            {
                return new Date(DateTime.Now);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return DateValue.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Date other)
        {
            return other.Year == Year && other.Month == Month && other.Day == Day;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o is Date)
            {
                return Equals((Date)o);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Date other)
        {
            return DateValue.CompareTo(other.DateValue);
        }

        /// <summary>
        /// 当月の日数を返す
        /// </summary>
        public int GetDaysInMonth()
        {
            return DateTime.DaysInMonth(Year, Month);
        }

        /// <summary>
        /// dateのdays日後(daysが負の場合は「前」)の日付を取得
        /// </summary>
        /// <returns>days日後(前)の日付</returns>
        public static Date operator +(Date date, int days)
        {
            return new Date(date.DateValue.AddDays((double)days));
        }

        /// <summary>
        /// dateのdays日前(daysが負の場合は「後」)の日付を取得
        /// </summary>
        /// <returns>days日前(後)の日付</returns>
        public static Date operator -(Date date, int days)
        {
            return new Date(date.DateValue.AddDays((double)-days));
        }

        /// <summary>
        /// monthesヶ月後（前）の日付取得
        /// </summary>
        /// <param name="monthes">進める（戻す）月の数</param>
        /// <returns>monthesヶ月後（前）の日付</returns>
        /// <remarks>
        /// 月を戻す場合はパラメーターにマイナスの値を設定する。
        /// </remarks>
        public Date PlusMonths(int monthes)
        {
            // DateTimeのAddMonths関数で月を進める（戻す）処理をする
            DateTime dateTime = DateValue.AddMonths(monthes);

            // 月を進めた状況で日が丸め込まれた(例.3月31日→2月28日)場合、翌月の１日を返す
            if (monthes > 0 && (dateTime.Day < Day))
            {
                return new Date(dateTime.AddDays(1));
            }
            else
            {
                return new Date(dateTime);
            }
        }

        /// <summary>
        /// years年後（前）の日付取得
        /// </summary>
        /// <param name="years">進める（戻す）年数</param>
        /// <returns>years年後の日付</returns>
        /// <remarks>
        /// 年を戻す場合はパラメーターにマイナスの値を設定する。
        /// </remarks>
        public Date PlusYears(int years)
        {
            return PlusMonths(years * 12);
        }

        /// <summary>
        /// 日付同士の差(日数)を取得
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>差(日数)</returns>
        /// <remarks>
        /// 左辺＝右辺の場合は0
        /// 左辺＞右辺の場合は正の値
        /// 左辺＜右辺の場合は負の値
        /// </remarks>
        public static int operator -(Date left, Date right)
        {
            // 日の間隔を取得する
            TimeSpan timeSpanTmp = left.DateValue - right.DateValue;
            return timeSpanTmp.Days;
        }

        /// <summary>
        /// 基点日から指定日付までの経過年数（指定日付の方が過去の場合は指定日付からこの日付までの経過年数のマイナス値）を返す
        /// </summary>
        /// <param name="origin">基点となる日付</param>
        /// <param name="dateTo">取得対象の日付</param>
        public static int GetPassageYear(Date origin, Date dateTo)
        {
            // 日付が同じなので無条件に０を返す
            if (origin.Equals(dateTo))
            {
                return 0;
            }
            else
            {
                bool minusFlg;

                Date from;
                Date to;

                // from → toになるように正規化する
                if (origin < dateTo)
                {
                    from = origin;
                    to = dateTo;
                    minusFlg = false;
                }
                else
                {
                    // fromの方が大きい(未来→過去)
                    from = dateTo;
                    to = origin;
                    minusFlg = true;    // 後でマイナス値にする
                }

                // 単純に年月日の年の差を求める
                int years = to.Year - from.Year;

                // fromのちょうどyears年後の日付がtoより大きかったらyearsを1つ減らす
                if (from.PlusYears(years) > to)
                {
                    years -= 1;
                }

                if (!minusFlg)
                {
                    return years;
                }
                else
                {
                    // fromとtoを入れ替えた場合はマイナス値を返す
                    return -years;
                }
            }
        }

        /// <summary>
        /// 基点日から指定日付までの経過月数（指定日付の方が過去の場合は指定日付からこの日付までの経過月数のマイナス値）を返す
        /// </summary>
        /// <param name="origin">基点となる日付</param>
        /// <param name="dateTo">取得対象の日付</param>
        public int GetPassageYear(Date dateTo)
        {
            return GetPassageYear(this, dateTo);
        }

        /// <summary>
        /// この日付から指定日付までの経過月数（指定日付の方が過去の場合は指定日付からこの日付までの経過月数のマイナス値）を返す
        /// </summary>
        /// <param name="dateTo">取得対象日付</param>
        public static int GetPassageMonth(Date origin, Date dateTo)
        {
            // 日付が同じなので無条件に０を返す
            if (origin.Equals(dateTo))
            {
                return 0;
            }
            else
            {
                bool minusFlg;

                Date from;
                Date to;

                // from → toになるように正規化する
                if (origin < dateTo)
                {
                    from = origin;
                    to = dateTo;
                    minusFlg = false;
                }
                else
                {
                    // originの方が大きい(未来→過去)
                    from = dateTo;
                    to = origin;
                    minusFlg = true;    // 後でマイナス値にする
                }

                int result = (to.Year - from.Year) * 12 + to.Month - from.Month;
                if (from.Day > to.Day)
                {
                    result -= 1;
                }

                // toとfromを入れ替えた場合は符号を反転
                if (minusFlg)
                {
                    return -result;
                }
                else
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// この日付から指定日付までの経過月数（指定日付の方が過去の場合は指定日付からこの日付までの経過月数のマイナス値）を返す
        /// </summary>
        /// <param name="dateTo">取得対象日付</param>
        public int GetPassageMonth(Date dateTo)
        {
            return GetPassageMonth(this, dateTo);
        }

        /// <summary>
        /// 等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:一致 false:不一致</returns>
        public static bool operator ==(Date left, Date right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 非等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:不一致 false:一致</returns>
        public static bool operator !=(Date left, Date right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// "左辺値が右辺値より大きい"関係演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:"左辺値が右辺値より大きい", false:"左辺値が右辺値と等しいか、より小さい"</returns>
        public static bool operator >(Date left, Date right)
        {
            int c = left.CompareTo(right);
            if (c > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// "左辺値が右辺値より小さい"関係演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:"左辺値が右辺値より小さい", false:"左辺値が右辺値と等しいか、より大きい"</returns>
        public static bool operator <(Date left, Date right)
        {
            int c = left.CompareTo(right);
            if (c < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// "左辺値が右辺値より大きいか等しい"関係演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:"左辺値が右辺値より大きいか等しい", false:"左辺値が右辺値より小さい"</returns>
        public static bool operator >=(Date left, Date right)
        {
            int c = left.CompareTo(right);
            if (c >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// "左辺値が右辺値より小さいか等しい"関係演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:"左辺値が右辺値より小さいか等しい", false:"左辺値が右辺値より大きい"</returns>
        public static bool operator <=(Date left, Date right)
        {
            int c = left.CompareTo(right);
            if (c <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// インクリメント演算子オーバーライド
        /// </summary>
        public static Date operator ++(Date a)
        {
            return a + 1;
        }

        /// <summary>
        /// デクリメント演算子オーバーライド
        /// </summary>
        public static Date operator --(Date a)
        {
            return a - 1;
        }
    }
}
