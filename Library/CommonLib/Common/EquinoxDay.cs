//
// EquinoxDay.cs
// 
// 春分の日・秋分の日の定義用クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;

namespace CommonLib.Common
{
	// 春分の日、秋分の日を定義
	public static class EquinoxDay
    {
        // 天文学的な春分日・秋分日による「春分の日」「秋分の日」の推定

        /*
         * wikipedia「春分の日」より抜粋
         * (http://ja.wikipedia.org/w/index.php?title=%E6%98%A5%E5%88%86%E3%81%AE%E6%97%A5&oldid=51086053)
         * 春分の日」の具体的日付は、たいてい3月20日から3月21日ごろのいずれか1日となる。
         * 祝日法の上では「春分の日　春分日」つまり同法上の「春分の日」を「春分日」とする
         * ことのみが規定され、日付は固定されていない。
         * 実際の各年の「春分の日」は、国立天文台が作成する『暦象年表』という小冊子に基づき
         * 閣議において決定され、決定する日の前年2月第1平日付の官報で暦要項として公告される。
         * すなわち、天文学における「春分日」は、天文観測に基づいて地球の運行状態などが変わ
         * らないと仮定できる範囲で2年後以降のものも計算により特定できるが、国民の祝日とし
         * ての「春分の日」は前年の2月にならなければその通りとまでは確定できない。
         * ただし、これまでのところ天文計算によって求められた「春分日」以外が「春分の日」
         * とされたことはない。
         * 天文計算の「春分日」は、1990年から2025年までは閏年とその翌年が3月20日になり、
         * その他の年は3月21日となる。それ以降、2026年からは閏年の前年が3月21日になり、
         * その他の年は3月20日となる。
         * また、2092年の春分日は3月19日となる。現行の祝日法ができる以前ではあるが1923年の
         * 春季皇霊祭（春分の日）は3月22日であった。
        */

        /// 現在のインプリメントは天文学的な計算式に基づき推定しているため、1980年～2099年の範囲でのみ有効。
        /// もし、今後天文学的な計算式で求められなくなった場合(政治的にずらされた場合や天変地異等)は、
        /// 今後はテーブル参照式に変更した方が良いのではないか。(計算可能範囲に高々120件しか存在しない)

        /// <summary>
        /// 計算基準開始年(春分秋分計算用)
        /// </summary>
        private const int BASIC_STARTYEAR = 1980;

        /// <summary>
        /// 計算基準終了年(春分秋分計算用)
        /// </summary>
        private const int BASIC_ENDYEAR = 2099;

        /// <summary>
        /// 春分の日係数(春分計算用)
        /// </summary>
        private const double SPRINGDAY_COEFFICIENT = 20.8431;

        /// <summary>
        /// 秋分の日係数(秋分計算用)
        /// </summary>
        private const double FALLDAY_COEFFICIENT = 23.2488;

        /// <summary>
        /// 計算係数(春分秋分計算用)
        /// </summary>
        private const double CALC_COEFFICIENT = 0.242194;

        /// <summary>
        /// 指定した年について、「春分の日」の「日」を取得
        /// <para>
        /// 現在のインプリメントは天文学的な計算式に基づき推定している。1980年～2099年の範囲でのみ取得可能。
        /// </para>
        /// </summary>        
        public static int CalcVernalEquinoxDay(int year)
        {
            // 計算対象範囲内かどうか
            if (year < BASIC_STARTYEAR || year > BASIC_ENDYEAR){
                throw new ArgumentException(string.Format("春分日計算可能範囲外の年[{0}]が指定された。", year));
            }else{
                double ansFrom = SPRINGDAY_COEFFICIENT + (CALC_COEFFICIENT * (year - BASIC_STARTYEAR));
                double ansTo = (year - BASIC_STARTYEAR) / 4;
                return (int)(ansFrom - ansTo);
            }
        }

        /// <summary>
        /// 日付が「春分の日」かどうか
        /// <para>
        /// 日付情報を対象に春分の日かどうかをチェックする。<br/>
        /// 現在のインプリメントは天文学的な計算式に基づき推定しているため、1980年～2099年の範囲でのみ有効。
        /// </para>
        /// </summary>
        public static bool IsVernalEquinoxDay(this Date date)
        {
            // 春分の日が存在する3月、かつ、計算対象範囲内かどうか
            if (BASIC_STARTYEAR <= date.Year && date.Year <= BASIC_ENDYEAR && date.Month == 3)
            {
                int day = CalcVernalEquinoxDay(date.Year);
                return day == date.Day;
            }
            else
            {
                // 3月以外、または、計算対象範囲外
                return false;
            }
        }

        /// <summary>
        /// 指定した年について、「秋分の日」の「日」を取得
        /// <para>
        /// 現在のインプリメントは天文学的な計算式に基づき推定している。1980年～2099年の範囲でのみ取得可能。
        /// </para>
        /// </summary>        
        public static int CalcAutumnEquinoxDay(int year)
        {
            // 計算対象範囲内かどうか
            if (year < BASIC_STARTYEAR || year > BASIC_ENDYEAR)
            {
                throw new ArgumentException(string.Format("秋分日計算可能範囲外の年[{0}]が指定された。", year));
            }
            else
            {
                double ansFrom = FALLDAY_COEFFICIENT + (CALC_COEFFICIENT * (year - BASIC_STARTYEAR));
                double ansTo = (year - BASIC_STARTYEAR) / 4;
                return (int)(ansFrom - ansTo);
            }
        }
        
        /// <summary>
        /// 日付が「秋分の日」かどうか
        /// <para>
        /// 日付情報を対象に秋分の日かどうかをチェックする。<br/>
        /// 現在のインプリメントは天文学的な計算式に基づき推定しているため、1980年～2099年の範囲でのみ有効。
        /// </para>
        /// </summary>
        public static bool IsAutumnEquinoxDay(this Date date)
        {
            // 秋分の日が存在する9月、かつ、計算対象範囲内かどうか
            if (date.Month == 9 && BASIC_STARTYEAR <= date.Year && date.Year <= BASIC_ENDYEAR)
            {
                int day = CalcAutumnEquinoxDay(date.Year);
                return day == date.Day;
            }
            else
            {
                // 9月以外、または、計算対象範囲外
                return false;
            }
        }
    }
}
