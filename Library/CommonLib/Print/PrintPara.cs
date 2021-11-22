//
// PrintPara.cs
// 
// パラメタファイル印刷クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using CommonLib.Common;
using Microsoft.International.JapaneseTextAlignment;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CommonLib.Print
{
	/// <summary>
	/// PrintPara 印刷パラメータクラス
	/// </summary>
	[Serializable]
    public class PrintPara : IEquatable<PrintPara>
    {
        /// <summary>
        /// デフォルトフォント名称
        /// </summary>
        public string Fontname { get; set; }

        /// <summary>
        /// オリジナル文字列
        /// </summary>
        public string Original { get; set; }

        /// <summary>
        /// エントリ
        /// </summary>
        public string Entry { get; set; }

        /// <summary>
        /// データ
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 印刷オプション
        /// </summary>
        public PrintOption Option { get; set; }

        /// <summary>
        /// C++互換 - 順次移行予定
        /// </summary>
        private bool Interchange { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="interchange">C++互換</param>
        public PrintPara(bool interchange = false)
        {
            Empty();
            this.Interchange = interchange;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="other"></param>
        public PrintPara(PrintPara other)
        {
            this.Fontname = other.Fontname;
            this.Original = other.Original;
            this.Entry = other.Entry;
            this.Data = other.Data;
            this.Option = other.Option.CloneDeep();
            this.Interchange = other.Interchange;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="font">フォント</param>
        /// <param name="original">印刷定義文字列</param>
        /// <param name="entry">エントリ</param>
        /// <param name="data">データ</param>
        public PrintPara(string font, string original, string entry, string data, bool interchange = false)
        {
            this.Fontname = font;
            this.Original = original;
            this.Entry = entry;
            this.Data = data;
            this.Option = new PrintOption(this.GetPrintParaType(), data);
            this.Interchange = interchange;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Empty()
        {
            this.Fontname = string.Empty;
            this.Original = string.Empty;
            this.Entry = string.Empty;
            this.Data = string.Empty;
            this.Option = new PrintOption();
            //this.Interchange = false;
        }

        /// <summary>
        /// 同一かどうかを判断する
        /// </summary>
        /// <param name="other">比較するPrintPara</param>
        /// <returns>判定</returns>
        public bool Equals(PrintPara other)
        {
            if (other != null)
            {
                if (this.Fontname == other.Fontname
                    && this.Original == other.Original
                    && this.Entry == other.Entry
                    && this.Data == other.Data
                    && this.Option.Equals(other.Option)
                    && this.Interchange == other.Interchange)
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
        /// <param name="obj">比較するPrintParaオブジェクト</param>
        /// <returns>判定</returns>
        public override bool Equals(object obj)
        {
            if (obj is PrintPara)
            {
                return this.Equals((PrintPara)obj);
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
            int code = Fontname.GetHashCode() ^ Original.GetHashCode() ^ Entry.GetHashCode()
                        ^ Data.GetHashCode() ^ Option.GetHashCode() ^ Interchange.GetHashCode();
            return code;

            //return ToString().GetHashCode();
        }

        /// <summary>
        /// 0.1mmから1/100インチに変換(int)
        /// </summary>
        /// <param name="x">mm</param>
        /// <returns>1/100インチ</returns>
        public static int ToInch(int value)
        {
            return (int)(value * 0.3937F);
        }

        /// <summary>
        /// 0.1mmから1/100インチに変換(float)
        /// </summary>
        /// <param name="x">mm</param>
        /// <returns>1/100インチ</returns>
        public static float ToInch(float value)
        {
            return value * 0.3937F;
        }

        /// <summary>
        /// 1/100インチから0.1mmに変換(float)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToMillimeter(float value)
        {
            return (value * 0.254F) * 10;
        }

        /// <summary>
        /// C++互換 Twips前提の値を1/100に変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float TwipsToInch(float value)
        {
            //return ToInch(value / 1440);
            return value / 14.4F;
        }

		/// <summary>
		/// 文字列から数字のみ抽出
		/// </summary>
		/// <param name="str">文字列</param>
		/// <returns>数字</returns>
		public static int ExtractionNumeral(string str)
		{
			string strDecimal = Regex.Replace(str, @"[^0-9]", "");
			return int.Parse(strDecimal);
		}

		/// <summary>
		/// カンマ数字文字列の取得（\付き）
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string CommaEditString(string str)
		{
			if (0 < str.Length)
			{
				return @"\" + StringUtil.CommaEdit(str);
			}
			return string.Empty;
		}

		/// <summary>
		/// 0.1mmから1/100インチに変換
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public static Rectangle ToInchRect(Rectangle rect)
        {
            return new Rectangle(ToInch(rect.Left), ToInch(rect.Top), ToInch(rect.Width), ToInch(rect.Height));
        }

        /// <summary>
        /// 0.1mmから1/100インチに変換
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static RectangleF ToInchRect(RectangleF rect)
        {
            return new RectangleF(ToInch(rect.Left), ToInch(rect.Top), ToInch(rect.Width), ToInch(rect.Height));
        }

        /// <summary>
        /// 均等割付用の矩形を算出する
        /// 箱指定 かつ 桁数変動のある場合に利用（横書きの数字のみ）
        /// </summary>
        /// <param name="rc">成形元の矩形</param>
        /// <param name="str"></param>
        /// <param name="padRight">true=右詰</param>
        /// <returns></returns>
        //public Rectangle GetJapaneseStringRect(Rectangle rc, string str, bool padRight = false)
        //{
            //var rect = rc;
            //if (this.Option.Width <= 0 || string.IsNullOrEmpty(str) || rc.Equals(Rectangle.Empty))
            //{
            //    return rect;
            //}

            //// 矩形算出
            //float oneBox = rect.Width / Option.Width;
            //if (str.Length <= Option.Width)
            //{
            //    int space = (int)(oneBox * (Option.Width - str.Length));
            //    rect.Width -= space;

            //    float width = 0;
            //    if (8 <= str.Length)
            //    {
            //        width = (float)rect.Width * 0.95F;    // 0.95F = 調整用の独自の補正値
            //    }
            //    else if (6 <= str.Length)
            //    {
            //        width = (float)rect.Width * 0.93F;
            //    }
            //    else
            //    {
            //        width = (float)rect.Width * 0.90F;
            //    }

            //    if (padRight)
            //    {
            //        // 右詰
            //        //float margin = ((float)rect.Width - width) * 0.3F; // 0.3F = 調整用の独自の補正値
            //        //rect.X += (space + (int)margin);
            //        rect.X += space;
            //    }
            //    rect.Width = (int)width;
            //}
            //return rect;
        //}

        /// <summary>
        /// 先頭ページかどうか？
        /// </summary>
        /// <param name="entry">印刷定義文字列エントリ</param>
        /// <returns>判定</returns>
        private bool IsPageFirst(string entry)
        {
            Debug.Assert(2 <= entry.Length);
            if (PrintParaDef.PAGE_FIRST == entry.Substring(0, 2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 最終ページかどうか？
        /// </summary>
        /// <param name="entry">印刷定義文字列エントリ</param>
        /// <returns>判定</returns>
        private bool IsPageLast(string entry)
        {
            Debug.Assert(2 <= entry.Length);
            if (PrintParaDef.PAGE_LAST == entry.Substring(0, 2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 印刷エントリーかどうか？
        /// </summary>
        /// <param name="entry">印刷定義文字列エントリ</param>
        /// <returns>判定</returns>
        public bool IsPrintEntry(string entry)
        {
            Debug.Assert(1 <= entry.Length);
            if ('%' == entry[0] && "%" == entry.Substring(entry.Length - 1))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 特殊エントリーかどうか？
        /// </summary>
        /// <param name="entry">印刷定義文字列エントリ</param>
        /// <returns>判定</returns>
        public bool IsSpecialEntry(string entry)
        {
            Debug.Assert(1 <= entry.Length);
            if ('<' == entry[0] && ">" == entry.Substring(entry.Length - 1))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 命令エントリーかどうか？
        /// </summary>
        /// <param name="entry">印刷定義文字列エントリ</param>
        /// <returns>判定</returns>
        public bool IsCommandEntry(string entry)
        {
            Debug.Assert(1 <= entry.Length);
            if ('@' == entry[0] && "@" == entry.Substring(entry.Length - 1))
            {
                return true;
            }
            return false;
        }

		/// <summary>
		/// 印刷種別コマンドを取得
		/// </summary>
		/// <returns>印刷種別コマンド</returns>
		public PrintParaDef.PrintParaType GetPrintParaType(string entryStr)
		{
			if (1 < entryStr.Length)
			{
				if (this.IsPageFirst(entryStr) || this.IsPageLast(entryStr))
				{
					entryStr = entryStr.Substring(2);
				}
			}
			if (this.IsPrintEntry(entryStr))
			{
				if (PrintParaDef.LINE == entryStr)
				{
					// 実線
					return PrintParaDef.PrintParaType.Line;
				}
				if (PrintParaDef.DOTLINE == entryStr)
				{
					// 破線
					return PrintParaDef.PrintParaType.DotLine;
				}
				if (PrintParaDef.ELLIPSE == entryStr)
				{
					// 楕円
					return PrintParaDef.PrintParaType.Ellipse;
				}
				if (PrintParaDef.FRAME == entryStr)
				{
					// 矩形
					return PrintParaDef.PrintParaType.Frame;
				}
				if (PrintParaDef.RNDFRAME == entryStr)
				{
					// 角丸矩形
					return PrintParaDef.PrintParaType.RoundFrame;
				}
				if (PrintParaDef.FILLBOX == entryStr)
				{
					// 矩形塗りつぶし
					return PrintParaDef.PrintParaType.FillBox;
				}
				if (PrintParaDef.FILLCOLORBOX == entryStr)
				{
					// 短形指定塗りつぶし
					return PrintParaDef.PrintParaType.FillColorBox;
				}
				if (PrintParaDef.FILLCOLORRNDBOX == entryStr)
				{
					// 短形指定塗りつぶし
					return PrintParaDef.PrintParaType.FillColorRoundBox;
				}
				if (PrintParaDef.PICTURE == entryStr)
				{
					// 画像
					return PrintParaDef.PrintParaType.Picture;
				}
				if (PrintParaDef.PICTURE_STRETCH == entryStr)
				{
					// 画像(ストレッチ)
					return PrintParaDef.PrintParaType.PictureStretch;
				}
			}
			else if (this.IsSpecialEntry(entryStr))
			{
				// 特殊エントリ
				return PrintParaDef.PrintParaType.Special;
			}
			else if (this.IsCommandEntry(entryStr))
			{
				// 命令エントリ
				return PrintParaDef.PrintParaType.Command;
			}
			// 印字文字列
			return PrintParaDef.PrintParaType.String;
		}

		/// <summary>
		/// 印刷種別コマンドを取得
		/// </summary>
		/// <returns>印刷種別コマンド</returns>
		public PrintParaDef.PrintParaType GetPrintParaType()
        {
			return GetPrintParaType(Entry);
        }

        /// <summary>
        /// 実線印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="foreColor">前景色</param>
        public void PrintLine(Graphics g, Point offset, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            Pen pen = null;
            if (!this.Interchange)
            {
                pen = new Pen(foreColor.Value, ToInch(this.Option.Width));
            }
            else
            {
                // C++互換
                float width = (this.Option.Width * 0.018F); 
                width *= 10;    // mm → 0.1㎜
                pen = new Pen(foreColor.Value, ToInch(width));

                // 始点・終点のキャップスタイル
                pen.StartCap = pen.EndCap = LineCap.Square;
            }
            RectangleF rect = Option.Rect;
            rect.Offset(offset);

            rect = ToInchRect(rect);
            g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Bottom);

            pen.Dispose();
        }

        /// <summary>
        /// 破線印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="foreColor">前景色</param>
        public void PrintDotLine(Graphics g, Point offset, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            Pen pen = null;
            if (!this.Interchange)
            {
                pen = new Pen(foreColor.Value, ToInch(this.Option.Width));
                pen.DashStyle = DashStyle.Dot;
            }
            else
            {
                // C++互換
                //float width = (this.Option.Width * 0.018F);
                //width *= 10;    // mm → 0.1㎜
                //pen = new Pen(foreColor.Value, ToInch(width));
                //pen.DashPattern = new float[] { 5F, 5F };

                //float width = this.Option.Width / 10F;
                //width = width * 0.10F;   // 0.10F = C++のペン幅１に対する相対値
                //pen = new Pen(foreColor.Value, width);
                pen = new Pen(foreColor.Value, 0.2F);

                // 始点・終点のキャップスタイル
                pen.StartCap = pen.EndCap = LineCap.Square;
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = new float[] { 8F, 8F };
            }

            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);

            rect = ToInchRect(rect);
            g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Bottom);
            pen.Dispose();
        }

        /// <summary>
        /// 十字線印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="foreColor">前景色</param>
        public void PrintCrossLine(Graphics g, Point offset, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            Pen pen = new Pen(foreColor.Value, ToInch(this.Option.Width));
            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);

            rect = ToInchRect(rect);
            g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Bottom);
            g.DrawLine(pen, rect.Right, rect.Top, rect.Left, rect.Bottom);
            pen.Dispose();
        }

        /// <summary>
        /// 楕円印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="foreColor">前景色</param>
        public void PrintEllipse(Graphics g, Point offset, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            Pen pen = null;
            // 	VISTAに正式に対応していないプリンタドライバでは、円の描画で線が細いと印字されない
            float lineWidth = ToInch(this.Option.Width);
            if (0 == this.Option.Width)
            {
                // パラメタファイルで線幅を設定していない
                //  lineWidth = ToInch(1);元々、左記のコードで、線幅が0で描画されていた。
                //  はっきりとした幅(0.7)で描画するために2.0指定に変更した。
                lineWidth = ToInch(2.0f);
                pen = new Pen(foreColor.Value, lineWidth);
            }
            else
            {
                if (!this.Interchange)
                {
                    pen = new Pen(foreColor.Value, lineWidth);
                }
                else
                {
                    lineWidth = (this.Option.Width * 0.018F);
                    lineWidth *= 10;    // 0.1㎜

                    pen = new Pen(foreColor.Value, ToInch(lineWidth));
                    // 始点・終点のキャップスタイル
                    pen.StartCap = pen.EndCap = LineCap.Square;
                }
            }

            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);

            rect = ToInchRect(rect);
            g.DrawEllipse(pen, rect.Left, rect.Top, rect.Width, rect.Height);
            pen.Dispose();
        }

        /// <summary>
        /// 矩形印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="foreColor">前景色</param>
        public void PrintFrame(Graphics g, Point offset, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            Pen pen = null;
            if (!this.Interchange)
            {
                pen = new Pen(foreColor.Value, ToInch(this.Option.Width));
            }
            else
            {
                // C++互換
                float width = (this.Option.Width * 0.018F);
                width *= 10;    // mm → 0.1㎜
                pen = new Pen(foreColor.Value, ToInch(width));

                // 始点・終点のキャップスタイル
                pen.StartCap = pen.EndCap = LineCap.Square;
            }

            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);

            rect = ToInchRect(rect);
            g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
            pen.Dispose();
        }

        /// <summary>
        /// 角丸矩形印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="round">角丸</param>
        /// <param name="foreColor">前景色</param>
        public void PrintRoundFrame(Graphics g, Point offset, PointF round, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);
            rect = ToInchRect(rect);

            float r = ToInch(round.X);
            float a = (float)(4 * (1.41421356 - 1) / 3 * r);
            float x = (float)rect.Left;
            float y = (float)rect.Top;
            float w = (float)rect.Width;
            float h = (float)rect.Height;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddBezier(x, y + r, x, y + r - a, x + r - a, y, x + r, y); // 左上
            path.AddBezier(x + w - r, y, x + w - r + a, y, x + w, y + r - a, x + w, y + r); // 右上
            path.AddBezier(x + w, y + h - r, x + w, y + h - r + a, x + w - r + a, y + h, x + w - r, y + h); // 右下
            path.AddBezier(x + r, y + h, x + r - a, y + h, x, y + h - r + a, x, y + h - r); // 左下
            path.CloseFigure();

            Pen pen = null;
            if (!this.Interchange)
            {
                pen = new Pen(foreColor.Value, ToInch(this.Option.Width));
            }
            else
            {
                // C++互換
                float width = (this.Option.Width * 0.018F);
                width *= 10;    // mm → 0.1㎜
                pen = new Pen(foreColor.Value, ToInch(width));

                // 始点・終点のキャップスタイル
                pen.StartCap = pen.EndCap = LineCap.Square;
            }

            g.DrawPath(pen, path);
            pen.Dispose();
        }

        /// <summary>
        /// 矩形塗りつぶし印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        public void PrintFillBox(Graphics g, Point offset)
        {
            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);

            rect = ToInchRect(rect);
            g.FillRectangle(Brushes.Black, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// 短形指定塗りつぶし印刷
        /// </summary>
        /// <param name="g"></param>
        /// <param name="offset"></param>
        public void PrintFillColorBox(Graphics g, Point offset)
        {
            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);
			rect = ToInchRect(rect);

			Color color = Color.White;
			if (Color.Black != Option.ForeColor)
			{
				// 指定色の生成
				color = Option.ForeColor;
			}
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
            }
        }

		/// <summary>
		/// 角丸短形指定塗りつぶし印刷
		/// </summary>
		/// <param name="g">Graphics クラス</param>
		/// <param name="offset">印刷オフセット</param>
		/// <param name="round">角丸</param>
		/// <param name="foreColor">前景色</param>
		public void PrintFillColorRoundFrame(Graphics g, Point offset, PointF round, Color? foreColor = null)
		{
			foreColor = foreColor ?? Option.ForeColor;

			RectangleF rect = this.Option.Rect;
			rect.Offset(offset);
			rect = ToInchRect(rect);

			float r = ToInch(round.X);
			float a = (float)(4 * (1.41421356 - 1) / 3 * r);
			float x = (float)rect.Left;
			float y = (float)rect.Top;
			float w = (float)rect.Width;
			float h = (float)rect.Height;

			GraphicsPath path = new GraphicsPath();
			path.StartFigure();
			path.AddBezier(x, y + r, x, y + r - a, x + r - a, y, x + r, y); // 左上
			path.AddBezier(x + w - r, y, x + w - r + a, y, x + w, y + r - a, x + w, y + r); // 右上
			path.AddBezier(x + w, y + h - r, x + w, y + h - r + a, x + w - r + a, y + h, x + w - r, y + h); // 右下
			path.AddBezier(x + r, y + h, x + r - a, y + h, x, y + h - r + a, x, y + h - r); // 左下
			path.CloseFigure();

			Color color = Color.White;
			if (Color.Black != Option.ForeColor)
			{
				// 指定色の生成
				color = Option.ForeColor;
			}
			SolidBrush brush = new SolidBrush(color);
			g.FillPath(brush, path);
			brush.Dispose();
		}

		/// <summary>
		///  画像印刷
		///  ストレッチ処理は行いません。画像サイズが大きい場合に、印刷領域をはみ出した部分は印字されません
		/// </summary>
		/// <param name="g">Graphics クラス</param>
		/// <param name="offset">印刷オフセット</param>
		/// <param name="filename">画像ファイル名称</param>
		public void PrintPicture(Graphics g, Point offset, string filename)
        {
            RectangleF destRect = this.Option.Rect;
            destRect.Offset(offset);
            destRect = ToInchRect(destRect);

            Image img = Image.FromFile(@filename);
            RectangleF srcRect = new RectangleF(0, 0, destRect.Width, destRect.Width);
            g.DrawImage(img, destRect, srcRect, GraphicsUnit.Display);
            img.Dispose();
        }

        /// <summary>
        /// 画像印刷（ストレッチ）
        /// 印刷領域内に画像全体が収まるようにストレッチを行って印刷します。
        /// ストレッチは、ストレッチ前後の画像のアスペクト比が同一になるように行います。
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="filename">画像ファイル名称</param>
        public void PrintPictureStretch(Graphics g, Point offset, string filename)
        {
            // 描画範囲取得
            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);
            rect = ToInchRect(rect);

            // 画像読込
            Image img = Image.FromFile(@filename);

            // アスペクト比が同一になるように計算
            var left = rect.Left;
            var top = rect.Top;
            var width = (float)img.Width;
            var height = (float)img.Height;
            var rate = height / width;  // 比率
            width = rect.Height / rate;
            height = rect.Height;
            if (width > rect.Width)
            {
                width = rect.Width;
                height = rect.Width * rate;
            }
            // 上下中央配置
            left += (rect.Width - width) / 2;
            top += (rect.Height - height) / 2;

            // 描画
            g.DrawImage(img, left, top, width, height);
            img.Dispose();
        }

        /// <summary>
        /// 画像印刷 (ストレッチ)
        /// 印刷領域内に画像全体が収まるようにストレッチを行って印刷します。
        /// ストレッチは、ストレッチ前後の画像のアスペクト比が同一になるように行います。
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="img">画像ファイル</param>
        public void PrintImage(Graphics g, Point offset, Image img)
        {
            // 描画範囲取得
            RectangleF rect = this.Option.Rect;
            rect.Offset(offset);
            rect = ToInchRect(rect);

            // アスペクト比が同一になるように計算
            var left = rect.Left;
            var top = rect.Top;
            var width = (float)img.Width;
            var height = (float)img.Height;
            var rate = height / width;  // 比率
            width = rect.Height / rate;
            height = rect.Height;
            if (width > rect.Width)
            {
                width = rect.Width;
                height = rect.Width * rate;
            }
            // 上下中央配置
            left += (rect.Width - width) / 2;
            top += (rect.Height - height) / 2;

            // 描画
            g.DrawImage(img, left, top, width, height);
            img.Dispose();
        }

        /// <summary>
        /// 文字列印刷
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="offset">印刷オフセット</param>
        /// <param name="str">印字文字列</param>
        /// <param name="foreColor">前景色</param>
        public void PrintString(Graphics g, Point offset, string str, Color? foreColor = null)
        {
            foreColor = foreColor ?? Option.ForeColor;

            if (string.IsNullOrEmpty(str) || this.Option.Rect.IsEmpty)
            {
                return;
            }

            string pstr = str;
            // 付加文字列生成
            if (!string.IsNullOrEmpty(this.Option.PlusString))
            {
                if (-1 != this.Option.PlusString.IndexOf("%s"))
                {
                    pstr = this.Option.PlusString.Replace("%s", str);
                }
                else if (!string.IsNullOrEmpty(pstr))
                {
                    pstr = this.Option.PlusString + pstr;
                }
                else
                {
                    pstr = this.Option.PlusString;
                }
            }

            // 文字列印字
            if (!string.IsNullOrEmpty(pstr))
            {
                SolidBrush brush = new SolidBrush(foreColor.Value);

                RectangleF rect = this.Option.Rect;
                rect.Offset(offset);
                rect = ToInchRect(rect);

                string fontname = this.Option.Fontname;
                if (fontname.Equals(string.Empty))
                {
                    // 指定されたフォントに変更
                    fontname = Fontname;
                }

                try
                {
                    if (!this.Interchange)
                    {
						// フォントの高さは指定矩形幅の4/7
						int font_points = (((this.Option.IsDirection) ? (int)rect.Width : (int)rect.Height) * 4) / 7;
						if (this.Option.IsLargeFont)
						{
							// フォントの高さは指定矩形幅の5/7
							font_points = (((this.Option.IsDirection) ? (int)rect.Width : (int)rect.Height) * 5) / 7;
						}
						FontStyle fs = FontStyle.Regular;
                        if (this.Option.IsItalic)
                        {
                            fs = FontStyle.Italic;
                        }
                        Font font = new Font(fontname, System.Math.Abs(font_points), fs, GraphicsUnit.Pixel);

                        // 配置
                        StringFormat sf = new StringFormat();
                        if (this.Option.IsDirection)
                        {
                            // 縦書き
                            sf.FormatFlags = StringFormatFlags.DirectionVertical;
                            sf.Alignment = StringAlignment.Center;
                        }
                        else
                        {
                            // 横書き
							sf.LineAlignment = StringAlignment.Center;
                            if ('C' == this.Option.Format)
                            {
                                sf.Alignment = StringAlignment.Center;
                            }
                            else if ('R' == this.Option.Format)
                            {
                                sf.Alignment = StringAlignment.Far;
                            }
                            else
                            {
                                sf.Alignment = StringAlignment.Near;
                            }
                        }

                        // 印字
                        PrintPara.PrintReduceString(g, font, brush, rect, pstr, sf, this.Interchange);
                        font.Dispose();
                    }
                    else
                    {
                        // C++互換
                        // フォントの高さは指定矩形幅の3/5
                        float font_height = (((this.Option.IsDirection) ? this.Option.Rect.Width : this.Option.Rect.Height) * 3) / 5;
                        font_height /= 10;

                        FontStyle fs = FontStyle.Regular;
                        if (Option.IsItalic)
                        {
                            fs = FontStyle.Italic;
                        }
                        var font = new Font(fontname, System.Math.Abs(font_height), fs, GraphicsUnit.Millimeter);

                        // 印字
                        if (0 < Option.Width)
                        {
                            // 箱指定(文字数指定)の均等割り
                            PrintKintowariByLength(g, font, rect, pstr, brush);
                        }
                        else if ('P' == this.Option.Format)
                        {
                            // 幅指定の均等割り　※正確には拡大/縮小
                            PrintKIntowariByWidth(g, font, rect, pstr, brush);
                        }
                        else
                        {
                            // 通常印字
                            // Center用の調整
                            if (!this.Option.IsDirection)
                            {
                                // 横書
                                rect.Y += ((rect.Height - ToInch(font_height * 10)) / 2);
                            }
                            else
                            {
                                // 縦書
                                //rect.X += ((rect.Width - ToInch(font_height * 10)) / 2);  // 縦書きの場合はCenter調整の必要なし
                            }

                            // 配置
                            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
                            sf.FormatFlags = (StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap);  // 共通

                            if (!this.Option.IsDirection)
                            {
                                // 横書き
                                if ('C' == this.Option.Format)
                                {
                                    sf.Alignment = StringAlignment.Center;
                                }
                                else if ('R' == this.Option.Format)
                                {
                                    sf.Alignment = StringAlignment.Far;
                                }
                                else
                                {
                                    sf.Alignment = StringAlignment.Near;
                                }
                                sf.LineAlignment = StringAlignment.Near; // Y軸を調整する事でCenterを実現する、そのためここでは最も近い位置を設定
                            }
                            else
                            {
                                // 縦書き
                                sf.FormatFlags |= (StringFormatFlags.DirectionVertical | StringFormatFlags.DirectionRightToLeft);  // 縦書き指定

                                sf.Alignment = StringAlignment.Near;
                                sf.LineAlignment = StringAlignment.Near; // X軸を調整する事でCenterを実現する、そのためここでは最も近い位置を設定
                            }

                            PrintPara.PrintReduceString(g, font, brush, rect, pstr, sf, this.Interchange);
                        }
                        font.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                brush.Dispose();
            }
        }

        /// <summary>
        /// 印字領域に印字できない時には文字列を縮小して印字する
        /// プロポーショナルフォントは指定しないこと
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="font">フォント</param>
        /// <param name="br">ブラシ</param>
        /// <param name="rect">座標</param>
        /// <param name="pstr">印字文字列</param>
        /// <param name="sf">レイアウト情報</param> 
        /// <param name="interchange">C++用パラメタ互換</param>
        public static void PrintReduceString(Graphics g, Font font, SolidBrush br, RectangleF rect, string pstr, StringFormat sf, bool interchange = false)
        {
            var calcPrint = CalcPrintString(g, font, rect, pstr, sf, false, interchange);

            g.DrawString(pstr, font, br, calcPrint.Item1, calcPrint.Item2);
            g.ResetTransform();
        }

        /// <summary>
        /// 文字列の印字幅を取得
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="font">フォント</param>
        /// <param name="rect">座標</param>
        /// <param name="pstr">印字文字列</param>
        /// <param name="halfPitch">ハーフピッチ</param>
        /// <param name="sf">レイアウト情報</param> 
        /// <param name="interchange">C++用パラメタ互換</param>
        /// <returns>印字座標</returns>
        public static Tuple<RectangleF, StringFormat> CalcPrintString(Graphics g, Font font, RectangleF rect, string pstr, StringFormat sf, bool halfPitch = false, bool interchange = false)
        {
            g.ResetTransform();
            g.ScaleTransform(1.0F, 1.0F);

            SizeF sz = new SizeF();
            sz = g.MeasureString(pstr, font, rect.Location, sf);

            float pitch = 0.01F;    // スケーリング時の刻み幅
            if ((sf.FormatFlags & StringFormatFlags.DirectionVertical) == 0)
            {
                // 横書
                if (sz.Width < rect.Width)
                {
                    // 領域内
                    return Tuple.Create(rect, sf);
                }

                // スケーリング開始
                RectangleF scaleRect = rect;
                float sxMin = (halfPitch) ? 0.50F : 0.20F;
                for (float sx = (1.0F - pitch); sxMin <= sx; sx -= pitch)
                {
                    g.ResetTransform();
                    g.ScaleTransform(sx, 1.0F);
                    
                    sz = g.MeasureString(pstr, font, rect.Location, sf);
                    scaleRect = new RectangleF(rect.Left * (1.0F / sx), rect.Top, rect.Width * (1.0F / sx), rect.Height);
                    if (sz.Width < scaleRect.Width)
                    {
                        return Tuple.Create(scaleRect, sf);
                    }
                }

                // 1行で印字する事ができないため2行印字に切替（2行印字の際はStringFormatを強制的に設定する）
                StringFormat scaleFormat = new StringFormat(sf);
                scaleFormat.Alignment = StringAlignment.Near;
                scaleFormat.FormatFlags &= ~StringFormatFlags.NoWrap;

                g.ResetTransform();
                g.ScaleTransform(sxMin, 0.50F); // フォントの高さを半分にする
                sz = g.MeasureString(pstr, font, rect.Location, scaleFormat);

                return Tuple.Create(new RectangleF(scaleRect.Left, rect.Top * (1.0F / 0.50F), scaleRect.Width, sz.Height * 2), scaleFormat);
            }
            else
            {
                // 縦書
                if (sz.Height < rect.Height)
                {
                    // 領域内
                    return Tuple.Create(rect, sf);
                }

                // スケーリング開始
                RectangleF scaleRect = rect;
                float syMin = (halfPitch) ? 0.50F : 0.20F;
                for (float sy = (1.0F - pitch); syMin <= sy; sy -= pitch)
                {
                    g.ResetTransform();
                    g.ScaleTransform(1.0F, sy);
                    sz = g.MeasureString(pstr, font, rect.Location, sf);
                    scaleRect = new RectangleF(rect.Left, rect.Top * (1.0F / sy), rect.Width, rect.Height * (1.0F / sy));
                    if (sz.Height < scaleRect.Height)
                    {
                        return Tuple.Create(scaleRect, sf);
                    }
                }

                // 1行で印字する事ができないため2行印字に切替（2行印字の際はStringFormatを強制的に設定する）
                StringFormat scaleFormat = new StringFormat(sf);
                scaleFormat.Alignment = StringAlignment.Near;
                scaleFormat.FormatFlags &= ~StringFormatFlags.NoWrap;
                scaleFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

                g.ResetTransform();
                g.ScaleTransform(0.50F, syMin); // フォントの幅を半分にする
                sz = g.MeasureString(pstr, font, rect.Location, scaleFormat);

                float width = sz.Width * 2;
                float spaces = (rect.Width * (1.0F / 0.50F)) - width;   // Nearの位置へ 
                return Tuple.Create(new RectangleF((rect.Left * (1.0F / 0.50F)) + spaces, scaleRect.Top, width, scaleRect.Height), scaleFormat);
            }
        }

        /// <summary>
        /// 均等割付
        /// </summary>
        /// <param name="g">Graphics クラス</param>
        /// <param name="font">フォント</param>
        /// <param name="foreColor">前景色</param>
        /// <param name="rc">座標</param>
        /// <param name="pstr">印字文字列</param>
        public static void PrintKintouWaritsuke(Graphics g, Font font, Rectangle rc, string pstr, Color? foreColor = null)
        {
            // Visual Studio International Pack: Japanese Kana Conversion Libraryを使用
            // http://wiki.dobon.net/index.php?.NET%A5%D7%A5%ED%A5%B0%A5%E9%A5%DF%A5%F3%A5%B0%B8%A6%B5%E6%2F85
            foreColor = foreColor ?? Color.Black;

            // TextAlignmentStyleInfoの作成
            var align = new TextAlignmentStyleInfo();
            align.Style = TextAlignmentStyle.Justify;
            Utility.DrawJapaneseString(g, pstr, font, foreColor.Value, rc, align);
        }

        /// <summary>
        /// 均等割付（箱指定Ver）
        /// 
        /// ※C++互換印字用
        /// </summary>
        /// <remarks>
        /// オプションで文字数を指定して(箱指定)均等割りしている個所の均等割り
        /// 
        /// ex) <保険者番号>=404,403,465,75,6G ← 最後の[6G]の"6"が文字数
        /// </remarks>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="rc"></param>
        /// <param name="pstr"></param>
        /// <param name="brush"></param>
        public void PrintKintowariByLength(Graphics g, Font font, RectangleF rc, string pstr, Brush brush)
        {
            string work = pstr;

            int length = work.Length;
            if (length != Option.Width)
            {
                if (work.Length < Option.Width)
                {
                    // 指定した桁数より文字数が少ない
                    // 空白右詰め
                    work = work.PadLeft(Option.Width);
                }
                else
                {
                    work = work.Remove(Option.Width);
                }
            }

            // 指定Rectを1文字数分に分解
            RectangleF rect1 = new RectangleF(new PointF(rc.X, rc.Y), new SizeF(rc.Width / Option.Width, rc.Height));
            // 印字
            foreach (char c in work)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                // 指定Rectと文字サイズの関係上、Centerでは真ん中に来ないためFarを設定
                format.LineAlignment = StringAlignment.Far;
                g.DrawString(Convert.ToString(c), font, brush, rect1, format);
                // X座標を1文字分移動
                rect1.X = rect1.X + rect1.Width;
            }
        }

        /// <summary>
        /// 均等割付（幅指定Ver）
        /// 
        /// ※C++互換印字用
        /// </summary>
        /// <remarks>
        /// オプションで"P"を指定している個所の均等割り
        /// 
        /// ※C++で"P"指定した場合、実際には均等割りではなく
        /// 　指定Rectに対して文字を「拡大/縮小」する処理である
        /// 　(印字文字幅が指定Rectより小さい場合には【拡大】、大きい場合には【縮小】)
        /// </remarks>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="rc"></param>
        /// <param name="pstr"></param>
        /// <param name="brush"></param>
        public void PrintKIntowariByWidth(Graphics g, Font font, RectangleF rc, string pstr, Brush brush)
        {
            StringFormat format = new StringFormat(StringFormat.GenericTypographic);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            if (this.Option.IsDirection)
            {
                // 縦書
                format.FormatFlags = StringFormatFlags.DirectionVertical | StringFormatFlags.MeasureTrailingSpaces;
            }
            else
            {
                format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;  // 各行の行末に空白を含める
            }

            ////////////////////////////////////////////////////////////////////////////
            // 比率計算(縮小率・拡大率計算)

            // 比率
            float enableRateX = 1.0F;
            float enableRateY = 1.0F;

            // 文字列を描画するときの大きさを計測する(空白を含んだ文字の印字サイズ)
            SizeF stringSize = g.MeasureString(pstr, font, int.MaxValue, format);

            ////////////////////////////////////////////////////////////////////////////
            // 縮小率の計算

            // 横書き
            if (!this.Option.IsDirection)
            {
                // 文字描画領域より印字RectのWidthが小さい
                if (rc.Width < stringSize.Width)
                {
                    // 印字Rectに入り切る、Widthの縮小率を計算
                    enableRateX = rc.Width / stringSize.Width;
                }
            }
            // 縦書き
            else
            {
                // 文字描画領域より印字RectのHeightが小さい
                if (rc.Height < stringSize.Height)
                {
                    // 印字Rectに入り切る、Heightの縮小率を計算
                    enableRateY = rc.Height / stringSize.Height;
                }
            }

            ////////////////////////////////////////////////////////////////////////////
            // 拡大率の計算

            // 横書き
            if (!this.Option.IsDirection)
            {
                // 文字描画領域より印字RectのWidthが大きい
                if (stringSize.Width < rc.Width)
                {
                    // 印字Rectに入り切る、Widthの拡大率を計算
                    enableRateX = rc.Width / stringSize.Width;
                }
            }
            // 縦書き
            else
            {
                // 印字Rectより文字描画領域のHeightが大きい
                if (stringSize.Height < rc.Height)
                {
                    // 印字Rectに入り切る、Heightの拡大率を計算
                    enableRateY = rc.Height / stringSize.Height;
                }
            }

            ////////////////////////////////////////////////////////////////////////////
            // 拡大・縮小 + 文字印字

            // 別コンテナ開始（文字印字の時のみ、スケールを変換する）
            // 　※これをすることにより、以下EndContainer()が来るまでの間のみ変更できる
            GraphicsContainer containerState = g.BeginContainer();
            ;
            // 印字Rectに入り切る比率で、スケールを拡大または縮小する
            g.ScaleTransform(enableRateX, enableRateY);
            // ScaleTransform()を実行すると、座標系がすべて拡大または縮小されるため
            // 実際に印字するRectは元の位置・サイズになるよう調整する
            RectangleF drawRect2 = rc;
            drawRect2.X = drawRect2.X / enableRateX;
            drawRect2.Y = drawRect2.Y / enableRateY;
            drawRect2.Width = drawRect2.Width / enableRateX;
            drawRect2.Height = drawRect2.Height / enableRateY;

            // 文字の描画
            g.DrawString(pstr, font, brush, drawRect2, format);

            // 別コンテナ終了
            g.EndContainer(containerState);
        }
    }
}
