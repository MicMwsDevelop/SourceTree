//
// ProgressForm.cs
// 
// プログレスバー画面
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
	/// <summary>
	/// プログレスバーダイアログ
	/// </summary>
	public partial class ProgressForm : Form
    {
        /// <summary>
        /// 進捗ダイアログ開始イベント用の引数クラスです。
        /// </summary>
        public class ProgressEventArgs : EventArgs
        {
            /// <summary>
            /// プログレスバー
            /// </summary>
            private ProgressBar _progressBar;

            /// <summary>
            /// メッセージラベル
            /// </summary>
            private Label _messageLabel;

            /// <summary>
            /// プログレス値
            /// </summary>
            public int ProgressValue
            {
                set
                {
                    // プログレスバー設定
                    if (_progressBar.InvokeRequired)
                    {
                        _progressBar.Invoke(new Action(delegate
                        {
                            // Ver1.063：プログレスバーの刻み値が最大値を超えた場合でも例外とならないようにする(bug15654) by masa
                            if (_progressBar.Value < value)
                            {
                                //値を増やす時

                                if (value < _progressBar.Maximum)
                                {
                                    //目的の値より一つ大きくしてから、目的の値にする
                                    _progressBar.Value = value + 1;
                                    _progressBar.Value = value;
                                }
                                else if (value == _progressBar.Maximum)
                                {
                                    //最大値にする時

                                    //最大値を1つ増やしてから、元に戻す
                                    _progressBar.Maximum++;
                                    _progressBar.Value = value + 1;
                                    _progressBar.Value = value;
                                    _progressBar.Maximum--;
                                }
                                else if (value > _progressBar.Maximum)
                                {
                                    // 最大値を超えている場合

                                    // 最大値とする
                                    _progressBar.Value = _progressBar.Maximum;
                                }
                            }
                            else
                            {
                                //値を減らす時

                                if (value >= _progressBar.Minimum)
                                {
                                    _progressBar.Value = value;
                                }
                                else if (value < _progressBar.Minimum)
                                {
                                    // 最小値より小さい場合

                                    // 最小値とする
                                    _progressBar.Value = _progressBar.Minimum;
                                }
                            }
                        }));
                    }
                }
                get { return _progressBar.Value; }

            }

            // $パフォーマンス改善$
            /// <summary>
            /// プログレス値
            /// </summary>
            public int ProgressMaximum
            {
                set
                {
                    // プログレスバー設定
                    if (_progressBar.InvokeRequired)
                    {
                        _progressBar.Invoke(new Action(delegate
                        {
                            _progressBar.Maximum = value;
                        }));
                    }
                }
                get { return _progressBar.Maximum; }
            }

            /// <summary>
            /// メッセージ
            /// </summary>
            public string Message
            {
                set
                {
                    // メッセージラベル設定
                    if (_messageLabel.InvokeRequired)
                    {
                        _messageLabel.Invoke(new Action(delegate
                        {
                            _messageLabel.Text = value;
                        }));
                    }
                }
                get { return _messageLabel.Text; }
            }

            /// <summary>
            /// プログレスダイアログのタイトル
            /// </summary>
            public string Title { set; get; }

            /// <summary>
            /// キャンセルかどうか
            /// </summary>
            public bool IsCancel { set; get; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="prg"></param>
            /// <param name="msg"></param>
            public ProgressEventArgs(ProgressBar prg, Label msg)
            {
                _progressBar = prg;
                _messageLabel = msg;
            }
        }

        /// <summary>
        /// イベント定義
        /// </summary>
        public delegate void ProgressEvent_Handler(object sender, ProgressEventArgs args);
        public event ProgressEvent_Handler ProgressEvent;

        /// <summary>
        /// イベント引数
        /// </summary>
        private ProgressEventArgs _eventArgs;

        /// <summary>
        /// プログレスバーの最大値設定
        /// </summary>
        public int Maximum
        {
            get
            {
                return progressBar.Maximum;
            }
            set
            {
                if (0 < value)
                {
                    progressBar.Maximum = value;
                }
            }
        }

        /// <summary>
        /// キャンセルを無効にするかどうか
        /// </summary>
        public bool IsDisableCancel { set; get; }

        /// <summary>
        /// CreateParams プロパティ
        /// </summary>
        /// <remarks>
        /// 閉じるボタンを無効にするため
        /// </remarks>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;

                CreateParams createParams = base.CreateParams;
                if (this.IsDisableCancel)
                {
                    createParams.ClassStyle |= CS_NOCLOSE;
                }

                return createParams;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgressForm(bool IsHomon = false)
        {
            InitializeComponent();

            // プログレスバー設定
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;

            // 画面設定
            this.messageLabel.Text = "";
            if (IsHomon)
            {
                // 訪問メニュー用モード(メッセージ欄2行表示)
                this.Height = 175;
                this.Width = 460;
                this.messageLabel.Height = 40;
            }

            this.IsDisableCancel = false;
        }

        /// <summary>
        /// ProgressFormのLoadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            // イベント引数設定
            _eventArgs = new ProgressEventArgs(this.progressBar, this.messageLabel);

            // タイトル設定
            this._eventArgs.Title = this.Text;

            //// タイマースレッド開始
            //// スレッドタイマーをトリガーにすることによって開始
            //// イベントのスレッドをメインから分離できる
            //System.Threading.Timer tm = new System.Threading.Timer(this.ProgressThreadProc
            //                                                            , null
            //                                                            , 500
            //                                                            , System.Threading.Timeout.Infinite);

            // 別スレッドにて処理を実行
            Thread workerThread = new Thread(this.ProgressThreadProc);
            workerThread.Start();
            this.Show();
        }

        /// <summary>
        /// プログレススレッド処理
        /// </summary>
        private void ProgressThreadProc(object state)
        {
            // キャンセルボタンを有効にする
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate
                {
                    if (!this.IsDisableCancel)
                    {
                        cancelButton.Enabled = true;
                        cancelButton.Focus();
                    }
                }));
            }

            // イベント発生
            this.ProgressEvent(this, _eventArgs);

            // 画面を閉じる
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate
                {
                    // 戻り値設定
                    if (_eventArgs.IsCancel)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    _eventArgs.IsCancel = true;
                    this.Close();
                }));
            }
        }

        /// <summary>
        /// キャンセルボタンのClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // キャンセルされた事を引数に設定す
            _eventArgs.IsCancel = true;
        }

        /// <summary>
        /// ProgressFormのFormClosingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (!_eventArgs.IsCancel)
            {
                // キャンセルされた事を引数に設定
                _eventArgs.IsCancel = true;
                e.Cancel = true;
            }
        }

        /// <summary>
        /// メッセージボックス表示用デリゲート定義
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        //public delegate DialogResult MessageBox(string message, string title, MessageBoxButtons button, MessageBoxIcon icon);

        /// <summary>
        /// メッセージボックス表示用メソッド
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        //public static DialogResult Show(string message, string title, MessageBoxButtons button, MessageBoxIcon icon)
        //{
        //    return MessageBox.Show(message, title, button, icon);
        //}

        /// <summary>
        /// ProgressFormのKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_KeyDown(object sender, KeyEventArgs e)
        {
            // 「Ctrlキー」「Altキー」「Shiftキー」どれも押されてない場合
            // ※通常キー
            if (!e.Control && !e.Alt && !e.Shift)
            {
                e.Handled = true;
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.Escape:
                        // エンターやスペースキーで画面を閉じる
                        cancelButton.PerformClick();
                        break;
                    default:
                        e.Handled = false;
                        break;
                }
            }
        }

    }
}
