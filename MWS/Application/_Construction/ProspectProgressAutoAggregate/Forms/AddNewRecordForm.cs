//
// AddNewRecordForm.cs
// 
// 見込進捗自動集計 来期売上実績追加画面フォームクラス（管理者用）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.04 来期追加と売上実績設定機能を追加（管理者用）(2021/10/20 勝呂)
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProspectProgressAutoAggregate.Forms
{
	public partial class AddNewRecordForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AddNewRecordForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddNew_Click(object sender, EventArgs e)
		{
			int period = textBoxPeriod.ToInt();
			if (period < 46)
			{
				MessageBox.Show("45期以前は追加できません。");
				return;
			}
			Date startDate = Program.PeriodToDate(period);
			List<売上実績> list = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 = {0}", startDate.ToIntYMD()), "", Program.gSettings.Charlie.ConnectionString);
			if (null != list)
			{
				MessageBox.Show("既に登録されています。");
				return;
			}
			try
			{
				売上実績 sale = new 売上実績();
				for (int i = 0; i < 12; i++)
				{
					sale.実績日 = startDate.ToIntYMD();
					foreach (string bumon in Program.gBumonCodes202002)
					{
						sale.営業部コード = bumon;
						CharlieDatabaseAccess.InsertInto_売上実績(sale, Program.gSettings.Charlie.ConnectionString);
					}
					startDate = startDate.PlusMonths(1);
				}
				MessageBox.Show("追加しました。");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
