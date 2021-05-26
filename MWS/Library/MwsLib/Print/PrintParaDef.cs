//
// PrintParaDef.cs
// 
// パラメタファイル印刷定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
namespace MwsLib.Print
{
	/// <summary>
	/// パラメタファイル定義群
	/// </summary>
	public static class PrintParaDef
    {
        /// <summary>
        /// 印刷種別コマンド
        /// </summary>
        public enum PrintParaType
        {
            /// <summary>実線(%LINE%)</summary>
            Line,
            /// <summary>破線(%DOTLINE%)</summary>
            DotLine,
            /// <summary>楕円(%ELLIPSE%)</summary>
            Ellipse,
            /// <summary>矩形(%FRAME%)</summary>
            Frame,
            /// <summary>角丸矩形(%RNDFRAME%)</summary>
            RoundFrame,
            /// <summary>矩形塗りつぶし(%FILLBOX%)</summary>
            FillBox,
            /// <summary>画像(%PICTURE%)</summary>
            Picture,
            /// <summary>画像(%PICTURE_STRETCH%)</summary>
            PictureStretch,
            /// <summary>文字列</summary>
            String,
            /// <summary>特殊エントリ(<XXXXXX>)</summary>
            Special,
            /// <summary>命令エントリ(@XXXXXXX)</summary>
            Command,
            /// <summary>短形指定塗りつぶし(%FILLCOLORBOX%)</summary>
            FillColorBox,
			/// <summary>角丸短形指定塗りつぶし(%FILLCOLORRNDBOX%)</summary>
			FillColorRoundBox,
		}

		/// <summary>
		/// 先頭ページコマンド
		/// </summary>
		public static readonly string PAGE_FIRST = "PF";

        /// <summary>
        /// 最終ページコマンド
        /// </summary>
        public static readonly string PAGE_LAST = "PL";

        /// <summary>
        /// 実線コマンド
        /// </summary>
        public static readonly string LINE = "%LINE%";

        /// <summary>
        /// 破線コマンド
        /// </summary>
        public static readonly string DOTLINE = "%DOTLINE%";

        /// <summary>
        /// 楕円コマンド
        /// </summary>
        public static readonly string ELLIPSE = "%ELLIPSE%";

        /// <summary>
        /// 矩形コマンド
        /// </summary>
        public static readonly string FRAME = "%FRAME%";

        /// <summary>
        /// 角丸矩形コマンド
        /// </summary>
        public static readonly string RNDFRAME = "%RNDFRAME%";

        /// <summary>
        /// 矩形塗りつぶしコマンド
        /// </summary>
        public static readonly string FILLBOX = "%FILLBOX%";

        /// <summary>
        /// 画像コマンド
        /// </summary>
        public static readonly string PICTURE = "%PICTURE%";

        /// <summary>
        /// 画像ストレッチコマンド
        /// </summary>
        public static readonly string PICTURE_STRETCH = "%PICTURE_STRETCH%";

        /// <summary>短形指定コマンド</summary>
        public static readonly string FILLCOLORBOX = "%FILLCOLORBOX%";

		/// <summary>角丸短形指定塗りつぶしコマンド</summary>
		public static readonly string FILLCOLORRNDBOX = "%FILLCOLORRNDBOX%";
	}
}
