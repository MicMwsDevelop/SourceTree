//
// SplitString.cs
// 
// 文字列とデリミタによる部分文字列リストの変換処理群
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.Common
{
    /// <summary>
    /// 文字列とデリミタによる部分文字列リストの変換処理群
    /// </summary>
    public static class SplitString
    {
        /*
        /// <summary>
        /// カンマ区切りの文字列から文字列リストに変換する
        /// </summary>
        /// <param name="str">変換元の文字列</param>
        /// <returns>文字列リスト</returns>
        public static IEnumerable<string> SplitCSV(this string str)
        {
            List<string> result = new List<string>();

            if (String.IsNullOrEmpty(str) == false)
            {
                // TODO 後でダブルクォーテーションで囲まれたケースの処理にする
                // C++ではCSVSplitLine2に相当する処理

                string[] split = str.Split(new Char[] { ',' });
                foreach (string s in split)
                {
                    result.Add(s.Trim());
                }
            }
            return result;
        }
        */

        /*
        /// <summary>
        /// 文字列リストから、各要素をカンマで連結した文字列に変換する
        /// </summary>
        /// <param name="list">文字列リスト</param>
        /// <returns>CSV文字列</returns>
        public static string ToCSV(this IEnumerable<string> list)
        {
            // TODO 後でダブルクォーテーションで囲む処理を付加する
            // C++ではStringListToCSV2に相当する処理

            
            return string.Join(",", list);
        }
        */

        /// <summary>
        /// 文字列のコレクションからCSV文字列を作成する(内部メソッド)
        /// </summary>
        /// <remarks>
        /// palette C++からの移植なので、データ自体がダブルクォーテーションで始まっている場合と、カンマとダブルクォーテーションの両方が含まれて
        /// いる場合には対応していない。
        /// </remarks>
        /// <param name="fields">文字列コレクション</param>
        /// <returns>作成したCSV文字列</returns>
        private static string ToCSV(this IEnumerable<string> fields)
        {
            // 必要に応じてダブルクォーテーションを付加したコレクションを作成
            var fieldStrings = fields.Select(fieldStr =>
            {
                if (CSVDelimiters.Any(delimiter => fieldStr.Contains(delimiter)))
                {
                    // forceQuotationがtrueの場合、または、フィールドにカンマが含まれている場合
                    // フィールド全体をダブルクォーテーションで括る
                    return string.Format("\"{0}\"", fieldStr);
                }
                else
                {
                    return fieldStr;
                }
            });
            return string.Join(",", fieldStrings);  // カンマ区切りで連結
        }

        // 以下、互換性の為のラッパー
        /// <summary>
        /// カンマ区切りの文字列から文字列リストに変換する
        /// </summary>
        /// <param name="str">変換元の文字列</param>
        /// <returns>文字列リスト</returns>
        public static List<string> CSVSplitLine(string str)
        {
            return str.SplitCSV().ToList();  // この場合、ToList()のコストはO(1)
        }

        /// <summary>
        /// 文字列リストから、各要素をカンマで連結した文字列に変換する
        /// </summary>
        /// <param name="list">文字列リスト</param>
        /// <returns>CSV文字列</returns>
        public static string StringListToCSV(List<string> list)
        {
            return list.ToCSV();
        }


        // CSV文字列からのフィールド切り出し処理

        private static readonly char[] CSVDelimiters = { ',' };
        private static readonly char[] CSVWordEdges = { '\"' };
        private static readonly char[] WhiteSpaces = { ' ', '\t', '\n' }; // char.IsWhiteSpaceだと全角空白も含まれてしまうかもしれない

        /// <summary>
        /// CSV文字列を解析して各フィールドの文字列を列挙する
        /// </summary>
        /// <remarks>
        /// palette C++からの移植なので、元々のデータ自体がダブルクォーテーションで始まっていた場合、
        /// カンマとダブルクォーテーションの両方が含まれていた場合に対処した形式のCSV文字列の解析には対応していない。
        /// palette C++のCSVSplitLineメソッド(2ではない方)だと、最後のフィールドが空だとそのフィールドはなかった事になるが、
        /// このメソッドでは空のフィールドとして追加する。
        /// フィールドの先頭にある空白文字はその前にダブルクォーテーションがついていない限り無視する。
        /// </remarks>
        /// <param name="csv">解析対象のCSV文字列</param>
        /// <param name="ignoreFieldHeadWhiteSpace">フィールド先頭の空白文字を無視するかどうか(省略時の既定値:true)</param>
        /// <returns>解析の結果CSVに含まれていたフィールド文字列の列挙</returns>
        public static IEnumerable<string> SplitCSV(this string csv, bool ignoreFieldHeadWhiteSpace = true)
        {
            // 本当に空文字列の場合は、フィールドが一個もないものとする
            // カンマが一個も含まれない場合に空白文字が１個だけでも入っていたら、フィールドが１個あるものとする。
            if (csv == string.Empty)
            {
                yield break;
            }
            else
            {
                for (int i = 0; i < csv.Length; )
                {
                    if (ignoreFieldHeadWhiteSpace)
                    {
                        // フィールド先頭の空白文字を読み飛ばす処理
                        var headerWhiteSpace = csv.Substring(i).TakeWhile(c => WhiteSpaces.Contains(c));
                        i += headerWhiteSpace.Count();
                        if (i >= csv.Length)
                        {
                            // 終端に達した場合はforループを終了する
                            break;
                        }
                    }

                    if (CSVWordEdges.Contains(csv[i]))
                    {
                        // ダブルクォーテーションあり

                        // 次の文字以降の閉じのダブルクォーテーションを探す
                        int endOfWord = csv.IndexOfAny(CSVWordEdges, i + 1);
                        if (endOfWord == -1)
                        {
                            // 閉じのダブルクォーテーションが見つからなかったので残りの文字列全部を返して列挙完了
                            yield return csv.Substring(i, csv.Length - i);
                            yield break;
                        }
                        else
                        {
                            // ダブルクォーテーションの次の文字から、閉じのダブルクォーテーションの前までを返す
                            yield return csv.Substring(i + 1, endOfWord - (i + 1));

                            // 閉じのダブルクォーテーションまでを読み飛ばす
                            i = endOfWord + 1;

                            // 次のカンマを探す
                            int nextDelimiter = csv.IndexOfAny(CSVDelimiters, i);
                            if (nextDelimiter == -1)
                            {
                                // 次のカンマが見つからないので列挙完了
                                yield break;
                            }
                            else
                            {
                                i = nextDelimiter + 1;  // カンマの次→次のフィールドの先頭
                            }
                        }
                    }
                    else
                    {
                        // ダブルクォーテーションで始まっていないので、次のカンマの前までを読み取り
                        int nextDelimiter = csv.IndexOfAny(CSVDelimiters, i);
                        if (nextDelimiter == -1)
                        {
                            // 次のカンマが見つからなかったので残りの文字列全部を返して列挙完了
                            yield return csv.Substring(i, csv.Length - i);
                            yield break;
                        }
                        else
                        {
                            yield return csv.Substring(i, nextDelimiter - i);
                            i = nextDelimiter + 1;  // カンマの次→次のフィールドの先頭
                        }
                    }
                }
                // ここへ来るのはcsv全体が空ではなく、かつ、最後のフィールドが空か空白文字だけだった場合のみ
                yield return "";
            }
        }


        ////////////////////////////////////////////
        // C++ StringListToCSV2、CSVSplitLine2の移植

        /// <summary>
        /// エスケープ文字変換用のテーブル
        /// </summary>
        private static readonly string[,] CSVEscTable = new string[5, 2]{{ "\"\"", "\""},   // "" -> "
                                                                         { "\\n", "\n"},    // \n -> LF
                                                                         { "\\r", "\r"},    // \r -> CR
                                                                         { "\\\\", "\\"},   // \\ -> \
                                                                         { "\\\"", "\""}};  // \" -> "

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static int WhatsKind(string str)
        {
            if (str == "") return 0;
            for (int i = 0; i < 5; i++)
            {
                string org = CSVEscTable[i, 0];
                int orglen = org.Length;
                if (str.StartsWith(org))
                {
                    return i + 1;
                }
            }
            return 0;
        }


        private static string TakeCharFromEscString(string wordStr)
        {
            string ret = wordStr;
            for (int i = 0; i < 5; i++)
            {
                if (wordStr == CSVEscTable[i, 0])
                {
                    ret = CSVEscTable[i, 1];
                    break;
                }
            }
            return ret;
        }

        private static string TakeCharFromNormalString(string wordStr)
        {
            string ret = wordStr;
            for (int i = 0; i < 5; i++)
            {
                if (wordStr == CSVEscTable[i, 1])
                {
                    ret = CSVEscTable[i, 0];
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 文字列をエスケープ文字列に変換する
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ConvertToEscString(string src)
        {
            string str = src;
            bool isEnclosedDoubleQuotation = false;
            if (str.StartsWith(" ") || str.EndsWith(" "))
            {
                // スペースで始まっているか、スペースで終わっている場合は、Trimされないようにダブルクオーテーションで囲んでしまう。
                isEnclosedDoubleQuotation = true;
            }
            string ret = string.Empty;
            // １文字ずつ操作し、エスケープ文字列への変換を行う
            for (int i = 0; i < src.Length; i++)
            {
                if (!isEnclosedDoubleQuotation && (CSVDelimiters.Contains(src[i]) || CSVWordEdges.Contains(src[i])))
                {
                    // デリミタ（,）またはダブルクオーテーションがあれば、ダブルクオーテーションで囲んでしまう。
                    isEnclosedDoubleQuotation = true;
                }
                ret += TakeCharFromNormalString(src.Substring(i, 1));
            }

            if (isEnclosedDoubleQuotation)
            {
                ret = "\"" + ret + "\"";
            }
            return ret;
        }

        /// <summary>
        /// エスケープ文字列から元の文字列を復元する
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ConvertEscToNormal(string src)
        {
            string ret = string.Empty;
            // １文字ずつ操作し、元の文字列への変換を行う
            for (int i = 0; i < src.Length; i++)
            {
                ret += TakeCharFromEscString(src.Substring(i, 1));
            }
            return ret;
        }

        /// <summary>
        /// 各要素をカンマで連結した文字列から、文字列リストに変換する
        /// </summary>
        /// <param name="src">CSV文字列</param>
        /// <returns>文字列リスト</returns>
        public static List<string> CSVSplitLine2(string src)
        {
            List<string> ret = new List<string>();
            if (!string.IsNullOrEmpty(src))
            {
                string work = src;
                while (!string.IsNullOrEmpty(work))
                {
                    work = work.Trim(StringUtil.DefalutTrimCharSet);
                    string field = string.Empty;
                    if (!string.IsNullOrEmpty(work))
                    {
                        if (CSVWordEdges.Contains(work[0]))
                        {
                            // "で始まっている場合は、次の"までを読み取り、その後のカンマの前までは無視する。
                            field = work.Substring(1);
                            int fieldEndIdx = field.IndexOfAny(CSVWordEdges);
                            if (fieldEndIdx >= 0)
                            {
                                if (fieldEndIdx == 0)
                                {
                                    // ""の場合⇒空文字
                                    field = string.Empty;
                                }
                                else if (fieldEndIdx > 0)
                                {
                                    // "が見つかった
                                    field = field.Substring(0, fieldEndIdx);
                                }

                                // 次のデリミタ（カンマ）まで読み飛ばす
                                int delimiterIdx = work.IndexOfAny(CSVDelimiters, fieldEndIdx + 1);
                                if (delimiterIdx >= 0)
                                {
                                    // デリミタ（カンマ）が見つかった
                                    if (delimiterIdx == work.Count() - 1)
                                    {
                                        // カンマで終わっている場合　→　次のループで空文字を入れるためのダミー
                                        work = " ";
                                    }
                                    else
                                    {
                                        work = work.Substring(delimiterIdx + 1);
                                    }
                                }
                                else
                                {
                                    // 見つからない場合は全て
                                    work = string.Empty;
                                }
                            }
                            else
                            {
                                // 見つからない場合は全て列挙するので終わり
                                work = string.Empty;
                            }
                            field = ConvertEscToNormal(field);
                        }
                        else
                        {
                            // 次のデリミタ（カンマ）まで読み込む
                            int delimiterIdx = work.IndexOfAny(CSVDelimiters);
                            if (delimiterIdx >= 0)
                            {
                                // デリミタ（カンマ）が見つかった
                                field = work.Substring(0, delimiterIdx);
                                if (delimiterIdx == work.Count() - 1)
                                {
                                    // カンマで終わっている場合　→　空文字を入れるためのダミー
                                    work = " ";
                                }
                                else
                                {
                                    work = work.Substring(delimiterIdx + 1);
                                }
                            }
                            else
                            {
                                // 見つからない場合は全て
                                field = work;
                                work = string.Empty;
                            }
                            field = ConvertEscToNormal(field);
                        }
                    }
                    ret.Add(ConvertEscToNormal(field));
                }
            }
            return ret;
        }


        /// <summary>
        /// 文字列リストから、各要素をカンマで連結した文字列に変換する
        /// </summary>
        /// <param name="list">文字列リスト</param>
        /// <returns>CSV文字列</returns>
        public static string StringListToCSV2(List<string> list)
        {
            string ret = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    ret += CSVDelimiters[0];
                }
                ret += ConvertToEscString(list[i]);
            }
            return ret;
        }

        // $パフォーマンス改善$
        /// <summary>
        /// 文字列リストから、各要素をカンマで連結した文字列に変換する
        /// </summary>
        /// <param name="list">文字列リスト</param>
        /// <returns>CSV文字列</returns>
        public static string StringListToCSV2(IEnumerable<string> list)
        {
            StringBuilder result = new StringBuilder();
            bool first = true;
            foreach (var field in list)
            {
                if (!first)
                {
                    result.Append(CSVDelimiters[0]);
                }
                else
                {
                    first = false;
                }
                result.Append(ConvertToEscString(field));
            }
            return result.ToString();
        }
    }
}
