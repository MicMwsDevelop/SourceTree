//
// BankForm.cs
// 
// 銀行振込請求書発行メモ追加画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/17 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.Junp;
using EntryMemo.BaseFactory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace EntryMemo.Forms
{
	public partial class BankForm : Form
	{
		/// <summary>
		/// 請求書発行先ファイル名
		/// </summary>
		private string Filename { get; set; }

		/// <summary>
		/// 請求書発行先パス名
		/// </summary>
		private string Pathname { get; set; }

		/// <summary>
		/// 請求書発行先エクセルデータ
		/// </summary>
		private List<MemoBank> MemoBankList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>

		public BankForm()
		{
			InitializeComponent();

			Filename = string.Empty;
			Pathname = string.Empty;
			MemoBankList = new List<MemoBank>();
		}

		/// <summary>
		/// 請求書発行先ファイルの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "請求書発行先ファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					MemoBankList.Clear();

					Filename = Path.GetFileName(dlg.FileName);
					textBoxFilename.Text = Filename;
					Pathname = dlg.FileName;

					using (XLWorkbook wb = new XLWorkbook(Pathname, XLEventTracking.Disabled))
					{
						// 元のカーソルを保持
						Cursor preCursor = Cursor.Current;

						// カーソルを待機カーソルに変更
						Cursor.Current = Cursors.WaitCursor;

						IXLWorksheet ws = wb.Worksheet(1);
						for (int i = 2; ; i++)
						{
							if ("" == ws.Cell(i, 1).GetString())
							{
								break;
							}
							MemoBank data = new MemoBank();
							data.ReadWorksheet(ws, i);
							try
							{
								List<tMik基本情報> basicList = JunpDatabaseAccess.Select_tMik基本情報(string.Format("[fkj得意先情報] = '{0}'", data.得意先コード), "[fkjCliMicID] ASC", Program.gSettings.ConnectJunp.ConnectionString);
								if (null != basicList && 0 < basicList.Count)
								{
									data.顧客No = basicList[0].fkjCliMicID;
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message);
							}
							MemoBankList.Add(data);
						}
						// カーソルを元に戻す
						Cursor.Current = preCursor;
					}
				}
			}
		}
		
		/// <summary>
		/// メモ追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddMemo_Click(object sender, EventArgs e)
		{
			if (0 == MemoBankList.Count)
			{
				return;
			}
			Date date = dateTimePickerBank.Value.ToDate();
			if (DialogResult.No == MessageBox.Show(string.Format("WonderWebのメモを追加します。\r\n締日は {0} で間違いないですか？", date.GetNormalString()), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// {JunpDB].[dbo].[tMemo]にメモの新規追加
				List<SqlParameter[]> paramList = new List<SqlParameter[]>();
				foreach (MemoBank bank in MemoBankList)
				{
					tMemo memo = bank.GetMemo(date);
					paramList.Add(memo.GetInsertIntoParameters());
				}
				DatabaseAccess.InsertIntoListDatabase(tMemo.InsertIntoSqlString, paramList, Program.gSettings.ConnectJunp.ConnectionString);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show(string.Format("{0}件のメモを追加しました。", MemoBankList.Count), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
