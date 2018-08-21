//
// PrintPreviewControl.cs
// 
// 印刷プレビュー汎用コントロール
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CommonDialog.PrintPreview
{
	/// <summary>
	/// palette印刷プレビュー汎用コントロール
	/// </summary>
	public partial class PrintPreviewControl : PictureBox
    {
        /// <summary>分割表示オフセット値定数</summary>
        private const int SplitOffsetValue = 10;

        /// <summary>
        /// PrintDocumentオブジェクトプロパティ
        /// </summary>
        public PrintDocument Document { get; set; }

        /// <summary>
        /// AutoZoomプロパティ
        /// </summary>
        public bool AutoZoom
        {
            get
            {
                return AutoZoomFld;
            }
            set
            {
                if (IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    if (AutoZoomFld != value)
                    {
                        AutoZoomFld = value;
                        AutoZoomFldChanged();
                    }
                }
                else
                {
                    AutoZoomFld = value;
                }
            }
        }

        /// <summary>
        /// Zoomプロパティ
        /// </summary>
        public double Zoom
        {
            get
            {
                return ZoomFld;
            }
            set
            {
                if (value <= 0)
                {
                    // 0以下は無効
                    throw new System.ArgumentOutOfRangeException();
                }

                if (IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    AutoZoomFld = false;
                    if (ZoomFld != value)
                    {
                        ZoomFld = value;
                        ZoomFldChanged();
                    }
                }
                else
                {
                    ZoomFld = value;
                }
            }
        }

        /// <summary>
        /// Zoomsプロパティ
        /// </summary>
        public double[] Zooms
        {
            get
            {
                return ZoomsFld;
            }
            set
            {
                ZoomsFld = value;
            }
        }

        /// <summary>
        /// GradePerZoomsプロパティ
        /// </summary>
        public int GradePerZooms
        {
            get
            {
                return GradePerZoomsFld;
            }
            set
            {
                if ((value < 1) || (value > 10))
                {
                    // 1～10以外は無効
                    throw new System.ArgumentOutOfRangeException();
                }
                GradePerZoomsFld = value;
            }
        }

        /// <summary>
        /// Columnsプロパティ
        /// </summary>
        public int Columns
        {
            get
            {
                return ColumnsFld;
            }
            set
            {
                if (value < 1)
                {
                    // 1未満は無効
                    throw new System.ArgumentOutOfRangeException();
                }

                if (IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    if (ColumnsFld != value)
                    {
                        ColumnsFld = value;
                        ColumnsRowsChanged();
                    }
                }
                else
                {
                    ColumnsFld = value;
                }
            }
        }

        /// <summary>
        /// Rowsプロパティ
        /// </summary>
        public int Rows
        {
            get
            {
                return RowsFld;
            }
            set
            {
                if (value < 1)
                {
                    // 1未満は無効
                    throw new System.ArgumentOutOfRangeException();
                }

                if (IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    if (RowsFld != value)
                    {
                        RowsFld = value;
                        ColumnsRowsChanged();
                    }
                }
                else
                {
                    RowsFld = value;
                }
            }
        }

        /// <summary>
        /// StartPageプロパティ
        /// </summary>
        public int StartPage
        {
            get
            {
                return StartPageFld;
            }
            set
            {
                if (value < 0)
                {
                    // 0以下は無効
                    throw new System.ArgumentOutOfRangeException();
                }

                if (IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合はイベント発生
                    if (StartPageFld != value)
                    {
                        StartPageFld = value;
                        OnStartPageChanged(new EventArgs());
                    }
                }
                else
                {
                    StartPageFld = value;
                }
            }
        }

        /// <summary>自動ズーム有無フィールド</summary>
        private bool AutoZoomFld;
        /// <summary>拡大率フィールド</summary>
        private double ZoomFld;
        /// <summary>既定拡大率フィールド</summary>
        private double[] ZoomsFld;
        /// <summary>開始ページフィールド</summary>
        private int StartPageFld;
        /// <summary>ズーム時の段階数フィールド</summary>
        private int GradePerZoomsFld;
        /// <summary>表示ページ列数フィールド</summary>
        private int ColumnsFld;
        /// <summary>表示ページ行数フィールド</summary>
        private int RowsFld;
        /// <summary>印刷プレビュー情報</summary>
        private PreviewPageInfo[] PageInfo;
        /// <summary>基本拡大率</summary>
        private double BaseZoom;
        /// <summary>表示させる範囲</summary>
        private Rectangle DispRectangle;
        /// <summary>ドラッグ時の位置</summary>
        private Point DragPoint;
        /// <summary>画像の表示幅</summary>
        private int ImageDispWidth;
        /// <summary>画像の表示高さ</summary>
        private int ImageDispHeight;
        /// <summary>プレビュー描画済み有無</summary>
        private bool IsInvalidatePreview;
        /// <summary>ページ分割毎の描画範囲</summary>
        private Rectangle[,] ColRowRectangle;
        /// <summary>マウスダウン及びムーブ判定</summary>
        private bool IsMouseDownMove = false;

        /// <summary>
        /// StartPageChangedイベント
        /// </summary>
        public event EventHandler StartPageChanged;

        /// <summary>
        /// ZoomsChangedイベント
        /// </summary>
        public event EventHandler ZoomsChanged;

        /// <summary>
        /// AutoZoomChangedイベント
        /// </summary>
        public event EventHandler AutoZoomChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrintPreviewControl()
        {
            // メンバ変数初期化
            this.Document = null;
            GradePerZoomsFld = 1;
            ColumnsFld = 1;
            RowsFld = 1;
            PageInfo = null;
            AutoZoomFld = true;
            ZoomFld = 1D;
            BaseZoom = 1D;
            ZoomsFld = null;
            StartPageFld = 0;
            DispRectangle = Rectangle.Empty;
            DragPoint = Point.Empty;
            ImageDispWidth = 0;
            ImageDispHeight = 0;
            IsInvalidatePreview = false;
        }

        /// <summary>
        /// WndProcのオーバーライド
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WmMouseWheel = 0x20A;

            if (m.Msg == WmMouseWheel)
            {
                // マウスホイールウィンドウメッセージの場合、内部イベント発生
                int wparam = (int)(((ulong)m.WParam) & 0xFFFFFFFF);
                int delta = wparam >> 16;
                MouseWheelChaned(delta);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// OnPaintのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (PageInfo != null)
            {
                if ((ColumnsFld == 1) && (RowsFld == 1))
                {
                    // ページ分割が無い場合

                    // 表示画像サイズの取得
                    int width = (int)Math.Round((PageInfo[StartPageFld].Image.Width / PageInfo[StartPageFld].Image.HorizontalResolution) * e.Graphics.DpiX);
                    int height = (int)Math.Round((PageInfo[StartPageFld].Image.Height / PageInfo[StartPageFld].Image.VerticalResolution) * e.Graphics.DpiY);

                    // 表示範囲に対する描画拡大率を取得
                    double zoomX = ((double)(DispRectangle.Width - 20) / (double)width);
                    double zoomY = ((double)(DispRectangle.Height - 20) / (double)height);
                    double zoomFld;
                    if (zoomX > zoomY)
                    {
                        zoomFld = zoomY;
                    }
                    else
                    {
                        zoomFld = zoomX;
                    }

                    // 描画するページの位置・サイズを設定
                    Rectangle drawRectangle = Rectangle.Empty;
                    drawRectangle.Width = (int)Math.Round(width * zoomFld);
                    drawRectangle.Height = (int)Math.Round(height * zoomFld);
                    drawRectangle.X = DispRectangle.X;
                    drawRectangle.Y = DispRectangle.Y;
                    // 表示範囲に対して位置を中央揃え
                    drawRectangle.X += (int)Math.Round((double)((DispRectangle.Width - drawRectangle.Width) / 2));
                    drawRectangle.Y += (int)Math.Round((double)((DispRectangle.Height - drawRectangle.Height) / 2));

                    // 画像を指定された範囲に描画
                    e.Graphics.DrawImage(PageInfo[StartPageFld].Image, drawRectangle);
                    // 枠を描画
                    e.Graphics.DrawRectangle(Pens.Black, drawRectangle);

                    if (AutoZoomFld == true)
                    {
                        // 自動ズームで1ページ表示の場合、拡大率に基本拡大率を設定
                        if (ZoomFld != BaseZoom)
                        {
                            ZoomFld = BaseZoom;
                        }
                    }
                }
                else
                {
                    // ページ分割有りの場合

                    // 分割ページ毎に描画
                    int idx = 0;
                    for (int j = 0; j < RowsFld; j++)
                    {
                        for (int i = 0; i < ColumnsFld; i++)
                        {
                            // StartPageプロパティからのインデックス設定
                            idx = i + (j * ColumnsFld);
                            if ((StartPageFld + idx) > (PageInfo.Count() - 1))
                            {
                                break;
                            }

                            // 表示画像サイズの取得
                            int width = (int)Math.Round((PageInfo[StartPageFld + idx].Image.Width / PageInfo[StartPageFld + idx].Image.HorizontalResolution) * e.Graphics.DpiX);
                            int height = (int)Math.Round((PageInfo[StartPageFld + idx].Image.Height / PageInfo[StartPageFld + idx].Image.VerticalResolution) * e.Graphics.DpiY);

                            // 表示範囲に対する描画拡大率を取得
                            double zoomX = ((double)(DispRectangle.Width / ColumnsFld - 20) / (double)width);
                            double zoomY = ((double)(DispRectangle.Height / RowsFld - 20) / (double)height);
                            double zoomFld;
                            if (zoomX > zoomY)
                            {
                                zoomFld = zoomY;
                            }
                            else
                            {
                                zoomFld = zoomX;
                            }

                            // 描画するページの位置・サイズを設定
                            Rectangle drawRectangle = Rectangle.Empty;
                            drawRectangle.Width = (int)Math.Round(width * zoomFld);
                            drawRectangle.Height = (int)Math.Round(height * zoomFld);
                            drawRectangle.X = DispRectangle.X + ((DispRectangle.Width / ColumnsFld) + SplitOffsetValue) * i;
                            drawRectangle.Y = DispRectangle.Y + ((DispRectangle.Height / RowsFld) + SplitOffsetValue) * j;
                            // 表示範囲に対して位置を中央揃え
                            drawRectangle.X += (int)Math.Round((double)(((DispRectangle.Width / ColumnsFld) - drawRectangle.Width) / 2));
                            drawRectangle.Y += (int)Math.Round((double)(((DispRectangle.Height / RowsFld) - drawRectangle.Height) / 2));
                            
                            // 画像を指定された範囲に描画
                            e.Graphics.DrawImage(PageInfo[StartPageFld + idx].Image, drawRectangle);
                            // 枠を描画
                            e.Graphics.DrawRectangle(Pens.Black, drawRectangle);
                            // ページ分割毎の描画範囲取得
                            ColRowRectangle[i, j] = drawRectangle;
                        }
                    }
                }
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// OnMouseDoubleClickのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            // 既定拡大率が設定されている場合、ダブルクリックでズーム
            //  設定されていない場合は未処理
            if ((ZoomsFld == null) || (ZoomsFld.Count() <= 0))
            {
                return;
            }

            try
            {   
                // 座標オフセット値算出
                int offsetX = 0;
                int offsetY = 0;
                if ((RowsFld > 1) || (ColumnsFld > 1))
                {
                    // クリックした座標がページ分割毎の描画範囲のどこに属するかチェック
                    offsetX = -1;
                    offsetY = -1;
                    for (int j = 0; j < RowsFld; j++)
                    {
                        // 行方向へチェック
                        for (int i = 0; i < ColumnsFld; i++)
                        {
                            //　列方向へチェック
                            if ((e.X >= (ColRowRectangle[i, j].X - SplitOffsetValue)) &&
                                (e.X <= (ColRowRectangle[i, j].X + ColRowRectangle[i, j].Width)))
                            {
                                // X座標が描画範囲内の場合、Y座標もチェック
                                if ((e.Y >= (ColRowRectangle[i, j].Y - SplitOffsetValue)) &&
                                    (e.Y <= (ColRowRectangle[i, j].Y + ColRowRectangle[i, j].Height)))
                                {
                                    // X・Y座標ともに描画範囲に一致
                                    offsetX = i * SplitOffsetValue;
                                    offsetY = j * SplitOffsetValue;
                                    break;
                                }
                            }
                        }
                    }

                    // ページ分割毎の描画範囲に一致しない場合
                    if (offsetX == -1)
                    {
                        // X座標が一致しない場合
                        if (e.X > (ColRowRectangle[ColumnsFld - 1, 0].X + ColRowRectangle[ColumnsFld - 1, 0].Width))
                        {
                            // 大外より大きい場合
                            offsetX = (ColumnsFld - 1) * SplitOffsetValue;
                        }
                        else
                        {
                            // 大内より小さい場合
                            offsetX = 0;
                        }
                    }
                    if (offsetY == -1)
                    {
                        // Y座標が一致しない場合
                        if (e.Y > (ColRowRectangle[0, RowsFld - 1].Y + ColRowRectangle[0, RowsFld - 1].Height))
                        {
                            // 大外より大きい場合
                            offsetY = (RowsFld - 1) * SplitOffsetValue;
                        }
                        else
                        {
                            // 大内より小さい場合
                            offsetY = 0;
                        }
                    }
                }

                // クリックされた位置を画像上の位置に変換
                Point imgPoint = new Point((int)Math.Round((e.X - offsetX - DispRectangle.X) / (ZoomFld)),
                                            (int)Math.Round((e.Y - offsetY - DispRectangle.Y) / (ZoomFld)));
                     
                // 一番近い既定拡大率を取得
                double orgZoom = ZoomFld;
                if (ZoomFld > ZoomsFld[ZoomsFld.Count() - 1])
                {
                    // 既定拡大率の最大値より大きい場合、既定拡大率の最大値
                    if (e.Button == MouseButtons.Right)
                    {
                        ZoomFld = ZoomsFld[ZoomsFld.Count() - 1];
                    }
                    else
                    {
                        return;
                    }
                }
                else if (ZoomFld == ZoomsFld[ZoomsFld.Count() - 1])
                {
                    // 既定拡大率の最大値の場合、既定拡大率の最大値の次拡大率
                    if (e.Button == MouseButtons.Right)
                    {
                        if (ZoomsFld.Count() < 2)
                        {
                            return;
                        }
                        ZoomFld = ZoomsFld[ZoomsFld.Count() - 1 - 1];
                    }
                    else
                    {
                        return;
                    }
                }
                else if (ZoomFld < ZoomsFld[0])
                {
                    // 既定拡大率の最小値より小さい場合、既定拡大率の最小値
                    if (e.Button == MouseButtons.Left)
                    {
                        ZoomFld = ZoomsFld[0];
                    }
                    else
                    {
                        return;
                    }
                }
                else if (ZoomFld == ZoomsFld[0])
                {
                    // 既定拡大率の最小値の場合、既定拡大率の最小値の次拡大率
                    if (e.Button == MouseButtons.Left)
                    {
                        if (ZoomsFld.Count() < 2)
                        {
                            return;
                        }
                        ZoomFld = ZoomsFld[0 + 1];
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // 既定拡大率内の場合、どの既定拡大率間の値か
                    int zoomCount = ZoomsFld.Count();

                    for (int i = 0; i < zoomCount; i++)
                    {
                        if (ZoomFld < ZoomsFld[i])
                        {
                            continue;
                        }
                        
                        // 既定拡大率以上の場合、次拡大率より小さいか
                        if (ZoomFld < ZoomsFld[i + 1])
                        {
                            int idx;
                            // 既定拡大率間のうち、どちらに近いか
                            if (ZoomFld < (ZoomsFld[i] + ZoomsFld[i + 1]) / 2)
                            {
                                // 小さいほうの既定拡大率を一番近い既定拡大率として設定
                                idx = i;
                            }
                            else
                            {
                                // 大きいほうの既定拡大率を一番近い既定拡大率として設定
                                idx = i + 1;
                            }

                            // 次拡大率を設定
                            if (e.Button == MouseButtons.Left)
                            {
                                if (zoomCount - 1 < idx + 1)
                                {
                                    ZoomFld = ZoomsFld[zoomCount - 1];
                                }
                                else
                                {
                                    ZoomFld = ZoomsFld[idx + 1];
                                }
                            }
                            else
                            {
                                if (-1 >= idx - 1)
                                {
                                    ZoomFld = ZoomsFld[0];
                                }
                                else
                                {
                                    ZoomFld = ZoomsFld[idx - 1];
                                }
                            }

                            break;
                        }
                        
                    }
                }

                // 段階ズーム描画
                Thread trd = new Thread(new ParameterizedThreadStart(GradeZoomThread));
                trd.IsBackground = true;
                object[] obj = new object[5];
                obj[0] = e;
                obj[1] = orgZoom;
                obj[2] = imgPoint;
                obj[3] = offsetX;
                obj[4] = offsetY;
                trd.Start(obj);

                // ZoomChangedイベント発生
                if (ZoomsChanged != null)
                {
                    ZoomsChanged(this, new EventArgs());
                }                
            }
            finally
            {
                base.OnMouseDoubleClick(e);
            }
        }

        /// <summary>
        /// OnMouseDownのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.CanFocus)
            {
                // フォーカスセット
                SetFocus(this.Handle);
            }

            if (e.Button == MouseButtons.Left)
            {
                // ドラッグされた位置を画像上の位置に変換
                DragPoint = new Point((int)Math.Round((e.X - DispRectangle.X) / ZoomFld),
                                        (int)Math.Round((e.Y - DispRectangle.Y) / ZoomFld));
         
                using (var ms = new MemoryStream(Properties.Resources.R_CROS_M))
                {
                    // リソースから読み込んだカーソルをコントロールに設定。
                    Cursor.Current = new Cursor(ms);
                    this.IsMouseDownMove = true;
                }
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// OnMouseMoveのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseDownMove)
            {
                // ドロップされた位置を画像上の位置に変換
                Point imgPoint = new Point((int)Math.Round((e.X - DispRectangle.X) / ZoomFld),
                                           (int)Math.Round((e.Y - DispRectangle.Y) / ZoomFld));

                // 画像の表示範囲を計算する
                DispRectangle.X = DispRectangle.X + (int)Math.Round((imgPoint.X - DragPoint.X) * ZoomFld);
                DispRectangle.Y = DispRectangle.Y + (int)Math.Round((imgPoint.Y - DragPoint.Y) * ZoomFld);

                // 画像を表示する
                this.Refresh();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// OnMouseUpのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // マウスカーソルを既定に設定
                Cursor.Current = Cursors.Default;
                this.IsMouseDownMove = false;
            }

            base.OnMouseUp(e);
        }

        /// <summary>
        /// OnResizeのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (PageInfo != null)
            {
                if (AutoZoomFld == true)
                {
                    // 自動ズームの場合、リサイズ後の拡大率・描画座標を算出

                    // 描画拡大率を取得
                    double zoomX = ((double)this.Width - 20) / (double)ImageDispWidth;
                    double zoomY = ((double)this.Height -20) / (double)ImageDispHeight;
                    if (zoomX > zoomY)
                    {
                        // BaseZoomに設定する値は丸めた後の値を設定する
                        BaseZoom = Math.Round(zoomY, 2);
                    }
                    else
                    {
                        // BaseZoomに設定する値は丸めた後の値を設定する
                        BaseZoom = Math.Round(zoomX, 2);
                    }
                    ZoomFld = BaseZoom;

                    // 描画範囲を中央表示で設定
                    DispRectangle.X = (int)Math.Round((this.Width - ImageDispWidth * ZoomFld) / 2);
                    DispRectangle.Y = (int)Math.Round((this.Height - ImageDispHeight * ZoomFld) / 2);
                }

                DispRectangle.Width = (int)Math.Round(ImageDispWidth * ZoomFld);
                DispRectangle.Height = (int)Math.Round(ImageDispHeight * ZoomFld);

                // 画像を再描画する
                this.Refresh();
            }

            base.OnResize(e);
        }

        /// <summary>
        /// StartPageChangedイベントを発生させる
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStartPageChanged(EventArgs e)
        {
            // 表示矩形の更新
            UpdateDispRectangle(ZoomFld);

            // 画像を描画する
            this.Refresh();

            if (StartPageChanged != null)
            {
                // StartPageChangedイベント発生
                StartPageChanged(this, e);
            }
        }

        /// <summary>
        /// Columns・Rowsプロパティ変更内部イベント
        /// </summary>
        private void ColumnsRowsChanged()
        {
            // ページ分割毎の描画範囲フィールド作成
            ColRowRectangle = new Rectangle[ColumnsFld, RowsFld];

            // 画像表示サイズの更新
            UpdateImageDispSize();

            // 自動ズーム
            AutoZoomFld = true;

            // AutoZoomChangedイベント発生
            if (AutoZoomChanged != null)
            {
                AutoZoomChanged(this, new EventArgs());
            }      

            // 再描画
            this.Refresh();
        }

        /// <summary>
        /// AutoZoomプロパティ変更内部イベント
        /// </summary>
        private void AutoZoomFldChanged()
        {
            if (PageInfo == null)
            {
                return;
            }

            if (AutoZoomFld == true)
            {
                // 画像表示サイズの更新
                UpdateImageDispSize();

                // 再描画
                this.Refresh();
            }
        }

        /// <summary>
        /// Zoomプロパティ変更内部イベント
        /// </summary>
        private void ZoomFldChanged()
        {
            if (PageInfo == null)
            {
                return;
            }
            
            // 変更後拡大率での描画サイズ設定
            DispRectangle.Width = (int)Math.Round(ImageDispWidth * ZoomFld);
            DispRectangle.Height = (int)Math.Round(ImageDispHeight * ZoomFld);

            // 再描画
            this.Refresh();
        }

        /// <summary>
        /// MouseWheel変更メッセージ内部イベント
        /// </summary>
        /// <param name="delta">ホイール移動量</param>
        private void MouseWheelChaned(int delta)
        {
            // マウスホイールの移動量から移動ページ数取得
            int page = delta / -120;

            // 現在ページから移動ページ数分の移動
            if ((StartPageFld + page) < 0)
            {
                page = 0;
            }
            else if ((StartPageFld + page * (ColumnsFld * RowsFld)) > (PageInfo.Count() - 1))
            {
                return;
            }
            this.StartPage += page;
        }

        /// <summary>
        /// 段階ズームスレッド
        /// </summary>
        /// <param name="args">起動引数</param>
        private void GradeZoomThread(object args)
        {
            // 引数の展開
            object[] argsTmp = (object[])args;
            MouseEventArgs e = (MouseEventArgs)argsTmp[0];
            double orgZoom = (double)argsTmp[1];
            Point imgPoint = (Point)argsTmp[2];
            int offsetX = (int)argsTmp[3];
            int offsetY = (int)argsTmp[4];

            // 段階的に描画
            if (e.Button == MouseButtons.Left)
            {
                // 拡大
                for (int i = 1; i <= GradePerZoomsFld; i++)
                {
                    Thread.Sleep(50);

                    // 拡大率算出
                    double zoom = orgZoom + ((ZoomFld - orgZoom) / GradePerZoomsFld) * i;
                    if (zoom > ZoomFld)
                    {
                        zoom = ZoomFld;
                    }
                    
                    // クリックされた位置を画像上の位置に変換（拡大率変換後）
                    Point imgZoomPoint = new Point((int)Math.Round((e.X - offsetX - DispRectangle.X) / (zoom)),
                                                   (int)Math.Round((e.Y - offsetY - DispRectangle.Y) / (zoom)));
                    // 表示矩形の更新
                    UpdateDispRectangle(zoom);
                    // 画像の表示範囲を計算する
                    DispRectangle.X = DispRectangle.X + (int)Math.Round((imgZoomPoint.X - imgPoint.X) * zoom);
                    DispRectangle.Y = DispRectangle.Y + (int)Math.Round((imgZoomPoint.Y - imgPoint.Y) * zoom);

                    // 再描画
                    this.Invalidate();
                }
            }
            else
            {
                // 縮小
                for (int i = 1; i <= GradePerZoomsFld; i++)
                {
                    Thread.Sleep(50);

                    // 拡大率算出
                    double zoom = orgZoom - ((orgZoom - ZoomFld) / GradePerZoomsFld) * i;
                    if (zoom < ZoomFld)
                    {
                        zoom = ZoomFld;
                    }
                    
                    // クリックされた位置を画像上の位置に変換（拡大率変換後）
                    Point imgZoomPoint = new Point((int)Math.Round((e.X - offsetX - DispRectangle.X) / (zoom)),
                                                    (int)Math.Round((e.Y - offsetY - DispRectangle.Y) / (zoom)));

                    // 表示矩形の更新
                    UpdateDispRectangle(zoom);
                    // 画像の表示範囲を計算する
                    DispRectangle.X = DispRectangle.X + (int)Math.Round((imgZoomPoint.X - imgPoint.X) * zoom);
                    DispRectangle.Y = DispRectangle.Y + (int)Math.Round((imgZoomPoint.Y - imgPoint.Y) * zoom);

                    // 再描画
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// プレビュー描画
        /// </summary>
        public void InvalidatePreview()
        {
            if (IsInvalidatePreview)
            {
                // メンバ変数クリア
                GradePerZoomsFld = 1;
                ColumnsFld = 1;
                RowsFld = 1;
                AutoZoomFld = true;
                ZoomFld = 1D;
                BaseZoom = 1D;
                StartPageFld = 0;
                DispRectangle = Rectangle.Empty;
                DragPoint = Point.Empty;
                ImageDispWidth = 0;
                ImageDispHeight = 0;
                IsInvalidatePreview = false;
            }

            try
            {
                // マウスカーソルを待機に設定
                Cursor.Current = Cursors.WaitCursor;

                // DocumentのPritController取得
                PrintController conbak = this.Document.PrintController;
                // プレビューイメージ取得用のPreviewPrintController作成
                PreviewPrintController con = new PreviewPrintController();

                // プレビューイメージ取得用のPreviewPrintControllerにて印刷プロセス開始
                this.Document.PrintController = con;
                this.Document.Print();

                // Documentに既存のPritControllerを設定
                this.Document.PrintController = conbak;

                PageInfo = con.GetPreviewPageInfo();

                // 画像表示サイズの更新
                UpdateImageDispSize();
                
                // プレビュー描画済み
                IsInvalidatePreview = true;
            }
            finally
            {
                // マウスカーソルを規定に設定
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 画像表示サイズの更新
        /// </summary>
        private void UpdateImageDispSize()
        {
            if (this.IsMouseDownMove) return;
            
            // イメージのディスプレイ上の実サイズを取得
            using (Graphics g = this.CreateGraphics())
            {
                // 全画像の最大値でサイズ設定
                int width = 0;
                int height = 0;
                float horizontalResolution = 0;
                float verticalResolution = 0;
                for (int i = 0; i < PageInfo.Count(); i++)
                {
                    if (width < PageInfo[i].Image.Width) width = PageInfo[i].Image.Width;
                    if (height < PageInfo[i].Image.Height) height = PageInfo[i].Image.Height;
                    if (horizontalResolution < PageInfo[i].Image.HorizontalResolution) horizontalResolution = PageInfo[i].Image.HorizontalResolution;
                    if (verticalResolution < PageInfo[i].Image.VerticalResolution) verticalResolution = PageInfo[i].Image.VerticalResolution;
                }
                ImageDispWidth = (int)Math.Round((width / horizontalResolution) * g.DpiX) * ColumnsFld;
                ImageDispHeight = (int)Math.Round((height / verticalResolution) * g.DpiY) * RowsFld;

                // 描画拡大率を取得
                double zoomX = ((double)this.Width - 20) / (double)ImageDispWidth;
                double zoomY = ((double)this.Height - 20) / (double)ImageDispHeight;
                if (zoomX > zoomY)
                {
                    // BaseZoomに設定する値は丸めた後の値を設定する
                    BaseZoom = Math.Round(zoomY, 2);
                }
                else
                {
                    // BaseZoomに設定する値は丸めた後の値を設定する
                    BaseZoom = Math.Round(zoomX, 2);
                }
                ZoomFld = BaseZoom;

                // 表示矩形の更新
                UpdateDispRectangle(ZoomFld);
                // 描画範囲を中央表示で設定
                DispRectangle.X = (int)Math.Round((this.Width - ImageDispWidth * ZoomFld) / 2);
                DispRectangle.Y = (int)Math.Round((this.Height - ImageDispHeight * ZoomFld) / 2);
            }
        }

        /// <summary>
        /// 表示矩形の更新
        /// </summary>
        /// <param name="zoom">倍率</param>
        private void UpdateDispRectangle(double zoom)
        {           
            // イメージのディスプレイ上の実サイズを取得
            using (Graphics g = this.CreateGraphics())
            {
                int width = ImageDispWidth;
                int height = ImageDispHeight;

                DispRectangle.Width = (int)Math.Round(width * zoom);
                DispRectangle.Height = (int)Math.Round(height * zoom);
            }
        }

        // 外部参照メソッド
        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);
    }
}
