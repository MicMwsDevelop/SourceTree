//
// SpanString.cs
// 
// 期間(Span)クラス - 標準文字列生成処理
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
namespace MwsLib.Common
{
	public static class SpanString
    {
        /// <summary>
        /// 和暦文字列(元号YY年MM月DD日形式 ex."平成05年05月21日"）による期間文字列の生成
        /// <para>DateStringHelper.GetJapaneseString()を参照</para>
        /// </summary>
        /// <param name="span">期間</param>
        /// <param name="Separater">開始日と終了日の間に挿入する文字列。省略時は"～"。</param>
        /// <param name="Padding">年月日それぞれ２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <param name="ByADYear">元号年ではなく、西暦年("西暦"は付加しない)で出力する。省略時はfalse。ex.1993年05月21日</param>
        /// <param name="ContractInSame"> 開始日と終了日が同一の場合は日付(開始日)のみ出力する。省略時はtrue</param>
        /// <returns>
        /// 期間文字列を返す。<br/>
        /// ただし、全ての期間の場合は空文字列、該当期間が無い場合は"(該当期間無し)"。<br/>
        /// 開始日と終了日が同一の場合はその日付のみ(セパレーターは出力しない)。
        /// 開始日あるいは終了日いずれかの設定が無い場合は、該当部分について空文字列で出力し、セパレーターも出力する。
        /// </returns>
        /// <remarks>
        /// <para>
        /// 「該当期間無し」の場合、旧クラスでは便宜的に開始日・終了日として設定されている日付が直接出力されていたが、<br/>
        /// 本クラスでは"(該当期間無し)"を出力する。
        /// </para>
        /// <para>
        /// 開始日の指定が無いとき、c++のDSpanクラスでは開始日のみを出力し、セパレーターの"～"は出力されなかった。
        /// c#のSpanクラスでは開始日または終了日のいずれかの指定が無いとき、指定がある方の日付だけを出力していた。
        /// 本クラスでは、"開始日～"または"～終了日"と表記する。
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// Span span = new Span(1965, 5, 21, 2004, 12, 1);
        /// string str1 = span.GetJapaneseString();                 // "昭和40年05月21日～平成06年12月01日"
        /// string str2 = span.GetJapaneseString(PaddingChar:' ');  // "昭和40年 5月21日～平成06年12月 1日"
        /// string str3 = span.GetJapaneseString(Padding:false);    // "昭和40年5月21日～平成06年12月1日"
        /// string str4 = span.GetJapaneseString(Seperator:"-");    // "昭和40年5月21日-平成06年12月1日"
        /// string str5 = span.GetJapaneseString(ByADYear:true);    // "1965年05月21日～2004年12月1日"
        /// string str6 = Span.All.GetJapaneseString();             // ""
        /// string str7 = Span.Nothing.GetJapaneseString();         // "(該当期間無し)"
        /// string str8 = (new Span(new Date(1965, 5, 21))).GetJapaneseString();    // "昭和40年05月21日"
        /// </code>
        /// </example>
        public static string GetJapaneseString(this Span span,
                                               string Separater = "～",
                                               bool Padding = true, char PaddingChar = '0',
                                               bool ByADYear = false,
                                               bool ContractInSame = true)
        {
            if (span.IsNothing())
            {
                // 該当期間無しの場合
                // 旧クラス(c++:DSpan, c#:Span)ではこの場合、実際に設定されている日付が出力されていた
                //   ex."西暦1年1月2日～西暦1年1月1日"
                // このクラスでは、"(該当期間無し)"と出力するようにした
                return "(該当期間無し)";
            }
            else if (span.IsAll())
            {
                // 全ての日付を含む期間の場合は空文字列を返す
                return string.Empty;
            }
            else if (ContractInSame && span.Start == span.End)
            {
                // 開始日と終了日が同一の場合は日付のみ
                return span.Start.GetJapaneseString(Padding, PaddingChar, true, ByADYear);
            }
            else
            {
                string sstr, estr;
                if (span.Start > Date.MinValue)
                {
                    sstr = span.Start.GetJapaneseString(Padding, PaddingChar, true, ByADYear);
                }
                else
                {
                    sstr = "";
                }
                if (span.End < Date.MaxValue)
                {
                    estr = span.End.GetJapaneseString(Padding, PaddingChar, true, ByADYear);
                }
                else
                {
                    estr = "";
                }
                return string.Format("{0}{1}{2}", sstr, Separater, estr);
            }
        }

        /// <summary>
        /// AN表記和暦文字列([MTSH]YY/MM/DD形式 ex."H05/05/21"）による期間文字列の生成
        /// <para>DateStringHelper.GetJapaneseANString()を参照</para>
        /// </summary>
        /// <param name="span">期間</param>
        /// <param name="Separater">開始日と終了日の間に挿入する文字列。省略時は"～"。</param>
        /// <param name="Padding">年月日それぞれ２桁への桁揃えをするかどうか。省略時はtrue。</param>
        /// <param name="PaddingChar">Paddingがtrueの場合に、桁揃えの埋め草に使う文字を指定。省略時は'0'。</param>
        /// <param name="ByADYear">元号年ではなく、西暦年("西暦"は付加しない)で出力する。省略時はfalse。ex.1993/05/21</param>
        /// <returns>
        /// 期間文字列を返す。<br/>
        /// ただし、全ての期間の場合は空文字列、該当期間が無い場合は"(nothing)"。<br/>
        /// 開始日と終了日が同一の場合はその日付のみ(セパレーターは出力しない)。
        /// 開始日あるいは終了日いずれかの設定が無い場合は、該当部分について空文字列で出力し、セパレーターも出力する。
        /// </returns>
        /// <remarks>
        /// <para>
        /// 「該当期間無し」の場合、旧クラスでは便宜的に開始日・終了日として設定されている日付が直接出力されていたが、<br/>
        /// 本クラスでは"(span is nothing)"を出力する。
        /// </para>
        /// <para>
        /// 開始日の指定が無いとき、c++のDSpanクラスでは開始日のみを出力し、セパレーターは出力されなかった。
        /// c#のSpanクラスでは開始日または終了日のいずれかの指定が無いとき、指定がある方の日付だけを出力していた。
        /// 本クラスでは、"開始日～"または"～終了日"と表記する。
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// Span span = new Span(1965, 5, 21, 2004, 12, 1);
        /// string str1 = span.GetJapaneseANString();                 // "S40/05/21～H06/12/01"
        /// string str2 = span.GetJapaneseANString(PaddingChar:' ');  // "S40/ 5/21～H06/12/ 1"
        /// string str3 = span.GetJapaneseANString(Padding:false);    // "S40/5/21～H06/12\1"
        /// string str4 = span.GetJapaneseANString(Seperator:"-");    // "S40/5/21-H06/12/1"
        /// string str5 = span.GetJapaneseANString(ByADYear:true);    // "1965/05/21～2004/12/1"
        /// string str6 = Span.All.GetJapaneseANString();             // ""
        /// string str7 = Span.Nothing.GetJapaneseANString();         // "(span is nothing)"
        /// string str8 = (new Span(new Date(1965, 5, 21))).GetJapaneseANString();    // "S40/05/21"
        /// </code>
        /// </example>
        public static string GetJapaneseANString(this Span span,
                                               string Separater = "～",
                                               bool Padding = true, char PaddingChar = '0',
                                               bool ByADYear = false,
                                               bool ContractInSame = true)
        {
            if (span.IsNothing())
            {
                // 該当期間無しの場合
                // 旧クラス(c++:DSpan, c#:Span)ではこの場合、実際に設定されている日付が出力されていた
                //   ex."西暦1年1月2日～西暦1年1月1日"
                // このクラスでは、"(該当期間無し)"と出力するようにした
                return "(span is nothing)";
            }
            else if (span.IsAll())
            {
                // 全ての日付を含む期間の場合は空文字列を返す
                return string.Empty;
            }
            else if (ContractInSame && span.Start == span.End)
            {
                // 開始日と終了日が同一の場合は日付のみ
                return span.Start.GetJapaneseANString(Padding, PaddingChar, ByADYear);
            }
            else
            {
                string sstr, estr;
                if (span.Start > Date.MinValue)
                {
                    sstr = span.Start.GetJapaneseANString(Padding, PaddingChar, ByADYear);
                }
                else
                {
                    sstr = "";
                }
                if (span.End < Date.MaxValue)
                {
                    estr = span.End.GetJapaneseANString(Padding, PaddingChar, ByADYear);
                }
                else
                {
                    estr = "";
                }
                return string.Format("{0}{1}{2}", sstr, Separater, estr);
            }
        }
    }
}