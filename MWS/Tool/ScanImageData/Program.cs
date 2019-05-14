//
// Program.cs
// 
// 文書インデックス管理 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using MwsLib.BaseFactory.ScanImageData;
using MwsLib.DB.SqlServer.ScanImageData;
using MwsLib.Log;
using ScanImageData.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ScanImageData
{
	static class Program
	{
		/// <summary>
		/// 起動引数
		/// </summary>
		public enum ProgramBootType
		{
			/// <summary>
			/// メイン画面起動
			/// </summary>
			Menu = 0,

			/// <summary>
			/// スキャンデータ登録情報の再作成
			/// </summary>
			Remake = 1,
		}

		/// <summary>
		/// プログラム名
		/// </summary>
		public static readonly string PROGRAM_NAME = "文書インデックス管理";

		/// <summary>
		/// ログファイル名
		/// </summary>
		public static readonly string LOG_FILENAME = "ScanImageData-{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}.log";

		/// <summary>
		/// 顧客情報リスト
		/// </summary>
		public static List<ScanImageDataFileInfo> CustomerInfoList;

		/// <summary>
		/// 環境設定ファイル
		/// </summary>
		public static ScanImageDataSettings gSettings = null;

		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public const bool DATABACE_ACCEPT_CT = true;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// コマンドライン引数を配列で取得する
			ProgramBootType bootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("1" == cmds[1])
				{
					bootType = ProgramBootType.Remake;
				}
			}
			gSettings = ScanImageDataSettingsIF.GetScanImageDataSettings();
			string scanPath = gSettings.ScanImageDataPath;
			if (DATABACE_ACCEPT_CT)
			{
				scanPath = gSettings.TestScanImageDataPath;
			}
			// 顧客情報リストの取得
			CustomerInfoList = ScanImageDataAccess.GetCustomerInfoList(DATABACE_ACCEPT_CT);

			switch (bootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Forms.RegistScanImageForm(scanPath));
					break;
				// スキャンデータ登録情報の再作成
				case ProgramBootType.Remake:
					Program.RemakeScanImageData(scanPath);
					break;
			}
		}

		/// <summary>
		/// ログファイルのパス名を取得
		/// </summary>
		/// <returns>ログファイルパス名</returns>
		private static string GetLogPathname()
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format(LOG_FILENAME, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute));
		}

		/// <summary>
		/// 再作成
		/// </summary>
		/// <param name="scanPath">文書パス名</param>
		/// <param name="logPathname">ログファイルパス名</param>
		/// <returns>結果</returns>
		public static int RemakeScanImageData(string scanPath)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			//try
			//{
			//	// SCAN_DATAの全削除
			//	SQLiteScanImageDataSetIO.DeleteScanData(Directory.GetCurrentDirectory());
			//}
			//catch (Exception ex)
			//{
			//	msg = string.Format("ScanImageDataSetIO.DeleteScanData() Error({0})", ex.Message);
			//	return -1;
			//}
			List<ScanImageDataFileInfo> dataList = new List<ScanImageDataFileInfo>();

			// 登録・変更
			Logger.Out(logPathname, "登録・変更 開始");
			List<Tuple<string, string>> searchToroku = null;
			List<string> scanToroku = null;
			string pathToroku = Path.Combine(scanPath, ScanImageDataDef.FolderToroku);
			MakeReadFolderList(pathToroku, out searchToroku, out scanToroku);
			if (0 < searchToroku.Count + scanToroku.Count)
			{
				MakeScanDataFileInfoList(scanPath, logPathname, ScanImageDataDef.ScanDocumentType.Toroku, searchToroku, scanToroku, ref dataList);
			}
			Logger.Out(logPathname, "登録・変更 終了");

			// 保守契約（解約）
			Logger.Out(logPathname, "保守契約（解約） 開始");
			List<Tuple<string, string>> searchKaiyaku = null;
			List<string> scanKaiyaku = null;
			string pathKaiyaku = Path.Combine(scanPath, ScanImageDataDef.FolderHoshuKaiyaku);
			MakeReadFolderList(pathKaiyaku, out searchKaiyaku, out scanKaiyaku);
			if (0 < searchKaiyaku.Count + scanKaiyaku.Count)
			{
				MakeScanDataFileInfoList(scanPath, logPathname, ScanImageDataDef.ScanDocumentType.Hosyu, searchKaiyaku, scanKaiyaku, ref dataList);
			}
			Logger.Out(logPathname, "保守契約（解約） 終了");

			// 保守契約（加入）
			Logger.Out(logPathname, "保守契約（加入） 開始");
			List<Tuple<string, string>> searchKanyu = null;
			List<string> scanKanyu = null;
			string pathKanyu = Path.Combine(scanPath, ScanImageDataDef.FolderHoshuKanyu);
			MakeReadFolderList(pathKanyu, out searchKanyu, out scanKanyu);
			if (0 < searchKanyu.Count + scanKanyu.Count)
			{
				MakeScanDataFileInfoList(scanPath, logPathname, ScanImageDataDef.ScanDocumentType.Hosyu, searchKanyu, scanKanyu, ref dataList);
			}
			Logger.Out(logPathname, "保守契約（加入） 終了");

			// 口座振替
			Logger.Out(logPathname, "口座振替  開始");
			List<Tuple<string, string>> searchKofuri = null;
			List<string> scanKofuri = null;
			string pathKofuri = Path.Combine(scanPath, ScanImageDataDef.FolderKofuri);
			MakeReadFolderList(pathKofuri, out searchKofuri, out scanKofuri);
			if (0 < searchKofuri.Count + scanKofuri.Count)
			{
				MakeScanDataFileInfoList(scanPath, logPathname, ScanImageDataDef.ScanDocumentType.Kofuri, searchKofuri, scanKofuri, ref dataList);
			}
			Logger.Out(logPathname, "口座振替  終了");

			// 取引条件確認書
			Logger.Out(logPathname, "取引条件確認書  開始");
			List<Tuple<string, string>> searchTransaction = null;
			List<string> scanTransaction = null;
			string transactionPath = Path.Combine(scanPath, ScanImageDataDef.FolderTransaction);
			MakeReadFolderList(transactionPath, out searchTransaction, out scanTransaction);
			if (0 < searchTransaction.Count + scanTransaction.Count)
			{
				MakeScanDataFileInfoList(scanPath, logPathname, ScanImageDataDef.ScanDocumentType.Transaction, searchTransaction, scanTransaction, ref dataList);
			}
			Logger.Out(logPathname, "取引条件確認書  終了");

			// リモートサービス利用規約同意書
			Logger.Out(logPathname, "リモートサービス利用規約同意書  開始");
			List<Tuple<string, string>> searchConcent = null;
			List<string> scanConcent = null;
			string pathConcent = Path.Combine(scanPath, ScanImageDataDef.FolderRemote);
			MakeReadFolderList(pathConcent, out searchConcent, out scanConcent);
			if (0 < searchConcent.Count + scanConcent.Count)
			{
				MakeScanDataFileInfoList(scanPath, logPathname, ScanImageDataDef.ScanDocumentType.Remote, searchConcent, scanConcent, ref dataList);
			}
			Logger.Out(logPathname, "リモートサービス利用規約同意書  終了");

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
				}
				catch (Exception ex)
				{
					Logger.Out(logPathname, string.Format("#ERROR:ScanImageDataSetIO.InsertIntoDocumentIndexList ({0})", ex.Message));
					Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
					return -1;
				}
				Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));

				return dataList.Count;
			}
			Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			return 0;
		}

		/// <summary>
		/// スキャンデータファイル格納情報の取得
		/// </summary>
		/// <param name="path">検索パス</param>
		/// <param name="searchFiles">得意先検索ファイル名リスト</param>
		/// <param name="searchFolders">得意先検索フォルダリスト</param>
		/// <param name="scanFolders">スキャンデータ登録リスト</param>
		private static void MakeReadFolderList(string path, out List<Tuple<string, string>> searchList, out List<string> scanFolders)
		{
			// 得意先検索フォルダにある得意先検索ファイルからスキャンデータフォルダ名の取得
			searchList = new List<Tuple<string, string>>();
			string searchPath = Path.Combine(path, ScanImageDataDef.TokuisakiSearch);
			if (Directory.Exists(searchPath))
			{
				// c:\ScanImageData\touroku\得意先検索
				List<string> searchFiles = Directory.EnumerateFiles(searchPath, "*.txt", SearchOption.TopDirectoryOnly).ToList();
				searchFiles.Remove(Path.Combine(searchPath, "Thumbs.db"));
				searchFiles.Sort();
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
		private static void SearchFolder(string path, List<Tuple<string, string>> searchList, ref List<string> scanFolders)
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
		/// <param name="logPathname"></param>
		/// <param name="searchList"></param>
		/// <param name="scanFolders"></param>
		/// <param name="dataList">スキャンデータファイル登録情報リスト</param>
		/// <returns>スキャンデータファイル登録情報数</returns>
		private static int MakeScanDataFileInfoList(string rootPath, string logPathname, ScanImageDataDef.ScanDocumentType document, List<Tuple<string, string>> searchList, List<string> scanFolders, ref List<ScanImageDataFileInfo> dataList)
		{
			// 得意先検索からスキャンデータファイルを登録
			Encoding enc = Encoding.GetEncoding("shift_jis");
			for (int i = 0; i < searchList.Count; i++)
			{
				List<string> scanDataFiles = Directory.EnumerateFiles(searchList[i].Item2, "*.*", SearchOption.TopDirectoryOnly).ToList();
				scanDataFiles.Remove(Path.Combine(searchList[i].Item2, "Thumbs.db"));
				scanDataFiles.Sort();
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
												data.SetFolderName(rootPath, searchList[i].Item2);				// c:\ScanImageData\touroku\20100913
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
				scanFiles.Sort();
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
			// ログ出力
			foreach (ScanImageDataFileInfo info in dataList)
			{
				Logger.Out(logPathname, info.LogOut());
			}
			return dataList.Count;
		}
	}
}
