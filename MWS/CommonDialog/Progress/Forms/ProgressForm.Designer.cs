namespace CommonDialog.Progress
{
    partial class ProgressForm
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
            this.progressBar = new MwsLib.Component.PercentageProgressBar();
            this.messageLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(21, 45);
            this.progressBar.Maximum = 1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(320, 23);
            this.progressBar.TabIndex = 0;
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.AutoEllipsis = true;
            this.messageLabel.Location = new System.Drawing.Point(12, 9);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(339, 25);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(131, 80);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "ｷｬﾝｾﾙ";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ProgressForm
            // 
            this.ClientSize = new System.Drawing.Size(364, 122);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.progressBar);
            this.KeyPreview = true;
            this.Name = "ProgressForm";
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProgressForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private MwsLib.Component.PercentageProgressBar progressBar;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}
