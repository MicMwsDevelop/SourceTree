//
// ImportCsvFileForm.cs
// 
// CSVファイルインポートによるメモ追加画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.04(2024/06/18 勝呂):CSVファイルによるメモ追加機能の追加（企画推進部で使用）
//
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WonderWebEntryMemo.Forms
{
	/// <summary>
	/// CSVファイルインポートによるメモ追加
	/// </summary>
	public partial class ImportCsvFileForm : Form
	{
		/// <summary>
		/// CSVファイルパス名
		/// </summary>
		private string Pathname { get; set; }

		/// <summary>
		/// メモ情報クラス [JunpDB].[dbo].[tMemo]
		/// </summary>
		private List<tMemo> MemoList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ImportCsvFileForm()
		{
			InitializeComponent();

			Pathname = string.Empty;
			MemoList = new List<tMemo>();
		}

		/// <summary>
		/// CSVファイルの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "CSVLファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
				dlg.Title = "CSVファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					MemoList.Clear();

					textBoxFilename.Text = dlg.FileName;
					Pathname = dlg.FileName;

					using (var sr = new StreamReader(Pathname, Encoding.GetEncoding("Shift_JIS")))
					{
						while (!sr.EndOfStream)
						{
							string line = sr.ReadLine();
							if (';' != line[0])
							{
								// コメント行以外
								tMemo memo = this.SetCsvData(line);
								if (memo is null)
								{
									MessageBox.Show("メモ情報のCSVファイルの内容が正しくありません。お確かめください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
								MemoList.Add(memo);
							}
						}
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
			if (0 == MemoList.Count)
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
				foreach (tMemo memo in MemoList)
				{
					paramList.Add(memo.GetInsertIntoParameters());
				}
				DatabaseAccess.InsertIntoListDatabase(tMemo.InsertIntoSqlString, paramList, Program.gSettings.ConnectJunp.ConnectionString);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show(string.Format("{0}件のメモを追加しました。", MemoList.Count), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// メモの格納
		/// </summary>
		/// <param name="line">CSV文字列</param>
		/// <returns>判定</returns>
		public tMemo SetCsvData(string line)
		{
			List<string> split = SplitString.CSVSplitLine2(line);
			if (6 == split.Count)
			{
				tMemo memo = new tMemo();
				memo.fMemKey = split[0].ToInt();		// 1:顧客No
				memo.fMemType = split[1].Trim();        // 2: メモタイプ

				// 3:メモ
				string[] array = split[2].Trim().Split('|');
				foreach (string str in array)
				{
					memo.fMemMemo += str + "\r\n";
				}
				memo.fMemUpdateMan = split[3].Trim();   // 4:更新者

				// 5:メモ区分
				if (0 == split[4].Trim().Length || 3 != split[4].Trim().Length)
				{
					memo.fMemKubun = "002"; // 顧客情報
				}
				else
				{
					memo.fMemKubun = split[4].Trim(); 
				}
				// 6:コメント
				array = split[5].Trim().Split('|');
				foreach (string str in array)
				{
					memo.fMemComment += str + "\r\n";
				}
				memo.fMemTable = "tClient";
				memo.fMemUpdate = DateTime.Now;
				return memo;
			}
			return null;
		}
	}
}
