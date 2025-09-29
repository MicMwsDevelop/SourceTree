//
// StringUtil.cs
// 
// 文字列のユーティリティ関数群
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CommonLib.Common
{
    /// <summary>
    /// 文字列のユーティリティ関数群
    /// </summary>
    public static class StringUtil
    {
        ////////////////////////////////////////////////////////////
        // IsMatch(), IsInclude()用の正規表現パターン文字列

        /// <summary>
        /// 正規表現パターン文字列（半角数字）
        /// </summary>
        const string regex_pattern_numeral = @"0-9";

        /// <summary>
        /// 正規表現パターン文字列（半角アルファベット）
        /// </summary>
        const string regex_pattern_alphabet = @"a-zA-Z";

        /// <summary>
        /// 正規表現パターン文字列（半角カタカナ）
        /// </summary>
        /// <remarks>
        /// ※C++の_ismbbkana()の範囲(Shift-JIS 0xA1～0xDF)に合わせる
        /// </remarks>
        const string regex_pattern_katakana = @"\uFF61-\uFF9F";


        ////////////////////////////////////////////////////////////
        // 

        /// <summary>
        /// 
        /// </summary>
        private const string FORMAT_COMMA = "{0:#,0}";


        ////////////////////////////////////////////////////////////
        // Trim指定文字列

        /// <summary>
        /// 標準Trim文字列群
        /// </summary>
        /// <remarks>
        /// C++のTrim()指定時と同じ文字列群
        /// ※C#のTrim()では全角スペースが削除される
        /// </remarks>
        public static readonly char[] DefalutTrimCharSet = {' ' ,'\t' ,'\n' };

        ////////////////////////////////////////////////////////////
        // 半角・全角変換

        /// <summary>
        /// 半角への変換対象外文字列定義
        /// </summary>
        /// <remarks>変換処理を再考する必要があるが、時間の制約の都合上、変換しないようにする</remarks>
        private static readonly string[] NotNarrowStr = new string[]
        {
            "～",
        };

        /// <summary>
        /// 全角への変換対象外文字列定義
        /// </summary>
        /// <remarks>変換処理を再考する必要があるが、時間の制約の都合上、変換しないようにする</remarks>
        private static readonly string[] NotUpperStr = new string[]
        {
            "~",
        };

        /// <summary>
        /// 半角→全角でC++と結果が異なる文字のマッピング
        /// </summary>
        private static readonly Dictionary<string, string> ShiftJisWideDic = new Dictionary<string, string>()
        {
            // C# -> C++
            { "＂", "”" },
            { "＇", "’" },
            { "\\", "￥" },
            { "｀", "‘" },
            { "゙", "゛" },
            { "゚", "゜" },
            { "ヺ", "ヲ" },
            { "ヷ", "ワ" },
        };

        /// <summary>
        /// 濁点の付けられないカナ文字定義
        /// </summary>
        private static readonly string[] NotVSoundKana = new string[]
        {
            "ｧ", "ｨ", "ｩ", "ｪ", "ｫ", "ｬ", "ｭ", "ｮ", "ｯ", "ｰ", 
            "ｱ", "ｲ", "ｴ", "ｵ", 
            "ﾅ", "ﾆ", "ﾇ", "ﾈ", "ﾉ", 
            "ﾏ", "ﾐ", "ﾑ", "ﾒ", "ﾓ",
            "ﾔ", "ﾕ", "ﾖ", 
            "ﾗ", "ﾘ", "ﾙ", "ﾚ", "ﾛ", 
            "ﾜ", "ｦ", "ﾝ", " ",
        };

        /// <summary>
        /// 半濁点のつけられないカナ文字定義
        /// </summary>
        private static readonly string[] NotPSoundKana = new string[]
        {
            "ｧ", "ｨ", "ｩ", "ｪ", "ｫ", "ｬ", "ｭ", "ｮ", "ｯ", "ｰ", 
            "ｱ", "ｲ", "ｳ", "ｴ", "ｵ", 
            "ｶ", "ｷ", "ｸ", "ｹ", "ｺ", 
            "ｻ", "ｼ", "ｽ", "ｾ", "ｿ", 
            "ﾀ", "ﾁ", "ﾂ", "ﾃ", "ﾄ", 
            "ﾅ", "ﾆ", "ﾇ", "ﾈ", "ﾉ", 
            "ﾏ", "ﾐ", "ﾑ", "ﾒ", "ﾓ", 
            "ﾔ", "ﾕ", "ﾖ", 
            "ﾗ", "ﾘ", "ﾙ", "ﾚ", "ﾛ",
            "ﾜ", "ｦ", "ﾝ", " ",
        };

        /// <summary>
        /// 文字列マッピング処理関数
        /// </summary>
        /// <param name="Locale"></param>
        /// <param name="dwMapFlags"></param>
        /// <param name="lpSrcStr"></param>
        /// <param name="cchSrc"></param>
        /// <param name="lpDestStr"></param>
        /// <param name="cchDest"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        static extern private int LCMapStringW(int Locale, uint dwMapFlags,
            [MarshalAs(UnmanagedType.LPWStr)]string lpSrcStr, int cchSrc,
            [MarshalAs(UnmanagedType.LPWStr)] string lpDestStr, int cchDest);

        /// <summary>
        /// 文字列マッピング時の変換方法
        /// </summary>
        public enum dwMapFlags : uint
        {
            NORM_IGNORECASE = 0x00000001,           // 大文字と小文字を区別しません。
            NORM_IGNORENONSPACE = 0x00000002,       // 送りなし文字を無視します。このフラグをセットすると、日本語アクセント文字も削除されます。
            NORM_IGNORESYMBOLS = 0x00000004,        // 記号を無視します。
            LCMAP_LOWERCASE = 0x00000100,           // 小文字を使います。
            LCMAP_UPPERCASE = 0x00000200,           // 大文字を使います。
            LCMAP_SORTKEY = 0x00000400,             // 正規化されたワイド文字並び替えキーを作成します。
            LCMAP_BYTEREV = 0x00000800,             // Windows NT のみ : バイト順序を反転します。たとえば 0x3450 と 0x4822 を渡すと、結果は 0x5034 と 0x2248 になります。
            SORT_STRINGSORT = 0x00001000,           // 区切り記号を記号と同じものとして扱います。
            NORM_IGNOREKANATYPE = 0x00010000,       // ひらがなとカタカナを区別しません。ひらがなとカタカナを同じと見なします。
            NORM_IGNOREWIDTH = 0x00020000,          // シングルバイト文字と、ダブルバイトの同じ文字とを区別しません。
            LCMAP_HIRAGANA = 0x00100000,            // ひらがなにします。
            LCMAP_KATAKANA = 0x00200000,            // カタカナにします。
            LCMAP_HALFWIDTH = 0x00400000,           // 半角文字にします（適用される場合）。
            LCMAP_FULLWIDTH = 0x00800000,           // 全角文字にします（適用される場合）。
            LCMAP_LINGUISTIC_CASING = 0x01000000,   // 大文字と小文字の区別に、ファイルシステムの規則（既定値）ではなく、言語上の規則を使います。LCMAP_LOWERCASE、または LCMAP_UPPERCASE とのみ組み合わせて使えます。
            LCMAP_SIMPLIFIED_CHINESE = 0x02000000,  // 中国語の簡体字を繁体字にマップします。
            LCMAP_TRADITIONAL_CHINESE = 0x04000000, // 中国語の繁体字を簡体字にマップします。
        }

        /// <summary>
        /// 文字列を変換する
        /// </summary>
        /// <param name="str"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static string StringConvert(this string str, dwMapFlags flags)
        {
            var ci = System.Globalization.CultureInfo.CurrentCulture;
            string result = new string('\0', str.Length * 2);   // 半角変換時、"ブ"（1文字） ⇒ "ﾌﾞ"（2文字）となり濁点が消えるのを防止
            LCMapStringW(ci.LCID, (uint)flags, str, str.Length, result, result.Length);
            return result.TrimEnd('\0');    // nullトリミング
        }

        /// <summary>
        /// 引数の1/1000数値を小数点以下３桁までの数値文字列に変換
        /// </summary>
        /// <remarks>
        /// ex. padding = false の場合
        ///      ・99999 -> 99.999
        ///      ・99990 -> 99.99
        ///      ・99900 -> 99.9
        /// ex. padding = true の場合
        ///      ・99999 -> 99.999
        ///      ・99990 -> 99.990
        ///      ・99900 -> 99.900
        /// </remarks>
        /// <param name="value">変換対象数値</param>
        /// <param padding="value">小数点以下を0で桁埋する</param>
        /// <returns>小数点以下３桁までの1/1000数値文字</returns>
        public static string FixedDecimalString(int value, bool padding = false)
        {
            decimal calcResult = value / 1000m;
            string num = string.Empty;
            if(padding)
            {
                // 0で桁埋
                num = calcResult.ToString("0.000");
            }
            else
            {
                // 0の値は表示されない
                num = calcResult.ToString("0.###");
            }
            return num;
        }

        /// <summary>
        /// 引数の1/100数値を小数点以下２桁までの数値文字列に変換
        /// </summary>
        /// <remarks>
        /// ex. padding = false の場合
        ///      ・99   -> 0.99
        ///      ・999  -> 9.99
        ///      ・9999 -> 99.99
        ///      ・9990 -> 99.9
        /// ex. padding = true の場合
        ///      ・99   -> 0.99
        ///      ・999  -> 9.99
        ///      ・9999 -> 99.99
        ///      ・9990 -> 99.90
        /// </remarks>
        /// <param name="value">変換対象数値</param>
        /// <param padding="value">小数点以下を0で桁埋する</param>
        /// <returns>小数点以下２桁までの1/10数値文字</returns>
        public static string FixedDecimalString100(int value, bool padding = false)
        {
            decimal calcResult = value / 100m;
            string num = string.Empty;
            if (padding)
            {
                // 0で桁埋
                num = calcResult.ToString("0.000");
            }
            else
            {
                // 0の値は表示されない
                num = calcResult.ToString("0.###");
            }
            return num;
        }

        /// <summary>
        /// 引数の1/10数値を小数点以下１桁までの数値文字列に変換
        /// </summary>
        /// <remarks>
        /// ex. 9 -> 0.9
        /// ex. 999 -> 99.9
        /// </remarks>
        /// <param name="value">変換対象数値</param>
        /// <returns>小数点以下１桁までの1/10数値文字列</returns>
        public static string FixedDecimalString10(int value)
        {
            decimal calcResult = value / 10m;
            return calcResult.ToString();
        }

        /// <summary>
        /// 小数点以下１桁までの数値文字列を数値に変換
        /// </summary>
        /// <param name="str">小数点以下３桁までの数値文字列</param>
        /// <returns>数値</returns>
        public static int FixedDecimalNumber10(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return (int)(decimal.Parse(str) * 10);
        }

        /// <summary>
        /// 小数点以下３桁までの数値文字列を数値に変換
        /// </summary>
        /// <param name="str">小数点以下３桁までの数値文字列</param>
        /// <returns>数値</returns>
        public static int FixedDecimalNumber(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return (int)(decimal.Parse(str) * 1000);
        }

        /// <summary>
        /// 文字列の小文字を大文字に変換する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertUpperCase(string str)
        {
            return Strings.StrConv(str, VbStrConv.Uppercase);
        }

        /// <summary>
        /// 文字列の大文字を小文字に変換する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertLowerCase(string str)
        {
            return Strings.StrConv(str, VbStrConv.Lowercase);
        }
        /// <summary>
        /// 文字列の先頭のみ大文字にする
        /// </summary>
        /// <param name="str">変換元文字列</param>
        /// <param name="split">区切り文字</param>
        /// <returns></returns>
        public static string ConvertFirstCharaUpper(string str,char split = '\0')
        {
            string result = string.Empty;

            if(split == '\0')
            {
                string first = Strings.StrConv(str.Substring(0, 1), VbStrConv.Uppercase);
                string left = Strings.StrConv(str.Substring(1, str.Length - 1), VbStrConv.Lowercase);
                result += first + left;
            }
            else
            {
                var splitStr = str.Split(split);
                int len = splitStr.Length;
                for (int i = 0; i < len;i++ )
                {
                    string first = Strings.StrConv(splitStr[i].Substring(0, 1), VbStrConv.Uppercase);
                    string left = Strings.StrConv(splitStr[i].Substring(1, splitStr[i].Length - 1), VbStrConv.Lowercase);

                    result += first + left;

                    if(i < len - 1)
                    {
                        result += "_";
                    }
                }                
            }            
            return result;
        }

        ///// <summary>
        ///// 文字列を全角→半角に変換する
        ///// </summary>
        ///// <remarks>ShiftJisにない文字は?に変換される</remarks>
        ///// <param name="str">変換対象文字列</param>
        ///// <returns>半角文字列</returns>
        //[Obsolete("理由が無い限りConvertNarrowForUnicodeを使用してください。", false)]
        //public static string ConvertNarrow(string str)
        //{
        //    return Strings.StrConv(str, VbStrConv.Narrow);
        //}

        /// <summary>
        /// 文字列を全角→半角に変換する
        /// </summary>
        /// <remarks>ShiftJisにない文字は変換しない</remarks>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertNarrowForUnicode(string str)
        {
            string ret = string.Empty;

            // 暫定処理
            string work = str;
            while (!string.IsNullOrEmpty(work))
            {
                int findIndex = -1;
                foreach (var notStr in NotNarrowStr)
                {
                    int index = work.IndexOf(notStr);
                    if (-1 < index)
                    {
                        if (-1 == findIndex)
                        {
                            // 他の文字で見つかったものが無い場合
                            findIndex = index;
                        }
                        else
                        {
                            // 他の文字で見つかったものがある場合、先頭に近いものを優先する
                            if (index < findIndex)
                            {
                                findIndex = index;
                            }
                        }
                    }
                }

                if (-1 != findIndex)
                {
                    // 変換対象外文字がある場合

                    // 左辺を変換
                    string leftStr = Left(work, findIndex);
                    if (!string.IsNullOrEmpty(leftStr))
                    {
                        ret += StringConvert(leftStr, dwMapFlags.LCMAP_HALFWIDTH);
                    }

                    // 変換対象外文字を付加
                    ret += work[findIndex];

                    // 右辺+1以降
                    work = Mid(work, findIndex + 1);
                }
                else
                {
                    // 変換対象外文字が無い場合
                    ret += StringConvert(work, dwMapFlags.LCMAP_HALFWIDTH);

                    // 完了
                    work = string.Empty;
                }
            }
            return ret;
        }

        ///// <summary>
        ///// 文字列を半角→全角に変換する
        ///// </summary>
        ///// <remarks>ShiftJisにない文字は？に変換される</remarks>
        ///// <param name="str">変換対象文字列</param>
        ///// <returns>全角文字列</returns>
        //[Obsolete("理由が無い限りConvertWideForUnicodeを使用してください。", false)]
        //public static string ConvertWide(string str)
        //{
        //    return Strings.StrConv(str, VbStrConv.Wide);
        //}

        /// <summary>
        /// 文字列を半角→全角に変換する
        /// </summary>
        /// <remarks>ShiftJisにない文字は変換しない</remarks>
        /// <param name="str">変換対象文字列</param>
        /// <returns>全角文字列</returns>
        public static string ConvertWideForUnicode(string str)
        {
#if false
            return StringConvert(str, dwMapFlags.LCMAP_FULLWIDTH);
#else
            // 暫定処理
            string retStr = str;

            ////////////////////////////////////////////
            // ①C++では削除される濁点、半濁点を削除

            // 変換できない濁点を削除候補に追加
            List<int> delIndexList = new List<int>();

            int index = 0;
            //while (-1 < (index = retStr.IndexOf("ﾞ", index))) // これだとなぜか引っかからない
            while (-1 < (index = retStr.IndexOf('ﾞ', index)))
            {
                if (0 != index && NotVSoundKana.Contains(retStr[index - 1].ToString()))
                {
                    // 文字列の先頭ではなく、前の文字が濁点が付けられない場合は、濁点を削除
                    delIndexList.Add(index);
                }
                index++;
            }

            // 変換できない半濁点を削除候補に追加
            index = 0;
            //while (-1 < (index = retStr.IndexOf("ﾟ", index))) // これでも引っかかるが↑と合わせる
            while (-1 < (index = retStr.IndexOf('ﾟ', index)))
            {
                if (0 != index && NotPSoundKana.Contains(retStr[index - 1].ToString()))
                {
                    // 文字列の先頭ではなく、前の文字が半濁点が付けられない場合は、半濁点を削除
                    delIndexList.Add(index);
                }
                index++;
            }

            // 削除候補を削除
            delIndexList.Sort();
            foreach (int delIndex in delIndexList.Reverse<int>())
            {
                retStr = retStr.Remove(delIndex, 1);
            }

            ////////////////////////////////////////////
            // ②半角→全角変換

            // C++ではチルダはチルダのままなので、チルダは変換しないようにする
            string work = retStr;
            retStr = string.Empty;
            while (!string.IsNullOrEmpty(work))
            {
                int findIndex = -1;
                foreach (var notStr in NotUpperStr)
                {
                    int workIndex = work.IndexOf(notStr);
                    if (-1 < workIndex)
                    {
                        if (-1 == findIndex)
                        {
                            // 他の文字で見つかったものが無い場合
                            findIndex = workIndex;
                        }
                        else
                        {
                            // 他の文字で見つかったものがある場合、先頭に近いものを優先する
                            if (workIndex < findIndex)
                            {
                                findIndex = workIndex;
                            }
                        }
                    }
                }

                if (-1 != findIndex)
                {
                    // 変換対象外文字がある場合

                    // 左辺を変換
                    string leftStr = Left(work, findIndex);
                    if (!string.IsNullOrEmpty(leftStr))
                    {
                        retStr += StringConvert(leftStr, dwMapFlags.LCMAP_FULLWIDTH);
                    }

                    // 変換対象外文字を付加
                    retStr += work[findIndex];

                    // 右辺+1以降
                    work = Mid(work, findIndex + 1);
                }
                else
                {
                    // 変換対象外文字が無い場合
                    retStr += StringConvert(work, dwMapFlags.LCMAP_FULLWIDTH);

                    // 完了
                    work = string.Empty;
                }
            }

            ////////////////////////////////////////////
            // ③C++と変換結果が異なるものを再変換
            foreach (var dic in ShiftJisWideDic)
            {
                retStr = retStr.Replace(dic.Key, dic.Value);
            }

            return retStr;
#endif
        }

        /// <summary>
        /// チェック対象文字列が正規表現で指定した文字列のみで構成するか？
        /// </summary>
        /// <param name="input">チェック対象文字列</param>
        /// <param name="pattern">正規表現パターン文字列（定義済み定数を複数指定可能）</param>
        /// <returns></returns>
        private static bool IsMatchAll(string input, string regex_pattern)
        {
            Debug.Assert(!string.IsNullOrEmpty(regex_pattern));
            Debug.Assert(regex_pattern[0] != '[' && regex_pattern[regex_pattern.Length - 1] != ']');

            string pattern = @"^[" + regex_pattern + @"]+$";
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// チェック対象文字列が正規表現で指定した文字列を含むか？
        /// </summary>
        /// <param name="input">チェック対象文字列</param>
        /// <param name="pattern">正規表現パターン文字列（定義済み定数を複数指定可能）</param>
        /// <returns></returns>
        private static bool IsInclude(string input, string regex_pattern)
        {
            Debug.Assert(!string.IsNullOrEmpty(regex_pattern));
            Debug.Assert(regex_pattern[0] != '[' && regex_pattern[regex_pattern.Length - 1] != ']');

            string pattern = @"[" + regex_pattern + @"]";
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 指定文字列が全て半角かどうか判定
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns></returns>
        public static bool IsAllHankaku(string str)
        {
            Encoding sjisStr = Encoding.GetEncoding("Shift_JIS");
            foreach (char c in str)
            {
                string s = c.ToString();
                if (sjisStr.GetByteCount(s) >= 2)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 指定文字列が全て全角かどうか判定
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns></returns>
        public static bool IsAllZenkaku(string str)
        {
            Encoding sjisStr = Encoding.GetEncoding("Shift_JIS");
            foreach (char c in str)
            {
                string s = c.ToString();
                if (sjisStr.GetByteCount(s) <= 1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 指定文字列が全て半角数値かどうか判定
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns></returns>
        public static bool IsAllHankakuNumeral(string str)
        {
            //Encoding utfStr = Encoding.GetEncoding("utf-8");
            Encoding utfStr = Encoding.GetEncoding("Shift_JIS");  
            foreach (char c in str)
            {
                string s = c.ToString();
                if (utfStr.GetByteCount(s) >= 2)
                {
                    return false;
                }
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }            
             return true;
        }

        /// <summary>
        /// 指定文字列が全て全角数値かどうか判定
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns></returns>
        public static bool IsAllZenkakuNumeral(string str)
        {
            Encoding utfStr = Encoding.GetEncoding("Shift_JIS");
            foreach (char c in str)
            {
                string s = c.ToString();
                if (utfStr.GetByteCount(s) <= 1)
                {
                    return false;
                }
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// 全角文字かどうか
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsZenkakuMoji(char c)
        {
            Encoding utfStr = Encoding.GetEncoding("Shift_JIS");
            string s = c.ToString();
            if (utfStr.GetByteCount(s) >= 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 全て半角カタカナ又は半角アルファベットか？
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAllHankakuKatakanaOrHankakuAlphabet(string str)
        {
            return IsMatchAll(str, regex_pattern_alphabet + regex_pattern_katakana);
        }

        /// <summary>
        /// 指定文字列が半角カタカナを含んでいるか判定
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns></returns>
        public static bool IsIncludeHankakuKatakana(string str)
        {
            return IsInclude(str, regex_pattern_katakana);
        }

        // TODO: メソッド名を本クラスの他のメソッドの命名規則に合わせる by KOSHI
        // TODO: IsMatchAll()を呼び出すよう差し替える by KOSHI
        /// <summary>
        /// 文字列が半角英数字のみか
        /// </summary>
        /// <param name="val">指定文字列</param>
        /// <returns></returns>
        public static bool IsAlphanumericSingleByte(this string val)
        {
            if ((Regex.Match(val, "^[a-zA-Z0-9]+$")).Success)
            {
                return true;
            }
            return false;
        }

        /*
        入力不可とする半角記号に含まれない記号がある使用箇所なし・作成目的不明なためいったんコメントアウトする(2015/08/04 越田)
        /// <summary>
        /// 文字列が半角記号のみか
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsHankakuKigo(this string val)
        {
            if ((Regex.Match(val, "^[!-/:-@¥[-`{-~]+$")).Success)
            {
                return true;
            }
            return false;
        }
        */

        /// <summary>
        /// 文字列に使用範囲(記号、英数、かな、第一水準漢字、第二水準漢字)外の文字が存在するか？
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>true:使用不可 false:使用可能</returns>
        public static bool IsExistNonstandardCharacter(this string val)
        {
            var shiftJIS = MultiByteStrings.GetShiftJISEncoding();

            // 全て全角にし、使用範囲外の文字の存在を確認する
            // ※本来は、半角、全角の混在もエラーだが、レセ電の出力ではすべて全角にしてから出力するので、
            //   同じ状態にしてチェックする
            string expand = val.ToZenkaku();

            char[] chars = expand.ToCharArray();

            foreach (char ch in chars)
            {
                // Unicode→Shift_Jis
                byte[] temp = Encoding.Unicode.GetBytes(ch.ToString());
                byte[] sjisTemp = Encoding.Convert(Encoding.Unicode, shiftJIS, temp);

                ushort sjisCode;
                if (1 < sjisTemp.Length)
                {
                    if (BitConverter.IsLittleEndian)
                    {
                        // リトルエンディアンの場合は反転させる
                        Array.Reverse(sjisTemp);
                    }
                    sjisCode = BitConverter.ToUInt16(sjisTemp, 0);
                }
                else
                {
                    sjisCode = sjisTemp[0];
                }

                // 使用可能範囲(0x8140～0x84FD(記号・英数・かな) or 
                //				0x889F～0xEAA2(第一、二水準漢字)
                if ((0x8140 <= sjisCode && sjisCode <= 0x84FD) || (0x889F <= sjisCode && sjisCode <= 0xEAA2))
                {
                    // 使用可能
                }
                else
                {
                    // 使用不可
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 文字列が、複数の文字列群のいずれかと等しいか
        /// </summary>
        /// <param name="self">指定文字列</param>
        /// <param name="values">いずれかの文字列</param>
        /// <returns></returns>
        public static bool IsAny(this string self, params string[] values)
        {
            return values.Any(c => c == self);
        }
        
        /// <summary>
        /// 文字列の先頭から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得文字列</returns>
        public static string Left(string str, int len)
        {  
            if (str.Length <= len)
            {
                return str;
            }
            return str.Substring(0, len);
        }

		/// <summary>
		/// 文字列の先頭から指定したバイト数の文字列を取得する
		/// </summary>
		/// <param name="str">文字列</param>
		/// <param name="len">長さ</param>
		/// <returns>取得文字列</returns>
		/// <remarks>
		/// ※マルチバイト文字対応版は、MultiByteStrings.CutByMultiByteLength()
		/// </remarks>
		public static string ByteLeft(string str, int len)
        {
            return ByteMid(str, 0, len);
        }

        /// <summary>
        /// 文字列の先頭を基準に指定したバイト数にそろえた文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <param name="paddingChar">文字</param>
        /// <returns>取得文字列</returns>
        public static string PadByteLeft(string str, int len, char paddingChar = ' ')
        {
            int byteLength = ByteLength(str);

            if (byteLength >= len)
            {
                // 指定バイト数が文字列バイト数より小さい場合、指定バイト数の文字列を返す
                return ByteRight(str, len);
            }

            string pads = new string(paddingChar, len - byteLength);

            return pads + str;
        }

        /// <summary>
        /// 文字列の末尾から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得文字列</returns>
        public static string Right(string str, int len)
        {
            if (str.Length <= len)
            {
                return str;
            }
            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// 文字列の末尾から指定したバイト長の文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得文字列</returns>
        public static string ByteRight(string str, int len)
        {
            System.Text.Encoding utfStr = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] bytes = utfStr.GetBytes(str);

            if (bytes.Length <= len)
            {
                return str;
            }

            return utfStr.GetString(bytes, bytes.Length - len, len);
        }

        /// <summary>
        /// 文字列の末尾を基準に指定したバイト数にそろえた文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <param name="paddingChar">文字</param>
        /// <returns>取得文字列</returns>
        public static string PadByteRight(string str, int len, char paddingChar = ' ')
        {
            int byteLength = ByteLength(str);
            
            if (byteLength >= len)
            {
                // 指定バイト数が文字列バイト数より小さい場合、指定バイト数の文字列を返す
                return ByteLeft(str, len);
            }

            string pads = new string(paddingChar, len - byteLength);

            return str + pads;
        }

        /// <summary>
        /// 文字列から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <param name="len">長さ</param>
        /// <returns>取得文字列</returns>
        public static string Mid(string str, int start, int len)
        {
            if (str == "")
            {
                return str;
            }

            if (str.Length < (start + len))
            {
                return str.Substring(start);
            }
            return str.Substring(start, len);
        }

        /// <summary>
        /// 文字列から指定した文字位置以降の文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <returns>取得文字列</returns>
        public static string Mid(string str, int start)
        {
            if (str == "")
            {
                return str;
            }
            if (start > str.Length)
            {
                return "";
            }

            return Mid(str, start, str.Length - start);
        }

        /// <summary>
        /// 文字列から指定したバイト長の文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <param name="len">長さ</param>
        /// <returns>取得文字列</returns>
        public static string ByteMid(string str, int start, int len)
        {
            if (str == "")
            {
                return str;
            }

            System.Text.Encoding utfStr = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] bytes = utfStr.GetBytes(str);

            // Ver1.XXXX(2017/02/06)：１文字のみの特殊欄設定の集計でエラーが発生する場合がある(Bug 15422)
            if (start > bytes.Length)
            {
                return "";
            }

            if (bytes.Length < (start + len))
            {
                return utfStr.GetString(bytes, start, bytes.Length - start);
            }

            return utfStr.GetString(bytes, start, len);
        }

        /// <summary>
        /// 文字列から指定したバイト位置以降の文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <returns>取得文字列</returns>
        public static string ByteMid(string str, int start)
        {
            if (str == "")
            {
                return str;
            }

            System.Text.Encoding utfStr = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] bytes = utfStr.GetBytes(str);

            if (start > bytes.Length)
            {
                return "";
            }

            return utfStr.GetString(bytes, start, bytes.Length - start);
        }

        /// <summary>
        /// 文字列のバイト長を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>バイト長</returns>
        public static int ByteLength(string str)
        {
            System.Text.Encoding utfStr = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] bytes = utfStr.GetBytes(str);

            return bytes.Length;
        }

        /// <summary>
        /// カンマを挿入
        /// </summary>
        /// <remarks>ex. 99999 -> 99,999</remarks>
        /// <param name="num">数値</param>
        /// <returns>カンマ入り数値文字列</returns>
        public static string CommaEdit(this int num)
        {
            return String.Format(FORMAT_COMMA, num);
        }

        /// <summary>
        /// カンマを挿入
        /// </summary>
        /// <remarks>ex. 99999 -> 99,999</remarks>
        /// <param name="num">数字文字列</param>
        /// <returns>カンマ入り数値文字列</returns>
        public static string CommaEdit(string numStr)
        {
            int result;
            if (int.TryParse(numStr, out result))
            {
                return String.Format(FORMAT_COMMA, result);
            }
            return string.Empty;
        } 

        /// <summary>
        /// カンマを挿入(小数点以下三桁まで有効)
        /// </summary>
        /// <remarks>引数numにはあらかじめ有効桁数にそろえた、
        /// 小数点以下3桁までの数値のみを入れてください
        /// 小数点以下4桁以上入れた場合の挙動は保証しない
        /// 引数digitは末尾が0の場合に表示桁数を減らさないためのものであり、
        /// 指定桁以下を表示しなくなるものではありません
        /// ex. 99,999
        /// digit = 1 の場合　-> 99,999.0
        /// digit = 2 の場合　-> 99,999.00
        /// digit = 3 の場合　-> 99,999.000
        /// 桁指定なしの場合　-> 99,999
        /// ex. 99,999.9
        /// digit = 1 の場合　-> 99,999.9
        /// digit = 2 の場合　-> 99,999.90
        /// digit = 3 の場合　-> 99,999.900
        /// 桁指定なしの場合　-> 99,999.9
        /// ex. 99,999.999
        /// digit = 1 の場合　-> 99,999.999
        /// digit = 2 の場合　-> 99,999.999
        /// digit = 3 の場合　-> 99,999.999
        /// 桁指定なしの場合　-> 99,999.999
        /// ex. 99,999.9999
        /// digit = 1 の場合　-> 動作保証しない
        /// digit = 2 の場合　-> 動作保証しない
        /// digit = 3 の場合　-> 動作保証しない
        /// 桁指定なしの場合　-> 動作保証しない
        /// </remarks>
        /// <param name="num">数値(小数点以下三桁まで)</param>
        /// <param name="digit">小数点以下の末尾0を、省略せず表示する桁数</param>
        /// <returns>カンマ入り数値文字列</returns>
        public static string CommaEdit(decimal num, int digit = 0)
        {
            string FORMAT_COMMA_DECIMAL;

            switch (digit)
            {
                case 1:
                    FORMAT_COMMA_DECIMAL = "{0:#,0.0##}";
                    break;
                case 2:
                    FORMAT_COMMA_DECIMAL = "{0:#,0.00#}";
                    break;
                case 3:
                    FORMAT_COMMA_DECIMAL = "{0:#,0.000}";
                    break;
                default:
                    FORMAT_COMMA_DECIMAL = "{0:#,0.###}";
                    break;

            }
            return String.Format(FORMAT_COMMA_DECIMAL, num);
        }

        /// <summary>
        /// 半角数字の文字列のみ抽出
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>半角数字の文字列</returns>
        public static string DigitOnlyString(string str)
        {
            // 半角文字列に変換
            string s = ConvertNarrowForUnicode(str);

            // 正規表現による抽出
            Regex re = new Regex(@"[^0-9]");
            string result = re.Replace(s, "");

            return result;
        }
        
        /// <summary>
        /// 文字列を数値(int型)に変換する
        /// 
        /// ※数字以外の文字は除外
        /// </summary>
        /// <remarks>
        /// 標準で用意されている int.Parse() や Convert.ToInt32() は、
        /// 対象文字列に数値以外が入力されていると変換時エラーになるが、
        /// 当メソッドは、変換できない文字の場合には０を返す
        /// </remarks>
        /// <param name="val">文字列</param>
        /// <returns>数値</returns>
        public static int Parse(this string val)
        {
            // 文字列が空
            if (string.IsNullOrWhiteSpace(val))
            {
                return 0;
            }
            val = val.Trim();

            // 先頭の符号を切り出し
            char? sign = null;
            if (val[0] == '+' || val[0] == '-')
            {
                sign = val[0];
                val = StringUtil.Right(val, val.Length - 1);
            }
            // 数字のみ取り出し
            string num = DigitOnlyString(val);
            if(!string.IsNullOrEmpty(num))
            {
                if (null != sign)
                {
                    // 先頭に符号を付加
                    num = sign + num;
                }
                // 数字が取得出来た場合には、数値に変換
                return int.Parse(num);
            }
            return 0;
        }

        /// <summary>
        /// 文字列を数値(int型)に変換する
        /// 
        /// ※先頭の数字のみを変換(C++のatoi関数と同一処理)
        /// </summary>
        /// <remarks>
        /// 標準で用意されている int.Parse() や Convert.ToInt32() は、
        /// 対象文字列に数値以外が入力されていると変換時エラーになるが、
        /// 当メソッドは、変換できない文字の場合には０を返す
        /// </remarks>
        /// <param name="val">文字列</param>
        /// <returns>数値</returns>
        public static int ToInt(this string val)
        {
            // 文字列が空
            if (string.IsNullOrWhiteSpace(val))
            {
                return 0;
            }
            val = val.Trim();

            // 先頭の符号を切り出し
            char? sign = null;
            if (val[0] == '+' || val[0] == '-')
            {
                sign = val[0];
                val = StringUtil.Right(val, val.Length - 1);
            }
            // 先頭の数字のみを切り出し
            string num = string.Empty;
            foreach(char c in val)
            {
                if (char.IsDigit(c))
                {
                    num += c;
                }
                else
                {
                    break;
                }
            }
            // 数字に変換
            if (!string.IsNullOrEmpty(num))
            {
                // 数字が取得出来た場合には、数値に変換

                // 半角文字列に変換
                string s = ConvertNarrowForUnicode(num);
                if (null != sign)
                {
                    // 先頭に符号を付加
                    s = sign + s;
                }
                return int.Parse(s);
            }

            return 0;
        }

        /// <summary>
        /// 文字列を数値(long型)に変換する
        /// 
        /// ※先頭の数字のみを変換(C++のatol関数と同一処理)
        /// </summary>
        /// <remarks>
        /// 標準で用意されている int.Parse() や Convert.ToInt32() は、
        /// 対象文字列に数値以外が入力されていると変換時エラーになるが、
        /// 当メソッドは、変換できない文字の場合には０を返す
        /// </remarks>
        /// <param name="val">文字列</param>
        /// <returns>数値</returns>
        public static long ToLong(this string val)
        {
            // 文字列が空
            if (string.IsNullOrWhiteSpace(val))
            {
                return 0;
            }
            val = val.Trim();

            // 先頭の符号を切り出し
            char? sign = null;
            if (val[0] == '+' || val[0] == '-')
            {
                sign = val[0];
                val = StringUtil.Right(val, val.Length - 1);
            }
            // 先頭の数字のみを切り出し
            string num = string.Empty;
            foreach (char c in val)
            {
                if (char.IsDigit(c))
                {
                    // 先頭に符号を付加
                    num += c;
                }
                else
                {
                    break;
                }
            }
            // 数字に変換
            if (!string.IsNullOrEmpty(num))
            {
                // 数字が取得出来た場合には、数値に変換

                // 半角文字列に変換
                string s = ConvertNarrowForUnicode(num);
                if (null != sign)
                {
                    s = sign + s;
                }
                return long.Parse(s);
            }

            return 0L;
        }

        /// <summary>
        /// 文字列を指定のバイト数で区切り、結果をリストで返す
        /// </summary>
        /// <param name="org">文字列</param>
        /// <param name="separateLen">区切る基準となるバイト数</param>
        /// <returns></returns>
        public static List<string> SplitStringList(this string org, int separateLen)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(org) && 0 < separateLen)
            {
                if (org.GetMultiByteLength() <= separateLen)
                {
                    list.Add(org);
                }
                else if (separateLen < org.GetMultiByteLength())
                {
                    string work = org;

                    while (0 < work.Length)
                    {
                        // separateLen分切り出し
                        string left = work.CutByMultiByteLength(separateLen);
                        int len = left.GetMultiByteLength();
                        list.Add(left);

                        if (left.Length != work.Length)
                        {
                            // workに切った後の残りを入れる
                            work = work.Substring(left.Length);
                        }
                        else
                        {
                            work = string.Empty;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// バイト長を指定して文字列の先頭を切り出す
        /// </summary>
        /// <param name="str">対象となる文字列</param>
        /// <param name="len">バイト長</param>
        /// <returns></returns>
        public static string GetSubstringByByte(string str, int len)
        {
            if (!string.IsNullOrEmpty(str) && 0 < len)
            {
                Encoding utfStr = Encoding.GetEncoding("Shift_JIS");
                byte[] bytes = utfStr.GetBytes(str);
                if (bytes.Length <= len)
                {
                    // 指定サイズ以下の場合そのまま返す
                    return str;
                }
                string s = utfStr.GetString(bytes, 0, len);

                // 最後の文字が多バイト文字の途中で切れていないかをチェック
                int l = utfStr.GetString(bytes, 0, len + 1).Length;
                if (s.Length == l)
                {
                    s = s.Remove(s.Length - 1);
                }
                return s;
            }
            return string.Empty;
        }

        /// <summary>
        /// 文字列を指定のバイト数で区切り、結果をリストで返す
        /// </summary>
        /// <param name="str">対象となる文字列</param>
        /// <param name="len">バイト長</param>
        /// <returns></returns>
        public static List<string> GetListSubstringByByte(string str, int len)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(str) && 0 < len)
            {
                Encoding utfStr = Encoding.GetEncoding("Shift_JIS");
                byte[] bytes = utfStr.GetBytes(str);
                if (bytes.Length <= len)
                {
                    // 指定サイズ以下の場合そのまま返す
                    list.Add(str);
                    return list;
                }
                string work = str;
                while (0 < work.Length)
                {
                    string left = StringUtil.GetSubstringByByte(work, len);
                    list.Add(left);
                    if (left.Length != work.Length)
                    {
                        // workに切った後の残りを入れる
                        work = work.Substring(left.Length);
                    }
                    else
                    {
                        work = string.Empty;
                    }
                }
            }
            return list;
        }
    }
}
