//
// Span.cs
// 
// 共通データ型：期間
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.Common
{
	/// <summary>
	/// 共通データ型(参照型)：期間
	/// </summary>
	/// <remarks>
	/// 参照型。ただし、string同様インスタンス生成後の内部データ書き換えは絶対できないのでコピー時のClone処理は不要。
	/// </remarks>
	[Serializable]
    public sealed class Span : IEquatable<Span>
    {
        /// <summary>
        /// 開始日データフィールド(Spanインスタンス初期化後の変更不可)
        /// </summary>
        private Date StartValue;

        /// <summary>
        /// 終了日データフィールド(Spanインスタンス初期化後の変更不可)
        /// </summary>
        private Date EndValue;

        /// <summary>
        /// あらゆる日が該当する定義済みインスタンス
        /// </summary>
        public static Span All = new Span { StartValue = Date.MinValue, EndValue = Date.MaxValue };

        /// <summary>
        /// いかなる日も該当しない定義済みインスタンス
        /// </summary>
        public static Span Nothing = new Span { StartValue = Date.MinValue + 1, EndValue = Date.MinValue };

        /// <summary>
        /// 開始日
        /// </summary>
        public Date Start
        {
            get
            {
                return StartValue;
            }
        }

        /// <summary>
        /// 終了日のプロパティ
        /// </summary>
        public Date End
        {
            get
            {
                return EndValue;
            }
        }

        /// <summary>
        /// パラメータなしのコンストラクタ
        /// </summary>
        /// <remarks>
        /// パラメータなしのコンストラクタは使用不可。
        /// 全ての期間を表す場合は明示的にSpan.Allを設定する。(cppのDSpan()に相当)
        /// </remarks>
        private Span()
        {
        }

        /// <summary>
        /// 開始日・終了日を指定して初期化
        /// </summary>
        /// <param name="start">開始日の指定</param>
        /// <param name="end">終了日の指定</param>
        /// <remarks>
        /// 対象日が全く無い状態を表したい時は基本的には固定インスタンス(staticフィールド)のNothingを使用する。
        /// </remarks>
        public Span(Date start, Date end)
        {
            StartValue = start;
            EndValue = end;
        }

        /// <summary>
        /// 開始年・月・日・終了年・月・日を直接指定して初期化
        /// </summary>
        /// <param name="start">開始日の指定</param>
        /// <param name="end">終了日の指定</param>
        /// <remarks>
        /// 対象日が全く無い状態を表したい時は基本的には固定インスタンス(staticフィールド)のNothingを使用する。
        /// </remarks>
        public Span(int start_year, int start_month, int start_day, int end_year, int end_month, int end_day)
        {
            StartValue = new Date(start_year, start_month, start_day);
            EndValue = new Date(end_year, end_month, end_day);
        }

        /// <summary>
        /// 標準文字列を解析し期間を作成する
        /// </summary>
        /// <param name="source">開始日(YYYY/MM/DD)-終了日(YYYY/MM/DD)形式の文字列</param>
        /// <returns>解析結果を格納した期間インスタンス</returns>
        /// <example>
        /// var span1 = Span.Parse("2014/05/25-2016/12/31");
        /// var span2 = Span.Parse("-2016/12/31");  // 開始日指定なし
        /// var span3 = Span.Parse("2012/01/01-");  // 終了日指定なし
        /// var span4 = Span.Parse("-");            // 開始日・終了日とも指定なし
        /// var span5 = Span.Parse("");             // 空文字列(開始日・終了日とも指定なし)
        /// </example>
        public static Span Parse(string source)
        {
            string str = source.Trim();
            if (str == string.Empty)
            {
                return Span.All;
            }
            else
            {
                char[] sepatator = new[] { '-' };

                // セパレーターで開始日と終了日を分離
                string[] dateTimeList = str.Split(sepatator).Select(x => x.Trim()).ToArray();

                Date start = Date.MinValue;
                Date end = Date.MaxValue;

                // 開始日と終了日の２項目が存在する場合
                if (dateTimeList.Length >= 2)
                {

                    // 文字列を元に開始日生成
                    if (dateTimeList[0] != "")
                    {
                        Date startDate;
                        if (!Date.TryParse(dateTimeList[0], out startDate))
                        {
                            // 解析失敗
                            throw new ArgumentException(string.Format("期間開始日の解析に失敗({0})", dateTimeList[0]));
                        }
                        else
                        {
                            start = startDate;
                        }
                    }

                    // 文字列を元に終了日生成
                    if (dateTimeList[1] != "")
                    {
                        Date endDate;
                        if (!Date.TryParse(dateTimeList[1], out endDate))
                        {
                            // 解析失敗
                            throw new ArgumentException(string.Format("期間終了日の解析に失敗({0})", dateTimeList[1]));
                        }
                        else
                        {
                            end = endDate;
                        }
                    }
                    return new Span(start, end);
                }
                else
                {
                    // 期間文字列として解析できない('-'が含まれていない)
                    throw new ArgumentException(string.Format("期間として解析できない文字列({0})", str));
                }
            }
        }

        //--- public

        /// <summary>
        /// 標準文字列を作成する
        /// </summary>
        /// <returns>開始日(YYYY/MM/DD)-終了日(YYYY/MM/DD)形式の文字列</returns>
        public override string ToString()
        {
            if (StartValue == Date.MinValue && EndValue == Date.MaxValue)
            {
                return string.Empty;
            }

            string startDateStr;
            string endDateStr;
            // 開始日を書式化する
            if (StartValue != Date.MinValue)
            {
                startDateStr = StartValue.ToString();
            }
            else
            {
                startDateStr = string.Empty;
            }
            // 終了日を書式化する
            if (EndValue != Date.MaxValue)
            {
                endDateStr = EndValue.ToString();
            }
            else
            {
                endDateStr = string.Empty;
            }
            // 「開始日-終了日」の文字列を返す
            return string.Format("{0}-{1}", startDateStr, endDateStr);
        }

        /// <summary>
        /// 全ての日が該当(Date.MinDate ～ Date.MaxDate)を表す期間かどうか
        /// </summary>
        /// <returns>true:全て日が該当する期間 false:それ以外</returns>
        public bool IsAll()
        {
            return StartValue == Date.MinValue && EndValue == Date.MaxValue;
        }

        /// <summary>
        /// 該当期間の無い期間かどうか
        /// <para>
        /// <returns>true:該当期間無し false:該当期間あり</returns>
        /// <remarks>
        /// 開始日＞終了日の状態の場合、該当期間が無い状態となる。
        /// </remarks>
        public bool IsNothing()
        {
            return StartValue > EndValue;
        }

        /// <summary>
        /// 指定日付が範囲内かどうか
        /// </summary>
        public bool IsInside(Date date)
        {
            return StartValue <= date && date <= EndValue;
        }

        /// <summary>
        /// 指定日付を含む様に拡張した期間を取得
        /// </summary>
        public Span AddDay(Date date)
        {
            if (IsNothing())
            {
                // 該当期間無しの場合は、指定日付だけを含む期間を返す
                return new Span(date, date);
            }
            else if (IsInside(date))
            {
                // 日付が既に含まれている場合はこのインスタンス値をそのまま返す
                return this;
            }
            else
            {
                if (date < StartValue)
                {
                    // 指定日付が開始日より過去なので開始日を指定日付に変更
                    return new Span(date, EndValue);
                }
                else
                {
                    // 指定日付が期間に含まれず、開始日より前でもないという事は、終了日より未来。
                    // 終了日を指定日付に変更
                    return new Span(StartValue, date);
                }
            }
        }

        /// <summary>
        /// 同値かどうか
        /// </summary>
        public bool Equals(Span compareSpan)
        {
            // 比較元と比較先の開始日、終了日の一致比較を行い、比較結果が同一ならtrueを返す
            if (compareSpan != null)
            {
                return StartValue == compareSpan.StartValue && EndValue == compareSpan.EndValue;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object o)
        {
            if (o is Span)
            {
                return Equals(o as Span);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        public override int GetHashCode()
        {
            int sh = StartValue.GetHashCode();
            int hash = (sh << 8) + (sh >> 24); // 開始日のHashCodeを8bitローテイト
            return hash ^ EndValue.GetHashCode(); // 終了日のHashCodeと排他的論理和する
        }

        /// <summary>
        /// 等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:一致 false:不一致</returns>
        public static bool operator == (Span left, Span right)
        {
            if (left is Span && right is Span)
            {
                return left.Equals(right);
            }
            else if(!ReferenceEquals(left, null))
            {
                return left.Equals(right);
            }
            else
            {
                return ReferenceEquals(right, null);
            }
        }

        /// <summary>
        /// 非等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>true:不一致 false:一致</returns>
        public static bool operator !=(Span left, Span right)
        {
            return ! (left == right);
        }

        // Ver1.001 Span同士のoperator -(マイナス演算子)と、二つのSpanに交差部分があるかどうかを判定するIsClossing()メソッドを追加
        /// <summary>
        /// leftの期間から、rightの期間を除外した期間(0個～2個)を取得
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>除外した結果を返す(0個～2個)</returns>
        public static IEnumerable<Span> operator - (Span left, Span right)
        {
            var result = new List<Span>();
            if (left.IsNothing())
            {
                return result;
            }
            else if (right.IsNothing())
            {
                // 右辺の期間には該当期間が無い
                result.Add(left);
                return result;
            }
            else if (right.IsInside(left.Start) && right.IsInside(left.End))
            {
                // leftの開始日も終了日もrightに含まれる場合は全ての期間を除外
                return result;
            }
            else if ((left.Start < right.Start && left.End < right.Start)
                     || (left.Start > right.End && left.End > right.End)
                   )
            {
                // leftは完全にrightの外にあるのでそのまま格納
                result.Add(left);
                return result;
            }
            else
            {
                // leftの一部がrightと交差しているのでleftの両方、または、片方の側が残されるパターン

                if (!right.IsInside(left.Start))
                {
                    // 開始日側に残される期間が存在
                    result.Add(new Span(left.Start, right.Start - 1));
                }

                if (!right.IsInside(left.End))
                {
                    // 終了日側に残される期間が存在
                    result.Add(new Span(right.End + 1, left.End));
                }
                return result;
            }
        }

        /// <summary>
        /// 指定した期間との間に交差部分があるかどうか
        /// </summary>
        public bool IsCrossing(Span target)
        {
            if (this.IsNothing() || target.IsNothing())
            {
                // 期間無し同志、あるいは、いずれかが期間無しの場合の比較は交差部分無しとして解釈する
                return false;
            }
            else if(this.IsAll() || target.IsAll())
            {
                // どちらも期間無しではなく、どちらかが全ての期間の場合は、交差部分あり
                return true;
            }else{
                // 交差部分の有無を確認
                var diff = this - target;
                if (diff.Count() == 1 && diff.Single() == this)
                {
                    // 引き算して左辺値１個のみがそのまま帰ってきた場合は交差部分無し
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public static bool IsClossing(Span left, Span right)
        {
            return left.IsCrossing(right);
        }

        /// <summary>
        /// 二つの期間の交差部分を返す
        /// </summary>
        /// <returns>交差部分を表す期間。交差部分がない場合はSpan.Nothing</returns>
        public static Span operator & (Span left, Span right)
        {
            if (left.IsNothing() || right.IsNothing())
            {
                // 期間無し同志、あるいは、いずれかが期間無しの場合の比較は交差部分無し
                return Span.Nothing;
            }
            else
            {
                Span s1, s2;    // 開始日で正規化する
                if (left.Start < right.Start)
                {
                    s1 = left;
                    s2 = right;
                }
                else
                {
                    s1 = right;
                    s2 = left;
                }

                if (s1.End < s2.Start)
                {
                    // s1の終了日よりs2の開始日の方が未来→交差しない
                    return Span.Nothing;
                }
                else
                {
                    // s2の開始日はs1の終了日と同じか過去
                    if (s1.End <= s2.End)
                    {
                        // s2の開始日はs1の終了日と同じか過去で、s2の終了日はs1の終了日と同じか未来
                        if (s1.Start == s2.Start)
                        {
                            // 開始日が同じならs1全体(無駄にインスタンスを生成させないため)
                            return s1;
                        }
                        else
                        {
                            // s2の開始日からs1の終了日まで
                            return new Span(s2.Start, s1.End);
                        }
                    }
                    else
                    {
                        // s2の終了日はs1の終了日より前。s2全体(s2はs1に内包されている)
                        return s2;
                    }
                }
            }
        }
    }
}
