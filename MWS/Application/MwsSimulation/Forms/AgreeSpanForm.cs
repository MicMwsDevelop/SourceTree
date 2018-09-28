//
// AgreeSpanForm.cs
//
// 契約期間入力画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
// 
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	public partial class AgreeSpanForm : Form
	{
		/// <summary>
		/// 契約期間
		/// </summary>
		public Span AgreeSpan { get; set; }

		/// <summary>
		/// 契約月数
		/// </summary>
		public int AgreeMonthes { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private AgreeSpanForm()
		{
			InitializeComponent();

			AgreeSpan = Span.Nothing;
			AgreeMonthes = 0;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="agreeSpan">契約期間</param>
		/// <param name="agreeMonthes">契約月数</param>
		public AgreeSpanForm(Span agreeSpan, int agreeMonthes)
		{
			InitializeComponent();

			AgreeSpan = agreeSpan;
			AgreeMonthes = agreeMonthes;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AgreeSpanForm_Load(object sender, EventArgs e)
		{
			dateTimePickerStartDate.Value = AgreeSpan.Start.ToDateTime();
			dateTimePickerEndDate.Value = AgreeSpan.End.ToDateTime();
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (dateTimePickerEndDate.Value <= dateTimePickerStartDate.Value)
			{
				MessageBox.Show("契約終了日が契約開始日より前の日付になっています。ご確認ください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				dateTimePickerEndDate.Focus();
				return;
			}
			Date startDate = new Date(dateTimePickerStartDate.Value);
			Date endDate = new Date(dateTimePickerEndDate.Value);
			Date compareDate = Estimate.GetAgreeEndDate(startDate, AgreeMonthes);
			if (compareDate != endDate)
			{
				if (DialogResult.No == MessageBox.Show("契約月数より算出された契約終了日と合致しません。よろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					dateTimePickerEndDate.Focus();
					return;
				}
			}
			AgreeSpan = new Span(startDate, endDate);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
