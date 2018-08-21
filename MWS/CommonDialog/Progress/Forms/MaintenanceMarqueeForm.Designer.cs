namespace CommonDialog.Progress
{
    partial class MaintenanceMarqueeForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.messageLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.lapsedTimeLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.White;
            this.messageLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.messageLabel.Location = new System.Drawing.Point(0, 27);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(398, 52);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "　⇒1行目\r\n　　2行目\r\n　　3行目\r\n　　4行目";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 89);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.progressBar.MarqueeAnimationSpeed = 25;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(398, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.Controls.Add(this.titleLabel);
            this.flowLayoutPanel.Controls.Add(this.messageLabel);
            this.flowLayoutPanel.Controls.Add(this.progressBar);
            this.flowLayoutPanel.Controls.Add(this.lapsedTimeLabel);
            this.flowLayoutPanel.Location = new System.Drawing.Point(15, 15);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(398, 133);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(398, 22);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "データーベースの最適化処理実行中です";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lapsedTimeLabel
            // 
            this.lapsedTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lapsedTimeLabel.Location = new System.Drawing.Point(0, 119);
            this.lapsedTimeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.lapsedTimeLabel.Name = "lapsedTimeLabel";
            this.lapsedTimeLabel.Size = new System.Drawing.Size(398, 15);
            this.lapsedTimeLabel.TabIndex = 2;
            this.lapsedTimeLabel.Text = "経過時間　00:00:00";
            this.lapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MaintenanceMarqueeForm
            // 
            this.ClientSize = new System.Drawing.Size(428, 163);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "MaintenanceMarqueeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MarqueeForm_FormClosing);
            this.Load += new System.EventHandler(this.MarqueeForm_Load);
            this.Shown += new System.EventHandler(this.MarqueeForm_Shown);
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label lapsedTimeLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}
