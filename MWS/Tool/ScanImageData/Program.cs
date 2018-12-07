using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.ScanImageData;
using MwsLib.DB.SqlServer.ScanImageData;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

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
		/// ログファイル名
		/// </summary>
		public static readonly string LOG_FILENAME = "ScanImageData-{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}.log";

		/// <summary>
		/// 顧客情報リスト
		/// </summary>
		public static List<ScanImageDataFileInfo> CustomerInfoList;

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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 顧客情報リストの取得
			CustomerInfoList = ScanImageDataAccess.GetCustomerInfoList();

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
			switch (bootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					//Application.Run(new Forms.MainForm());
					Application.Run(new Forms.RegistScanImageForm(@"D:\_●営業管理部\●文書インデックス\ScanImageData"));
					//Application.Run(new Forms.DisplayScanImageForm(@"D:\_●営業管理部\●文書インデックス\ScanImageData\【終了届】㈱TMP.PDF"));
					//Application.Run(new Forms.DisplayScanImageForm(@"D:\_●営業管理部\●文書インデックス\ScanImageData\口座振替申込書010003.tif"));
					break;
				// スキャンデータ登録情報の再作成
				case ProgramBootType.Remake:
					string msg;
					RemakeScanImageData("", out msg);
					break;
			}
		}

		/// <summary>
		/// 再作成
		/// </summary>
		/// <param name="scanPath"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public static int RemakeScanImageData(string scanPath, out string msg)
		{
			msg = string.Empty;
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
			List<Tuple<string, string>> searchToroku = null;
			List<string> scanToroku = null;
			string pathToroku = Path.Combine(scanPath, "toroku");
			MakeReadFolderList(pathToroku, out searchToroku, out scanToroku);
			if (0 < searchToroku.Count + scanToroku.Count)
			{
				MakeScanDataFileInfoList(scanPath, ScanImageDataDef.ScanDocumentType.User, searchToroku, scanToroku, ref dataList);
			}
			// 保守契約（解約）
			List<Tuple<string, string>> searchKaiyaku = null;
			List<string> scanKaiyaku = null;
			string pathKaiyaku = Path.Combine(scanPath, @"hosyu\Kaiyaku");
			MakeReadFolderList(pathKaiyaku, out searchKaiyaku, out scanKaiyaku);
			if (0 < searchKaiyaku.Count + scanKaiyaku.Count)
			{
				MakeScanDataFileInfoList(scanPath, ScanImageDataDef.ScanDocumentType.Mainte, searchKaiyaku, scanKaiyaku, ref dataList);
			}
			// 保守契約（加入）
			List<Tuple<string, string>> searchKanyu = null;
			List<string> scanKanyu = null;
			string pathKanyu = Path.Combine(scanPath, @"hosyu\Kanyu");
			MakeReadFolderList(pathKanyu, out searchKanyu, out scanKanyu);
			if (0 < searchKanyu.Count + scanKanyu.Count)
			{
				MakeScanDataFileInfoList(scanPath, ScanImageDataDef.ScanDocumentType.Mainte, searchKanyu, scanKanyu, ref dataList);
			}
			// 口座振替
			List<Tuple<string, string>> searchKofuri = null;
			List<string> scanKofuri = null;
			string pathKofuri = Path.Combine(scanPath, @"kofuri");
			MakeReadFolderList(pathKofuri, out searchKofuri, out scanKofuri);
			if (0 < searchKofuri.Count + scanKofuri.Count)
			{
				MakeScanDataFileInfoList(scanPath, ScanImageDataDef.ScanDocumentType.AccountTransfer, searchKofuri, scanKofuri, ref dataList);
			}
			// 取引条件確認書
			List<Tuple<string, string>> searchTransaction = null;
			List<string> scanTransaction = null;
			string transactionPath = Path.Combine(scanPath, @"取引条件確認書");
			MakeReadFolderList(transactionPath, out searchTransaction, out scanTransaction);
			if (0 < searchTransaction.Count + scanTransaction.Count)
			{
				MakeScanDataFileInfoList(scanPath, ScanImageDataDef.ScanDocumentType.Transaction, searchTransaction, scanTransaction, ref dataList);
			}
			// リモートサービス利用規約同意書
			List<Tuple<string, string>> searchConcent = null;
			List<string> scanConcent = null;
			string pathConcent = Path.Combine(scanPath, @"リモートサービス利用規約同意書");
			MakeReadFolderList(pathConcent, out searchConcent, out scanConcent);
			if (0 < searchConcent.Count + scanConcent.Count)
			{
				MakeScanDataFileInfoList(scanPath, ScanImageDataDef.ScanDocumentType.Consent, searchConcent, scanConcent, ref dataList);
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
				}
				catch (Exception ex)
				{
					msg = string.Format("ScanImageDataSetIO.InsertIntoDocumentIndexList() Error({0})", ex.Message);
					return -1;
				}
				return dataList.Count;
			}
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
			string searchPath = Path.Combine(path, "得意先検索");
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
		private static void SearchFolder(string path, List<Tuple<string, string>> searchList, ref List<string> scanFolders)
		{
			string[] folders = Directory.GetDirectories(path, "*", System.IO.SearchOption.TopDirectoryOnly);
			if (0 < folders.Count())
			{
				foreach (string folder in folders)
				{
					// c:\ScanImageData\touroku\01～05 → 01～05
					if (-1 == searchList.FindIndex(p => p.Item2 == folder) && Path.Combine(path, "得意先検索") != folder)
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
		private static int MakeScanDataFileInfoList(string rootPath, ScanImageDataDef.ScanDocumentType document, List<Tuple<string, string>> searchList, List<string> scanFolders, ref List<ScanImageDataFileInfo> dataList)
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
