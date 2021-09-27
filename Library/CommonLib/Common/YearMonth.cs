//
// YearMonth.cs
// 
// 共通データ型：年月を表すクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;

namespace CommonLib.Common
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
    public struct YearMonth : IComparable<YearMonth>, IEquatable<YearMonth>
    {
        //
        // 内部フィールド
        //

        /// <summary></summary>
        public int YearValue;

        /// <summary></summary>
        public int MonthValue;

        /// <summary>年を取得</summary>
        public int Year
        {
            get
            {
                return YearValue;
            }
        }

        /// <summary>月を取得</summary>
        public int Month
        {
            get
            {
                return MonthValue;
            }
        }

        // 
        // 定義済みインスタンス
        //

        /// <summary></summary>
        public static readonly YearMonth MinValue = new YearMonth { YearValue = 1, MonthValue = 1 };


        /// <summary></summary>
        public static readonly YearMonth MaxValue = new YearMonth { YearValue = 9999, MonthValue = 12 };

        /// <summary>
        /// 
        /// </summary>
        public Date First
        {
            get
            {
                return this.ToDate(1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Date Last
        {
            get
            {
                return this.ToDate(GetDays());
            }
        }

        /// <summary>
        /// コントラクタ
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public YearMonth(int year, int month)
        {
            if (year < MinValue.Year || year > MaxValue.Year)
            {
                throw new ArgumentException(string.Format("年として受け入れられない値({0})が指定された", year));
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException(string.Format("月として受け入れられない値({0})が指定された", month));
            }

            YearValue = year;
            MonthValue = month;
        }

        /// <summary>
        /// 標準文字列(yyyy/mm/dd形式)取得
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0:D}/{1:D2}", Year, Month);
        }

        /// <summary>
        /// 文字列を解析して年月インスタンスを生成する
        /// </summary>
        /// <param name="source"></param>
        /// <returns>文字列に該当する日付</returns>
        /// <exception cref="ArgumentException">文字列を日付として解析できなかった。または表現可能な範囲外。</exception>
        public static YearMonth Parse(string source)
        {
            string[] separated = source.Split(new[] { '/' });
            if (separated.Length < 2)
            {
                throw new ArgumentException("年月文字列(yyyy/mm形式)として解析できない({0})", source);
            }
            else
            {
                int year = int.Parse(separated[0]);
                int month = int.Parse(separated[1]);

                if (year == 0 && month == 0)
                {
                    return MinValue;
                }
                else
                {
                    return new YearMonth(year, month);
                }
            }
        }

        /// <summary>
        /// 数値を解析して年月インスタンスを生成する
        /// </summary>
        /// <param name="source"></param>
        /// <returns>数値に該当する日付</returns>
        public static YearMonth Parse(int source)
        {
            int year = source / 100;
            int month = source % 100;

            if (year == 0 && month == 0)
            {
                return MinValue;
            }
            else
            {
                return new YearMonth(year, month);
            }

        }

        /// <summary>
        /// 文字列を解析して月インスタンスを生成し、引数retValにセット。解析に成功したかどうかを返す。
        /// </summary>
        public static bool TryParse(string source, out YearMonth retVal)
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
        /// 指定年月の日数を取得
        /// </summary>
        public static int GetDays(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        /// 年月の日数を取得
        /// </summary>
        public int GetDays()
        {
            return GetDays(Year, Month);
        }

        /// <summary>
        /// years年後（前）の年月取得
        /// </summary>
        public YearMonth PlusYears(int years)
        {
            // インスタンスの日付にyears年分加減する
            return new YearMonth(Year + years, Month);
        }

        /// <summary>
        /// 基点年月から指定年月までの経過年数（指定年月の方が過去の場合は指定年月からこの年月までの経過年数のマイナス値）を返す
        /// </summary>
        /// <param name="origin">基点となる年月</param>
        /// <param name="ymTo">取得対象の年月</param>
        public static int GetPassageYear(YearMonth origin, YearMonth ymTo)
        {
            int m = ymTo - origin;  // 経過月数を求める
            return m / 12;          // その１２分の１(使用数点以下切り捨て)が答
        }

        /// <summary>
        /// 基点年月から指定年月までの経過年数（指定年月の方が過去の場合は指定年月からこの年月までの経過年数のマイナス値）を返す
        /// </summary>
        /// <param name="origin">基点となる日付</param>
        /// <param name="ymTo">取得対象の日付</param>
        public int GetPassageYear(YearMonth ymTo)
        {
            return GetPassageYear(this, ymTo);
        }

        /// <summary>
        /// +演算子ユーザー定義(指定月後の年月取得)
        /// </summary>
        public static YearMonth operator +(YearMonth ym, int n)
        {
            // パラメータの年月情報にmonthes分加算した年月情報を新たに取得する
            int year = ym.Year + n / 12;
            int month = ym.Month + n % 12;
            if (month > 12)
            {
                // 月が12より大きいので１年繰り上げ
                ++year;
                month -= 12;
            }
            else if (month < 1)
            {
                // 月が１より少ないので１年繰り下げ
                --year;
                month += 12;
            }

            return new YearMonth(year, month);
        }

        /// <summary>
        /// +演算子ユーザー定義(指定月前の年月取得)
        /// </summary>
        public static YearMonth operator -(YearMonth ym, int n)
        {
            return ym + (-n);
        }

        /// <summary>
        /// 年月同士の引き算を月数で取得
        /// </summary>
        public static int operator -(YearMonth from, YearMonth to)
        {
            // Year * 12 + Month - 1で求めた値の差
            int vFrom = from.Year * 12 + from.Month - 1;
            int vTo = to.Year * 12 + to.Month - 1;
            return vFrom - vTo;
        }

        /// <summary>
        /// 等値かどうか
        /// </summary>
        public bool Equals(YearMonth ym)
        {
            return ym.Year == Year && ym.Month == Month;
        }

        /// <summary>
        /// 等値かどうか
        /// </summary>
        public override bool Equals(object o)
        {
            if (o is YearMonth)
            {
                return Equals((YearMonth)o);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// GetHashCode()のオーバーライド
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int yh = Year.GetHashCode();
            int hash = (yh << 8) + (yh >> 24); // 年のHashCodeを8bitローテイト
            return hash ^ Month.GetHashCode(); // 月のHashCodeと排他的論理和する
        }

        /// <summary>
        /// 等値演算子オーバーライド
        /// </summary>
        public static bool operator ==(YearMonth cLeft, YearMonth cRight)
        {
            return cLeft.Equals(cRight);
        }

        /// <summary>
        /// 不等値演算子オーバーライド
        /// </summary>
        public static bool operator !=(YearMonth cLeft, YearMonth cRight)
        {
            return !cLeft.Equals(cRight);
        }

        /// <summary>
        /// 大小関係比較
        /// </summary>
        /// <returns>
        /// this > other : 正の値<br/>
        /// this == other : 0<br/>
        /// this < other : 負の値
        /// </returns>
        public int CompareTo(YearMonth other)
        {
            int r = this.Year - other.Year;
            if (r == 0)
            {
                r = this.Month - other.Month;
            }
            return r;

        }

        /// <summary>
        /// "左辺値が右辺値より大きい"関係演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:"左辺値が右辺値より大きい", false:"左辺値が右辺値と等しいか、より小さい"</returns>
        public static bool operator >(YearMonth left, YearMonth right)
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
        public static bool operator <(YearMonth left, YearMonth right)
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
        public static bool operator >=(YearMonth left, YearMonth right)
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
        public static bool operator <=(YearMonth left, YearMonth right)
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
        public static YearMonth operator ++(YearMonth a)
        {
            return a + 1;
        }

        /// <summary>
        /// デクリメント演算子オーバーライド
        /// </summary>
        public static YearMonth operator --(YearMonth a)
        {
            return a - 1;
        }
    }
}