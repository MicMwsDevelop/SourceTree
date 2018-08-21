//
// PrintPreviewPictureBox.cs
// 
// 印刷プレビューピクチャーボックスクラス
// 
// Copyright (C) Project Darwin. All Rights Reserved.
// 
// Ver1.000 新規作成(2014/02/01 勝呂)
//
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MwsLib.Components
{
	/// <summary>
	/// 印刷プレビュークラス
	/// </summary>
	public partial class PrintPreviewPictureBox : PictureBox
	{
        /// <summary>分割表示オフセット値定数</summary>
        private const int SplitOffsetValue = 10;

        /// <summary>PrintDocumentオブジェクトプロパティ</summary>
        [Category("表示")]
        [Description("プレビューするPrintDocumentです。")]
        public PrintDocument Document { get; set; }

        /// <summary>AutoZoomプロパティ</summary>
        [Category("表示")]
        [Description("利用可能なスペースを埋めるため、ズームを自動的に調節するかどうかを示します。")]
        public bool AutoZoom
        {
            get
            {
                return m_AutoZoom;
            }
            set
            {
                if (m_IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    if (m_AutoZoom != value)
                    {
                        m_AutoZoom = value;
                        AutoZoomChanged();
                    }
                }
                else
                {
                    m_AutoZoom = value;
                }
            }
        }

        /// <summary>Zoomプロパティ</summary>
        [Category("表示")]
        [Description("ページの拡大率を示します。")]
        public double Zoom
        {
            get
            {
                return m_Zoom;
            }
            set
            {
                if (value <= 0)
                {
                    // 0以下は無効
                    throw new System.ArgumentOutOfRangeException();
                }

                if (m_IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    m_AutoZoom = false;
                    if (m_Zoom != value)
                    {
                        m_Zoom = value;
                        ZoomChanged();
                    }
                }
                else
                {
                    m_Zoom = value;
                }
            }
        }

        /// <summary>Zoomsプロパティ</summary>
        [Category("表示")]
        [Description("ダブルクリックによるズーム時のページ拡大率を配列で示します。左クリックで配列内を右方向、右クリックで左方向の拡大率に遷移します。")]
        public double[] Zooms
        {
            get
            {
                return m_Zooms;
            }
            set
            {
                m_Zooms = value;
            }
        }

        /// <summary>GradePerZoomsプロパティ</summary>
        [Category("表示")]
        [Description("ダブルクリックによるズーム時の段階数を示します。各段階での拡大率は、Zoomsプロパティの両値の差を段階数で割った値になります。")]
        public int GradePerZooms
        {
            get
            {
                return m_GradePerZooms;
            }
            set
            {
                if ((value < 1) || (value > 10))
                {
                    // 1～10以外は無効
                    throw new System.ArgumentOutOfRangeException();
                }
                m_GradePerZooms = value;
            }
        }

        /// <summary>Columnsプロパティ</summary>
        [Category("表示")]
        [Description("横方向のページ数です。")]
        public int Columns
        {
            get
            {
                return m_Columns;
            }
            set
            {
                if (value < 1)
                {
                    // 1未満は無効
                    throw new System.ArgumentOutOfRangeException();
                }
                if (m_IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    if (m_Columns != value)
                    {
                        m_Columns = value;
                        ColumnsRowsChanged();
                    }
                }
                else
                {
                    m_Columns = value;
                }
            }
        }

        /// <summary>Rowsプロパティ</summary>
        [Category("表示")]
        [Description("縦方向のページ数です。")]
        public int Rows
        {
            get
            {
                return m_Rows;
            }
            set
            {
                if (value < 1)
                {
                    // 1未満は無効
                    throw new System.ArgumentOutOfRangeException();
                }
                if (m_IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合は内部イベント発生
                    if (m_Rows != value)
                    {
                        m_Rows = value;
                        ColumnsRowsChanged();
                    }
                }
                else
                {
                    m_Rows = value;
                }
            }
        }

        /// <summary>StartPageプロパティ</summary>
        [Category("表示")]
        [Description("プレビューを開始するページ番号を示します。")]
        public int StartPage
        {
            get
            {
                return m_StartPage;
            }
            set
            {
                if (value < 0)
                {
                    // 0以下は無効
                    throw new System.ArgumentOutOfRangeException();
                }
                if (m_IsInvalidatePreview == true)
                {
                    // プレビュー描画済みで、値に変更がある場合はイベント発生
                    if (m_StartPage != value)
                    {
                        m_StartPage = value;
                        OnStartPageChanged(new EventArgs());
                    }
                }
                else
                {
                    m_StartPage = value;
                }
            }
        }

        /// <summary>自動ズーム有無フィールド</summary>
        private bool m_AutoZoom;

        /// <summary>拡大率フィールド</summary>
        private double m_Zoom;

        /// <summary>既定拡大率フィールド</summary>
        private double[] m_Zooms;

        /// <summary>開始ページフィールド</summary>
        private int m_StartPage;

        /// <summary>ズーム時の段階数フィールド</summary>
        private int m_GradePerZooms;

        /// <summary>表示ページ列数フィールド</summary>
        private int m_Columns;

        /// <summary>表示ページ行数フィールド</summary>
        private int m_Rows;

        /// <summary>印刷プレビュー情報フィールド</summary>
        private PreviewPageInfo[] m_PageInfo;

        /// <summary>基本拡大率フィールド</summary>
        private double m_BaseZoom;

        /// <summary>表示させる範囲フィールド</summary>
        private Rectangle m_displayRectahgle;

        /// <summary>ドラッグ時の位置フィールド</summary>
        private Point m_DragPoint;

        /// <summary>画像の表示幅フィールド</summary>
        private int m_ImageDispWidth;

        /// <summary>画像の表示高さフィールド</summary>
        private int m_ImageDispHeight;

        /// <summary>プレビュー描画済み有無フィールド</summary>
        private bool m_IsInvalidatePreview;

        /// <summary>ページ分割毎の描画範囲フィールド</summary>
        private Rectangle[,] m_ColRowRectangle;

        /// <summary>StartPageChangedイベント</summary>
        public event EventHandler StartPageChanged;

        /// <summary>ZoomChangedイベント</summary>
        public event EventHandler ZoomsChanged;

        /// <summary>コンストラクタ</summary>
		public PrintPreviewPictureBox()
        {
            // メンバ変数初期化

            this.Document = null;
            m_GradePerZooms = 1;
            m_Columns = 1;
            m_Rows = 1;
            m_PageInfo = null;
            m_AutoZoom = false;//true;
            m_Zoom = 0.5D;//1D;
            m_BaseZoom = 0.5D;//1D;
            m_Zooms = null;
            m_StartPage = 0;
            m_displayRectahgle = Rectangle.Empty;
            m_DragPoint = Point.Empty;
            m_ImageDispWidth = 0;
            m_ImageDispHeight = 0;
            m_IsInvalidatePreview = false;
        }

        /// <summary>WndProcのオーバーライド</summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WmMouseWheel = 0x20A;

            if (m.Msg == WmMouseWheel)
            {
                // マウスホイールウィンドウメッセージの場合、内部イベント発生

                MouseWheelChaned(((int)m.WParam >> 16));
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        /// <summary>OnPaintのオーバーライド</summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_PageInfo != null)
            {
                if ((m_Columns == 1) && (m_Rows == 1))
                {
                    // ページ分割が無い場合

					// 背景を白で描画
					e.Graphics.FillRectangle(Brushes.White, m_displayRectahgle);

                    // 画像を指定された範囲に描画
                    e.Graphics.DrawImage(m_PageInfo[m_StartPage].Image, m_displayRectahgle);

					// 枠を黒で描画
					e.Graphics.DrawRectangle(Pens.Black, m_displayRectahgle);

                    if (m_AutoZoom == true)
                    {
                        // 自動ズームで1ページ表示の場合、拡大率に基本拡大率を設定
                        if (m_Zoom != m_BaseZoom)
                        {
                            m_Zoom = m_BaseZoom;
                        }
                    }
                }
                else if (m_AutoZoom == true)    
                {
                    // ページ分割有りで自動ズームの場合

                    // 分割値設定
                    double split = 1;
                    if (m_Rows <= m_Columns)
                    {
                        // 列数に列数分のオフセット値を加算
                        split = m_Columns;
                        split = split + ((SplitOffsetValue * split) / ((double)m_displayRectahgle.Width / split));
                    }
                    else
                    {
                        // 行数に行数分のオフセット値を加算
                        split = m_Rows;
                        split = split + ((SplitOffsetValue * split) / ((double)m_displayRectahgle.Height / split));
                    }

                    // 分割ページ毎に描画
                    int idx = 0;
                    for (int j = 0; j < m_Rows; j++)
                    {
                        for (int i = 0; i < m_Columns; i++)
                        {
                            // StartPageプロパティからのインデックス設定
                            idx = i + (j * m_Columns);
                            if ((m_StartPage + idx) > (m_PageInfo.Count() - 1))
                            {
                                break;
                            }
                            // 描画するページの位置・サイズを設定
                            Rectangle drawRectangle = Rectangle.Empty;
                            drawRectangle.Width = (int)Math.Round((double)m_displayRectahgle.Width / split);
                            drawRectangle.Height = (int)Math.Round((double)m_displayRectahgle.Height / split);
                            drawRectangle.X = m_displayRectahgle.X + (drawRectangle.Width + SplitOffsetValue) * i;
                            drawRectangle.Y = m_displayRectahgle.Y + (drawRectangle.Height + SplitOffsetValue) * j;

                            // 画像を指定された範囲に描画
                            e.Graphics.DrawImage(m_PageInfo[m_StartPage + idx].Image, drawRectangle);

                            // 枠を描画
                            e.Graphics.DrawRectangle(Pens.Black, drawRectangle);

                            // ページ分割後の拡大率を取得
                            double zoomX = drawRectangle.Width / (double)m_ImageDispWidth;
                            double zoomY = drawRectangle.Height / (double)m_ImageDispHeight;
                            double orgZoom = m_Zoom;
                            if (zoomX > zoomY)
                            {
                                m_Zoom = zoomY;
                            }
                            else
                            {
                                m_Zoom = zoomX;
                            }
                            // ページ分割毎の描画範囲取得
                            m_ColRowRectangle[i, j] = drawRectangle;
                        }
                    }
                }
                else
                {
                    // ページ分割有りで手動ズームの場合

                    // 分割ページ毎に描画
                    int idx = 0;
                    for (int j = 0; j < m_Rows; j++)
                    {
                        for (int i = 0; i < m_Columns; i++)
                        {
                            // StartPageプロパティからのインデックス設定
                            idx = i + (j * m_Columns);
                            if ((m_StartPage + idx) > (m_PageInfo.Count() - 1))
                            {
                                break;
                            }
                            // 描画するページの位置・サイズを設定
                            Rectangle drawRectangle = Rectangle.Empty;
                            drawRectangle.X = m_displayRectahgle.X + (m_displayRectahgle.Width + SplitOffsetValue) * i;
                            drawRectangle.Y = m_displayRectahgle.Y + (m_displayRectahgle.Height + SplitOffsetValue) * j;
                            drawRectangle.Width = m_displayRectahgle.Width;
                            drawRectangle.Height = m_displayRectahgle.Height;

                            // 画像を指定された範囲に描画
                            e.Graphics.DrawImage(m_PageInfo[m_StartPage + idx].Image, drawRectangle);

                            // 枠を描画
                            e.Graphics.DrawRectangle(Pens.Black, drawRectangle);

                            // ページ分割毎の描画範囲取得
                            m_ColRowRectangle[i, j] = drawRectangle;
                        }
                    }
                }
            }
            base.OnPaint(e);
        }

        /// <summary>OnMouseDoubleClickのオーバーライド</summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            try
            {
                if ((m_Zooms != null) && (m_Zooms.Count() > 0))
                {
                    // 既定拡大率が設定されている場合、ダブルクリックでズーム

                    // 座標オフセット値算出
                    int offsetX = 0;
                    int offsetY = 0;
                    if ((m_Rows > 1) || (m_Columns > 1))
                    {
                        // クリックした座標がページ分割毎の描画範囲のどこに属するかチェック
                        offsetX = -1;
                        offsetY = -1;
                        for (int j = 0; j < m_Rows; j++)
                        {
                            // 行方向へチェック
                            for (int i = 0; i < m_Columns; i++)
                            {
                                //　列方向へチェック
                                if ((e.X >= (m_ColRowRectangle[i, j].X - SplitOffsetValue)) &&
                                    (e.X <= (m_ColRowRectangle[i, j].X + m_ColRowRectangle[i, j].Width)))
                                {
                                    // X座標が描画範囲内の場合、Y座標もチェック
                                    if ((e.Y >= (m_ColRowRectangle[i, j].Y - SplitOffsetValue)) &&
                                        (e.Y <= (m_ColRowRectangle[i, j].Y + m_ColRowRectangle[i, j].Height)))
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
                            if (e.X > (m_ColRowRectangle[m_Columns - 1, 0].X + m_ColRowRectangle[m_Columns - 1, 0].Width))
                            {
                                // 大外より大きい場合
                                offsetX = (m_Columns - 1) * SplitOffsetValue;
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
                            if (e.Y > (m_ColRowRectangle[0, m_Rows - 1].Y + m_ColRowRectangle[0, m_Rows - 1].Height))
                            {
                                // 大外より大きい場合
                                offsetY = (m_Rows - 1) * SplitOffsetValue;
                            }
                            else
                            {
                                // 大内より小さい場合
                                offsetY = 0;
                            }
                        }
                    }
                    // クリックされた位置を画像上の位置に変換
                    Point imgPoint = new Point((int)Math.Round((e.X - offsetX - m_displayRectahgle.X) / (m_Zoom)),
                                               (int)Math.Round((e.Y - offsetY - m_displayRectahgle.Y) / (m_Zoom)));
                     
                    // 一番近い既定拡大率を取得
                    double orgZoom = m_Zoom;
                    if (m_Zoom > m_Zooms[m_Zooms.Count() - 1])
                    {
                        // 既定拡大率の最大値より大きい場合、既定拡大率の最大値
                        if (e.Button == MouseButtons.Right)
                        {
                            m_Zoom = m_Zooms[m_Zooms.Count() - 1];
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (m_Zoom == m_Zooms[m_Zooms.Count() - 1])
                    {
                        // 既定拡大率の最大値の場合、既定拡大率の最大値の次拡大率
                        if (e.Button == MouseButtons.Right)
                        {
                            if (m_Zooms.Count() < 2)
                            {
                                return;
                            }
                            m_Zoom = m_Zooms[m_Zooms.Count() - 1 - 1];
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (m_Zoom < m_Zooms[0])
                    {
                        // 既定拡大率の最小値より小さい場合、既定拡大率の最小値
                        if (e.Button == MouseButtons.Left)
                        {
                            m_Zoom = m_Zooms[0];
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (m_Zoom == m_Zooms[0])
                    {
                        // 既定拡大率の最小値の場合、既定拡大率の最小値の次拡大率
                        if (e.Button == MouseButtons.Left)
                        {
                            if (m_Zooms.Count() < 2)
                            {
                                return;
                            }
                            m_Zoom = m_Zooms[0 + 1];
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        // 既定拡大率内の場合、どの既定拡大率間の値か
                        for (int i = 0; i < (m_Zooms.Count() - 1); i++)
                        {
                            if (m_Zoom >= m_Zooms[i])
                            {
                                // 既定拡大率以上の場合、次拡大率より小さいか
                                if (m_Zoom < m_Zooms[i + 1])
                                {
                                    int idx;
                                    // 既定拡大率間のうち、どちらに近いか
                                    if (m_Zoom < (m_Zooms[i] + m_Zooms[i + 1]) / 2)
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
                                        m_Zoom = m_Zooms[idx + 1];
                                    }
                                    else
                                    {
                                        m_Zoom = m_Zooms[idx - 1];
                                    }
                                    break;
                                }
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
            }
            finally
            {
                base.OnMouseDoubleClick(e);
            }
        }

        /// <summary>OnMouseDownのオーバーライド</summary>
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
                m_DragPoint = new Point((int)Math.Round((e.X - m_displayRectahgle.X) / m_Zoom),
                                        (int)Math.Round((e.Y - m_displayRectahgle.Y) / m_Zoom));

                // マウスカーソルをハンドに設定
                Cursor.Current = Cursors.Hand;
            }
            base.OnMouseDown(e);
        }

        /// <summary>OnMouseMoveのオーバーライド</summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Cursor.Current == Cursors.Hand)
            {
                // ドロップされた位置を画像上の位置に変換
                Point imgPoint = new Point((int)Math.Round((e.X - m_displayRectahgle.X) / m_Zoom),
                                           (int)Math.Round((e.Y - m_displayRectahgle.Y) / m_Zoom));

                // 画像の表示範囲を計算する
                m_displayRectahgle.X = m_displayRectahgle.X + (int)Math.Round((imgPoint.X - m_DragPoint.X) * m_Zoom);
                m_displayRectahgle.Y = m_displayRectahgle.Y + (int)Math.Round((imgPoint.Y - m_DragPoint.Y) * m_Zoom);

                // 画像を表示する
                this.Refresh();
            }
            base.OnMouseMove(e);
        }

        /// <summary>OnMouseUpのオーバーライド</summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // マウスカーソルを既定に設定
                Cursor.Current = Cursors.Default;
            }
            base.OnMouseUp(e);
        }

        /// <summary>OnResizeのオーバーライド</summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (m_PageInfo != null)
            {
                if (m_AutoZoom == true)
                {
                    // 自動ズームの場合、リサイズ後の拡大率・描画座標を算出

                    // 描画拡大率を取得
                    double zoomX = ((double)this.Width - 20) / (double)m_ImageDispWidth;
                    double zoomY = ((double)this.Height -20) / (double)m_ImageDispHeight;
                    if (zoomX > zoomY)
                    {
                        m_BaseZoom = zoomY;
                    }
                    else
                    {
                        m_BaseZoom = zoomX;
                    }
                    m_Zoom = m_BaseZoom;

                    // 描画範囲を中央表示で設定
                    m_displayRectahgle.X = (int)Math.Round((this.Width - m_ImageDispWidth * m_Zoom) / 2);
                    m_displayRectahgle.Y = (int)Math.Round((this.Height - m_ImageDispHeight * m_Zoom) / 2);
                }
                m_displayRectahgle.Width = (int)Math.Round(m_ImageDispWidth * m_Zoom);
                m_displayRectahgle.Height = (int)Math.Round(m_ImageDispHeight * m_Zoom);

                // 画像を再描画する
                this.Refresh();
            }
            base.OnResize(e);
        }

        /// <summary>StartPageChangedイベントを発生させる</summary>
        /// <param name="e"></param>
        protected virtual void OnStartPageChanged(EventArgs e)
        {
            // 画像を描画する
            this.Refresh();

            if (StartPageChanged != null)
            {
                // StartPageChangedイベント発生
                StartPageChanged(this, e);
            }
        }

        /// <summary>Columns・Rowsプロパティ変更内部イベント</summary>
        private void ColumnsRowsChanged()
        {
            // ページ分割毎の描画範囲フィールド作成
            m_ColRowRectangle = new Rectangle[m_Columns, m_Rows];

            // 再描画
            this.Refresh();
        }

        /// <summary>AutoZoomプロパティ変更内部イベント</summary>
        private void AutoZoomChanged()
        {
            if (m_PageInfo == null)
            {
                return;
            }
            if (m_AutoZoom == true)
            {
                // 描画範囲を中央表示で設定
                m_displayRectahgle.X = (int)Math.Round((this.Width - m_ImageDispWidth * m_BaseZoom) / 2);
                m_displayRectahgle.Y = (int)Math.Round((this.Height - m_ImageDispHeight * m_BaseZoom) / 2);
                m_displayRectahgle.Width = (int)Math.Round(m_ImageDispWidth * m_BaseZoom);
                m_displayRectahgle.Height = (int)Math.Round(m_ImageDispHeight * m_BaseZoom);
                
                // 拡大率を基準拡大率に設定
                m_Zoom = m_BaseZoom;

                // 再描画
                this.Refresh();
            }
        }

        /// <summary>Zoomプロパティ変更内部イベント</summary>
        private void ZoomChanged()
        {
            if (m_PageInfo == null)
            {
                return;
            }
            // 変更後拡大率での描画サイズ設定
            m_displayRectahgle.Width = (int)Math.Round(m_ImageDispWidth * m_Zoom);
            m_displayRectahgle.Height = (int)Math.Round(m_ImageDispHeight * m_Zoom);

            // 再描画
            this.Refresh();
        }

        /// <summary>MouseWheel変更メッセージ内部イベント</summary>
        /// <param name="delta">ホイール移動量</param>
        private void MouseWheelChaned(int delta)
        {
            // マウスホイールの移動量から移動ページ数取得
            int page = delta / -120;

            // 現在ページから移動ページ数分の移動
            if ((m_StartPage + page) < 0)
            {
                page = 0;
            }
            else if ((m_StartPage + page) >= m_PageInfo.Count())
            {
                return;
            }
            m_StartPage += page;
        }

        /// <summary>段階ズームスレッド</summary>
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
                for (int i = 1; i <= m_GradePerZooms; i++)
                {
                    Thread.Sleep(50);

                    // 拡大率算出
                    double zoom = orgZoom + ((m_Zoom - orgZoom) / m_GradePerZooms) * i;
                    if (zoom > m_Zoom)
                    {
                        zoom = m_Zoom;
                    }
                    // クリックされた位置を画像上の位置に変換（拡大率変換後）
                    Point imgZoomPoint = new Point((int)Math.Round((e.X - offsetX - m_displayRectahgle.X) / (zoom)),
                                                   (int)Math.Round((e.Y - offsetY - m_displayRectahgle.Y) / (zoom)));
                    
                    // 画像の表示範囲を計算する
                    m_displayRectahgle.Width = (int)Math.Round(m_ImageDispWidth * zoom);
                    m_displayRectahgle.Height = (int)Math.Round(m_ImageDispHeight * zoom);
                    m_displayRectahgle.X = m_displayRectahgle.X + (int)Math.Round((imgZoomPoint.X - imgPoint.X) * zoom);
                    m_displayRectahgle.Y = m_displayRectahgle.Y + (int)Math.Round((imgZoomPoint.Y - imgPoint.Y) * zoom);

                    // 再描画
                    this.Invalidate();
                }
            }
            else
            {
                // 縮小
                for (int i = 1; i <= m_GradePerZooms; i++)
                {
                    Thread.Sleep(50);

                    // 拡大率算出
                    double zoom = orgZoom - ((orgZoom - m_Zoom) / m_GradePerZooms) * i;
                    if (zoom < m_Zoom)
                    {
                        zoom = m_Zoom;
                    }
                    // クリックされた位置を画像上の位置に変換（拡大率変換後）
                    Point imgZoomPoint = new Point((int)Math.Round((e.X - offsetX - m_displayRectahgle.X) / (zoom)),
                                                    (int)Math.Round((e.Y - offsetY - m_displayRectahgle.Y) / (zoom)));

                    // 画像の表示範囲を計算する
                    m_displayRectahgle.Width = (int)Math.Round(m_ImageDispWidth * zoom);
                    m_displayRectahgle.Height = (int)Math.Round(m_ImageDispHeight * zoom);
                    m_displayRectahgle.X = m_displayRectahgle.X + (int)Math.Round((imgZoomPoint.X - imgPoint.X) * zoom);
                    m_displayRectahgle.Y = m_displayRectahgle.Y + (int)Math.Round((imgZoomPoint.Y - imgPoint.Y) * zoom);

                    // 再描画
                    this.Invalidate();
                }
            }
        }

        /// <summary>プレビュー描画</summary>
        public void InvalidatePreview()
        {
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

                m_PageInfo = con.GetPreviewPageInfo();

                // イメージのディスプレイ上の実サイズを取得
                Graphics g = this.CreateGraphics();
                m_ImageDispWidth = (int)Math.Round((m_PageInfo[0].Image.Width / m_PageInfo[0].Image.HorizontalResolution) * g.DpiX);
                m_ImageDispHeight = (int)Math.Round((m_PageInfo[0].Image.Height / m_PageInfo[0].Image.VerticalResolution) * g.DpiY);

                // 描画拡大率を取得
                double zoomX = ((double)this.Width - 20) / (double)m_ImageDispWidth;
                double zoomY = ((double)this.Height - 20) / (double)m_ImageDispHeight;
                if (zoomX > zoomY)
                {
                    m_BaseZoom = zoomY;
                }
                else
                {
                    m_BaseZoom = zoomX;
                }
                //m_Zoom = m_BaseZoom;

                // 描画範囲を中央表示で設定
                m_displayRectahgle.X = 4;//(int)Math.Round((this.Width - m_ImageDispWidth * m_Zoom) / 2);
                m_displayRectahgle.Y = 4;//(int)Math.Round((this.Height - m_ImageDispHeight * m_Zoom) / 2);
                m_displayRectahgle.Width = (int)Math.Round(m_ImageDispWidth * m_Zoom);
                m_displayRectahgle.Height = (int)Math.Round(m_ImageDispHeight * m_Zoom);
 
                // 画像を描画する
                this.Refresh();

                // プレビュー描画済み
                m_IsInvalidatePreview = true;
            }
            finally
            {
                // マウスカーソルを規定に設定
                Cursor.Current = Cursors.Default;
            }
        }

        // 外部参照メソッド
        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);
	}
}
