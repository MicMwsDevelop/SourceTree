//
// PrintPreviewForm.cs
// 
// 印刷プレビュー画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using CommonDialog.Progress;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace CommonDialog.PrintPreview
{
	/// <summary>
	/// 印刷プレビューダイアログ
	/// </summary>
	public partial class PrintPreviewForm : Form
    {
        /// <summary>
        /// PrintDocumentオブジェクトプロパティ
        /// </summary>
        public PrintDocument Document
        {
            get
            {
                return printPreviewControl.Document;
            }
            set
            {
                printPreviewControl.Document = value;
                // ドキュメント名設定
                if ((printPreviewControl.Document.DocumentName == "document") || (printPreviewControl.Document.DocumentName == ""))
                {
                    printPreviewControl.Document.DocumentName = "無題";
                }
            }
        }

        /// <summary>
        /// プレビュー表示のみかどうかプロパティ
        /// </summary>
        public bool IsPreviewOnly
        {
            get
            {
                return !printButton.Enabled;
            }
            set
            {
                printButton.Enabled = !value;
            }
        }

        /// <summary>
        /// 最大ページ数プロパティ
        /// </summary>
        public int MaxPage { get; set; }

        /// <summary>
        /// 1ページ毎の表示数プロパティ
        /// </summary>
        public int DispPerPage { get; set; }

        /// <summary>現在印刷ページ番号</summary>
        private int CurrentPrintPage;
        /// <summary>最終印刷ページ番号</summary>
        private int LastPrintPage;
        /// <summary>最大表示ページ数</summary>
        private int MaxDispPage;
        /// <summary>最大表示印刷ページ数</summary>
        private int MaxDispPrintPage;
        /// <summary>イベント登録有無フィールド</summary>
        private bool IsPrintEventRegistered;
        /// <summary>プログレスダイアログイベント引数</summary>
        private ProgressForm.ProgressEventArgs ProgEventArgs;
        /// <summary>基本引数</summary>
        //private BaseArgs Args;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrintPreviewForm()
        {
            InitializeComponent();

            // メンバ変数初期化
            MaxDispPage = 0;
            CurrentPrintPage = 0;
            LastPrintPage = 0;
            DispPerPage = 1;
            MaxDispPrintPage = 0;
            IsPrintEventRegistered = false;
            MaxPage = 1;
            printPreviewControl.Document = null;
            IsPreviewOnly = false;
            //ProgEventArgs = null;
            //Args = args;
        }

        /// <summary>
        /// PrintEventHandlerデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PrintEventHandler(object sender, PrintEventArgs e);

        /// <summary>
        /// PrintPageEventHandlerデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="page"></param>
        public delegate void PrintPageEventHandler(object sender, PrintPageEventArgs e, int page);

        /// <summary>
        /// QueryPageSettingsEventHandlerデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void QueryPageSettingsEventHandler(object sender, QueryPageSettingsEventArgs e);

        /// <summary>
        /// BeginPrintイベント
        /// </summary>
        public event PrintEventHandler BeginPrint;

        /// <summary>
        /// QueryPageSettingsイベント
        /// </summary>
        public event QueryPageSettingsEventHandler QueryPageSettings;

        /// <summary>
        /// PrintPageイベント
        /// </summary>
        public event PrintPageEventHandler PrintPage;

        /// <summary>
        /// EndPrintイベント
        /// </summary>
        public event PrintEventHandler EndPrint;

        /// <summary>
        /// Loadイベント
        /// </summary>
        private void PrintPreviewForm_Load(object sender, EventArgs e)
        {
            // PrintDocumentコントロールのインスタンスチェック
            if (printPreviewControl.Document == null)
            {
                throw new ApplicationException("ドキュメントにページが含まれていません。");
            }

            // Printイベント登録
            PrintEventRegistered();

            // カレントページクリア
            currentPageTextBox.Text = "1";

            // プレビュー表示倍率選択（ズーム（自動））
            zoomAutoRadioButton.Checked = true;

            // プレビュー表示ページ数選択（1ページ表示）
            disp1RadioButton.Checked = true;
            MaxDispPage = MaxPage;
            MaxDispPrintPage = MaxPage;

            // プレビュー表示最大ページ数設定
            maxPageLabel.Text = "/" + MaxPage + " ページ";

            // プレビュー表示次ページボタン位置設定
            nextPageButton.Location = new Point(maxPageLabel.Location.X + maxPageLabel.Width + 5, 4);
            
            // プレビュー表示次ページ使用不可設定
            if (MaxPage == 1)
            {
                nextPageButton.Enabled = false;
            }
            else if (MaxPage > 1)
            {
                nextPageButton.Enabled = true;
            }
            
            // プレビュー表示垂直スクロールバー設定
            printPreviewVScrollBar.Minimum = 1;
            printPreviewVScrollBar.Maximum = MaxDispPage;
            printPreviewVScrollBar.SmallChange = 1;
            printPreviewVScrollBar.LargeChange = 1;
            printPreviewVScrollBar.Value = 1;

            // 10ページ以上の場合はプログレスダイアログを表示
            if (MaxPage >= 10)
            {
                this.Show();

                // プログレスダイアログの生成
                ProgressForm pf = new ProgressForm();
                pf.Text = this.Text;
                pf.ProgressEvent += new ProgressForm.ProgressEvent_Handler(ProgressForm_Event);
                // 対象月のレセプト件数をプログレスバーの最大値に設定
                pf.Maximum = MaxPage;

                // プログレスダイアログの表示（＝プレビュー生成開始）
                if (pf.ShowDialog(this) == DialogResult.Cancel)
                {
                    // プログレスダイアログ上からキャンセルされた場合、終了
                    DialogResult = DialogResult.Cancel;
                    ProgEventArgs = null;
                    return;
                }
                ProgEventArgs = null;
            }
            else
            {
                //プレビュー描画
                printPreviewControl.InvalidatePreview();
            }

            // 指定表示倍率毎にZoomTrackBar経由でPrintPreviewMultiControlのZoomプロパティ設定
            zoomTrackBar.Value = (int)Math.Round(printPreviewControl.Zoom * 100);
            // 選択倍率を表示
            zoomLabel.Text = zoomTrackBar.Value.ToString() + "%";

            // ページ毎表示数プロパティが規定(1)でない場合、該当するものを選択
            if (DispPerPage > 1)
            {
                int row = 1;
                int col = 1;
                foreach (RadioButton rb in dispGroupBox.Controls)
                {
                    // Tagプロパティの行・列値取得
                    string[] tag = rb.Tag.ToString().Split(',');
                    row = int.Parse(tag[0]);
                    col = int.Parse(tag[1]);
                    if (DispPerPage == (row * col))
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }

            // 画像を描画する
            printPreviewControl.Refresh();

            // 初期フォーカス設定
            // Ver1.053(2016/11/23):印刷プレビュー画面で「ENTER」キーで印刷できない (Bug 15054)
            //printPreviewControl.Focus();
            printButton.Focus();
        }

        /// <summary>
        /// FormClosedイベント
        /// </summary>
        private void PalettePrintPreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Printイベント解除
            PrintEventUnRegistered();
        }

        /// <summary>
        /// PrintDocument BeginPrintイベント
        /// </summary>
        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            if (ProgEventArgs != null) ProgEventArgs.Message = "プレビュー生成中";

            // イベント発生
            if (this.BeginPrint != null)
            {
                this.BeginPrint(this, e);
            }

            // このプロパティに値を設定しない限り、参照するたびに内部的にプリンタから値が取得される。
            // このプロパティを設定することにより、プロパティ参照時にプリンタからの再取得は行われずに設定した値が返されるようになる
            //printPreviewControl.Document.DefaultPageSettings.SetHdevmode(printPreviewControl.Document.DefaultPageSettings.getGetHdevmode());
            //printPreviewControl.Document.PrinterSettings.SetHdevmode(printPreviewControl.Document.PrinterSettings.GetHdevmode());
            //printPreviewControl.Document.PrinterSettings.SetHdevnames(printPreviewControl.Document.PrinterSettings.GetHdevnames());

            // 現在ページ番号・最終ページ番号設定
            if (e.PrintAction == PrintAction.PrintToPreview)
            {
                // プレビュー表示
                CurrentPrintPage = 0;
                LastPrintPage = MaxPage;
            }
            else
            {
                // PrintDocumentより印刷開始・終了ページの取得
                int fromPage = printPreviewControl.Document.DefaultPageSettings.PrinterSettings.FromPage;
                int toPage = printPreviewControl.Document.DefaultPageSettings.PrinterSettings.ToPage;

                // 印刷範囲ごとに設定
                switch (printPreviewControl.Document.DefaultPageSettings.PrinterSettings.PrintRange)
                {
                    case PrintRange.CurrentPage:    // 現在のページ
                        CurrentPrintPage = fromPage - 1;
                        LastPrintPage = fromPage;
                        break;
                    case PrintRange.SomePages:      // ページ指定
                        CurrentPrintPage = fromPage - 1;
                        LastPrintPage = toPage;
                        break;
                    case PrintRange.AllPages:       // 全てのページ（デフォルト）
                    default:
                        CurrentPrintPage = 0;
                        LastPrintPage = MaxPage;
                        break;
                }
            }
        }

        /// <summary>
        /// PrintDocumentのQueryPageSettingsイベント
        /// </summary>
        private void PrintDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            // イベント発生
            if (this.QueryPageSettings != null)
            {
                this.QueryPageSettings(this, e);
            }
        }

        /// <summary>
        /// PrintDocument PrintPageイベント
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (ProgEventArgs != null)
            {
                if (ProgEventArgs.IsCancel)
                {
                    // プログレスダイアログ上からキャンセルされた場合、終了
                    e.HasMorePages = false;
                    return;
                }
                ProgEventArgs.ProgressValue = CurrentPrintPage;
            }

            // 現在ページ番号設定
            CurrentPrintPage++;

            // 印刷継続
            e.HasMorePages = true;

            // イベント発生
            if (this.PrintPage != null)
            {
                this.PrintPage(this, e, this.CurrentPrintPage);
            }

            if (CurrentPrintPage >= MaxPage)
            {
                // 最大ページ数を超えた場合、印刷終了
                e.HasMorePages = false;
            }
            else if (CurrentPrintPage >= LastPrintPage)
            {
                // 最終印刷ページ番号を超えた場合、印刷終了
                e.HasMorePages = false;
            }
        }

        /// <summary>
        /// PrintDocument EndPrintイベント
        /// </summary>
        private void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            if (ProgEventArgs != null)
            {
                if (ProgEventArgs.IsCancel)
                {
                    // プログレスダイアログ上からキャンセルされた場合、終了
                    return;
                }
            }
            
            if (e.PrintAction == PrintAction.PrintToPreview)
            {
                // 最大ページ数と実際の描画ページ数が違う場合、実際の描画ページ数でプレビュー表示関連のみ再設定
                // （印刷系については、印刷時に再度描画処理が発生するため再設定しない）
                if (this.CurrentPrintPage != MaxPage)
                {
                    // 最大表示ページ数設定
                    MaxDispPage = this.CurrentPrintPage;
                    // 最大表示印刷ページ数設定
                    MaxDispPrintPage = this.CurrentPrintPage;

                    // プレビュー表示最大ページ数設定
                    maxPageLabel.Text = "/" + this.CurrentPrintPage + " ページ";
                    // プレビュー表示次ページボタン位置設定
                    nextPageButton.Location = new Point(maxPageLabel.Location.X + maxPageLabel.Width + 5, 4);
                    // dispRadioButtonのCheckedChangedイベント同等
                    dispRadioButton_CheckedChanged(null, null);
                }
            }
            
            // イベント発生
            if (this.EndPrint != null)
            {
                this.EndPrint(this, e);
            }
        }

        /// <summary>
        /// 印刷ボタン Clickイベント
        /// </summary>
        private void printButton_Click(object sender, EventArgs e)
        {
            ProgEventArgs = null;

            // 印刷実行            
            printPreviewControl.Document.Print();

            // ダイアログクローズ
            DialogResult = DialogResult.OK;    
        }

        /// <summary>
        /// 印刷ボタン EnabledChangedイベント
        /// </summary>
        private void printButton_EnabledChanged(object sender, EventArgs e)
        {
            if (printButton.Enabled)
            {
                // ボタンの背景色を標準に設定
                printButton.BackColor = Color.GhostWhite;
            }
            else
            {
                // ボタンの背景色をシステム指定に設定（文字色を灰色表示するため）
                printButton.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// ズームトラックバー Scrollイベント
        /// </summary>
        private void ZoomTrackBar_Scroll(object sender, EventArgs e)
        {
            // PrintPreviewMultiControlのZoom値更新
            double val = (double)zoomTrackBar.Value;
            printPreviewControl.Zoom = (val * 0.01);

            // ZoomTrackBarの設定値を表示
            zoomLabel.Text = zoomTrackBar.Value.ToString() + "%";
            // ZoomManualRadioButtonの選択倍率を「手動」に
            zoomManualRadioButton.Checked = true;
        }

        /// <summary>
        /// ズームラジオボタン CheckedChangedイベント
        /// </summary>
        private void zoomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // 指定表示倍率のZoom値設定
            int zoom = 100;
            foreach (RadioButton rb in zoomGroupBox.Controls)
            {
                if (rb.Checked)
                {
                    if (rb.Equals(zoomAutoRadioButton))
                    {
                        // 自動
                        printPreviewControl.AutoZoom = true;

                        // ZoomTrackBarの最小・最大値を超える場合、最小・最大値を設定
                        zoom = (int)Math.Round(printPreviewControl.Zoom * 100);
                        if (zoom < zoomTrackBar.Minimum)
                        {
                            zoom = zoomTrackBar.Minimum;
                        }
                        else if (zoom > zoomTrackBar.Maximum)
                        {
                            zoom = zoomTrackBar.Maximum;
                        }

                        zoomTrackBar.Value = zoom;
                        zoomLabel.Text = zoomTrackBar.Value.ToString() + "%";
                        return;
                    }
                    else if (rb.Equals(zoomManualRadioButton))
                    {
                        // 手動
                        printPreviewControl.AutoZoom = false;
                        zoomLabel.Text = zoomTrackBar.Value.ToString() + "%";
                        return;
                    }
                    else
                    {   // 10%～500%
                        zoom = int.Parse(rb.Tag.ToString());
                    }
                    break;
                }
            }

            // 指定表示倍率毎にZoomTrackBar経由でPrintPreviewMultiControlのZoomプロパティ設定
            zoomTrackBar.Value = zoom;

            // PrintPreviewMultiControlのZoom値更新
            printPreviewControl.Zoom = ((double)zoom * 0.01);

            // 選択倍率を表示
            zoomLabel.Text = zoom.ToString() + "%";
        }

        /// <summary>
        /// 表示ページ数ラジオボタン CheckedChangedイベント
        /// </summary>
        private void dispRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // 表示ページ数毎にPrintPreviewMultiControlの行・列プロパティ設定
            int row = 1;
            int col = 1;
            foreach (RadioButton rb in dispGroupBox.Controls)
            {
                if (rb.Checked)
                {
                    // Tagプロパティの行・列値取得
                    string[] tag = rb.Tag.ToString().Split(',');
                    row = int.Parse(tag[0]);
                    col = int.Parse(tag[1]);
                    break;
                }
            }
            printPreviewControl.Rows = row;
            printPreviewControl.Columns = col;

            // 1ページ毎の表示数設定
            DispPerPage = col * row;

            // 最大表示ページ数設定
            if (MaxDispPrintPage < DispPerPage)
            {
                MaxDispPage = 1;
            }
            else
            {
                MaxDispPage = MaxDispPrintPage - DispPerPage + 1;
            }

            // プレビュー垂直スクロールバーの最大値・現在値を設定
            printPreviewVScrollBar.Maximum = MaxDispPage;
            if ((printPreviewControl.StartPage + 1) > MaxDispPage)
            {
                // PrintPreviewMultiControlの表示ページが最大表示ページ数を超えていた場合、最大表示ページ数に合わせる
                printPreviewControl.StartPage = MaxDispPage - 1;
            }
            printPreviewVScrollBar.Value = printPreviewControl.StartPage + 1;
            currentPageTextBox.Text = (printPreviewControl.StartPage + 1).ToString();

            // ページ移動ボタン使用可-不可設定
            if (currentPageTextBox.Text == "1")
            {
                if (int.Parse(currentPageTextBox.Text) == MaxDispPage)
                {
                    prevPageButton.Enabled = false;
                    nextPageButton.Enabled = false;
                }
                else
                {
                    prevPageButton.Enabled = false;
                    nextPageButton.Enabled = true;
                }
            }
            else if (int.Parse(currentPageTextBox.Text) == MaxDispPage)
            {
                prevPageButton.Enabled = true;
                nextPageButton.Enabled = false;
            }
            else
            {
                prevPageButton.Enabled = true;
                nextPageButton.Enabled = true;
            }

            if (printPreviewControl.AutoZoom == true)
            {
                int zoom = (int)Math.Round(printPreviewControl.Zoom * 100);
                // ZoomTrackBarの最小・最大値を超える場合、最小・最大値を設定
                if (zoom < zoomTrackBar.Minimum)
                {
                    zoom = zoomTrackBar.Minimum;
                }
                else if (zoom > zoomTrackBar.Maximum)
                {
                    zoom = zoomTrackBar.Maximum;
                }

                // 指定表示倍率毎にZoomTrackBar経由でPrintPreviewMultiControlのZoomプロパティ設定
                zoomTrackBar.Value = zoom;
                // 選択倍率を表示
                zoomLabel.Text = zoomTrackBar.Value.ToString() + "%";
            }
        }

        /// <summary>
        /// 前ページボタン Clickイベント
        /// </summary>
        private void prevPageButton_Click(object sender, EventArgs e)
        {
            // PalettePrintPreviewControl現ページの前ページを設定
            currentPageTextBox.Text = (printPreviewControl.StartPage + 1 - 1).ToString();
        }

        /// <summary>
        /// 次ページボタン Clickイベント
        /// </summary>
        private void nextPageButton_Click(object sender, EventArgs e)
        {
            // PalettePrintPreviewControl現ページの次ページを設定
            currentPageTextBox.Text = (printPreviewControl.StartPage + 1 + 1).ToString();
        }

        /// <summary>
        /// 現在ページ Leaveイベント
        /// </summary>
        private void currentPageTextBox_Leave(object sender, EventArgs e)
        {
            if (currentPageTextBox.Text == "")
            {
                // 空の場合は現在ページを表示
                currentPageTextBox.Text = (printPreviewControl.StartPage + 1).ToString();
            }
        }

        /// <summary>
        /// 現在ページ TextChangedイベント
        /// </summary>
        private void currentPageTextBox_TextChanged(object sender, EventArgs e)
        {
            // 現在ページの入力値チェック（不正な場合は変更前のページを設定）
            int num;
            if (currentPageTextBox.Text == "")
            {
                // 空文字
                return;
            }
            else if (int.TryParse(currentPageTextBox.Text, out num) == false)
            {
                // int型以外
                currentPageTextBox.Text = (printPreviewControl.StartPage + 1).ToString();
                return;
            }
            else if (int.Parse(currentPageTextBox.Text) > MaxDispPage)
            {
                // 最大表示ページ数より大きい
                currentPageTextBox.Text = (printPreviewControl.StartPage + 1).ToString();
                return;
            }
            else if (int.Parse(currentPageTextBox.Text) < 1)
            {
                // 1より小さい
                currentPageTextBox.Text = (printPreviewControl.StartPage + 1).ToString();
                return;
            }

            // プレビューの表示ページ更新
            printPreviewControl.StartPage = int.Parse(currentPageTextBox.Text) - 1;

            // ページ移動ボタン使用可-不可設定
            if (currentPageTextBox.Text == "1")
            {
                if (int.Parse(currentPageTextBox.Text) == MaxDispPage)
                {
                    // 先頭ページが最大表示ページ
                    prevPageButton.Enabled = false;
                    nextPageButton.Enabled = false;
                }
                else
                {
                    prevPageButton.Enabled = false;
                    nextPageButton.Enabled = true;
                }
            }
            else if (int.Parse(currentPageTextBox.Text) == MaxDispPage)
            {
                // 最大表示ページ
                prevPageButton.Enabled = true;
                nextPageButton.Enabled = false;
            }
            else
            {
                prevPageButton.Enabled = true;
                nextPageButton.Enabled = true;
            }

            // プレビュー垂直スクロールバーの現在値更新
            printPreviewVScrollBar.Value = printPreviewControl.StartPage + 1;
        }

        /// <summary>
        /// プレビューコントール AutoZoomChangedイベント
        /// </summary>
        private void printPreviewControl_AutoZoomChanged(object sender, EventArgs e)
        {
            zoomAutoRadioButton.Checked = printPreviewControl.AutoZoom;
        }

        /// <summary>
        /// プレビュー垂直スクロールバー Scrollイベント
        /// </summary>
        private void printPreviewVScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // プレビュー垂直スクロールバーの現在値を現在ページに設定
            printPreviewControl.StartPage = e.NewValue - 1;
            currentPageTextBox.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// プレビューコントール StartPageChangedイベント
        /// </summary>
        private void printPreviewControl_StartPageChanged(object sender, EventArgs e)
        {
            // 現在ページ番号エディットボックスの入力値をプレビューコントロールと一致させる
            if (printPreviewControl.StartPage < printPreviewVScrollBar.Maximum)
            {
                printPreviewVScrollBar.Value = printPreviewControl.StartPage + 1;
                currentPageTextBox.Text = printPreviewVScrollBar.Value.ToString();
            }
        }

        /// <summary>
        /// プレビューコントール ZoomsChangedイベント
        /// </summary>
        private void printPreviewControl_ZoomsChanged(object sender, EventArgs e)
        {
            int idx = 2;
            for (int i = printPreviewControl.Zooms.Count() - 1; i >= 0; i--)
            {
                if (printPreviewControl.Zoom == printPreviewControl.Zooms[i])
                {
                    ((RadioButton)zoomGroupBox.Controls[idx]).Checked = true;
                }
                idx++;
            }

            zoomManualRadioButton.Checked = true;
        }

        /// <summary>
        /// プレビューコントール Resizeイベント
        /// </summary>
        private void printPreviewControl_Resize(object sender, EventArgs e)
        {
            // ZoomTrackBarの最小・最大値を超える場合、最小・最大値を設定
            int zoom = (int)Math.Round(printPreviewControl.Zoom * 100);
            if (zoom < zoomTrackBar.Minimum)
            {
                zoom = zoomTrackBar.Minimum;
            }
            else if (zoom > zoomTrackBar.Maximum)
            {
                zoom = zoomTrackBar.Maximum;
            }

            // 指定表示倍率毎にZoomTrackBar経由でPalettePrintPreviewControlのZoomプロパティ設定
            zoomTrackBar.Value = zoom;
            // 選択倍率を表示
            zoomLabel.Text = zoomTrackBar.Value.ToString() + "%";

            zoomTrackBar.Location = new Point(printPreviewControl.Width - zoomTrackBar.Width, zoomTrackBar.Location.Y);
            zoomLabel.Location = new Point(zoomTrackBar.Location.X - 28, zoomLabel.Location.Y);
        }

        /// <summary>
        /// 操作パネル Resizeイベント
        /// </summary>
        private void LeftPanel_Resize(object sender, EventArgs e)
        {
            // 右線位置の調整
            if (LeftPanel.Height > (closeButton.Location.Y + closeButton.Height))
            {
                // LeftPanelが規定サイズを超えている場合、サイズに合わせる
                rightLineLabel.Height = LeftPanel.Height;

                // 自動スクロール不可
                LeftPanel.AutoScroll = false;
            }
            else
            {
                // LeftPanelが規定サイズ内の場合、既定サイズ
                rightLineLabel.Height = 668;

                // 自動スクロール可
                LeftPanel.AutoScroll = true;
            }
        }

        /// <summary>
        /// Print関連イベント登録
        /// </summary>
        private void PrintEventRegistered()
        {
            // Printイベント未登録の場合、イベント登録
            if (IsPrintEventRegistered == false)
            {
                // PalettePrintPreviewControl.DocumentオブジェクトのBeginPrintイベント登録
                printPreviewControl.Document.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
                // PalettePrintPreviewControl.DocumentオブジェクトのQueryPageSettingsイベント登録
                printPreviewControl.Document.QueryPageSettings += new System.Drawing.Printing.QueryPageSettingsEventHandler(PrintDocument_QueryPageSettings);
                // PalettePrintPreviewControl.DocumentオブジェクトのPrintPageイベント登録
                printPreviewControl.Document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
                // PalettePrintPreviewControl.DocumentオブジェクトのEndPrintイベント登録
                printPreviewControl.Document.EndPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
                IsPrintEventRegistered = true;
            }
        }

        /// <summary>
        /// Print関連イベント解除
        /// </summary>
        private void PrintEventUnRegistered()
        {
            // Printイベント登録の場合、イベント解除
            if (IsPrintEventRegistered == true)
            {
                // PalettePrintPreviewControl.DocumentオブジェクトのBeginPrintイベント解除
                printPreviewControl.Document.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
                // PalettePrintPreviewControl.DocumentオブジェクトのQueryPageSettingsイベント解除
                printPreviewControl.Document.QueryPageSettings -= new System.Drawing.Printing.QueryPageSettingsEventHandler(PrintDocument_QueryPageSettings);
                // PalettePrintPreviewControl.DocumentオブジェクトのPrintPageイベント解除
                printPreviewControl.Document.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
                // PalettePrintPreviewControl.DocumentオブジェクトのEndPrintイベント解除
                printPreviewControl.Document.EndPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
                IsPrintEventRegistered = false;
            }
        }

        /// <summary>
        /// ProgressForm イベント（プレビュー生成処理）
        /// </summary>
        private void ProgressForm_Event(object sender, ProgressForm.ProgressEventArgs args)
        {
            ProgEventArgs = args;

            //プレビュー描画
            printPreviewControl.InvalidatePreview();
        }

        /// <summary>
        /// 印刷
        /// </summary>
        public void Print()
        {
            if (printPreviewControl.Document != null)
            {
                ProgEventArgs = null;

                // Printイベント登録
                PrintEventRegistered();

                // 印刷実行
                printPreviewControl.Document.Print();

                // Printイベント解除
                PrintEventUnRegistered();
            }
        }
    }
}