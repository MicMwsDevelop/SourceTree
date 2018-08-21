//
// CharConversion.cs
// 
// 文字変換処理定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MwsLib.Common
{
    /// <summary>
    /// 文字変換種別の定義
    /// </summary>
    public enum CharConversionType
    {
        None,
        /// <summary>
        /// 半角→全角変換
        /// </summary>
        ToZenkaku,

        /// <summary>
        /// 全角→半角変換
        /// </summary>
        ToHankaku,

        /// <summary>
        /// 半角カナ→ローマ字変換
        /// </summary>
        HankakuKanaToRomaji,
    }

    public interface ICharConversionProvider
    {
        /*
        /// <summary>
        /// 指定文字列内の指定位置の文字が変換対象の文字かどうか
        /// </summary>
        /// <param name="source">変換対象文字列</param>
        /// <param name="index">変換対象位置</param>
        static bool IsMatch(string source, int index);

        /// <summary>
        /// 指定文字列内の指定位置の文字の変換を試みる
        /// </summary>
        /// <param name="source">変換対象文字列</param>
        /// <param name="index">変換対象位置</param>
        /// <param name="target">変換出来た場合、変換元の文字(単一または複数文字)を格納</param>
        /// <param name="converted">変換出来た場合、変換後の文字(単一または複数文字)を格納</param>
        /// <returns>変換に成功したかどうか</returns>
        /// <remarks>
        /// 変換元も変換後も単一文字の場合、複数文字の場合があるので注意
        ///   "ヴ"→"ｳﾞ", "ｳﾞ"→"ヴ" ※全角・半角変換
        ///   "りゃ"→"rya" ※ローマ字変換
        /// </remarks>
        static bool TryConvert(string source, int index, out string target, out string converted);
        */
    }

    /// <summary>
    /// リフレクション処理の為に文字変換クラスである事を宣言するためのカスタムアトリビュート
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CharConversionProviderAttribute : Attribute
    {
        public CharConversionType Type { get; private set; }
        public CharConversionProviderAttribute(CharConversionType type)
        {
            Type = type;
        }
    }

    public static class CharConversion
    {
        internal static Type[] AllProviders = null;

        internal static IEnumerable<Type> GetAllProviders()
        {
            if (AllProviders == null)
            {
                var asm = Assembly.GetExecutingAssembly();
                var query = from x in asm.GetTypes()
                            where x.IsDefined(typeof(CharConversionProviderAttribute), false)
                            select x;
                AllProviders = query.ToArray();
            }
            return AllProviders;
        }


        private static string DoConvert(CharConversionType conversionType, string source)
        {
            // 全ての全角変換クラス
            var ToZenkakuProviders = from x in GetAllProviders() 
                                        where (
                                                x.GetCustomAttributes(typeof(CharConversionProviderAttribute), true)
                                                .Any(y => (y as CharConversionProviderAttribute).Type == conversionType)
                                              )
                                        select x;

            // public bool TryConvert(string source, int index, out string target, out string converted)
            var methods = ToZenkakuProviders.Select(x => x.GetMethod("TryConvert", new Type[] { typeof(string), typeof(int), typeof(string).MakeByRefType(), typeof(string).MakeByRefType() })).ToArray();

            var result = new StringBuilder();
            for (int i = 0; i < source.Length;)
            {
                string org = null, converted = null;

                if (methods.Any(x =>
                                    {
                                        var args = new object[] { source, i, string.Empty, string.Empty };
                                        if ((bool)x.Invoke(null, args))
                                        {
                                            org = args[2] as string;
                                            converted = args[3] as string;
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                              )
                )
                {
                    result.Append(converted);
                    i += org.Length;
                    continue;
                }
                else if (char.IsSurrogatePair(source, i))
                {
                    result.Append(source.Substring(i, 2));
                    i += 2;
                }
                else
                {
                    result.Append(source[i]);
                    ++i;
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// 全角変換
        /// </summary>
        /// <remarks>
        /// 文字列中の全角変換可能な文字を全て全角に変換する
        /// </remarks>
        public static string ToZenkaku(this string source)
        {
            return DoConvert(CharConversionType.ToZenkaku, source);
        }

        /// <summary>
        /// 全角変換(変換未作成の為呼ぶ事は可能だが変換は行われない)
        /// </summary>
        /// <remarks>
        /// 文字列中の半角変換可能な文字を全て半角に変換する
        /// </remarks>
        public static string ToHankaku(this string source)
        {
            // プロバイダクラス未作成なので機能しない
             return DoConvert(CharConversionType.ToHankaku, source);
        }

        /// <summary>
        /// ローマ字変換(変換未作成の為呼ぶ事は可能だが変換は行われない)
        /// </summary>
        /// <remarks>
        /// 文字列中のローマ字変換可能な文字を全てローマ字に変換する
        /// </remarks>
        public static string ToRomaji(this string source)
        {
            // プロバイダクラス未作成なので機能しない
            return DoConvert(CharConversionType.HankakuKanaToRomaji, source);
        }
    }
}
