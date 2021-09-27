//
// PrintOption.cs
// 
// パラメタファイル印刷オプションクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using CommonLib.Common;
using System;
using System.Drawing;
using System.Linq;

namespace CommonLib.Print
{
	/// <summary>
	/// 印刷オプションクラス
	/// </summary>
	[Serializable]
    public class PrintOption : IEquatable<PrintOption>
    {
        /// <summary>
        /// 座標
        /// </summary>
        public Rectangle Rect { get; set; }

        /// <summary>
        /// 座標（印字精度をあげる際に利用する）
        /// </summary>
        private RectangleF rectF;

        public RectangleF RectF
        {
            get
            {
                return !rectF.IsEmpty ? rectF : Rect;
            }
            set
            {
                rectF = value;
                Rect = new Rectangle((int)RectF.X, (int)RectF.Y, (int)RectF.Width, (int)RectF.Height);
            }
        }

        /// <summary>
        /// フォント名称 'G':ＭＳ ゴシック, 'M':ＭＳ 明朝
        /// </summary>
        public string Fontname { get; set; }

        /// <summary>
        /// 縦書き '@'
        /// </summary>
        public bool IsDirection { get; set; }

        /// <summary>
        /// 斜体 'S'
        /// </summary>
        public bool IsItalic { get; set; }

        /// <summary>
        /// 線幅	mm単位, 塗りつぶし 色, 文字列 箱数
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// フォーマット 'L':DT_LEFT, 'C':DT_CENTER, 'R':DT_RIGHT, 'P':均等割付
        /// </summary>
        public char Format { get; set; }

        /// <summary>
        /// 印字文字列の前に付加する文字列
        /// </summary>
        public string PlusString { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrintOption()
        {
            Empty();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type">印刷種別コマンド</param>
        /// <param name="data">印刷定義文字列</param>
        public PrintOption(PrintParaDef.PrintParaType type, string data)
            : this()
        {
            // コマンド除外
            if (type == PrintParaDef.PrintParaType.Command)
            {
                return;
            }

            string[] split = data.Split(',');
            if (4 <= split.Count())
            {
                Point point = new Point(split[0].ToInt(), split[1].ToInt());
                Size size = new Size(split[2].ToInt(), split[3].ToInt());
                this.Rect = new Rectangle(point, size);
                if (5 <= split.Count())
                {
                    if (PrintParaDef.PrintParaType.Line == type || PrintParaDef.PrintParaType.DotLine == type
                        || PrintParaDef.PrintParaType.Ellipse == type || PrintParaDef.PrintParaType.Frame == type 
                        || PrintParaDef.PrintParaType.RoundFrame == type || PrintParaDef.PrintParaType.FillBox == type)
                    {
                        // 実線、破線、楕円、矩形、角丸矩形、矩形
                        short val = (short)split[4].ToInt();
                        if (0 < val)
                        {
                            // 数値(文字桁数または線幅指定あり)
                            this.Width = val;
                        }
                    }
                    else if (PrintParaDef.PrintParaType.String == type || PrintParaDef.PrintParaType.Special == type)
                    {
                        // 文字列印字
                        foreach (char p in split[4])
                        {
                            if ('@' == p)
                            {
                                // 縦書き
                                this.IsDirection = true;
                            }
                            else if ('S' == p)
                            {
                                // 斜体
                                this.IsItalic = true;
                            }
                            else if ('G' == p)
                            {
                                // ＭＳ ゴシック指定
                                this.Fontname = "ＭＳ ゴシック";
                            }
                            else if ('M' == p)
                            {
                                // ＭＳ 明朝指定
                                this.Fontname = "ＭＳ 明朝";
                            }
                            else if ('O' == p)
                            {
                                // Quria OCR-B
                                this.Fontname = "Quria OCR-B";
                            }
                            else if (Char.IsDigit(p))
                            {
                                // 数値(文字桁数または線幅指定あり)
                                this.Width = (short)(this.Width * 10 + Convert.ToString(p).ToInt());
                            }
                            else
                            {
                                // フォーマット
                                this.Format = p;
                            }
                        }
                    }
                    if (6 <= split.Count())
                    {
                        // 付加文字列
                        this.PlusString = split[5];
                    }
                }
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Empty()
        {
            this.Rect = Rectangle.Empty;
            this.rectF = RectangleF.Empty;
            this.Fontname = string.Empty;
            this.IsDirection = false;
            this.IsItalic = false;
            this.Width = 0;
            this.Format = '\0';
            this.PlusString = string.Empty;
        }

        /// <summary>
        /// 同一かどうかを判断する
        /// </summary>
        /// <param name="other">比較するPrintOption</param>
        /// <returns>判定</returns>
        public bool Equals(PrintOption other)
        {
            if (other != null)
            {
                if (this.Rect == other.Rect
                    && this.Fontname == other.Fontname
                    && this.IsDirection == other.IsDirection
                    && this.IsItalic == other.IsItalic
                    && this.Width == other.Width
                    && this.Format == other.Format
                    && this.PlusString == other.PlusString)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
        /// (Object.Equals(Object)をオーバーライドする)
        /// </summary>
        /// <param name="obj">比較するPrintOptionオブジェクト</param>
        /// <returns>判定</returns>
        public override bool Equals(object obj)
        {
            if (obj is PrintOption)
            {
                return this.Equals((PrintOption)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// ハッシュコードを返す
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            int code = Rect.GetHashCode() ^ Fontname.GetHashCode() ^ IsDirection.GetHashCode()
                        ^ IsItalic.GetHashCode() ^ Width.GetHashCode() ^ Format.GetHashCode()
                        ^ PlusString.GetHashCode();
            return code;
            //return ToString().GetHashCode();
        }

        /// <summary>
        /// 0.1mmから1/100インチに変換(int)
        /// </summary>
        /// <param name="x">mm</param>
        /// <returns>1/100インチ</returns>
        public static int Inch(int x)
        {
            return (int)(x * 0.3937F);
        }

        /// <summary>
        /// 0.1mmから1/100インチに変換(float)
        /// </summary>
        /// <param name="x">mm</param>
        /// <returns>1/100インチ</returns>
        public static float Inch(float x)
        {
            return (x * 0.3937F);
        }
    }
}
