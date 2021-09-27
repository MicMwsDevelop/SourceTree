//
// Program.cs
// 
// 文書インデックス管理 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// Ver1.01 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
//
using CommonLib.BaseFactory.ScanImageManager;
using CommonLib.DB.SqlServer.ScanImageManager;
using MwsLib.Log;
using ScanImageManager.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ScanImageManager
{
	static class Program
	{
		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.01 (2021/09/07)";

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
		public static List<ScanImageFile> CustomerInfoList;

		/// <summary>
		/// 環境設定ファイル
		/// </summary>
		public static ScanImageManagerSettings gSettings = null;

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
			gSettings = ScanImageManagerSettingsIF.GetScanImageDataSettings();

			// 顧客情報リストの取得
			CustomerInfoList = ScanImageManagerAccess.GetCustomerInfoList(gSettings.Connect.Junp.ConnectionString);

			switch (bootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Forms.MainForm());
					break;
				// スキャンデータ登録情報の再作成
				case ProgramBootType.Remake:
					Program.RemakeScanImageData(gSettings.ImagePath);
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
		/// <param name="imagePath">文書パス名</param>
		/// <returns>結果</returns>
		public static int RemakeScanImageData(string imagePath)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			// 登録・変更
			List<ScanImageFile> torokuList = new List<ScanImageFile>();
			Logger.Out(logPathname, "登録・変更 開始");
			List<Tuple<string, string>> searchToroku = null;
			List<string> scanToroku = null;
			string pathToroku = Path.Combine(imagePath, ScanImageManagerDef.FolderToroku);
			MakeReadFolderList(pathToroku, out searchToroku, out scanToroku);
			if (0 < searchToroku.Count + scanToroku.Count)
			{
				MakeScanDataFileInfoList(imagePath, logPathname, ScanImageManagerDef.ScanDocumentType.Toroku, searchToroku, scanToroku, ref torokuList);
			}
			Logger.Out(logPathname, "登録・変更 終了");

			// 保守契約（解約）
			List<ScanImageFile> kaiyakuList = new List<ScanImageFile>();
			Logger.Out(logPathname, "保守契約（解約） 開始");
			List<Tuple<string, string>> searchKaiyaku = null;
			List<string> scanKaiyaku = null;
			string pathKaiyaku = Path.Combine(imagePath, ScanImageManagerDef.FolderHoshuKaiyaku);
			MakeReadFolderList(pathKaiyaku, out searchKaiyaku, out scanKaiyaku);
			if (0 < searchKaiyaku.Count + scanKaiyaku.Count)
			{
				MakeScanDataFileInfoList(imagePath, logPathname, ScanImageManagerDef.ScanDocumentType.Hosyu, searchKaiyaku, scanKaiyaku, ref kaiyakuList);
			}
			Logger.Out(logPathname, "保守契約（解約） 終了");

			// 保守契約（加入）
			List<ScanImageFile> kanyuList = new List<ScanImageFile>();
			Logger.Out(logPathname, "保守契約（加入） 開始");
			List<Tuple<string, string>> searchKanyu = null;
			List<string> scanKanyu = null;
			string pathKanyu = Path.Combine(imagePath, ScanImageManagerDef.FolderHoshuKanyu);
			MakeReadFolderList(pathKanyu, out searchKanyu, out scanKanyu);
			if (0 < searchKanyu.Count + scanKanyu.Count)
			{
				MakeScanDataFileInfoList(imagePath, logPathname, ScanImageManagerDef.ScanDocumentType.Hosyu, searchKanyu, scanKanyu, ref kanyuList);
			}
			Logger.Out(logPathname, "保守契約（加入） 終了");

			// 口座振替
			List<ScanImageFile> kofuriList = new List<ScanImageFile>();
			Logger.Out(logPathname, "口座振替  開始");
			List<Tuple<string, string>> searchKofuri = null;
			List<string> scanKofuri = null;
			string pathKofuri = Path.Combine(imagePath, ScanImageManagerDef.FolderKofuri);
			MakeReadFolderList(pathKofuri, out searchKofuri, out scanKofuri);
			if (0 < searchKofuri.Count + scanKofuri.Count)
			{
				MakeScanDataFileInfoList(imagePath, logPathname, ScanImageManagerDef.ScanDocumentType.Kofuri, searchKofuri, scanKofuri, ref kofuriList);
			}
			Logger.Out(logPathname, "口座振替  終了");

			// 取引条件確認書
			List<ScanImageFile> transList = new List<ScanImageFile>();
			Logger.Out(logPathname, "取引条件確認書  開始");
			List<Tuple<string, string>> searchTransaction = null;
			List<string> scanTransaction = null;
			string transactionPath = Path.Combine(imagePath, ScanImageManagerDef.FolderTransaction);
			MakeReadFolderList(transactionPath, out searchTransaction, out scanTransaction);
			if (0 < searchTransaction.Count + scanTransaction.Count)
			{
				MakeScanDataFileInfoList(imagePath, logPathname, ScanImageManagerDef.ScanDocumentType.Transaction, searchTransaction, scanTransaction, ref transList);
			}
			Logger.Out(logPathname, "取引条件確認書  終了");

			// リモートサービス利用規約同意書
			List<ScanImageFile> remoteList = new List<ScanImageFile>();
			Logger.Out(logPathname, "リモートサービス利用規約同意書  開始");
			List<Tuple<string, string>> searchConcent = null;
			List<string> scanConcent = null;
			string pathConcent = Path.Combine(imagePath, ScanImageManagerDef.FolderRemote);
			MakeReadFolderList(pathConcent, out searchConcent, out scanConcent);
			if (0 < searchConcent.Count + scanConcent.Count)
			{
				MakeScanDataFileInfoList(imagePath, logPathname, ScanImageManagerDef.ScanDocumentType.Remote, searchConcent, scanConcent, ref remoteList);
			}
			Logger.Out(logPathname, "リモートサービス利用規約同意書  終了");

			if (0 < torokuList.Count + kaiyakuList.Count + kanyuList.Count + kofuriList.Count + transList.Count + remoteList.Count)
			{
				try
				{
					// スキャンデータ登録情報リストの全削除
					ScanImageManagerSetIO.DeleteDocumentIndex(gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "スキャンデータ登録情報リストの全削除  終了");

					// 登録・変更の追加
					ScanImageManagerSetIO.InsertIntoDocumentIndexList(1, torokuList, gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "登録・変更の追加  終了");

					// 保守契約（解約）の追加
					ScanImageManagerSetIO.InsertIntoDocumentIndexList(torokuList.Count + 1, kaiyakuList, gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "保守契約（解約）の追加  終了");

					// 保守契約（加入）の追加
					ScanImageManagerSetIO.InsertIntoDocumentIndexList(torokuList.Count + kaiyakuList.Count + 1, kanyuList, gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "保守契約（加入）の追加  終了");

					// 口座振替の追加
					ScanImageManagerSetIO.InsertIntoDocumentIndexList(torokuList.Count + kaiyakuList.Count + kanyuList.Count + 1, kofuriList, gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "口座振替の追加  終了");

					// 取引条件確認書の追加
					ScanImageManagerSetIO.InsertIntoDocumentIndexList(torokuList.Count + kaiyakuList.Count + kanyuList.Count + kofuriList.Count + 1, transList, gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "取引条件確認書の追加  終了");

					// リモートサービス利用規約同意書の追加
					ScanImageManagerSetIO.InsertIntoDocumentIndexList(torokuList.Count + kaiyakuList.Count + kanyuList.Count + kofuriList.Count + transList.Count + 1, remoteList, gSettings.Connect.Junp.ConnectionString);
					Logger.Out(logPathname, "リモートサービス利用規約同意書の追加  終了");
				}
				catch (Exception ex)
				{
					Logger.Out(logPathname, string.Format("#ERROR:ScanImageManagerSetIO.InsertIntoDocumentIndexList ({0})", ex.Message));
					Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
					return -1;
				}
				Logger.Out(logPathname, string.Format("{0} {1}:文書インデックス管理 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));

				return torokuList.Count + kaiyakuList.Count + kanyuList.Count + kofuriList.Count + transList.Count + remoteList.Count;
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
			string searchPath = Path.Combine(path, ScanImageManagerDef.TokuisakiSearch);
			if (Directory.Exists(searchPath))
			{
				// c:\ScanImageData\touroku\得意先検索
				List<string> searchFiles = Directory.EnumerateFiles(searchPath, "*.txt", SearchOption.TopDirectoryOnly).ToList();
				searchFiles.Remove(Path.Combine(searchPath, ScanImageManagerDef.TempThumbnailFile));
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
					if (-1 == searchList.FindIndex(p => p.Item2 == folder) && Path.Combine(path, ScanImageManagerDef.TokuisakiSearch) != folder)
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
		private static int MakeScanDataFileInfoList(string rootPath, string logPathname, ScanImageManagerDef.ScanDocumentType document, List<Tuple<string, string>> searchList, List<string> scanFolders, ref List<ScanImageFile> dataList)
		{
			// 得意先検索からスキャンデータファイルを登録
			Encoding enc = Encoding.GetEncoding("shift_jis");
			for (int i = 0; i < searchList.Count; i++)
			{
				List<string> scanDataFiles = Directory.EnumerateFiles(searchList[i].Item2, "*.*", SearchOption.TopDirectoryOnly).ToList();
				scanDataFiles.Remove(Path.Combine(searchList[i].Item2, ScanImageManagerDef.TempThumbnailFile));
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
										ScanImageFile src = CustomerInfoList.Find(p => p.TokuisakiNo == values[1].Trim('\"'));
										if (null != src)
										{
											if (lineCnt - 1 < scanDataFiles.Count)
											{
												ScanImageFile data = new ScanImageFile(src);
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
										ScanImageFile src = CustomerInfoList.Find(p => p.TokuisakiNo == values[0].Trim('\"'));
										if (null != src)
										{
											if (lineCnt - 1 < scanDataFiles.Count)
											{
												ScanImageFile data = new ScanImageFile(src);
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
				scanFiles.Remove(Path.Combine(folder, ScanImageManagerDef.TempThumbnailFile));
				scanFiles.Sort();
				foreach (string filename in scanFiles)
				{
					// ファイル名から得意先番号を抜き出す
					string scanFilename = Path.GetFileNameWithoutExtension(filename);
					string no = Regex.Replace(scanFilename, @"[^0-9]", "");
					if (6 <= no.Length)
					{
						// 解約010080_1.tif→010080
						string tokuisakiNo = no.Substring(0, 6);
						ScanImageFile src = CustomerInfoList.Find(p => p.TokuisakiNo == tokuisakiNo);
						if (null != src)
						{
							ScanImageFile data = new ScanImageFile(src);
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
			foreach (ScanImageFile info in dataList)
			{
				Logger.Out(logPathname, info.LogOut());
			}
			return dataList.Count;
		}
	}
}
