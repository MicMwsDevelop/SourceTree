//
// MaintenanceMarqueeForm.cs
// 
// プログレスバー画面(マーキースタイル)
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace CommonDialog.Progress
{
	public partial class MaintenanceMarqueeForm : Form
    {
        /// <summary>
        /// 処理終了
        /// </summary>
        private bool Finished = false;

        /// <summary>
        /// 
        /// </summary>
        private string _titleString { set; get; }

        /// <summary>
        /// プログレスバーの上に表示する文字列
        /// </summary>
        public string TitleString
        {
            set
            {
                _titleString = value;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate { titleLabel.Text = _titleString; }));
                }
                else
                {
                    titleLabel.Text = _titleString;
                }
            }
            get
            {
                return _titleString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _messageString { set; get; }

        /// <summary>
        /// プログレスバーの上に表示する文字列
        /// </summary>
        public string MessageString
        {
            set
            {
                _messageString = value;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate { messageLabel.Text = _messageString; }));
                }
                else
                {
                    messageLabel.Text = _messageString;
                }
            }
            get
            {
                return _messageString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _timeString { set; get; }

        /// <summary>
        /// プログレスバーの上に表示する文字列
        /// </summary>
        public string TimeString
        {
            set
            {
                _timeString = value;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate { lapsedTimeLabel.Text = _timeString; }));
                }
                else
                {
                    lapsedTimeLabel.Text = _timeString;
                }
            }
            get
            {
                return _timeString;
            }
        }

        /// <summary>
        /// ストップウォッチ
        /// </summary>
        private Stopwatch sw = new Stopwatch();

        /// <summary>
        /// タイマー
        /// </summary>
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// 処理イベントデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FormRunningHandler();

        /// <summary>
        /// 処理イベント
        /// </summary>
        public event FormRunningHandler FormRunningEvent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MaintenanceMarqueeForm()
        {
            InitializeComponent();

            TitleString = string.Empty;
            MessageString = string.Empty;
            TimeString = string.Empty;

            // タイマー生成
            timer.Tick += new EventHandler(this.OnTimer);
            timer.Interval = 500;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarqueeForm_Load(object sender, EventArgs e)
        {
            // 空の時間を開始前に表示
            TimeString = "経過時間：" + sw.Elapsed.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Form Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarqueeForm_Shown(object sender, EventArgs e)
        {
            // ストップウォッチの開始
            sw.Start();
            // タイマーの開始
            timer.Start();

            Thread workerThread = new Thread(Running);
            workerThread.Start();
        }

        /// <summary>
        /// Form Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarqueeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Alt + F4 キーによるアプリケーション終了対策
            // 処理が終了するまで画面を閉じさせない
            if (!Finished)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 処理の実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Running()
        {
            if (null != FormRunningEvent)
            {
                FormRunningEvent();
            }
            Finished = true;
            // 処理終了後、画面を閉じる
            if (InvokeRequired)
            {
                Invoke(new Action(delegate
                {
                    this.Close();
                }));
            }
        }

        /// <summary>
        /// タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimer(object sender, EventArgs e)
        {
            // 経過時間を表示
            TimeString = "経過時間：" + sw.Elapsed.ToString(@"hh\:mm\:ss");
        }
    }
}
