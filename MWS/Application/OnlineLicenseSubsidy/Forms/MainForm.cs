//
// MainForm.cs
// 
// メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/09/20 勝呂):新規作成
// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.Junp;
using OnlineLicenseSubsidy.BaseFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OnlineLicenseSubsidy.Forms
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// ウィンドウタイトルにバージョン情報を表示
			this.Text = string.Format("{0} {1}", Program.ProgramName, Program.VersionStr);

			if (false == Directory.Exists(Program.gSettings.補助金額資料フォルダ_NTT東日本))
			{
				MessageBox.Show(this, "補助金額資料フォルダ_NTT東日本が存在しません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == Directory.Exists(Program.gSettings.補助金額資料フォルダ_NTT西日本))
			{
				MessageBox.Show(this, "補助金額資料フォルダ_NTT西日本が存在しません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			radioButtonEast.Checked = true;
#if DEBUG
			checkBoxNotPDF.Visible = true;
			checkBoxNotPDF.Checked = true;
#endif
		}

		/// <summary>
		/// NTT東日本
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonEast_CheckedChanged(object sender, EventArgs e)
		{
			// 作業対象月コンボボックスの設定
			List<Tuple<string, string>> list = new List<Tuple<string, string>>();
			string[] folders = Directory.GetDirectories(Program.gSettings.補助金額資料フォルダ_NTT東日本, "*", SearchOption.TopDirectoryOnly);
			for (int i = 0; i < folders.Length; i++)
			{
				list.Add(new Tuple<string, string>(Path.GetFileName(folders[i]), folders[i]));
			}
			comboBoxYearMonth.DataSource = list;
			comboBoxYearMonth.DisplayMember = "Item1";
			comboBoxYearMonth.ValueMember = "Item2";
			if (0 < list.Count)
			{
				comboBoxYearMonth.SelectedIndex = 0;
			}
			textBoxInputFolder.Text = Program.gSettings.補助金額資料フォルダ_NTT東日本;
		}

		/// <summary>
		/// NTT西日本
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonWest_CheckedChanged(object sender, EventArgs e)
		{
			// 作業対象月コンボボックスの設定
			List<Tuple<string, string>> list = new List<Tuple<string, string>>();
			string[] folders = System.IO.Directory.GetDirectories(Program.gSettings.補助金額資料フォルダ_NTT西日本, "*", SearchOption.TopDirectoryOnly);
			for (int i = 0; i < folders.Length; i++)
			{
				list.Add(new Tuple<string, string>(Path.GetFileName(folders[i]), folders[i]));
			}
			comboBoxYearMonth.DataSource = list;
			comboBoxYearMonth.DisplayMember = "Item1";
			comboBoxYearMonth.ValueMember = "Item2";
			if (0 < list.Count)
			{
				comboBoxYearMonth.SelectedIndex = 0;
			}
			textBoxInputFolder.Text = Program.gSettings.補助金額資料フォルダ_NTT西日本;
		}

		/// <summary>
		/// 作業リスト出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonWorkbook_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// 初期化
			textBoxWorkList.Text = string.Empty;
			buttonExport.Enabled = false;

			// orgファイル→Excelファイルをコピー
			string orgWorkPathname = Path.Combine(Directory.GetCurrentDirectory(), オン資補助金申請書類出力.OrgWorkListFilename);
			string workPathname = Path.Combine(Directory.GetCurrentDirectory(), オン資補助金申請書類出力.WorkListFilename(radioButtonEast.Checked));
			File.Copy(orgWorkPathname, workPathname, true);

			// \\wwsv\ons-pics\管理用_営業管理部\NTT東日本_提出用\05_補助金額資料\202207
			Tuple<string, string> folder = (Tuple<string, string>)comboBoxYearMonth.SelectedItem;
			string[] clinicFolders = Directory.GetDirectories(folder.Item2, "*", SearchOption.TopDirectoryOnly);

			List<補助金申請情報> dataList = new List<補助金申請情報>();
			foreach (string folderName in clinicFolders)
			{
				// 最後のフォルダ名の取得
				// ex. \\wwsv\ons-pics\管理用_営業管理部\NTT東日本_提出用\05_補助金額資料\202207\010251_東0591_浅沼歯科医院 → 010251_東0591_浅沼歯科医院
				string targetfolderName = Path.GetFileName(folderName);

				// フォルダ名から得意先Noの取得
				string tokuisakiNo = string.Empty;
				if ('_' == targetfolderName[6])
				{
					tokuisakiNo = targetfolderName.Substring(0, MwsDefine.TokuisakiNoLength);

					// 文字列から数字のみ抽出
					tokuisakiNo = Regex.Replace(tokuisakiNo, @"[^0-9]", "");

					if (MwsDefine.TokuisakiNoLength != tokuisakiNo.Length)
					{
						// フォルダ名に得意先Noが正しく設定されていない
						MessageBox.Show(this, string.Format("フォルダ名に得意先Noが正しく設定されていません。\n{0}", targetfolderName), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				else
				{
					// フォルダ名に得意先Noが正しく設定されていない
					MessageBox.Show(this, string.Format("フォルダ名に得意先Noが正しく設定されていません。\n{0}", targetfolderName), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				// フォルダ名から受付通番の取得 ex.東0339
				int acceptNoIndex = -1;
				if (radioButtonEast.Checked)
				{
					// NTT東日本：010251_東0591_浅沼歯科医院
					acceptNoIndex = targetfolderName.IndexOf("東");
				}
				else
				{
					// NTT西日本：020603_【西0515】いけだ歯科
					acceptNoIndex = targetfolderName.IndexOf("西");
				}
				string acceptNo = string.Empty;
				if (-1 != acceptNoIndex)
				{
					acceptNo = targetfolderName.Substring(acceptNoIndex, 5);
				}
				try
				{
					string whereStr = string.Format("得意先No = '{0}'", tokuisakiNo);
					List<vMicユーザーオン資用> work = JunpDatabaseAccess.Select_vMicユーザーオン資用(whereStr, "", Program.gSettings.Junp.ConnectionString);
					if (null != work)
					{
						vMicユーザーオン資用 userInfo = work.First();
						string[] files = Directory.GetFiles(folderName, "*.xlsx", SearchOption.TopDirectoryOnly);
						if (2 == files.Length)
						{
							string reportPathname, receiptPathname;
							if (-1 != files[0].IndexOf("完了報告書"))
							{
								reportPathname = files[0];
								receiptPathname = files[1];
							}
							else
							{
								reportPathname = files[1];
								receiptPathname = files[0];
							}
							// 事業完了報告書
							補助金申請情報 data = オン資補助金申請書類出力.ReadExcel事業完了報告書(userInfo, acceptNo, reportPathname, radioButtonEast.Checked);
							if (null == data)
							{
								continue;
							}
							// 領収書内訳書
							data.領収内訳情報List = オン資補助金申請書類出力.ReadExcel領収書内訳書(receiptPathname);

							// 注文確認書
							// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
							whereStr = string.Format("顧客No = {0}", userInfo.顧客No);
							List<vMicオンライン資格確認ソフト改修費> softList = JunpDatabaseAccess.Select_vMicオンライン資格確認ソフト改修費(whereStr, "受注番号 DESC", Program.gSettings.Junp.ConnectionString);
							if (null != softList && 0 < softList.Count)
							{
								data.発送日 = DateTime.Today;
								data.受注日 = softList[0].受注日;
								data.金額 = softList[0].受注金額税込;
							}
							if (null != data.領収内訳情報List)
							{
								dataList.Add(data);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			if (0 < dataList.Count)
			{
				// オン資補助金申請書類作業リスト.xlsxの出力
				オン資補助金申請書類出力.WriteExcel作業リスト(dataList, workPathname);

				MessageBox.Show(this, string.Format("{0} を出力しました。", workPathname), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// 作業リストの設定
				textBoxWorkList.Text = workPathname;
				buttonExport.Enabled = true;

				try
				{
					// Excelの起動
					using (Process process = new Process())
					{
						process.StartInfo.FileName = workPathname;
						process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
						process.Start();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this, "補助金額資料が見つかりませんでした。\nフォルダ名に得意先Noが設定されているかご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 作業リストの設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputWorkList_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "オン資補助金申請書類作業リストファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxWorkList.Text = dlg.FileName;
					buttonExport.Enabled = true;
				}
			}
		}

		/// <summary>
		/// 申請書類出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExport_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				// 作業リストを作業リストフォルダにコピー
				// \\wwsv\ons-pics\作業用_経理部
				if (false == Directory.Exists(Program.gSettings.作業リストフォルダ))
				{
					Directory.CreateDirectory(Program.gSettings.作業リストフォルダ);
				}
				File.Copy(textBoxWorkList.Text, Path.Combine(Program.gSettings.作業リストフォルダ, Path.GetFileName(textBoxWorkList.Text)), true);

				List<補助金申請出力情報> dataList = オン資補助金申請書類出力.ReadExcel作業リスト(textBoxWorkList.Text);
				if (null != dataList)
				{
					string orgPathname = Path.Combine(Directory.GetCurrentDirectory(), オン資補助金申請書類出力.OrgSubsidyFilename);
					if (Program.gSettings.Trial)
					{
						// 第１弾
						// \\wwsv\ons-pics\作業用_経理部\助成金申請書類エクセル
						string outputPathExcel = Path.Combine(Program.gSettings.補助金申請書類フォルダ, Program.gSettings.補助金申請書類エクセルフォルダ);
						if (false == Directory.Exists(outputPathExcel))
						{
							Directory.CreateDirectory(outputPathExcel);
						}
						// \\wwsv\ons-pics\作業用_経理部\助成金申請書類PDF
						string outputPathPdf = Path.Combine(Program.gSettings.補助金申請書類フォルダ, Program.gSettings.補助金申請書類PDFフォルダ);
						if (false == Directory.Exists(outputPathPdf))
						{
							Directory.CreateDirectory(outputPathPdf);
						}
						foreach (補助金申請出力情報 data in dataList)
						{
							string excelPathname = Path.Combine(outputPathExcel, data.GetExcelFilename);	// Excelファイル名
							string pdfPathname = Path.Combine(outputPathPdf, data.GetPdfFilename);			// PDFファイル名

							// Excelファイル出力
							オン資補助金申請書類出力.WriteExcel補助金申請書類(orgPathname, excelPathname, data);
							if (false == checkBoxNotPDF.Checked)
							{
								// PDFファイル出力
								using (ExcelManager manage = new ExcelManager())
								{
									manage.Open(excelPathname);
									manage.SaveAsPDF(pdfPathname);
								}
							}
						}
					}
					else
					{
						// 第２弾
						foreach (補助金申請出力情報 data in dataList)
						{
							// \\wwsv\ons-pics\顧客No\助成金申請書類
							string outputPath = Path.Combine(Program.gSettings.補助金申請書類フォルダ, data.顧客No.ToString());
							outputPath = Path.Combine(outputPath, オン資補助金申請書類出力.SubsidyFolderName);
							if (false == Directory.Exists(outputPath))
							{
								Directory.CreateDirectory(outputPath);
							}
							string excelPathname = Path.Combine(outputPath, data.GetExcelFilename);	// Excelファイル名
							string pdfPathname = Path.Combine(outputPath, data.GetPdfFilename);		// PDFファイル名

							// Excelファイル出力
							オン資補助金申請書類出力.WriteExcel補助金申請書類(orgPathname, excelPathname, data);
							if (false == checkBoxNotPDF.Checked)
							{
								// PDFファイル出力
								using (ExcelManager manage = new ExcelManager())
								{
									manage.Open(excelPathname);
									manage.SaveAsPDF(pdfPathname);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			MessageBox.Show(this, string.Format("補助金申請書類を出力しました。{0}", Program.gSettings.補助金申請書類フォルダ), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
