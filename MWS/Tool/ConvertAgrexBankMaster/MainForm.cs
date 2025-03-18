//
// MainForm.cs
//
// AGREX銀行マスタコンバーター メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
//////////////////////////////////////////////////////////////////
// Ver1.00(2025/02/25 勝呂):新規作成
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ConvertAgrexBankMaster
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// AGREX銀行マスタ（コンバート前）
		/// </summary>
		private List<List<string>> BeforeAgrexBankList { get; set; }

		/// <summary>
		/// AGREX銀行マスタ（コンバート後）
		/// </summary>
		private List<string> AfterAgrexBankList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			BeforeAgrexBankList = new List<List<string>>();
			AfterAgrexBankList = new List<string>();
		}

		/// <summary>
		/// AGREX銀行マスタファイルの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "TXTファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
				dlg.Title = "AGREX銀行マスタファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxPathname.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			string inputPathname = textBoxPathname.Text.Trim();

			if (0 == inputPathname.Length)
			{
				MessageBox.Show("AGREX銀行マスタファイルが指定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			BeforeAgrexBankList.Clear();
			AfterAgrexBankList.Clear();

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// AGREX銀行マスタファイルの読込み
				if (0 == ReadFile(inputPathname))
				{
					MessageBox.Show("AGREX銀行マスタファイルの読込に失敗しました。\nファイルを確認してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 銀行名カナの名称中の半角スペースを取り除いて、半角スペース20文字でパディング
				foreach (List<string> before in BeforeAgrexBankList)
				{
					string data = ConvertData(before);
					AfterAgrexBankList.Add(data);
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("AGREX銀行マスタファイル読込エラー発生：{0}", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				// AGREX銀行マスタコンバートファイルの出力
				string outputPathname = Path.Combine(Directory.GetCurrentDirectory(), "NewAgrexBank.txt");
				WriteFile(AfterAgrexBankList, outputPathname);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("AGREX銀行マスタコンバートファイル書込エラー発生：{0}", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			MessageBox.Show("AGREX銀行マスタファイルをコンバートして出力しました。", "終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// AGREX銀行マスタファイルの読込み
		/// </summary>
		/// <param name="pathname">AGREX銀行マスタファイルパス名</param>
		/// <returns>読込み行数</returns>
		private int ReadFile(string pathname)
		{
			using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
			{
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					if (';' == line[0])
					{
						// コメント行
						continue;
					}
					if (42 != line.Length)
					{
						BeforeAgrexBankList.Clear();
						return 0;
					}
					// 0：銀行コード(4)
					// 1：銀行名カナ(20)
					// 2：支店コード(3)
					// 3：支店名カナ(15)
					List<string> record = new List<string>();
					record.Add(line.Substring(0, 4));
					record.Add(line.Substring(4, 20));
					record.Add(line.Substring(24, 3));
					record.Add(line.Substring(27, 15));
					BeforeAgrexBankList.Add(record);
				}
			}
			return BeforeAgrexBankList.Count;
		}

		/// <summary>
		/// 銀行名カナの名称中の半角スペースを取り除いて、半角スペース20文字でパディング
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		private string ConvertData(List<string> record)
		{
			if (4 != record.Count)
			{
				return string.Empty;
			}
			string data = record[0];
			data += record[1].Replace(" ", "").PadRight(20, ' ');
			data += record[2];
			data += record[3].PadRight(15, ' ');
			return data;
		}

		/// <summary>
		/// AGREX銀行マスタコンバートファイルの出力
		/// </summary>
		/// <param name="bankList">コンバートデータリスト</param>
		/// <param name="outputPathname">AGREX銀行マスタコンバートファイル</param>
		private void WriteFile(List<string> bankList, string pathname)
		{
			using (var sw = new StreamWriter(pathname, false, Encoding.GetEncoding("shift_jis")))
			{
				foreach (string bank in bankList) 
				{
					sw.WriteLine(bank);
				}
			}
		}
	}
}
