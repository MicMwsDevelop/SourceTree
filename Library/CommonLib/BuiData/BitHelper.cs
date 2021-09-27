//
// class BitHelper -- ビット演算メソッドの定義
// 
// Copyright (C) MIC All Rights Reserved.
// 

namespace CommonLib.BuiData
{
	/// <summary>
	/// ビット演算メソッドの定義
	/// </summary>
	public static class BitHelper
    {
        /// <summary>
        /// 指定した値(32bitまでの整数)のビットの数を数える
        /// </summary>
        /// <remarks>
        /// 処理高速化の為下記のサイトを参考に作成した。
        /// http://www.nminoru.jp/~nminoru/programming/bitcount.html
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BitCount(this uint bits)
        {
            bits = (bits & 0x55555555u) + (bits >> 1 & 0x55555555u);
            bits = (bits & 0x33333333u) + (bits >> 2 & 0x33333333u);
            bits = (bits & 0x0f0f0f0fu) + (bits >> 4 & 0x0f0f0f0fu);
            bits = (bits & 0x00ff00ffu) + (bits >> 8 & 0x00ff00ffu);
            return (int)((bits & 0x0000ffffu) + (bits >>16 & 0x0000ffffu));
        }

        /// <summary>
        /// 指定した値(64bit)のビットの数を数える
        /// </summary>
        /// <remarks>
        /// 処理高速化の為下記のサイトを参考に作成した。
        /// http://www.nminoru.jp/~nminoru/programming/bitcount.html
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BitCount(this ulong bits)
        {
            return (int)((uint)bits).BitCount() + (int)(((uint)(bits >> 32)).BitCount());
        }
    }
}
