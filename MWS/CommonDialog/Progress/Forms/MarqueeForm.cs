//
// MarqueeForm.cs
// 
// プログレスバー画面(マーキースタイル)
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Threading;
using System.Windows.Forms;

namespace CommonDialog.Progress
{
	public partial class MarqueeForm : Form
    {
        /// <summary>
        /// 処理終了
        /// </summary>
        private bool Finished = false;

        /// <summary>
        /// プログレスバーの上に表示する文字列
        /// </summary>
        public string Message { set; get; }

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
        public MarqueeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarqueeForm_Load(object sender, EventArgs e)
        {
            labelMessage.Text = Message;
        }

        /// <summary>
        /// Form Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarqueeForm_Shown(object sender, EventArgs e)
        {
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
    }
}
