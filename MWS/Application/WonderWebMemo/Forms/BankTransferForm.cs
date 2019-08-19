using MwsLib.BaseFactory.WonderWebMemo;
using MwsLib.Common;
using MwsLib.DB.SQLite.WonderWebMemo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WonderWebMemo.Forms
{
	public partial class BankTransferForm : Form
	{
		/// <summary>
		/// 銀行振込請求書発行先情報リスト
		/// </summary>
		private List<MemoBankTransfer> BankList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BankTransferForm()
		{
			InitializeComponent();
		}
		
		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BankTransferForm_Load(object sender, EventArgs e)
		{
			BankList = SQLiteWonderWebMemoAccess.GetMemoBankTransfer();
			if (null != BankList)
			{
				foreach (MemoBankTransfer memo in BankList)
				{
					DataGridViewRow row = new DataGridViewRow();
					row.CreateCells(dataGridViewMemo);
					row.Cells[0].Value = memo.TokuisakiNo;
					row.Cells[1].Value = MemoBankTransfer.GetMemoType();
					row.Cells[2].Value = memo.GetMemo(new Date(2019, 8, 7));
					row.Tag = memo;
					dataGridViewMemo.Rows.Add(row);
				}
			}
		}

		/// <summary>
		/// WonderWebにメモを追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddMemo_Click(object sender, EventArgs e)
		{
			if (0 < BankList.Count)
			{
				
			}
		}
	}
}
