//
// OnlineForm.cs
// 
// オン資格補助金申請書類メモ追加画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/17 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using WonderWebEntryMemo.BaseFactory;

namespace WonderWebEntryMemo.Forms
{
	public partial class OnlineForm : Form
	{
		/// <summary>
		/// オン資格書類発送先ファイル名
		/// </summary>
		private string Filename { get; set; }

		/// <summary>
		/// オン資格書類発送先パス名
		/// </summary>
		private string Pathname { get; set; }

		/// <summary>
		/// オン資格書類発送先エクセルデータ
		/// </summary>
		private List<MemoOnline> MemoOnlineList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>

		public OnlineForm()
		{
			InitializeComponent();

			Filename = string.Empty;
			Pathname = string.Empty;
			MemoOnlineList = new List<MemoOnline>();
		}

		/// <summary>
		/// オン資格書類発送先ファイルの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "オン資格書類発送先ファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					MemoOnlineList.Clear();

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
							MemoOnline data = new MemoOnline();
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
							MemoOnlineList.Add(data);
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
			if (0 == MemoOnlineList.Count)
			{
				return;
			}
			if (DialogResult.No == MessageBox.Show("WonderWebのメモを追加します。よろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
				foreach (MemoOnline online in MemoOnlineList)
				{
					tMemo memo = online.GetMemo();
					paramList.Add(memo.GetInsertIntoParameters());
				}
				DatabaseAccess.InsertIntoListDatabase(tMemo.InsertIntoSqlString, paramList, Program.gSettings.ConnectJunp.ConnectionString);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show(string.Format("{0}件のメモを追加しました。", MemoOnlineList.Count), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
