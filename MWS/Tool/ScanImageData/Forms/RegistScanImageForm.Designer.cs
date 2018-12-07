namespace ScanImageData.Forms
{
	partial class RegistScanImageForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonUser = new System.Windows.Forms.RadioButton();
			this.radioButtonMainte = new System.Windows.Forms.RadioButton();
			this.radioButtonAccountTransfer = new System.Windows.Forms.RadioButton();
			this.radioButtonTransaction = new System.Windows.Forms.RadioButton();
			this.radioButtonConsent = new System.Windows.Forms.RadioButton();
			this.explorerListViewScanImage = new MwsLib.Component.ExplorerListView(this.components);
			this.explorerTreeViewScanImage = new MwsLib.Component.ExplorerTreeView(this.components);
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonConsent);
			this.groupBox1.Controls.Add(this.radioButtonTransaction);
			this.groupBox1.Controls.Add(this.radioButtonAccountTransfer);
			this.groupBox1.Controls.Add(this.radioButtonMainte);
			this.groupBox1.Controls.Add(this.radioButtonUser);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(563, 62);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "文書種別";
			// 
			// radioButtonUser
			// 
			this.radioButtonUser.AutoSize = true;
			this.radioButtonUser.Location = new System.Drawing.Point(18, 28);
			this.radioButtonUser.Name = "radioButtonUser";
			this.radioButtonUser.Size = new System.Drawing.Size(77, 16);
			this.radioButtonUser.TabIndex = 0;
			this.radioButtonUser.Text = "登録・変更";
			this.radioButtonUser.UseVisualStyleBackColor = true;
			this.radioButtonUser.CheckedChanged += new System.EventHandler(this.radioButtonUser_CheckedChanged);
			// 
			// radioButtonMainte
			// 
			this.radioButtonMainte.AutoSize = true;
			this.radioButtonMainte.Location = new System.Drawing.Point(101, 28);
			this.radioButtonMainte.Name = "radioButtonMainte";
			this.radioButtonMainte.Size = new System.Drawing.Size(71, 16);
			this.radioButtonMainte.TabIndex = 1;
			this.radioButtonMainte.Text = "保守契約";
			this.radioButtonMainte.UseVisualStyleBackColor = true;
			this.radioButtonMainte.CheckedChanged += new System.EventHandler(this.radioButtonMainte_CheckedChanged);
			// 
			// radioButtonAccountTransfer
			// 
			this.radioButtonAccountTransfer.AutoSize = true;
			this.radioButtonAccountTransfer.Location = new System.Drawing.Point(178, 28);
			this.radioButtonAccountTransfer.Name = "radioButtonAccountTransfer";
			this.radioButtonAccountTransfer.Size = new System.Drawing.Size(71, 16);
			this.radioButtonAccountTransfer.TabIndex = 2;
			this.radioButtonAccountTransfer.Text = "口座振替";
			this.radioButtonAccountTransfer.UseVisualStyleBackColor = true;
			this.radioButtonAccountTransfer.CheckedChanged += new System.EventHandler(this.radioButtonAccountTransfer_CheckedChanged);
			// 
			// radioButtonTransaction
			// 
			this.radioButtonTransaction.AutoSize = true;
			this.radioButtonTransaction.Location = new System.Drawing.Point(255, 28);
			this.radioButtonTransaction.Name = "radioButtonTransaction";
			this.radioButtonTransaction.Size = new System.Drawing.Size(107, 16);
			this.radioButtonTransaction.TabIndex = 3;
			this.radioButtonTransaction.Text = "取引条件確認書";
			this.radioButtonTransaction.UseVisualStyleBackColor = true;
			this.radioButtonTransaction.CheckedChanged += new System.EventHandler(this.radioButtonTransaction_CheckedChanged);
			// 
			// radioButtonConsent
			// 
			this.radioButtonConsent.AutoSize = true;
			this.radioButtonConsent.Location = new System.Drawing.Point(368, 28);
			this.radioButtonConsent.Name = "radioButtonConsent";
			this.radioButtonConsent.Size = new System.Drawing.Size(178, 16);
			this.radioButtonConsent.TabIndex = 4;
			this.radioButtonConsent.Text = "リモートサービス利用規約同意書";
			this.radioButtonConsent.UseVisualStyleBackColor = true;
			this.radioButtonConsent.CheckedChanged += new System.EventHandler(this.radioButtonConsent_CheckedChanged);
			// 
			// explorerListViewScanImage
			// 
			this.explorerListViewScanImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.explorerListViewScanImage.Location = new System.Drawing.Point(324, 82);
			this.explorerListViewScanImage.Name = "explorerListViewScanImage";
			this.explorerListViewScanImage.Size = new System.Drawing.Size(574, 558);
			this.explorerListViewScanImage.TabIndex = 2;
			this.explorerListViewScanImage.UseCompatibleStateImageBehavior = false;
			this.explorerListViewScanImage.View = System.Windows.Forms.View.Details;
			this.explorerListViewScanImage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.explorerListViewScanImage_MouseDoubleClick);
			// 
			// explorerTreeViewScanImage
			// 
			this.explorerTreeViewScanImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.explorerTreeViewScanImage.LinkedExplorerListView = this.explorerListViewScanImage;
			this.explorerTreeViewScanImage.Location = new System.Drawing.Point(13, 82);
			this.explorerTreeViewScanImage.Name = "explorerTreeViewScanImage";
			this.explorerTreeViewScanImage.Size = new System.Drawing.Size(311, 558);
			this.explorerTreeViewScanImage.TabIndex = 1;
			// 
			// RegistScanImageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(910, 652);
			this.Controls.Add(this.explorerListViewScanImage);
			this.Controls.Add(this.explorerTreeViewScanImage);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegistScanImageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "スキャナーイメージ登録";
			this.Load += new System.EventHandler(this.RegistScanImageForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonUser;
		private System.Windows.Forms.RadioButton radioButtonMainte;
		private System.Windows.Forms.RadioButton radioButtonAccountTransfer;
		private System.Windows.Forms.RadioButton radioButtonTransaction;
		private System.Windows.Forms.RadioButton radioButtonConsent;
		private MwsLib.Component.ExplorerTreeView explorerTreeViewScanImage;
		private MwsLib.Component.ExplorerListView explorerListViewScanImage;
	}
}