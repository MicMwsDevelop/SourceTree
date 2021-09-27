//
// Time.cs
// 
// 共通データ型：日付を特定しない時刻(Hour:Minute:Second)データの定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Linq;

namespace CommonLib.Common
{
	/// <summary>
	/// 時刻(日付を特定しない時刻。Hourは用途によっては負の値でも、24以上でも構わない)
	/// </summary>
	/// <remarks>
	/// Hourが負の値の場合、例えば-1時というのは前日の23時を表している事に注意。
	/// つまり、-1時1分1秒という時刻は、前日の23時1分１秒の事であって、0時0分0秒の１時間１分１秒前という意味ではない。
	/// </remarks>
	[Serializable]
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        // hourの最大値、最小値の定義(3600倍がintの最大値を超えない範囲で最大の桁数を設定)
        public const int MaxHour = 99999;
        public const int MinHour = -99999;

        private int HourValue;
        private int MinuteValue;   // 0-59
        private int SecondValue;   // 0-59

        public int Hour
        {
            get
            {
                return HourValue;
            }
        }

        public int Minute
        {
            get
            {
                return MinuteValue;
            }
        }

        public int Second
        {
            get
            {
                return SecondValue;
            }
        }

        /// <summary>
        /// 時刻が0-23の範囲外かどうか
        /// </summary>
        public bool IsHourOverflowed
        {
            get
            {
                return HourValue < 0 || HourValue >= 24;
            }
        }

        public static readonly Time MinValue = new Time { HourValue = MinHour, MinuteValue = 0, SecondValue = 0 };
        public static readonly Time MaxValue = new Time { HourValue = MaxHour, MinuteValue = 59, SecondValue = 59 };

        public Time(int hour, int minute, int second)
        {
            if (hour < MinHour || hour > MaxHour)
            {
                throw new ArgumentException(string.Format("時刻の時として受け入れられない値({0})が指定された", hour));
            }
            else if (minute < 0 || minute > 59)
            {
                throw new ArgumentException(string.Format("時刻の分として受け入れられない値({0})が指定された", minute));
            }
            else if (second < 0 || second > 59)
            {
                throw new ArgumentException(string.Format("時刻の秒として受け入れられない値({0})が指定された", second));
            }

            HourValue = hour;
            MinuteValue = minute;
            SecondValue = second;
        }

        /// <summary>
        /// 標準文字列の取得
        /// </summary>
        /// <remarks>
        /// Hourが0-99の場合はHH:MM:SSで、Hourが100以上または負の値の場合Hourについてのみ桁数を固定しない形式で出力する
        /// </remarks>
        /// <returns></returns>
        public override string ToString()
        {
            if (HourValue >= 0 && HourValue <= 99)
            {
                return string.Format("{0:D2}:{1:D2}:{2:D2}", HourValue, MinuteValue, SecondValue);
            }
            else
            {
                return string.Format("{0}:{1:D2}:{2:D2}", HourValue, MinuteValue, SecondValue);
            }
        }

        /// <summary>
        /// 標準文字列から時刻への変換
        /// </summary>
        /// <param name="str">標準文字列 HH:MM:SS or HH:MM</param>
        /// <returns>変換結果</returns>
        public static Time Parse(string str)
        {
            // Hourについてはintで表せるすべての値を受け入れる
            // MinuteとSecondは0-59のみ受け入れ
            var split = str.Split(new[] { ':' }).Select(x => x.Trim()).ToArray();
            //if (split.Length < 3)
            //{
            //	throw new ArgumentException(string.Format("時刻文字列の解析に失敗{0}", str));
            //}
            //else
            //{
            //	int hour = int.Parse(split[0]);
            //	int minute = int.Parse(split[1]);
            //	int second = int.Parse(split[2]);

            //	return new Time(hour, minute, second);
            //}
            try
            {
                if (3 == split.Length)
                {
                    int hour = int.Parse(split[0]);
                    int minute = int.Parse(split[1]);
                    int second = int.Parse(split[2]);
                    return new Time(hour, minute, second);
                }
                else if (2 == split.Length)
                {
                    int hour = int.Parse(split[0]);
                    int minute = int.Parse(split[1]);
                    return new Time(hour, minute, 0);
                }
                else
                {
                    throw new ArgumentException(string.Format("時刻文字列[{0}]の解析に失敗", str));
                }
            }
            catch (FormatException ex)
            {
                throw new ArgumentException(string.Format("時刻文字列[{0}]の解析に失敗({1})", str, ex.Message));
            }
        }

        /// <summary>
        /// 標準文字列から時刻への変換
        /// </summary>
        /// <param name="str">標準文字列</param>
        /// <param name="retVal">変換結果格納先</param>
        /// <returns>変換に成功したかどうか</returns>
        public static bool TryParse(string str, out Time retVal)
        {
            retVal = Time.MinValue;
            try
            {
                retVal = Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ハッシュコード生成
        /// </summary>
        public override int GetHashCode()
        {
            int seed = (HourValue << 12) + (MinuteValue << 6) + SecondValue;
            return seed.GetHashCode();
        }

        /// <summary>
        /// 指定Timeと同値かどうか
        /// </summary>
        public bool Equals(Time target)
        {
            return target.HourValue == HourValue && target.MinuteValue == MinuteValue && target.SecondValue == SecondValue;
        }

        /// <summary>
        /// 指定objecctがTimeで値が同じかどうか
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Time)
            {
                return Equals((Time)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 指定時刻との比較
        /// </summary>
        /// <param name="target">比較対象時刻</param>
        /// <returns>
        /// this > target :正の値
        /// this < target :負の値
        /// this == target:0
        /// </returns>
        public int CompareTo(Time target)
        {
            if (HourValue > target.HourValue)
            {
                return 1;
            }
            else if (HourValue < target.HourValue)
            {
                return -1;
            }
            else
            {
                if (MinuteValue > target.MinuteValue)
                {
                    return 1;
                }
                else if (MinuteValue < target.MinuteValue)
                {
                    return -1;
                }
                else
                {
                    return SecondValue - target.SecondValue;
                }
            }
        }

        /// <summary>
        /// ０時０分０秒からのオフセット秒数を取得
        /// </summary>
        public int GetTotalSeconds()
        {
            return HourValue * 3600 + MinuteValue * 60 + SecondValue;
        }

        /// <summary>
        /// ０時０分０秒からのオフセット秒数により時刻を取得
        /// </summary>
        public static Time TotalSecondsToTime(int seconds)
        {
            int hour = seconds / 3600;
            if (seconds >= 0)
            {
                // 0、または、正の値(０時０分０秒から未来方向)
                int minute = (seconds % 3600) / 60;
                int second = seconds % 60;
                return new Time(hour, minute, second);
            }
            else
            {
                // hour時0分0秒からの差を計算
                int sm = seconds - hour * 3600;
                if (sm == 0)
                {
                    // hour時ちょうど
                    return new Time(hour, 0, 0);
                }
                else
                {
                    // この時点でsmは、hour時0分0秒から何秒経過したか(負の値なので実際には何秒前か)かという数値を表している
                    --hour; // hourを繰り下げ

                    // １時間繰り下げた分、秒数を調整。
                    // 調整後のsmは繰り下げ後のhour時0分0秒から何秒経過したかという数値に変換されている
                    sm += 3600;

                    int minute = sm / 60;
                    int second = sm % 60;

                    return new Time(hour, minute, second);
                }
            }
        }

        public int GetTotalMinutes()
        {
            return GetTotalSeconds() / 60;
        }

        public static Time TotalMinutesToTime(int min)
        {
            int seconds = min * 60;
            return TotalSecondsToTime(seconds);
        }

        // PlusHours, PlusMinutesの動作仕様変更について
        // DUTimeやCppのDTime関数では、秒を丸める仕様になっているが、それだとDateのPlusMonthes等とも演算としての
        // 整合性も無くなる。(ある時刻t, 任意の値aについて、t.PlusXXXX(a).PlusXXXX(-a) == tが成り立たなくなる)
        // インプリメント上の都合でそうしていた疑いが強い。
        // もしかしたら何かアプリケーション上の必要性があるのかもしれないがその場合はその場合で別対処をした方が良い
        // と思われるので丸めずに元の秒を保存する仕様で作成する。

        public Time PlusHours(int hours)
        {
            return new Time(HourValue + hours, MinuteValue, SecondValue);
        }

        public Time PlusMinutes(int minutes)
        {
            // まず秒単位を求め、minutes * 60を足した結果を返す
            int totalSeconds = GetTotalSeconds();
            return Time.TotalSecondsToTime(totalSeconds + minutes * 60);
        }

        public Time PlusSeconds(int seconds)
        {
            int totalSeconds = GetTotalSeconds();
            return TotalSecondsToTime(totalSeconds + seconds);
        }

        /// <summary>
        /// 指定時刻からの経過秒数(this - from秒)を取得する。
        /// </summary>
        /// <param name="to">基点となる時刻を指定</param>
        public int GetDifferenceSeconds(Time from)
        {
            return GetTotalSeconds() - from.GetTotalSeconds();
        }

        public static Time operator -(Time left, Time right)
        {
            return TotalSecondsToTime(left.GetTotalSeconds() - right.GetTotalSeconds());
        }

        /// <summary>
        /// 等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:一致 false:不一致</returns>
        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 非等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:不一致 false:一致</returns>
        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// "左辺値が右辺値より大きい"関係演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:"左辺値が右辺値より大きい", false:"左辺値が右辺値と等しいか、より小さい"</returns>
        public static bool operator >(Time left, Time right)
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
        public static bool operator <(Time left, Time right)
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
        public static bool operator >=(Time left, Time right)
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
        public static bool operator <=(Time left, Time right)
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
        /// OSから現在時刻を取得
        /// </summary>
        /// <remarks>
        /// OSから時刻を取得する場合System.DateTime.Nowを使用する方が望ましいので不要なのではないか？
        /// 日付抜きの現在時刻というニーズが本当にあるのか？
        /// </remarks>
        public static Time Now
        {
            get
            {
                var now = DateTime.Now;
                return new Time(now.Hour, now.Minute, now.Day);
            }
        }
    }
}