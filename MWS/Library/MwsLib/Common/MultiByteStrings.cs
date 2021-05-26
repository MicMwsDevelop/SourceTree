//
// MultiByteStrings.cs
// 
// マルチバイト(shift_jis)文字列に関する機能のサポート
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Text;

namespace MwsLib.Common
{
	public static class MultiByteStrings
    {
        /// <summary>shift-JISエンコーディング</summary>
        private static Encoding m_ShiftJISEncoding = Encoding.GetEncoding("shift_jis");

        /// <summary>
        /// SHIFT-JISエンコーディングのインスタンスを取得
        /// </summary>
        public static Encoding GetShiftJISEncoding()
        {
            return m_ShiftJISEncoding;
        }

        /// <summary>
        /// マルチバイト(Shift-JIS)文字列長(全角2,半角1で数えた文字長)を取得する
        /// </summary>
        public static int GetMultiByteLength(this string str)
        {
            return GetShiftJISEncoding().GetByteCount(str);
        }

        /// <summary>
        /// 文字列を指定したマルチバイト文字列での長さ以下に切り詰める
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <param name="mbLength">最大マルチバイト文字列長</param>
        /// <returns></returns>
        public static string CutByMultiByteLength(this string str, int mbMax)
        {
            if (str.Length == 0 || mbMax == 0)
            {
                return string.Empty;
            }

            var shiftJIS = GetShiftJISEncoding();
            var bytes = shiftJIS.GetBytes(str.ToCharArray());

            int strlen;
            int mbLen = 0;
            // mbLenがmbMaxを超えない最大のstrlenを取得する
            for (strlen = 0, mbLen = 0;strlen < str.Length; ++strlen)
            {
                // Shift_JISの2バイトコードの空間は、第1バイトが81h-9FhならびにE0h-FCh、
                // 第2バイトが40h-7Ehならびに80h-FChである。
                // wikipedia「Shift_JIS」より(http://ja.wikipedia.org/w/index.php?title=Shift_JIS&oldid=49065856)
                if (0x81 <= bytes[mbLen] && bytes[mbLen] <= 0x9F || 0xE0 <= bytes[mbLen] && bytes[mbLen] <= 0xFC)
                {
                    // 2バイトコードが格納可能か
                    if (mbLen + 2 > mbMax)
                    {
                        // これ以上格納できないので終了
                        break;
                    }
                    else
                    {
                        mbLen += 2;
                    }
                }
                else
                {
                    // 1バイトコードが格納可能か
                    if (mbLen + 1 > mbMax)
                    {
                        // これ以上格納できないので終了
                        break;
                    }
                    else
                    {
                        mbLen += 1;
                    }
                }
            }
            return str.Substring(0, strlen);
        }
    }
}
