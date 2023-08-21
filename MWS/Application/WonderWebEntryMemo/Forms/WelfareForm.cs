//
// OnlineForm.cs
// 
// オン資格補助金申請書類メモ追加画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.03(2023/08/21 勝呂):厚生局データメモ追加機能の追加
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.DB.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using WonderWebEntryMemo.BaseFactory;

namespace WonderWebEntryMemo.Forms
{
	public partial class WelfareForm : Form
	{
		/// <summary>
		/// 厚生局データファイル名
		/// </summary>
		private string Filename { get; set; }

		/// <summary>
		/// 厚生局データファイルパス名
		/// </summary>
		private string Pathname { get; set; }

		/// <summary>
		/// 厚生局データファイルエクセルデータ
		/// </summary>
		private List<MemoWelfare> WelfareList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>

		public WelfareForm()
		{
			InitializeComponent();

			Filename = string.Empty;
			Pathname = string.Empty;
			WelfareList = new List<MemoWelfare>();
		}

		/// <summary>
		/// 厚生局データファイルの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "厚生局データファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					WelfareList.Clear();

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
							MemoWelfare data = new MemoWelfare();
							data.ReadWorksheet(ws, i);
							WelfareList.Add(data);
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
			if (0 == WelfareList.Count)
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
				foreach (MemoWelfare welfare in WelfareList)
				{
					tMemo memo = welfare.GetMemo();
					paramList.Add(memo.GetInsertIntoParameters());
				}
				DatabaseAccess.InsertIntoListDatabase(tMemo.InsertIntoSqlString, paramList, Program.gSettings.ConnectJunp.ConnectionString);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show(string.Format("{0}件のメモを追加しました。", WelfareList.Count), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
