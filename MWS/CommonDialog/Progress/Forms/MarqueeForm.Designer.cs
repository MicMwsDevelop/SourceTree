namespace CommonDialog.Progress
{
    partial class MarqueeForm
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
            this.labelMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            this.labelMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMessage.Location = new System.Drawing.Point(0, 0);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(398, 40);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 55);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar.MarqueeAnimationSpeed = 25;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(398, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.labelMessage);
            this.flowLayoutPanel.Controls.Add(this.progressBar);
            this.flowLayoutPanel.Location = new System.Drawing.Point(15, 15);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(398, 84);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // MarqueeForm
            // 
            this.ClientSize = new System.Drawing.Size(428, 114);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "MarqueeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MarqueeForm_FormClosing);
            this.Load += new System.EventHandler(this.MarqueeForm_Load);
            this.Shown += new System.EventHandler(this.MarqueeForm_Shown);
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}
