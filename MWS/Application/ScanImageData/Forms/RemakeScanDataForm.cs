//
// RemakeScanDataForm.cs
// 
// スキャンデータ登録情報再作成画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using MwsLib.BaseFactory.ScanImageData;
using MwsLib.DB.SqlServer.ScanImageData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ScanImageData.Forms
{
	/// <summary>
	/// スキャンデータ登録情報再作成画面
	/// </summary>
	public partial class RemakeScanDataForm : Form
	{
		/// <summary>
		/// 顧客情報リスト
		/// </summary>
		private static List<ScanImageDataFileInfo> CustomerInfoList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public RemakeScanDataForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemakeScanDataForm_Load(object sender, EventArgs e)
		{
			textBoxScanImageDataPath.Text = Program.gSettings.ScanImageDataPath;

			// 顧客情報リストの取得
			CustomerInfoList = ScanImageDataAccess.GetCustomerInfoList();
		}

		/// <summary>
		/// スキャンデータ登録パスの指定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputPath_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog form = new FolderBrowserDialog())
			{
				form.Description = "フォルダを指定してください。";
				form.RootFolder = Environment.SpecialFolder.Desktop;
				form.SelectedPath = @"C:\Windows";
				form.ShowNewFolderButton = true;
				if (DialogResult.OK == form.ShowDialog(this))
				{
					textBoxScanImageDataPath.Text = form.SelectedPath;
				}
			}
		}

		/// <summary>
		/// 再作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRemake_Click(object sender, EventArgs e)
		{
			if (0 == textBoxScanImageDataPath.Text.Length)
			{
				return;
			}
			if (false == Directory.Exists(textBoxScanImageDataPath.Text))
			{
				MessageBox.Show("指定されたフォルダが存在しません。", "再作成", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			if (DialogResult.No == MessageBox.Show("スキャンデータ登録情報を再作成します。よろしいですか？", "再作成", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				// SCAN_DATAの全削除
				//SQLiteScanImageDataSetIO.DeleteScanData(Directory.GetCurrentDirectory());
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("ScanImageDataSetIO.DeleteScanData() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			List<ScanImageDataFileInfo> dataList = new List<ScanImageDataFileInfo>();

			// 登録・変更
			List<Tuple<string, string>> searchToroku = null;
			List<string> scanToroku = null;
			string pathToroku = Path.Combine(textBoxScanImageDataPath.Text, ScanImageDataDef.FolderToroku);
			MakeReadFolderList(pathToroku, out searchToroku, out scanToroku);
			if (0 < searchToroku.Count + scanToroku.Count)
			{
				MakeScanDataFileInfoList(textBoxScanImageDataPath.Text, ScanImageDataDef.ScanDocumentType.Toroku, searchToroku, scanToroku, ref dataList);
			}
			// 保守契約（解約）
			List<Tuple<string, string>> searchKaiyaku = null;
			List<string> scanKaiyaku = null;
			string pathKaiyaku = Path.Combine(textBoxScanImageDataPath.Text, ScanImageDataDef.FolderHoshuKaiyaku);
			MakeReadFolderList(pathKaiyaku, out searchKaiyaku, out scanKaiyaku);
			if (0 < searchKaiyaku.Count + scanKaiyaku.Count)
			{
				MakeScanDataFileInfoList(textBoxScanImageDataPath.Text, ScanImageDataDef.ScanDocumentType.Hosyu, searchKaiyaku, scanKaiyaku, ref dataList);
			}
			// 保守契約（加入）
			List<Tuple<string, string>> searchKanyu = null;
			List<string> scanKanyu = null;
			string pathKanyu = Path.Combine(textBoxScanImageDataPath.Text, ScanImageDataDef.FolderHoshuKanyu);
			MakeReadFolderList(pathKanyu, out searchKanyu, out scanKanyu);
			if (0 < searchKanyu.Count + scanKanyu.Count)
			{
				MakeScanDataFileInfoList(textBoxScanImageDataPath.Text, ScanImageDataDef.ScanDocumentType.Hosyu, searchKanyu, scanKanyu, ref dataList);
			}
			// 口座振替
			List<Tuple<string, string>> searchKofuri = null;
			List<string> scanKofuri = null;
			string pathKofuri = Path.Combine(textBoxScanImageDataPath.Text, ScanImageDataDef.FolderKofuri);
			MakeReadFolderList(pathKofuri, out searchKofuri, out scanKofuri);
			if (0 < searchKofuri.Count + scanKofuri.Count)
			{
				MakeScanDataFileInfoList(textBoxScanImageDataPath.Text, ScanImageDataDef.ScanDocumentType.Kofuri, searchKofuri, scanKofuri, ref dataList);
			}
			// 取引条件確認書
			List<Tuple<string, string>> searchTransaction = null;
			List<string> scanTransaction = null;
			string transactionPath = Path.Combine(textBoxScanImageDataPath.Text, ScanImageDataDef.FolderTransaction);
			MakeReadFolderList(transactionPath, out searchTransaction, out scanTransaction);
			if (0 < searchTransaction.Count + scanTransaction.Count)
			{
				MakeScanDataFileInfoList(textBoxScanImageDataPath.Text, ScanImageDataDef.ScanDocumentType.Transaction, searchTransaction, scanTransaction, ref dataList);
			}
			// リモートサービス利用規約同意書
			List<Tuple<string, string>> searchConcent = null;
			List<string> scanConcent = null;
			string pathConcent = Path.Combine(textBoxScanImageDataPath.Text, ScanImageDataDef.FolderRemote);
			MakeReadFolderList(pathConcent, out searchConcent, out scanConcent);
			if (0 < searchConcent.Count + scanConcent.Count)
			{
				MakeScanDataFileInfoList(textBoxScanImageDataPath.Text, ScanImageDataDef.ScanDocumentType.Remote, searchConcent, scanConcent, ref dataList);
			}
			if (0 < dataList.Count)
			{
				try
				{
					// スキャンデータ登録情報リストの追加
					//SQLiteScanImageDataSetIO.InsertIntoUserScanDataList(Directory.GetCurrentDirectory(), dataList);

					// スキャンデータ登録情報リストの全削除
					ScanImageDataSetIO.DeleteDocumentIndex(Program.DATABACE_ACCEPT_CT);

					// スキャンデータ登録情報リストの追加
					ScanImageDataSetIO.InsertIntoDocumentIndexList(dataList, Program.DATABACE_ACCEPT_CT);


					MessageBox.Show("スキャンデータ登録情報の再作成が終了しました。", "再作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("ScanImageDataSetIO.InsertIntoDocumentIndexList() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			else
			{
				MessageBox.Show("登録するスキャンデータはありませんでした。", "再作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// スキャンデータファイル格納情報の取得
		/// </summary>
		/// <param name="path">検索パス</param>
		/// <param name="searchFiles">得意先検索ファイル名リスト</param>
		/// <param name="searchFolders">得意先検索フォルダリスト</param>
		/// <param name="scanFolders">スキャンデータ登録リスト</param>
		private void MakeReadFolderList(string path, out List<Tuple<string, string>> searchList, out List<string> scanFolders)
		{
			// 得意先検索フォルダにある得意先検索ファイルからスキャンデータフォルダ名の取得
			searchList = new List<Tuple<string, string>>();
			string searchPath = Path.Combine(path, ScanImageDataDef.TokuisakiSearch);
			if (Directory.Exists(searchPath))
			{
				// c:\ScanImageData\touroku\得意先検索
				List<string> searchFiles = Directory.EnumerateFiles(searchPath, "*.txt", SearchOption.TopDirectoryOnly).ToList();
				searchFiles.Remove(Path.Combine(searchPath, "Thumbs.db"));
				for (int i = 0; i < searchFiles.Count; i++)
				{
					string folder = Path.Combine(path, Path.GetFileNameWithoutExtension(searchFiles[i]));
					if (Directory.Exists(folder))
					{
						// Item1：c:\ScanImageData\touroku\得意先検索\20100913.txt
						// Item2：c:\ScanImageData\touroku\得意先検索\20100913.txt → c:\ScanImageData\touroku\20100913
						searchList.Add(new Tuple<string, string>(searchFiles[i], folder));
					}
				}
			}
			// 得意先検索で設定されていないフォルダのスキャンデータフォルダ名の取得
			scanFolders = new List<string>();

			// ルートフォルダを設定
			scanFolders.Add(path);

			// c:\ScanImageData\touroku
			SearchFolder(path, searchList, ref scanFolders);
		}

		/// <summary>
		/// スキャンデータ登録情報フォルダの検索
		/// </summary>
		/// <param name="path">検索パス</param>
		/// <param name="searchList">得意先検索フォルダリスト</param>
		/// <param name="scanFolders">スキャンデータ登録リスト</param>
		private void SearchFolder(string path, List<Tuple<string, string>> searchList, ref List<string> scanFolders)
		{
			string[] folders = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
			if (0 < folders.Count())
			{
				foreach (string folder in folders)
				{
					// c:\ScanImageData\touroku\01～05 → 01～05
					if (-1 == searchList.FindIndex(p => p.Item2 == folder) && Path.Combine(path, ScanImageDataDef.TokuisakiSearch) != folder)
					{
						// 得意先検索で設定されていないフォルダ
						scanFolders.Add(folder);
					}
					// 再帰
					SearchFolder(folder, searchList, ref scanFolders);
				}
			}
		}

		/// <summary>
		/// スキャンデータファイル登録情報リストの作成
		/// </summary>
		/// <param name="document">文書種別</param>
		/// <param name="searchList"></param>
		/// <param name="scanFolders"></param>
		/// <param name="dataList">スキャンデータファイル登録情報リスト</param>
		/// <returns>スキャンデータファイル登録情報数</returns>
		private int MakeScanDataFileInfoList(string rootPath, ScanImageDataDef.ScanDocumentType document, List<Tuple<string, string>> searchList, List<string> scanFolders, ref List<ScanImageDataFileInfo> dataList)
		{
			// 得意先検索からスキャンデータファイルを登録
			Encoding enc = Encoding.GetEncoding("shift_jis");
			for (int i = 0; i < searchList.Count; i++)
			{
				List<string> scanDataFiles = Directory.EnumerateFiles(searchList[i].Item2, "*.*", SearchOption.TopDirectoryOnly).ToList();
				scanDataFiles.Remove(Path.Combine(searchList[i].Item2, "Thumbs.db"));
				if (0 < scanDataFiles.Count)
				{
					try
					{
						// 得意先検索ファイルの読込み
						using (var sr = new StreamReader(searchList[i].Item1, enc))
						{
							int lineCnt = 0;
							while (!sr.EndOfStream)
							{
								string line = sr.ReadLine().Trim();
								if (0 == lineCnt)
								{
									lineCnt++;
									continue;
								}
								if (0 < line.Length)
								{
									string[] values = line.Split(',');
									if (3 == values.Length)
									{
										ScanImageDataFileInfo src = CustomerInfoList.Find(p => p.TokuisakiNo == values[1].Trim('\"'));
										if (null != src)
										{
											if (lineCnt - 1 < scanDataFiles.Count)
											{
												ScanImageDataFileInfo data = new ScanImageDataFileInfo(src);
												data.Document = document;
												data.SetFolderName(rootPath, searchList[i].Item2);                          // c:\ScanImageData\touroku\20100913
												data.FileName = Path.GetFileName(scanDataFiles[lineCnt - 1]);   // 登録カード100913134925(0001).tif
												data.FileDateTime = File.GetLastWriteTime(scanDataFiles[lineCnt - 1]);
												dataList.Add(data);
											}
										}
									}
									else if (2 == values.Length)
									{
										// 保守契約（加入）の得意先検索は顧客Noがない
										ScanImageDataFileInfo src = CustomerInfoList.Find(p => p.TokuisakiNo == values[0].Trim('\"'));
										if (null != src)
										{
											if (lineCnt - 1 < scanDataFiles.Count)
											{
												ScanImageDataFileInfo data = new ScanImageDataFileInfo(src);
												data.Document = document;
												data.SetFolderName(rootPath, searchList[i].Item2);              // c:\ScanImageData\touroku\20100913
												data.FileName = Path.GetFileName(scanDataFiles[lineCnt - 1]);   // 登録カード100913134925(0001).tif
												data.FileDateTime = File.GetLastWriteTime(scanDataFiles[lineCnt - 1]);
												dataList.Add(data);
											}
										}
									}
								}
								lineCnt++;
							}
						}
					}
					catch
					{
						throw;
					}
				}
			}
			// 得意先検索で設定されていないフォルダのスキャンデータの読込み ex. 01～05など
			// c:\ScanImageData\touroku
			foreach (string folder in scanFolders)
			{
				List<string> scanFiles = Directory.EnumerateFiles(folder, "*.*", SearchOption.TopDirectoryOnly).ToList();
				scanFiles.Remove(Path.Combine(folder, "Thumbs.db"));
				foreach (string filename in scanFiles)
				{
					// ファイル名から得意先番号を抜き出す
					string scanFilename = Path.GetFileNameWithoutExtension(filename);
					string tokuisakiNo = Regex.Replace(scanFilename, @"[^0-9]", "");
					if (6 == tokuisakiNo.Length)
					{
						ScanImageDataFileInfo src = CustomerInfoList.Find(p => p.TokuisakiNo == tokuisakiNo);
						if (null != src)
						{
							ScanImageDataFileInfo data = new ScanImageDataFileInfo(src);
							data.Document = document;
							data.SetFolderName(rootPath, folder);
							data.FileName = Path.GetFileName(filename);
							data.FileDateTime = File.GetLastWriteTime(filename);
							dataList.Add(data);
						}
					}
				}
			}
			return dataList.Count;
		}
	}
}
