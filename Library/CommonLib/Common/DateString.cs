//
// DateString.cs
// 
// 日付(Date)クラス - 標準文字列生成処理
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Text;

namespace CommonLib.Common
{
	public static class DateString
    {
        /// <summary>
        /// 標準文字列(西暦年、月、日を'/'(スラッシュ)で区切る ex."1965/05/21"）の生成
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="Padding">年４桁、月・日２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <param name="OutputYear">年を出力するかどうか。省略時はtrue。</param>
        /// <example>
        /// <code>
        /// Date date = new Date(1965, 5, 21);
        /// string str1 = date.GetNormalString();                   // 1965/05/21
        /// string str2 = date.GetNormalString(PaddingChar:' ');    // 1965/ 5/21
        /// string str3 = date.GetNormalString(Padding:false);      // 1965/5/21
        /// string str4 = date.GetNormalString(OutputYear:false);   // 05/21
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]SlashYMDZero,SlashMDZero,SlashYMD,SlashMD<br/>
        ///     [C++]eDST_NORMAL_ZERO1, eDST_NORMAL_ZERO3, eDST_NORMAL1, eDST_NORMAL3<br/>
        /// </remarks>
        public static string GetNormalString(this Date date, bool Padding = true, char PaddingChar = '0', bool OutputYear = true)
        {
            var result = new StringBuilder();

            if (OutputYear)
            {
                string ystr = date.Year.ToString();
                if (Padding)
                {
                    ystr = ystr.PadLeft(4, PaddingChar);
                }
                result.Append(string.Format("{0}/", ystr));
            }

            string mstr = date.Month.ToString();
            string dstr = date.Day.ToString();

            if (Padding)
            {
                mstr = mstr.PadLeft(2, PaddingChar);
                dstr = dstr.PadLeft(2, PaddingChar);
            }

            result.Append(string.Format("{0}/{1}", mstr, dstr));

            return result.ToString();
        }

        /// <summary>
        /// 和暦文字列(元号YY年MM月DD日形式 ex."平成05年05月21日"）の生成
        /// <para>ただし、日付が明治より前の場合は元号の代わりに"西暦"と頭記の上、年を４桁で出力する。 ex."西暦1868年10月22日</para>
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="Padding">年月日それぞれ２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <param name="OutputYear">年を出力するかどうか。省略時はtrue。</param>
        /// <param name="ByADYear">元号年ではなく、西暦年("西暦"は付加しない)で出力する。省略時はfalse。ex.1993年05月21日</param>
        /// <example>
        /// <code>
        /// Date date = new Date(1965, 5, 21);
        /// string str1 = date.GetJapaneseString();                 // 昭和40年05月21日
        /// string str2 = date.GetJapaneseString(PaddingChar:' ');  // 昭和40年 5月21日
        /// string str3 = date.GetJapaneseString(Padding:false);    // 昭和40年5月21日
        /// string str4 = date.GetJapaneseString(OutputYear:false); // 05月21日
        /// string str5 = date.GetJapaneseString(ByADYear:true);    // 1965年05月21日
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]JpnGengoYMD,JpnMD<br/>
        ///     [C++]eDST_JP1, eDST_JP3<br/>
        /// </remarks>
        public static string GetJapaneseString(this Date date,
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
                    ystr = date.Year.ToString();
                    ylen = 4;
                }
                else
                {
                    // 元号で出力
                    var gengo = date.GetGengoInformation();
                    if (gengo == null)
                    {
                        // 元号データが存在しない(明治より前)
                        gstr = "西暦";
                        ystr = date.Year.ToString();
                        ylen = 4;
                    }
                    else
                    {
                        gstr = gengo.Name;
                        ystr = gengo.ToGengoYear(date.Year).ToString();
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

            string mstr = date.Month.ToString();
            string dstr = date.Day.ToString();

            if (Padding)
            {
                mstr = mstr.PadLeft(2, PaddingChar);
                dstr = dstr.PadLeft(2, PaddingChar);
            }

            result.Append(string.Format("{0}月{1}日", mstr, dstr));

            return result.ToString();
        }

        /// <summary>
        /// AN表記和暦文字列([MTSH]YY/MM/DD形式 ex."H05/05/21"）の生成
        /// <para>ただし、日付が明治より前の場合は元号の代わりに"AD"と頭記の上、年を４桁で出力する。 ex."AD1868年10月22日</para>
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="Padding">年月日それぞれ２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <param name="ByADYear">元号年ではなく、西暦年("AD"は付加しない)で出力する。省略時はfalse。ex.1993/05/21</param>
        /// <example>
        /// <code>
        /// Date date = new Date(1965, 5, 21);
        /// string str1 = date.GetJapaneseANString();                 // S40/05/21
        /// string str2 = date.GetJapaneseANString(PaddingChar:' ');  // S40/ 5/21
        /// string str3 = date.GetJapaneseANString(Padding:false);    // S40/5/21
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]SlashGengoYMD,SlashGengoYMDZero,SlashGengoYMD<br/>
        ///     [C++]eDST_JP_ASZERO1,eDST_JP_ASZERO3,eDST_JP_AS1,eDST_JP_AS3<br/>
        /// </remarks>
        public static string GetJapaneseANString(this Date date, bool Padding = true, char PaddingChar = '0', bool ByADYear = false)
        {
            var result = new StringBuilder();

            string gstr;
            string ystr;
            int ylen;

            if (ByADYear)
            {
                gstr = "";  // 明示的に西暦指定された場合は"AD"を付けない
                ystr = date.Year.ToString();
                ylen = 4;
            }
            else
            {
                // 元号で出力
                var gengo = GengoYear.GetGengoInformation(date);
                if (gengo == null)
                {
                    // 元号データが存在しない(明治より前)
                    gstr = "";
                    ystr = date.Year.ToString();
                    ylen = 4;
                }
                else
                {
                    gstr = gengo.AlphaName;
                    ystr = gengo.ToGengoYear(date.Year).ToString();
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
            result.Append("/");

            string mstr = date.Month.ToString();
            string dstr = date.Day.ToString();

            if (Padding)
            {
                mstr = mstr.PadLeft(2, PaddingChar);
                dstr = dstr.PadLeft(2, PaddingChar);
            }

            result.Append(string.Format("{0}/{1}", mstr, dstr));

            return result.ToString();
        }

        /// <summary>
        /// 数字のみ固定桁表記(YYYYMMDD ex."19940521"）の生成
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="OutputYear">年を出力するかどうか。省略時はtrue。</param>
        /// <example>
        /// <code>
        /// Date date = new Date(1965, 5, 21);
        /// string str1 = date.GetNumeralString();                 // 19650521
        /// string str2 = date.GetNumeralString(OutputYear:false); // 0521
        /// </code>
        /// </example>
        /// <remarks>
        /// 旧コード<br/>
        ///     [C#]YMDZero, MDZero<br/>
        ///     [C++]eDST_NUMERAL1, eDST_NUMERAL3<br/>
        /// </remarks>
        public static string GetNumeralString(this Date date, bool OutputYear = true)
        {
            if (OutputYear)
            {
                return string.Format("{0:D4}{1:D2}{2:D2}", date.Year, date.Month, date.Day);
            }
            else
            {
                return string.Format("{0:D2}{1:D2}", date.Month, date.Day);
            }
        }

        // 使用していないのでサポートしない [C++]eDST_USA_ZERO, eDST_USA ex.9/4/1965
    }
}
