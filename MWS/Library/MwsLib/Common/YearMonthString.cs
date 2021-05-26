//
// YearMonthString.cs
// 
// 月(YearMonth)クラス - 標準文字列生成処理
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Text;

namespace MwsLib.Common
{
	public static class YearMonthString
    {
        /// <summary>
        /// 標準文字列(西暦年、月を'/'(スラッシュ)で区切る ex."1965/05"）の生成
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="Padding">年４桁、月２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <example>
        /// <code>
        /// YearMonth month = new YearMonth(1965, 5);
        /// string str1 = month.GetNormalString();                   // 1965/05
        /// string str2 = month.GetNormalString(PaddingChar:' ');    // 1965/ 5
        /// string str3 = month.GetNormalString(Padding:false);      // 1965/5
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]SlashYMZero,SlashYM<br/>
        ///     [C++]eDST_NORMAL_ZERO2, eDST_NORMAL2<br/>
        /// </remarks>
        public static string GetNormalString(this YearMonth month, bool Padding = true, char PaddingChar = '0')
        {
            string ystr = month.Year.ToString();
            string mstr = month.Month.ToString();
            if (Padding)
            {
                ystr = ystr.PadLeft(4, PaddingChar);
                mstr = mstr.PadLeft(2, PaddingChar);
            }
            return string.Format("{0}/{1}", ystr, mstr);
        }

        /// <summary>
        /// 和暦文字列(元号YY年MM月形式 ex."平成05年05月"）の生成
        /// <para>元号は月初日で判定する。</para>
        /// <para>明治より前の場合は元号の代わりに"西暦"と頭記の上、年を４桁で出力する。 ex."西暦1868年10月</para>
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="Padding">月、日の桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <param name="OutputYear">年を出力するかどうか。省略時はtrue。</param>
        /// <param name="ByADYear">元号年ではなく、西暦年("西暦"は付加しない)で出力する。省略時はfalse。ex.1993年05月</param>
        /// <example>
        /// <code>
        /// YearMonth month = new YearMonth(1965, 5);
        /// string str1 = month.GetJapaneseString();                 // 昭和40年05月
        /// string str2 = month.GetJapaneseString(PaddingChar:' ');  // 昭和40年 5月
        /// string str3 = month.GetJapaneseString(Padding:false);    // 昭和40年5月
        /// string str4 = month.GetJapaneseString(OutputYear:false); // 05月
        /// string str5 = month.GetJapaneseString(ByADYear:true);    // 1965年05月
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]JpnGengoYM<br/>
        ///     [C++]eDST_JP2<br/>
        /// </remarks>
        public static string GetJapaneseString(this YearMonth month,
                                               bool Padding = true, char PaddingChar = '0',
                                               bool OutputYear = true, bool ByADYear = false)
        {
            var result = new StringBuilder();

            if (OutputYear)
            {
                string gstr;
                string ystr;
                int ylen;

                if (ByADYear)
                {
                    gstr = "";  // 明示的に西暦指定された場合は"西暦"を付けない
                    ystr = month.Year.ToString();
                    ylen = 4;
                }
                else
                {
                    // 元号で出力
                    var gengo = GengoYear.GetGengoInformation(month.ToDate(1));
                    if (gengo == null)
                    {
                        // 元号データが存在しない(明治より前)
                        gstr = "西暦";
                        ystr = month.Year.ToString();
                        ylen = 4;
                    }
                    else
                    {
                        gstr = gengo.Name;
                        ystr = gengo.ToGengoYear(month.Year).ToString();
                        ylen = 2;
                    }
                }
                if (Padding)
                {
                    result.Append(gstr);
                    result.Append(ystr.PadLeft(ylen, PaddingChar));
                }
                else
                {
                    result.Append(gstr);
                    result.Append(ystr);
                }
                result.Append("年");
            }

            string mstr = month.Month.ToString();

            if (Padding)
            {
                mstr = mstr.PadLeft(2, PaddingChar);
            }

            result.Append(string.Format("{0}月", mstr));

            return result.ToString();
        }

        /// <summary>
        /// AN表記和暦文字列([MTSH]YY/MM形式 ex."H05/05"）の生成
        /// <para>元号は月初日で判定する。</para>
        /// <para>明治より前の場合は元号の代わりに"AD"と頭記の上、年を４桁で出力する。 ex."AD1868年10月</para>
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="Padding">年月それぞれ２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <example>
        /// <code>
        /// YearMonth month = new YearMonth(1965, 5);
        /// string str1 = month.GetJapaneseString();                 // S40/05/21
        /// string str2 = month.GetJapaneseString(PaddingChar:' ');  // S40/ 5/21
        /// string str3 = month.GetJapaneseString(Padding:false);    // S40/5/21
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]SlashGengoYM,SlashGengoYMZero<br/>
        ///     [C++]eDST_JP_ASZERO2,eDST_JP_AS2<br/>
        /// </remarks>
        public static string GetJapaneseANString(this YearMonth month, bool Padding = true, char PaddingChar = '0')
        {
            var result = new StringBuilder();

            string gstr;
            string ystr;
            int ylen;

            // 元号で出力
            var gengo = GengoYear.GetGengoInformation(month.ToDate(1));
            if (gengo == null)
            {
                // 元号データが存在しない(明治より前)
                gstr = "AD";
                ystr = month.Year.ToString();
                ylen = 4;
            }
            else
            {
                gstr = gengo.AlphaName;
                ystr = gengo.ToGengoYear(month.Year).ToString();
                ylen = 2;
            }

            if (Padding)
            {
                result.Append(gstr);
                result.Append(ystr.PadLeft(ylen, PaddingChar));
            }
            else
            {
                result.Append(gstr);
                result.Append(ystr);
            }
            result.Append("/");

            string mstr = month.Month.ToString();

            if (Padding)
            {
                mstr = mstr.PadLeft(2, PaddingChar);
            }

            result.Append(string.Format("{0}", mstr));

            return result.ToString();
        }

        /// <summary>
        /// 数字のみ固定桁表記(YYYYMM ex."199405"）の生成
        /// </summary>
        /// <param name="month">月</param>
        /// <example>
        /// <code>
        /// YearMonth month = new YearMonth(1965, 5);
        /// string str1 = month.GetJapaneseString();                     // 19650521
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]YMZero<br/>
        ///     [C++]eDST_NUMERAL2<br/>
        /// </remarks>
        public static string GetNumeralString(this YearMonth month)
        {
            return string.Format("{0:D2}{1:D2}", month.Year, month.Month);
        }

        /// <summary>
        /// 数字のみ固定桁表記(YYYYMM ex."199405"）の生成
        /// </summary>
        /// <param name="month">月</param>
        /// <example>
        /// <code>
        /// YearMonth month = new YearMonth(1965, 5);
        /// string str1 = month.GetJapaneseString();                     // 19650521
        /// </code>
        /// </example>
        /// <remarks>
        /// GetNumeralStringとの違い
        /// 西暦年が4ケタ未満のときの文字数
        /// ex.
        /// 西暦794年10月
        /// GetNumeralString=>"79410"
        /// GetNumeralStringLong=>"079410"
        /// 
        /// 西暦2015年10月
        /// GetNumeralString=>"201510"
        /// GetNumeralStringLong=>"201510"
        /// </remarks>
        public static string GetNumeralStringLong(this YearMonth month)
        {
            return string.Format("{0:D4}{1:D2}", month.Year, month.Month);
        }
    }
}
