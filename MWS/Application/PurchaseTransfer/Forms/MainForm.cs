using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PurchaseTransfer.Settings;
using CommonLib.BaseFactory.PurchaseTransfer;
using System.IO;
using CommonLib.Common;
using CommonLib.DB.SqlServer.PurchaseTransfer;
using CommonLib.DB.SqlServer;
using System.Data.SqlClient;
using CommonLib.BaseFactory.Pca;

namespace PurchaseTransfer.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static PurchaseTransferSettings gSettings;

		/// <summary>
		/// 集計月
		/// </summary>
		public YearMonth CollectMonth { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			// 先月
			//CollectMonth = Date.Today.FirstDayOfLasMonth().ToYearMonth();
			CollectMonth = new YearMonth(2018, 12);
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 環境設定の読込
			gSettings = PurchaseTransferSettingsIF.GetSettings();

			/////////////////////////////////////
			// 1.在庫一覧テーブルリンク接続
			// 環境設定.在庫一覧表入力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\在庫一覧表\在庫一覧表GG年MM月末.txt

			string msg;
			List<在庫一覧表> zaikoList = ReadZaikoListCsvFile(gSettings.在庫一覧表入力パス名, out msg);
			if (0 == zaikoList.Count)
			{
				MessageBox.Show(msg, "在庫一覧表の読込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				DatabaseAccess.DeleteDatabase("DELETE FROM TEST_在庫一覧表", gSettings.Connect.Charlie.ConnectionString);

				DataTable dt = new DataTable();
				在庫一覧表.SetDataColumn(dt);
				foreach (在庫一覧表 zaiko in zaikoList)
				{
					dt.Rows.Add(zaiko.GetDataRow(dt));
				}
				DatabaseAccess.BulkInsert(gSettings.Connect.Charlie.ConnectionString, "TEST_在庫一覧表", dt);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "在庫一覧表のレコード追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		/// <summary>
		/// START
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			try
			{
				// 仕入振替単価不明伝票ファイルの作成
				using (var fp = new StreamWriter(Program.FumeiListFilePathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					/////////////////////////////////////
					// 2.社内使用分出荷振替データ作成

					// (1)社内使用分出荷明細 選択クエリの実行：2-1 社内使用分出荷明細.sql
					List<PCA出荷明細> 社内使用分出荷明細_List = PurchaseTransferAccess.Select_社内使用分出荷明細(CollectMonth, gSettings.Connect.Junp.ConnectionString);

					// (2)在庫評価単価 選択クエリの実行：2-2 在庫評価単価.sql
					List<在庫評価単価> 在庫評価単価_List = PurchaseTransferAccess.Select_在庫評価単価(gSettings.Connect.Charlie.ConnectionString);

					// (3)当月仕入単価 選択クエリの実行：2-3 当月仕入単価.sql
					List<当月仕入単価> 当月仕入単価_List = PurchaseTransferAccess.Select_当月仕入単価(CollectMonth, gSettings.Connect.Junp.ConnectionString);

					// (4) 社内仕入振替データテーブルの全レコード削除
					// (5) 社内仕入振替データテーブルのレコード追加
					// (6)社内仕入振替データテーブルをエクセルファイルで出力
					// 環境設定.社内使用分振替出力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\仕入振替出力用ﾌｧｲﾙ.xlsx
					;

					/////////////////////////////////////
					// 3.貯蔵品社内使用分出荷振替データ作成

					// (1) 貯蔵品社内使用分出荷明細 選択クエリの実行：3-1 貯蔵品社内使用分出荷明細.sql
					List<PCA出荷明細貯蔵品> 貯蔵品社内使用分出荷明細_List = PurchaseTransferAccess.Select_貯蔵品社内使用分出荷明細(CollectMonth, gSettings.Connect.Junp.ConnectionString);

					// (2)在庫評価単価 選択クエリの実行：2-2 在庫評価単価.sql
					// (3)当月仕入単価貯蔵品 選択クエリの実行：3-2 当月仕入単価貯蔵品.sql
					List<当月仕入単価> 当月仕入単価貯蔵品_List = PurchaseTransferAccess.Select_当月仕入単価貯蔵品(CollectMonth, gSettings.Connect.Junp.ConnectionString);

					// (4)貯蔵品社内仕入振替データテーブルの全レコード削除
					// (5)貯蔵品社内仕入振替データテーブルレコード追加
					// (6)貯蔵品社内仕入振替データテーブルをエクセルファイルとして出力
					// 環境設定.貯蔵品社内使用分振替出力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\貯蔵品振替出力用ファイル.xlsx
					;

					/////////////////////////////////////
					// 4.社内仕入振替データ作成

					振替仕入データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 5.りすとん振替データ作成

					りすとん振替データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 6.問心伝振替データ作成

					問心伝振替データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 7. りすとん月額仕入振替月次データ作成

					りすとん月額仕入振替月次データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 8. Office365仕入振替月次データ作成

					Office365仕入振替月次データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 9. 問心伝月額仕入振替月次データ作成

					問心伝月額仕入振替月次データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 10. ソフトバンク仕入振替月次データ作成

					ソフトバンク仕入振替月次データファイル出力(fp, 在庫評価単価_List, 当月仕入単価_List);

					/////////////////////////////////////
					// 11. Curline本体アプリ仕入作成月次データ作成

					// Curline本体アプリ仕入作成月次 選択クエリの実行：11 Curline本体アプリ仕入作成月次.sql
					List<仕入振替月次> Curline本体アプリ仕入作成月次_List = PurchaseTransferAccess.Select_Curline本体アプリ仕入作成月次(CollectMonth, gSettings.Connect.Junp.ConnectionString);

					// (2)\\SQLSV\PCADATAにCurline本体アプリ仕入データファイルの新規作成
					// 環境設定.Curline本体アプリ出力パス名：\\SQLSV\PCADATA\Curline本体アプリ仕入データ.txt
					// Curline本体アプリ仕入作成月次の結果をファイルに出力
					// (3)プラス分振替データ出力
					;


					MessageBox.Show("終了", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "クエリ実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		/// <summary>
		///在庫一覧表GG年MM月.CSVの読み込み
		/// </summary>
		/// <param name="pathname">パラメタファイルパス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>在庫一覧表リスト</returns>
		public List<在庫一覧表> ReadZaikoListCsvFile(string pathname, out string msg)
		{
			msg = string.Empty;

			List<在庫一覧表> dataList = new List<在庫一覧表>();
			if (File.Exists(pathname))
			{
				try
				{
					// テキストファイルの読み込み
					using (StreamReader textfile = new StreamReader(pathname, System.Text.Encoding.GetEncoding("Shift_JIS")))
					{
						string line = textfile.ReadLine();
						bool firstLine = true;
						while (null != line)
						{
							if (0 < line.Length)
							{
								if (!firstLine)
								{
									line = line.Trim(StringUtil.DefalutTrimCharSet);
									if (';' != line[0])
									{
										// コメント行以外
										List<string> split = SplitString.CSVSplitLine(line);
										在庫一覧表 data = new 在庫一覧表();
										data.SetCsvRecord(split);
										dataList.Add(data);
									}
								}
								else
								{
									// １行目はタイトル行なのでスキップ
									firstLine = false;
								}
							}
							line = textfile.ReadLine();
						}
					}
				}
				catch (Exception ex)
				{
					msg = ex.ToString();
					return null;
				}
			}
			else
			{
				msg = pathname + "が存在しません。";
				return null;
			}
			return dataList;
		}

		/// <summary>
		/// 単価の取得
		/// 在庫がない時は仕入単価を使用
		/// </summary>
		/// <param name="商品コード"></param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		/// <returns>単価</returns>
		private int GetUnitPrice(string 商品コード, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			在庫評価単価 zaiko = 在庫単価.Find(p => p.商品コード == 商品コード);
			if (null != zaiko)
			{
				return zaiko.評価単価;
			}
			当月仕入単価 shiire = 仕入単価.Find(p => p.商品コード == 商品コード);
			if (null != shiire)
			{
				return shiire.単価;
			}
			return 0;
		}

		/// <summary>
		/// 仕入先コードから部門コードへの変換テーブル
		/// </summary>
		/// <param name="仕入先コード"></param>
		/// <returns>部門コード</returns>
		private string GetBumonCode(string 仕入先コード)
		{
			switch (仕入先コード)
			{
				// 000211 営業管理部へ発注 → 011 営業管理部
				case "000211": return "11";
				// 000250 配送センター出荷分 → 050 配送センター
				case "000250": return "50";
				// 000201 札幌倉庫へ発注 → 081 東日本営業部
				case "000201": return "81";
				// 000202 仙台倉庫へ発注 → 081 東日本営業部
				case "000202": return "81";
				// 000225 東京倉庫へ発注 → 082 首都圏営業部
				case "000225": return "82";
				// 000235(禁止)東京第二支店 → 082 首都圏営業部
				case "000235": return "82";
				// 000231 さいたま倉庫へ発注 → 083 関東営業部
				case "000231": return "83";
				// 000233 横浜倉庫へ発注 → 083 関東営業部
				case "000233": return "83";
				// 000234(禁止)八王子倉庫へ発注 → 083 関東営業部
				case "000234": return "83";
				// 000207 名古屋倉庫へ発注 → 086 中部営業部
				case "000207": return "86";
				// 000214 金沢倉庫へ発注 → 086 中部営業部
				case "000214": return "86";
				// 000244 大阪倉庫へ発注 → 087 関西営業部
				case "000244": return "87";
				// 000212 広島倉庫へ発注 → 085 西日本営業部
				case "000212": return "85";
				// 000209 福岡倉庫へ発注 → 085 西日本営業部
				case "000209": return "85";
				// 000275 ヘルスケア営業部へ発注 → 075 ヘルスケア営業部
				case "000275": return "75";
			}
			return string.Empty;
		}

		/// <summary>
		/// 4.社内仕入振替データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void 振替仕入データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1)対象月社内仕入伝票 選択クエリの実行：4-1 対象月社内仕入伝票.sql
			List<対象月社内仕入伝票> 対象月社内仕入伝票_List = PurchaseTransferAccess.Select_対象月社内仕入伝票(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (2)対象月社内仕入明細 選択クエリの実行：4-2 対象月社内仕入明細.sql
			List<PCA仕入明細> 対象月社内仕入れ明細_List = PurchaseTransferAccess.Select_対象月社内仕入明細(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();
			int denNo = 1000;
			foreach (対象月社内仕入伝票 header in 対象月社内仕入伝票_List)
			{
				// 対象月社内仕入れ明細の結果に対し、対象月社内仕入れ明細の結果でフィルタリングした結果をファイルに出力
				List<PCA仕入明細> detailList = 対象月社内仕入れ明細_List.FindAll(p => p.仕入日 == header.仕入日 && p.仕入先コード == header.仕入先コード && p.伝票No == header.伝票No);
				if (null != detailList && 0 < detailList.Count)
				{
					// (7)プラス分振替データ出力
					denNo++;
					foreach (PCA仕入明細 detail in detailList)
					{
						PCA仕入明細汎用データ pca = detail.SetPCA仕入明細汎用データ();
						pca.伝票No = denNo;
						pca.仕入先コード = "furi";
						pca.仕入先名 = string.Empty;    // 仕入先名は空白
						pca.区 = 2;  // 単価修正
						pca.倉庫コード = "0";
						pca.数量 = detail.数量; // 対象月社内仕入れ明細.数量
						pca.単価 = GetUnitPrice(detail.商品コード, 在庫単価, 仕入単価);    // 在庫評価単価.評価単価 or 当月仕入単価.単価 ※検索は在庫評価単価を優先
						pca.金額 = pca.数量 * pca.単価;   // 数量 x 単価
						outputList.Add(pca);
						if (0 == pca.単価)
						{
							// 仕入振替単価不明伝票.txtに出力
							fp.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", pca.伝票No, pca.部門コード, pca.商品コード, pca.数量, pca.商品名));
						}
					}
					// (8)マイナス分振替データ出力
					denNo++;
					foreach (PCA仕入明細 detail in detailList)
					{
						PCA仕入明細汎用データ pca = detail.SetPCA仕入明細汎用データ();
						pca.伝票No = denNo;
						pca.仕入先コード = "furi";
						pca.仕入先名 = string.Empty;
						pca.部門コード = GetBumonCode(detail.仕入先コード);    // 対象月社内仕入れ明細.仕入先コード→部門コード
						pca.担当者コード = "0";
						pca.区 = 2;  // 単価修正
						pca.倉庫コード = "0";
						pca.数量 = -(detail.数量); // 対象月社内仕入れ明細.数量
						pca.単価 = GetUnitPrice(detail.商品コード, 在庫単価, 仕入単価);    // 在庫評価単価.評価単価 or 当月仕入単価.単価 ※検索は在庫評価単価を優先
						pca.金額 = pca.数量 * pca.単価;   // 数量 x 単価
						outputList.Add(pca);
						if (0 == pca.単価)
						{
							// 仕入振替単価不明伝票.txtに出力
							fp.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", pca.伝票No, pca.部門コード, pca.商品コード, pca.数量, pca.商品名));
						}
					}
				}
			}
			// (6) \\SQLSV\PCADATAに振替仕入データファイルの新規作成
			// 環境設定.仕入振替出力パス名：\\SQLSV\PCADATA\振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.仕入振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 5.りすとん振替データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void りすとん振替データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1)tmpりすとん仕入振替月次削除 削除クエリの実行：tmpりすとん仕入振替月次テーブルの全レコード削除
			// (2)りすとん仕入振替月次合計行追加 追加クエリの実行：5-2 りすとん仕入振替月次合計行追加.sql
			List<仕入振替月次追加> りすとん仕入振替月次合計行追加 = PurchaseTransferAccess.Select_りすとん仕入振替月次合計行追加(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (3)りすとん仕入振替月次追加 追加クエリの実行：5-3 りすとん仕入振替月次追加.sql
			List<仕入振替月次追加> りすとん仕入振替月次追加 = PurchaseTransferAccess.Select_りすとん仕入振替月次追加(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			List<仕入振替月次追加> りすとん仕入振替 = new List<仕入振替月次追加>();
			りすとん仕入振替.AddRange(りすとん仕入振替月次合計行追加);
			りすとん仕入振替.AddRange(りすとん仕入振替月次追加);

			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			// (5)プラス分振替データ出力
			int denNo = 20000;
			string bumonCode = string.Empty;
			foreach (仕入振替月次追加 data in りすとん仕入振替)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = "furi";
				pca.仕入先名 =  string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = data.sykd_jtan.Substring(2);
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				if ("11" == bumonCode)
				{
					pca.区 = 0;	// 仕入
					pca.倉庫コード = "11";
					pca.数量 = -(data.数量);
					pca.単価 = 0;
					pca.金額 = 0;
				}
				else
				{
					pca.区 = 2;  // 単価修正
					pca.倉庫コード = "0";
					pca.数量 = data.数量;
					pca.単価 = data.評価単価;
					pca.金額 = pca.数量 * pca.単価;
				}
				pca.単位 = string.Empty;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (6)マイナス分振替データ出力
			bumonCode = string.Empty;
			foreach (仕入振替月次追加 data in りすとん仕入振替)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = "furi";
				pca.仕入先名 =  string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = "11";
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 2;  // 単価修正
				pca.倉庫コード = "11";
				pca.数量 = -(data.数量);
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.税率 = 8; // 10%の間違いでは？
				outputList.Add(pca);
			}
			// (4)\\SQLSV\PCADATAにりすとん振替仕入データファイルの新規作成
			// 環境設定.りすとん振替出力パス名：\\SQLSV\PCADATA\りすとん振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.りすとん振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 6.問心伝振替データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void 問心伝振替データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1)tmp問心伝仕入振替月次削除 削除クエリの実行
			// (2)問心伝仕入振替月次合計行追加 追加クエリの実行：6-2 問心伝仕入振替月次合計行追加.sql
			List<仕入振替月次追加> 問心伝仕入振替月次合計行追加 = PurchaseTransferAccess.Select_問心伝仕入振替月次合計行追加(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (3)問心伝仕入振替月次追加 追加クエリの実行：6-3 問心伝仕入振替月次追加.sql
			List<仕入振替月次追加> 問心伝仕入振替月次追加 = PurchaseTransferAccess.Select_問心伝仕入振替月次追加(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			List<仕入振替月次追加> 問心伝仕入振替 = new List<仕入振替月次追加>();
			問心伝仕入振替.AddRange(問心伝仕入振替月次合計行追加);
			問心伝仕入振替.AddRange(問心伝仕入振替月次追加);

			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			// (5)プラス分振替データ出力
			int denNo = 20100;
			string bumonCode = string.Empty;
			foreach (仕入振替月次追加 data in 問心伝仕入振替)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = "furi";
				pca.仕入先名 =  string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = data.sykd_jtan.Substring(2);
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				if ("11" == bumonCode)
				{
					pca.区 = 0;	// 仕入
					pca.倉庫コード = "11";
					pca.数量 = -(data.数量);
					pca.単価 = 0;
					pca.金額 = 0;
				}
				else
				{
					pca.区 = 2;  // 単価修正
					pca.倉庫コード = "0";
					pca.数量 = data.数量;
					pca.単価 = data.評価単価;
					pca.金額 = pca.数量 * pca.単価;
				}
				pca.単位 = string.Empty;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (6)マイナス分振替データ出力
			bumonCode = string.Empty;
			foreach (仕入振替月次追加 data in 問心伝仕入振替)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = "furi";
				pca.仕入先名 =  string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = "11";
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 2;  // 単価修正
				pca.倉庫コード = "11";
				pca.数量 = -(data.数量);
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.税率 = 8; // 10%の間違いでは？
				outputList.Add(pca);
			}
			// (4)\\SQLSV\PCADATAに問心伝振替仕入データファイルの新規作成
			// 環境設定.問心伝振替出力パス名：\\SQLSV\PCADATA\問心伝振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.問心伝振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}
		/// <summary>
		/// 7.りすとん月額仕入振替月次データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void りすとん月額仕入振替月次データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1)りすとん月額仕入振替月次 選択クエリの実行：7 りすとん月額仕入振替月次.sql
			List<仕入振替月次> りすとん月額仕入振替月次_List = PurchaseTransferAccess.Select_りすとん月額仕入振替月次(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (2)プラス分振替データ出力
			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			int denNo = 20020;
			string bumonCode = string.Empty;
			foreach (仕入振替月次 data in りすとん月額仕入振替月次_List)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード =  data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (3)\\SQLSV\PCADATAにりすとん月額振替仕入データファイルの新規作成
			// 環境設定.りすとん月額振替出力パス名：\\SQLSV\PCADATA\りすとん月額振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.りすとん月額振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 8.Office365仕入振替月次データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void Office365仕入振替月次データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1) Office365仕入振替月次 選択クエリの実行：8 Office365仕入振替月次.sql
			List<仕入振替月次> Office365仕入振替月次_List = PurchaseTransferAccess.Select_Office365仕入振替月次(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (2)プラス分振替データ出力
			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			int denNo = 20040;
			string bumonCode = string.Empty;
			foreach (仕入振替月次 data in Office365仕入振替月次_List)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = CollectMonth.First.FirstDayOfNextMonth().ToIntYMD(); // 対象月翌月初日
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (2)\\SQLSV\PCADATAにOffice365振替仕入データファイルの新規作成
			// 環境設定.Office365振替出力パス名：\\SQLSV\PCADATA\Office365振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.Office365振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 9.問心伝月額仕入振替月次データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void 問心伝月額仕入振替月次データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1) 問心伝月額仕入振替月次 選択クエリの実行：9 問心伝月額仕入振替月次.sql
			List<仕入振替月次> 問心伝月額仕入振替月次_List = PurchaseTransferAccess.Select_問心伝月額仕入振替月次(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (2)プラス分振替データ出力
			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			int denNo = 20080;
			string bumonCode = string.Empty;
			foreach (仕入振替月次 data in 問心伝月額仕入振替月次_List)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (3)\\SQLSV\PCADATAに問心伝月額振替仕入データファイルの新規作成
			// 環境設定.問心伝月額振替出力パス名：\\SQLSV\PCADATA\問心伝月額振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.問心伝月額振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 10.ソフトバンク仕入振替月次データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void ソフトバンク仕入振替月次データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// (1) ソフトバンク仕入振替月次 選択クエリの実行：10 ソフトバンク仕入振替月次.sql
			List<仕入振替月次> ソフトバンク仕入振替月次_List = PurchaseTransferAccess.Select_ソフトバンク仕入振替月次(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (2)プラス分振替データ出力
			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			int denNo = 20100;
			string bumonCode = string.Empty;
			foreach (仕入振替月次 data in ソフトバンク仕入振替月次_List)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = CollectMonth.First.FirstDayOfNextMonth().ToIntYMD(); // 対象月翌月初日
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (3)\\SQLSV\PCADATAにソフトバンク振替仕入データファイルの新規作成
			// 環境設定.ソフトバンク振替出力パス名：\\SQLSV\PCADATA\ソフトバンク振替仕入データ.txt
			using (var sw = new StreamWriter(gSettings.ソフトバンク振替出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 11.Curline本体アプリ仕入作成月次データ作成
		/// </summary>
		/// <param name="fp">仕入振替単価不明伝票ファイルポインタ</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void Curline本体アプリ仕入作成月次データファイル出力(StreamWriter fp, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			// Curline本体アプリ仕入作成月次 選択クエリの実行：11 Curline本体アプリ仕入作成月次.sql
			List<仕入振替月次> Curline本体アプリ仕入作成月次_List = PurchaseTransferAccess.Select_Curline本体アプリ仕入作成月次(CollectMonth, gSettings.Connect.Junp.ConnectionString);

			// (2)プラス分振替データ出力
			List<PCA仕入明細汎用データ> outputList = new List<PCA仕入明細汎用データ>();

			int denNo = 20120;
			string bumonCode = string.Empty;
			foreach (仕入振替月次 data in Curline本体アプリ仕入作成月次_List)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				PCA仕入明細汎用データ pca = new PCA仕入明細汎用データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = CollectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				//pca.備考 = string.Empty;
				//pca.規格型番 = string.Empty;
				pca.税率 = data.sykd_rate;
				outputList.Add(pca);
			}
			// (3)\\SQLSV\PCADATAにCurline本体アプリ仕入データファイルの新規作成
			// 環境設定.Curline本体アプリ出力パス名：\\SQLSV\PCADATA\Curline本体アプリ仕入データ.txt
			using (var sw = new StreamWriter(gSettings.Curline本体アプリ出力パス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (PCA仕入明細汎用データ pca in outputList)
				{
					string record = pca.ToCsvString(7);
					sw.WriteLine(record);
				}
			}
		}
	}
}
