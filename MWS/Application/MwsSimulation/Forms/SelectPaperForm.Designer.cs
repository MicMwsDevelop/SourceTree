namespace MwsSimulation.Forms
{
	partial class SelectPaperForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonEstimate = new System.Windows.Forms.RadioButton();
			this.radioButtonPurchaseOrder = new System.Windows.Forms.RadioButton();
			this.radioButtonOrderConfirm = new System.Windows.Forms.RadioButton();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonOrderConfirm);
			this.groupBox1.Controls.Add(this.radioButtonPurchaseOrder);
			this.groupBox1.Controls.Add(this.radioButtonEstimate);
			this.groupBox1.Location = new System.Drawing.Point(21, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(135, 111);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "印刷用紙";
			// 
			// radioButtonEstimate
			// 
			this.radioButtonEstimate.AutoSize = true;
			this.radioButtonEstimate.Checked = true;
			this.radioButtonEstimate.Location = new System.Drawing.Point(19, 23);
			this.radioButtonEstimate.Name = "radioButtonEstimate";
			this.radioButtonEstimate.Size = new System.Drawing.Size(65, 21);
			this.radioButtonEstimate.TabIndex = 0;
			this.radioButtonEstimate.TabStop = true;
			this.radioButtonEstimate.Text = "見積書";
			this.radioButtonEstimate.UseVisualStyleBackColor = true;
			// 
			// radioButtonPurchaseOrder
			// 
			this.radioButtonPurchaseOrder.AutoSize = true;
			this.radioButtonPurchaseOrder.Location = new System.Drawing.Point(19, 50);
			this.radioButtonPurchaseOrder.Name = "radioButtonPurchaseOrder";
			this.radioButtonPurchaseOrder.Size = new System.Drawing.Size(65, 21);
			this.radioButtonPurchaseOrder.TabIndex = 1;
			this.radioButtonPurchaseOrder.Text = "注文書";
			this.radioButtonPurchaseOrder.UseVisualStyleBackColor = true;
			// 
			// radioButtonOrderConfirm
			// 
			this.radioButtonOrderConfirm.AutoSize = true;
			this.radioButtonOrderConfirm.Location = new System.Drawing.Point(19, 77);
			this.radioButtonOrderConfirm.Name = "radioButtonOrderConfirm";
			this.radioButtonOrderConfirm.Size = new System.Drawing.Size(78, 21);
			this.radioButtonOrderConfirm.TabIndex = 2;
			this.radioButtonOrderConfirm.Text = "注文請書";
			this.radioButtonOrderConfirm.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(93, 130);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 32);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "ｷｬﾝｾﾙ";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(12, 130);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 32);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// SelectPaperForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(180, 174);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectPaperForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "印刷用紙の選択";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonOrderConfirm;
		private System.Windows.Forms.RadioButton radioButtonPurchaseOrder;
		private System.Windows.Forms.RadioButton radioButtonEstimate;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
	}
}