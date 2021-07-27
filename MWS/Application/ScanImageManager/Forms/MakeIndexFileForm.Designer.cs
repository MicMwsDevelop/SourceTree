namespace ScanImageManager.Forms
{
	partial class MakeIndexFileForm
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
			this.dataGridViewIndex = new System.Windows.Forms.DataGridView();
			this.buttonClear = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOutput = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonInputPath = new System.Windows.Forms.Button();
			this.textBoxImagePath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonGuess = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewIndex
			// 
			this.dataGridViewIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dataGridViewIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewIndex.Location = new System.Drawing.Point(15, 62);
			this.dataGridViewIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dataGridViewIndex.MultiSelect = false;
			this.dataGridViewIndex.Name = "dataGridViewIndex";
			this.dataGridViewIndex.ReadOnly = true;
			this.dataGridViewIndex.RowTemplate.Height = 21;
			this.dataGridViewIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewIndex.Size = new System.Drawing.Size(865, 540);
			this.dataGridViewIndex.TabIndex = 4;
			this.dataGridViewIndex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewIndex_MouseDoubleClick);
			// 
			// buttonClear
			// 
			this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClear.Location = new System.Drawing.Point(143, 610);
			this.buttonClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(125, 44);
			this.buttonClear.TabIndex = 6;
			this.buttonClear.Text = "出力対象リスト初期化";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "■出力対象リスト";
			// 
			// buttonOutput
			// 
			this.buttonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOutput.Location = new System.Drawing.Point(274, 610);
			this.buttonOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOutput.Name = "buttonOutput";
			this.buttonOutput.Size = new System.Drawing.Size(125, 44);
			this.buttonOutput.TabIndex = 7;
			this.buttonOutput.Text = "インデックスファイル出力";
			this.buttonOutput.UseVisualStyleBackColor = true;
			this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(755, 610);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(125, 44);
			this.buttonClose.TabIndex = 8;
			this.buttonClose.Text = "閉じる";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// buttonInputPath
			// 
			this.buttonInputPath.Location = new System.Drawing.Point(675, 12);
			this.buttonInputPath.Name = "buttonInputPath";
			this.buttonInputPath.Size = new System.Drawing.Size(30, 23);
			this.buttonInputPath.TabIndex = 2;
			this.buttonInputPath.Text = "...";
			this.buttonInputPath.UseVisualStyleBackColor = true;
			this.buttonInputPath.Click += new System.EventHandler(this.buttonInputPath_Click);
			// 
			// textBoxImagePath
			// 
			this.textBoxImagePath.BackColor = System.Drawing.Color.White;
			this.textBoxImagePath.Location = new System.Drawing.Point(144, 12);
			this.textBoxImagePath.Name = "textBoxImagePath";
			this.textBoxImagePath.ReadOnly = true;
			this.textBoxImagePath.Size = new System.Drawing.Size(525, 23);
			this.textBoxImagePath.TabIndex = 1;
			this.textBoxImagePath.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "■スキャンデータ登録パス";
			// 
			// buttonGuess
			// 
			this.buttonGuess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonGuess.Location = new System.Drawing.Point(12, 610);
			this.buttonGuess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonGuess.Name = "buttonGuess";
			this.buttonGuess.Size = new System.Drawing.Size(125, 44);
			this.buttonGuess.TabIndex = 5;
			this.buttonGuess.Text = "得意先番号推測";
			this.buttonGuess.UseVisualStyleBackColor = true;
			this.buttonGuess.Click += new System.EventHandler(this.buttonGuess_Click);
			// 
			// MakeIndexFileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(894, 663);
			this.Controls.Add(this.buttonGuess);
			this.Controls.Add(this.buttonInputPath);
			this.Controls.Add(this.textBoxImagePath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonOutput);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonClear);
			this.Controls.Add(this.dataGridViewIndex);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MakeIndexFileForm";
			this.Text = "インデックスファイル作成";
			this.Load += new System.EventHandler(this.MakeIndexFileForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewIndex;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOutput;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonInputPath;
		private System.Windows.Forms.TextBox textBoxImagePath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonGuess;
	}
}