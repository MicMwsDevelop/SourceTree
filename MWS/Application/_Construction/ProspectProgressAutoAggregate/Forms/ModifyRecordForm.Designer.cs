
namespace ProspectProgressAutoAggregate.Forms
{
	partial class ModifyRecordForm
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
			this.dataGridViewRecord = new System.Windows.Forms.DataGridView();
			this.comboBoxYearMonth = new System.Windows.Forms.ComboBox();
			this.buttonUpdateSet = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予算VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予測VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column営業部コード = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予算ES = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予算課金 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予算まとめ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予算売上 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予算営業損益 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予測ES = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予測課金 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予測まとめ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予測売上 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column予測営業損益 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績ES = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績課金 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績まとめ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績売上 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column実績営業損益 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecord)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewRecord
			// 
			this.dataGridViewRecord.AllowUserToAddRows = false;
			this.dataGridViewRecord.AllowUserToDeleteRows = false;
			this.dataGridViewRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column実績日,
            this.Column営業部コード,
            this.Column予算ES,
			this.Column予算課金,
			this.Column予算まとめ,
            this.Column予算売上,
            this.Column予算営業損益,
            this.Column予測ES,
			this.Column予測課金,
			this.Column予測まとめ,
            this.Column予測売上,
            this.Column予測営業損益,
            this.Column実績ES,
			this.Column実績課金,
			this.Column実績まとめ,
            this.Column実績売上,
            this.Column実績営業損益});
			this.dataGridViewRecord.Location = new System.Drawing.Point(17, 48);
			this.dataGridViewRecord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridViewRecord.Name = "dataGridViewRecord";
			this.dataGridViewRecord.RowTemplate.Height = 21;
			this.dataGridViewRecord.Size = new System.Drawing.Size(1180, 249);
			this.dataGridViewRecord.TabIndex = 0;
			// 
			// comboBoxYearMonth
			// 
			this.comboBoxYearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxYearMonth.FormattingEnabled = true;
			this.comboBoxYearMonth.Location = new System.Drawing.Point(17, 15);
			this.comboBoxYearMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.comboBoxYearMonth.Name = "comboBoxYearMonth";
			this.comboBoxYearMonth.Size = new System.Drawing.Size(123, 23);
			this.comboBoxYearMonth.TabIndex = 2;
			this.comboBoxYearMonth.SelectedIndexChanged += new System.EventHandler(this.comboBoxYearMonth_SelectedIndexChanged);
			// 
			// buttonUpdateSet
			// 
			this.buttonUpdateSet.Location = new System.Drawing.Point(1039, 11);
			this.buttonUpdateSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonUpdateSet.Name = "buttonUpdateSet";
			this.buttonUpdateSet.Size = new System.Drawing.Size(159, 29);
			this.buttonUpdateSet.TabIndex = 3;
			this.buttonUpdateSet.Text = "更新";
			this.buttonUpdateSet.UseVisualStyleBackColor = true;
			this.buttonUpdateSet.Click += new System.EventHandler(this.buttonUpdateSet_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "実績日";
			this.dataGridViewTextBoxColumn1.HeaderText = "実績日";
			this.dataGridViewTextBoxColumn1.MaxInputLength = 8;
			this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "営業部コード";
			this.dataGridViewTextBoxColumn2.HeaderText = "営業部";
			this.dataGridViewTextBoxColumn2.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn2.MinimumWidth = 2;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 50;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "予算ES";
			this.dataGridViewTextBoxColumn3.HeaderText = "予算ES";
			this.dataGridViewTextBoxColumn3.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.Width = 40;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "予算課金";
			this.dataGridViewTextBoxColumn4.HeaderText = "予算課金";
			this.dataGridViewTextBoxColumn4.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.Width = 40;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "予算まとめ";
			this.dataGridViewTextBoxColumn5.HeaderText = "予算まとめ";
			this.dataGridViewTextBoxColumn5.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.Width = 40;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "予算売上";
			this.dataGridViewTextBoxColumn6.HeaderText = "予算売上";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "予算営業損益";
			this.dataGridViewTextBoxColumn7.HeaderText = "予算営業損益";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "予測ES";
			this.dataGridViewTextBoxColumn8.HeaderText = "予測ES";
			this.dataGridViewTextBoxColumn8.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.Width = 40;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "予測課金";
			this.dataGridViewTextBoxColumn9.HeaderText = "予測課金";
			this.dataGridViewTextBoxColumn9.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.Width = 40;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "予測まとめ";
			this.dataGridViewTextBoxColumn10.HeaderText = "予測まとめ";
			this.dataGridViewTextBoxColumn10.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.Width = 40;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "予測売上";
			this.dataGridViewTextBoxColumn11.HeaderText = "予測売上";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "予測営業損益";
			this.dataGridViewTextBoxColumn12.HeaderText = "予測営業損益";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			// 
			// dataGridViewTextBoxColumn13
			// 
			this.dataGridViewTextBoxColumn13.DataPropertyName = "実績ES";
			this.dataGridViewTextBoxColumn13.HeaderText = "実績ES";
			this.dataGridViewTextBoxColumn13.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.Width = 40;
			// 
			// dataGridViewTextBoxColumn14
			// 
			this.dataGridViewTextBoxColumn14.DataPropertyName = "実績課金";
			this.dataGridViewTextBoxColumn14.HeaderText = "実績課金";
			this.dataGridViewTextBoxColumn14.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			this.dataGridViewTextBoxColumn14.Width = 40;
			// 
			// dataGridViewTextBoxColumn15
			// 
			this.dataGridViewTextBoxColumn15.DataPropertyName = "実績まとめ";
			this.dataGridViewTextBoxColumn15.HeaderText = "実績まとめ";
			this.dataGridViewTextBoxColumn15.MaxInputLength = 2;
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			this.dataGridViewTextBoxColumn15.Width = 40;
			// 
			// dataGridViewTextBoxColumn16
			// 
			this.dataGridViewTextBoxColumn16.DataPropertyName = "実績売上";
			this.dataGridViewTextBoxColumn16.HeaderText = "実績売上";
			this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
			// 
			// dataGridViewTextBoxColumn17
			// 
			this.dataGridViewTextBoxColumn17.DataPropertyName = "実績営業損益";
			this.dataGridViewTextBoxColumn17.HeaderText = "実績営業損益";
			this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
			// 
			// Column予算VP
			// 
			this.Column予算VP.Name = "Column予算VP";
			// 
			// Column予測VP
			// 
			this.Column予測VP.Name = "Column予測VP";
			// 
			// Column実績VP
			// 
			this.Column実績VP.Name = "Column実績VP";
			// 
			// Column実績日
			// 
			this.Column実績日.DataPropertyName = "実績日";
			this.Column実績日.FillWeight = 71.97581F;
			this.Column実績日.HeaderText = "実績日";
			this.Column実績日.MaxInputLength = 8;
			this.Column実績日.MinimumWidth = 8;
			this.Column実績日.Name = "Column実績日";
			this.Column実績日.ReadOnly = true;
			// 
			// Column営業部コード
			// 
			this.Column営業部コード.DataPropertyName = "営業部コード";
			this.Column営業部コード.FillWeight = 76.93691F;
			this.Column営業部コード.HeaderText = "営業部";
			this.Column営業部コード.MaxInputLength = 2;
			this.Column営業部コード.MinimumWidth = 2;
			this.Column営業部コード.Name = "Column営業部コード";
			this.Column営業部コード.ReadOnly = true;
			this.Column営業部コード.Width = 50;
			// 
			// Column予算ES
			// 
			this.Column予算ES.DataPropertyName = "予算ES";
			this.Column予算ES.FillWeight = 81.643F;
			this.Column予算ES.HeaderText = "予算ES";
			this.Column予算ES.MaxInputLength = 2;
			this.Column予算ES.Name = "Column予算ES";
			this.Column予算ES.Width = 40;
			// 
			// Column予算課金
			// 
			this.Column予算課金.DataPropertyName = "予算課金";
			this.Column予算課金.FillWeight = 81.643F;
			this.Column予算課金.HeaderText = "予算課金";
			this.Column予算課金.MaxInputLength = 2;
			this.Column予算課金.Name = "Column予算課金";
			this.Column予算課金.Width = 40;
			// 
			// Column予算まとめ
			// 
			this.Column予算まとめ.DataPropertyName = "予算まとめ";
			this.Column予算まとめ.FillWeight = 74.2885F;
			this.Column予算まとめ.HeaderText = "予算まとめ";
			this.Column予算まとめ.MaxInputLength = 2;
			this.Column予算まとめ.Name = "Column予算まとめ";
			this.Column予算まとめ.Width = 40;
			// 
			// Column予算売上
			// 
			this.Column予算売上.DataPropertyName = "予算売上";
			this.Column予算売上.FillWeight = 90.94933F;
			this.Column予算売上.HeaderText = "予算売上";
			this.Column予算売上.Name = "Column予算売上";
			this.Column予算売上.Width = 80;
			// 
			// Column予算営業損益
			// 
			this.Column予算営業損益.DataPropertyName = "予算営業損益";
			this.Column予算営業損益.FillWeight = 113.5497F;
			this.Column予算営業損益.HeaderText = "予算営業損益";
			this.Column予算営業損益.Name = "Column予算営業損益";
			this.Column予算営業損益.Width = 80;
			// 
			// Column予測ES
			// 
			this.Column予測ES.DataPropertyName = "予測ES";
			this.Column予測ES.FillWeight = 97.75877F;
			this.Column予測ES.HeaderText = "予測ES";
			this.Column予測ES.MaxInputLength = 2;
			this.Column予測ES.Name = "Column予測ES";
			this.Column予測ES.Width = 40;
			// 
			// Column予測課金
			// 
			this.Column予測課金.DataPropertyName = "予測課金";
			this.Column予測課金.FillWeight = 97.75877F;
			this.Column予測課金.HeaderText = "予測課金";
			this.Column予測課金.MaxInputLength = 2;
			this.Column予測課金.Name = "Column予測課金";
			this.Column予測課金.Width = 40;
			// 
			// Column予測まとめ
			// 
			this.Column予測まとめ.DataPropertyName = "予測まとめ";
			this.Column予測まとめ.FillWeight = 87.47749F;
			this.Column予測まとめ.HeaderText = "予測まとめ";
			this.Column予測まとめ.MaxInputLength = 2;
			this.Column予測まとめ.Name = "Column予測まとめ";
			this.Column予測まとめ.Width = 40;
			// 
			// Column予測売上
			// 
			this.Column予測売上.DataPropertyName = "予測売上";
			this.Column予測売上.FillWeight = 105.5585F;
			this.Column予測売上.HeaderText = "予測売上";
			this.Column予測売上.Name = "Column予測売上";
			this.Column予測売上.Width = 80;
			// 
			// Column予測営業損益
			// 
			this.Column予測営業損益.DataPropertyName = "予測営業損益";
			this.Column予測営業損益.FillWeight = 130.1251F;
			this.Column予測営業損益.HeaderText = "予測営業損益";
			this.Column予測営業損益.Name = "Column予測営業損益";
			this.Column予測営業損益.Width = 80;
			// 
			// Column実績ES
			// 
			this.Column実績ES.DataPropertyName = "実績ES";
			this.Column実績ES.FillWeight = 110.7647F;
			this.Column実績ES.HeaderText = "実績ES";
			this.Column実績ES.MaxInputLength = 2;
			this.Column実績ES.Name = "Column実績ES";
			this.Column実績ES.Width = 40;
			// 
			// Column実績課金
			// 
			this.Column実績課金.DataPropertyName = "実績課金";
			this.Column実績課金.FillWeight = 110.7647F;
			this.Column実績課金.HeaderText = "実績課金";
			this.Column実績課金.MaxInputLength = 2;
			this.Column実績課金.Name = "Column実績課金";
			this.Column実績課金.Width = 40;
			// 
			// Column実績まとめ
			// 
			this.Column実績まとめ.DataPropertyName = "実績まとめ";
			this.Column実績まとめ.FillWeight = 98.12144F;
			this.Column実績まとめ.HeaderText = "実績まとめ";
			this.Column実績まとめ.MaxInputLength = 2;
			this.Column実績まとめ.Name = "Column実績まとめ";
			this.Column実績まとめ.Width = 40;
			// 
			// Column実績売上
			// 
			this.Column実績売上.DataPropertyName = "実績売上";
			this.Column実績売上.FillWeight = 117.3486F;
			this.Column実績売上.HeaderText = "実績売上";
			this.Column実績売上.Name = "Column実績売上";
			this.Column実績売上.Width = 80;
			// 
			// Column実績営業損益
			// 
			this.Column実績営業損益.DataPropertyName = "実績営業損益";
			this.Column実績営業損益.FillWeight = 143.502F;
			this.Column実績営業損益.HeaderText = "実績営業損益";
			this.Column実績営業損益.Name = "Column実績営業損益";
			this.Column実績営業損益.Width = 80;
			// 
			// ModifyRecordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1216, 318);
			this.Controls.Add(this.buttonUpdateSet);
			this.Controls.Add(this.comboBoxYearMonth);
			this.Controls.Add(this.dataGridViewRecord);
			this.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModifyRecordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "売上実績設定";
			this.Load += new System.EventHandler(this.ModifyRecordForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecord)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewRecord;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予算VP;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予測VP;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績VP;
		private System.Windows.Forms.ComboBox comboBoxYearMonth;
		private System.Windows.Forms.Button buttonUpdateSet;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績日;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column営業部コード;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予算ES;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予算課金;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予算まとめ;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予算売上;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予算営業損益;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予測ES;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予測課金;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予測まとめ;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予測売上;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column予測営業損益;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績ES;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績課金;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績まとめ;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績売上;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column実績営業損益;
	}
}