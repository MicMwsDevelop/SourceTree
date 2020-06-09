using ClosedXML.Excel;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.BaseFactory.Pca;
using MwsLib.BaseFactory.PurchaseTransfer;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PurchaseTransfer;
using PurchaseTransfer.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PurchaseTransfer.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		private PurchaseTransferSettings Settings;

		/// <summary>
		/// 
		/// </summary>
		private List<PCA仕入明細> 対象月全仕入れ明細List;

		/// <summary>
		/// 
		/// </summary>
		private List<Tuple<string, int>> 当月仕入単価List;

		/// <summary>
		/// 
		/// </summary>
		private List<Tuple<string, int>> 在庫評価単価List;

		/// <summary>
		/// 
		/// </summary>
		private List<在庫一覧表> 在庫一覧表List;

		/// <summary>
		/// 検索対象期間
		/// </summary>
		private Span 対象年月;

		/// <summary>
		/// 
		/// </summary>
		private const string 仕入振替単価不明リストファイル名 = "仕入振替単価不明伝票.txt";

		//private const string FileExportFolder = @"\\sqlsv\pcadata";
		private const string FileExportFolder = @"c:\_aaa";

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			対象月全仕入れ明細List = new List<PCA仕入明細>();
			当月仕入単価List = new List<Tuple<string, int>>();
			在庫評価単価List = new List<Tuple<string, int>>();


			在庫一覧表List = new List<在庫一覧表>();

			// 先月
			対象年月 = Date.Today.PlusMonths(-1).ToSpan();
			対象年月 = new Span(2019, 10, 1, 2019, 10, 31);
			//対象年月 = new Span(2019, 1, 1, 2019, 1, 31);
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 環境設定の読込
			Settings = PurchaseTransferSettingsIF.GetSettings();

			// 在庫一覧表入力ファイルの読み込み
			try
			{
				using (var sr = new StreamReader(Settings.在庫一覧表入力ファイル名, Encoding.GetEncoding("Shift_JIS")))
				{
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split(',');
						在庫一覧表List.Add(new 在庫一覧表(values));
					}
				}
				在庫評価単価List = 在庫一覧表.在庫評価単価(在庫一覧表List);
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル読込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// START
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			社内使用分出荷振替データ作成();

			貯蔵品社内使用分出荷振替データ作成();

			社内仕入振替データ作成(10000);

			りすとん振替データ作成(20000);

			問心伝振替データ作成(20100);

			りすとん月額仕入振替月次データ作成(20020);

			Office365仕入振替月次データ作成(20040);

			問心伝月額仕入振替月次データ作成(20080);

			ソフトバンク仕入振替月次データ作成(20100);

			Curline本体アプリ仕入作成月次データ作成(20120);

			ナルコーム仕入データ作成(20060);

			クラウドデータバンク仕入データ作成(20140);

			MessageBox.Show("ファイル出力終了", "出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 単価検索
		/// 在庫評価単価 から検索し、在庫評価単価にない時、当月仕入単価から検索
		/// </summary>
		/// <param name="scdCode"></param>
		/// <param name="list当月仕入単価">当月仕入単価</param>
		/// <returns>単価</returns>
		private int SearchAmount(string scdCode, List<Tuple<string, int>> list当月仕入単価)
		{
			Tuple<string, int> price = 在庫評価単価List.Find(p => p.Item1 == scdCode);
			if (null != price)
			{
				return price.Item2;
			}
			price = list当月仕入単価.Find(p => p.Item1 == scdCode);
			if (null != price)
			{
				return price.Item2;
			}
			return 0;
		}

		/// <summary>
		/// 営業部コードの取得
		/// </summary>
		/// <param name="code"></param>
		/// <returns>営業部コード</returns>
		private string GetSectionCode(string code)
		{
			switch (code)
			{
				case "000211":
					return "11";// 営業管理部 
				case "000250":
					return "50";// 配送センター
				case "000201":
				case "000202":
					return "81";// 東日本営業部
				case "000225":
				case "000235":
					return "82";// 首都圏営業部
				case "000231":
				case "000233":
				case "000234":
					return "83";// 関東営業部
				case "000207":
				case "000214":
					return "86";// 中部営業部
				case "000244":
					return "87";// 関西営業部
				case "000212":
				case "000209":
					return "85";// 西日本営業部
				case "000275":
					return "75";// ヘルスケア営業部
			}
			return "00";
		}

		/// <summary>
		/// 社内使用分出荷振替データ作成
		/// </summary>
		public void 社内使用分出荷振替データ作成()
		{
			対象月全仕入れ明細List = PurchaseTransferAccess.Get対象月全仕入れ明細(対象年月, Program.DATABASE_ACCESS_CT);
			List<社内使用分出荷明細> list社内使用分出荷明細 = PurchaseTransferAccess.Get社内使用分出荷明細(対象年月, Program.DATABASE_ACCESS_CT);

			var query = from 対象月全仕入れ明細 in 対象月全仕入れ明細List
						join 在庫評価単価 in 在庫評価単価List on 対象月全仕入れ明細.商品コード equals 在庫評価単価.Item1
						orderby 対象月全仕入れ明細.商品コード, 対象月全仕入れ明細.仕入日 descending
						where 対象月全仕入れ明細.商品コード != "" && 対象月全仕入れ明細.単価 != 0 && 在庫評価単価.Item1 != null
						group new { 対象月全仕入れ明細, 在庫評価単価 } by new { 対象月全仕入れ明細.商品コード, 対象月全仕入れ明細.単価, 在庫評価単価.Item1, 対象月全仕入れ明細.仕入日 } into X
						select new { X.Key.商品コード, X.Key.単価 };
			foreach (var a in query)
			{
				当月仕入単価List.Add(new Tuple<string, int>(a.商品コード, (int)a.単価));
			}
			// 社内使用分振替出力エクセルファイルの出力
			var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("社内仕入振替データ");
			worksheet.Cell(1, 1).Value = "日付";
			worksheet.Cell(1, 2).Value = "部署名";
			worksheet.Cell(1, 3).Value = "科目";
			worksheet.Cell(1, 4).Value = "摘要";
			worksheet.Cell(1, 5).Value = "商品コード";
			worksheet.Cell(1, 6).Value = "品名";
			worksheet.Cell(1, 7).Value = "数量";
			worksheet.Cell(1, 8).Value = "単価";
			worksheet.Cell(1, 9).Value = "金額";
			for (int i = 0; i < list社内使用分出荷明細.Count; i++)
			{
				worksheet.Cell(i + 2, 1).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 2).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 3).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 4).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 5).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 6).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 7).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 8).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 9).Style.NumberFormat.Format = "@";

				社内使用分出荷明細 data = list社内使用分出荷明細[i];
				int price = SearchAmount(data.商品コード, 当月仕入単価List);
				worksheet.Cell(i + 2, 1).Value = data.出荷日;
				worksheet.Cell(i + 2, 2).Value = data.部署名;
				worksheet.Cell(i + 2, 3).Value = "";
				worksheet.Cell(i + 2, 4).Value = data.先方担当者名;
				worksheet.Cell(i + 2, 5).Value = data.商品コード;
				worksheet.Cell(i + 2, 6).Value = data.品名;
				worksheet.Cell(i + 2, 7).Value = (int)data.数量;
				worksheet.Cell(i + 2, 8).Value = price;
				worksheet.Cell(i + 2, 9).Value = price * (int)data.数量;
			}
			try
			{
				string pathName = Path.Combine(FileExportFolder, Settings.社内使用分振替出力ファイル名);
				workbook.SaveAs(pathName);
				//MessageBox.Show(string.Format("{0} を出力しました。", pathName), "出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 貯蔵品社内使用分出荷振替データ作成
		/// </summary>
		private void 貯蔵品社内使用分出荷振替データ作成()
		{
			List<PCA出荷明細貯蔵品> listPCA出荷明細貯蔵品 = PurchaseTransferAccess.Get貯蔵品社内使用分出荷明細(対象年月, Program.DATABASE_ACCESS_CT);
			List<PCA仕入明細貯蔵品> list対象月仕入明細貯蔵品 = PurchaseTransferAccess.Get対象月仕入明細貯蔵品(対象年月, Program.DATABASE_ACCESS_CT);

			var query = from 対象月仕入明細貯蔵品 in list対象月仕入明細貯蔵品
						join 在庫評価単価 in 在庫評価単価List on 対象月仕入明細貯蔵品.商品コード equals 在庫評価単価.Item1
						orderby 対象月仕入明細貯蔵品.商品コード, 対象月仕入明細貯蔵品.仕入日 descending
						where 対象月仕入明細貯蔵品.商品コード != "" && 対象月仕入明細貯蔵品.単価 != 0 && 在庫評価単価.Item1 != null
						group new { 対象月仕入明細貯蔵品, 在庫評価単価 } by new { 対象月仕入明細貯蔵品.商品コード, 対象月仕入明細貯蔵品.単価, 在庫評価単価.Item1, 対象月仕入明細貯蔵品.仕入日 } into X
						select new { X.Key.商品コード, X.Key.単価 };

			List<Tuple<string, int>> list当月仕入単価貯蔵品 = new List<Tuple<string, int>>();
			foreach (var a in query)
			{
				list当月仕入単価貯蔵品.Add(new Tuple<string, int>(a.商品コード, (int)a.単価));
			}

			// 貯蔵品社内使用分振替出力エクセルファイルの出力
			var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("貯蔵品社内仕入振替データ");
			worksheet.Cell(1, 1).Value = "商品コード";
			worksheet.Cell(1, 2).Value = "品名";
			worksheet.Cell(1, 3).Value = "日付";
			worksheet.Cell(1, 4).Value = "伝票No";
			worksheet.Cell(1, 5).Value = "部署名";
			worksheet.Cell(1, 6).Value = "数量";
			worksheet.Cell(1, 7).Value = "単価";
			worksheet.Cell(1, 8).Value = "金額";
			worksheet.Cell(1, 9).Value = "摘要";
			for (int i = 0; i < listPCA出荷明細貯蔵品.Count; i++)
			{
				worksheet.Cell(i + 2, 1).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 2).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 3).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 4).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 5).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 6).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 7).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 8).Style.NumberFormat.Format = "@";
				worksheet.Cell(i + 2, 9).Style.NumberFormat.Format = "@";

				PCA出荷明細貯蔵品 data = listPCA出荷明細貯蔵品[i];
				int price = SearchAmount(data.商品コード, list当月仕入単価貯蔵品);
				worksheet.Cell(i + 2, 1).Value = data.商品コード;
				worksheet.Cell(i + 2, 2).Value = data.品名;
				worksheet.Cell(i + 2, 3).Value = data.出荷日;
				worksheet.Cell(i + 2, 4).Value = data.伝票No;
				worksheet.Cell(i + 2, 5).Value = data.部署名;
				worksheet.Cell(i + 2, 6).Value = (int)data.数量;
				worksheet.Cell(i + 2, 7).Value = price;
				worksheet.Cell(i + 2, 8).Value = price * (int)data.数量;
				worksheet.Cell(i + 2, 9).Value = data.先方担当者名;
			}
			try
			{
				string pathName = Path.Combine(FileExportFolder, Settings.貯蔵品社内使用分振替出力ファイル名);
				workbook.SaveAs(pathName);
				//MessageBox.Show(string.Format("{0} を出力しました。", pathName), "出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 社内仕入振替データ作成
		/// </summary>
		/// <param name="initDen">伝票No</param>
		private void 社内仕入振替データ作成(int initDen)
		{
			List<string> fumeiList = new List<string>();
			try
			{
				// 仕入振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.仕入振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					var query = from 対象月全仕入れ明細 in 対象月全仕入れ明細List
								where 対象月全仕入れ明細.伝区 != "5"
								orderby 対象月全仕入れ明細.仕入日, 対象月全仕入れ明細.伝票No, 対象月全仕入れ明細.仕入先コード
								group 対象月全仕入れ明細 by new { 対象月全仕入れ明細.仕入日, 対象月全仕入れ明細.伝票No, 対象月全仕入れ明細.仕入先コード, 対象月全仕入れ明細.伝区 } into X
								select new { X.Key.仕入日, X.Key.伝票No, X.Key.仕入先コード };

					List<対象月社内仕入れ伝票> list対象月社内仕入れ伝票 = new List<対象月社内仕入れ伝票>();
					if (0 == list対象月社内仕入れ伝票.Count)
					{
						return;
					}
					foreach (var a in query)
					{
						対象月社内仕入れ伝票 data = new 対象月社内仕入れ伝票();
						data.仕入日 = a.仕入日;
						data.伝票No = a.伝票No;
						data.仕入先コード = a.仕入先コード;
						list対象月社内仕入れ伝票.Add(data);
					}
					List<PCA仕入明細> list対象月社内仕入れ明細 = PurchaseTransferAccess.Get対象月社内仕入れ明細(対象年月, Program.DATABASE_ACCESS_CT);

					int denno = initDen;
					List<PCA仕入明細> outputList = new List<PCA仕入明細>();
					foreach (var den in list対象月社内仕入れ伝票)
					{
						List<PCA仕入明細> filter = list対象月社内仕入れ明細.FindAll(p => p.仕入日 == den.仕入日 && p.伝票No == den.伝票No && p.仕入先コード == den.仕入先コード);
						if (0 < filter.Count)
						{
							// プラス分振替伝票データを作成
							denno++;
							foreach (PCA仕入明細 pca in filter)
							{
								PCA仕入明細 plus = pca.CloneDeep();
								plus.仕入先コード = "furi";       // 仕入先コードを "furi" に差し替え
								plus.仕入先名 = string.Empty;   // 仕入先名は空白
								plus.区 = 2; // 2:単価修正
								int amount = SearchAmount(pca.商品コード, 当月仕入単価List);
								plus.単価 = amount;// 単価
								plus.伝票No = denno;// 伝票No
								plus.倉庫コード = "0";// 倉庫コードを"0"
								plus.金額 = amount * pca.数量;
								outputList.Add(plus);
								if (0 == amount)
								{
									// 単価不明→在庫単価不明リスト出力用テキストファイルに出力
									fumeiList.Add(plus.ToFumeiList());
								}
							}
							// マイナス分振替伝票データを作成
							denno++;
							foreach (PCA仕入明細 pca in filter)
							{
								PCA仕入明細 minus = pca.CloneDeep();
								minus.仕入先コード = "furi";       // 仕入先コードを "furi" に差し替え
								minus.仕入先名 = string.Empty;   // 仕入先名は空白
								minus.区 = 2; // 2:単価修正
								int amount = SearchAmount(pca.商品コード, 当月仕入単価List);
								minus.単価 = amount;// 単価
								minus.伝票No = denno;// 伝票No
								minus.部門コード = GetSectionCode(pca.仕入先コード);   // 仕入先→部門コード
								minus.倉庫コード = "0";// 倉庫コードを"0"
								minus.担当者コード = "0";// 担当者コードを"0"
								minus.数量 = -pca.数量;// 数量マイナス
								minus.金額 = amount * -pca.数量;
								outputList.Add(minus);
								if (0 == amount)
								{
									// 単価不明→在庫単価不明リスト出力用テキストファイルに出力
									fumeiList.Add(minus.ToFumeiList());
								}
							}
						}
					}
					using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
					{
						foreach (PCA仕入明細 pca in outputList)
						{
							writer.WriteLine(pca.ToCsvString(7));
						}
						writer.Close();
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			if (0 < fumeiList.Count)
			{
				try
				{
					// 在庫単価不明リスト出力用テキストファイルの出力
					using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, 仕入振替単価不明リストファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (string buf in fumeiList)
							{
								writer.WriteLine(buf);
							}
							writer.Close();
						}
					}
				}
				catch (IOException ex)
				{
					MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// りすとん振替データ作成
		/// </summary>
		/// <param name="initDen">伝票No</param>
		private void りすとん振替データ作成(int initDen)
		{
			try
			{
				// りすとん振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.りすとん振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.ListonGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.Palette商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query仕入振替月次合計行追加 = from 売上明細月次 in list売上明細月次
										   group 売上明細月次 by new { 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
										   select new { sykd_jbmn = "11", sykd_jtan = "0099", X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					var query仕入振替月次追加 = from 売上明細月次 in list売上明細月次
										group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
										select new { X.Key, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query仕入振替月次合計行追加)
					{
						仕入振替月次 data = new 仕入振替月次();
						MasterGoods goods = Settings.ListonGoodsList.Find(p => p.Palette商品コード == a.sykd_scd);
						if (null != goods)
						{
							Tuple<string, int> zaiko = 在庫評価単価List.Find(p => p.Item1 == goods.仕入商品コード);
							if (null != zaiko)
							{
								data.sykd_scd = zaiko.Item1;
								data.評価単価 = zaiko.Item2;
								List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(zaiko.Item1, Program.DATABASE_ACCESS_CT);
								if (0 < mst.Count)
								{
									data.sykd_mei = mst.First().sms_mei;
								}
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					foreach (var a in query仕入振替月次追加)
					{
						仕入振替月次 data = new 仕入振替月次();
						MasterGoods goods = Settings.ListonGoodsList.Find(p => p.Palette商品コード == a.Key.sykd_scd);
						if (null != goods)
						{
							Tuple<string, int> zaiko = 在庫評価単価List.Find(p => p.Item1 == goods.仕入商品コード);
							if (null != zaiko)
							{
								data.sykd_scd = zaiko.Item1;
								data.評価単価 = zaiko.Item2;
								List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(zaiko.Item1, Program.DATABASE_ACCESS_CT);
								if (0 < mst.Count)
								{
									data.sykd_mei = mst.First().sms_mei;
								}
							}
						}
						data.sykd_jbmn = a.Key.sykd_jbmn;
						data.sykd_jtan = a.Key.sykd_jtan;
						data.sykd_mkbn = a.Key.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.Key.sykd_tani;
						data.sykd_rate = (int)a.Key.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					int lastDay = 対象年月.End.ToIntYMD();
					int denno = initDen;

					// プラス分振替伝票データを作成 ------------------------------------------------------
					List<PCA仕入明細> outputList = new List<PCA仕入明細>();
					string sykd_jbmn = string.Empty;
					for (int i = 0; i < list仕入振替月次.Count; i++)
					{
						仕入振替月次 data = list仕入振替月次[i];
						if (sykd_jbmn != data.sykd_jbmn)
						{
							denno++;
						}
						PCA仕入明細 pca = new PCA仕入明細();
						pca.仕入日 = lastDay;// 3仕入日を月末日に変更
						pca.精算日 = lastDay;//4
						pca.伝票No = denno;//5
						pca.仕入先コード = "furi";// 6仕入先コードを "furi" に差し替え
						//pca.仕入先名 = "";//7
						//pca.先方担当者名 = "";//8
						pca.部門コード = data.部門コード;//9
						pca.担当者コード = data.担当者コード;//10
						pca.摘要コード = "0";//11
						//pca.摘要名 = "";//12
						pca.商品コード = data.sykd_scd;//13
						pca.商品名 = data.sykd_mei;//15
						if ("11" == data.部門コード)
						{
							pca.区 = 0;//16
							pca.倉庫コード = "11";//17
							pca.数量 = -(data.数量);//20
							pca.単価 = 0;//22
							pca.金額 = 0;//23
						}
						else
						{
							pca.区 = 2;// 16 区:2は単価修正
							pca.倉庫コード = "0";//17
							pca.数量 = data.数量;//20
							pca.単価 = data.評価単価;//22
							pca.金額 = data.金額;//23
						}
						//pca.単位 = "";//21
						pca.税区分 = 2;//26
						pca.税率 = data.sykd_rate;//39
						outputList.Add(pca);

						sykd_jbmn = data.sykd_jbmn;
					}

					// マイナス分振替伝票データを作成
					sykd_jbmn = string.Empty;
					for (int i = 0; i < list仕入振替月次.Count; i++)
					{
						仕入振替月次 data = list仕入振替月次[i];
						if ("11" == data.部門コード)
						{
							sykd_jbmn = data.sykd_jbmn;
							continue;
						}
						if (sykd_jbmn != data.sykd_jbmn)
						{
							denno++;
						}
						PCA仕入明細 pca = new PCA仕入明細();
						pca.仕入日 = lastDay;// 3仕入日を月末日に変更
						pca.精算日 = lastDay;//4
						pca.伝票No = denno;//5
						pca.仕入先コード = "furi";// 6仕入先コードを "furi" に差し替え
						//pca.仕入先名 = "";//7
						//pca.先方担当者名 = "";//8
						pca.部門コード = "11";//9
						pca.担当者コード = "0";//10 担当者コードを"0"
						pca.摘要コード = "0";//11
						//pca.摘要名 = "";//12
						pca.商品コード = data.sykd_scd;//13
						pca.商品名 = data.sykd_mei;//15
						pca.区 = 2;// 16 区:2は単価修正
						pca.倉庫コード = "11";//17
						pca.数量 = -(data.数量);//20
						//pca.単位 = "";//21
						pca.単価 = data.評価単価;//22
						pca.金額 = -(data.金額);//23
						pca.税区分 = 2;//26
						pca.税率 = data.sykd_rate;//39
						outputList.Add(pca);

						sykd_jbmn = data.sykd_jbmn;
					}
					using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
					{
						foreach (PCA仕入明細 pca in outputList)
						{
							writer.WriteLine(pca.ToCsvString(7));
						}
						writer.Close();
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 問心伝振替データ作成
		/// </summary>
		/// <param name="initDen">伝票No</param>
		private void 問心伝振替データ作成(int initDen)
		{
			try
			{
				// 問心伝振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.問心伝振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.MonshindenGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.Palette商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query仕入振替月次合計行追加 = from 売上明細月次 in list売上明細月次
										   group 売上明細月次 by new { 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
										   select new { sykd_jbmn = "11", sykd_jtan = "0099", X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					var query仕入振替月次追加 = from 売上明細月次 in list売上明細月次
										group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
										select new { X.Key, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query仕入振替月次合計行追加)
					{
						仕入振替月次 data = new 仕入振替月次();
						MasterGoods goods = Settings.MonshindenGoodsList.Find(p => p.Palette商品コード == a.sykd_scd);
						if (null != goods)
						{
							Tuple<string, int> zaiko = 在庫評価単価List.Find(p => p.Item1 == goods.仕入商品コード);
							if (null != zaiko)
							{
								data.sykd_scd = zaiko.Item1;
								data.評価単価 = zaiko.Item2;
								List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(zaiko.Item1, Program.DATABASE_ACCESS_CT);
								if (0 < mst.Count)
								{
									data.sykd_mei = mst.First().sms_mei;
								}
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					foreach (var a in query仕入振替月次追加)
					{
						仕入振替月次 data = new 仕入振替月次();
						MasterGoods goods = Settings.MonshindenGoodsList.Find(p => p.Palette商品コード == a.Key.sykd_scd);
						if (null != goods)
						{
							Tuple<string, int> zaiko = 在庫評価単価List.Find(p => p.Item1 == goods.仕入商品コード);
							if (null != zaiko)
							{
								data.sykd_scd = zaiko.Item1;
								data.評価単価 = zaiko.Item2;
								List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(zaiko.Item1, Program.DATABASE_ACCESS_CT);
								if (0 < mst.Count)
								{
									data.sykd_mei = mst.First().sms_mei;
								}
							}
						}
						data.sykd_jbmn = a.Key.sykd_jbmn;
						data.sykd_jtan = a.Key.sykd_jtan;
						data.sykd_mkbn = a.Key.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.Key.sykd_tani;
						data.sykd_rate = (int)a.Key.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					int lastDay = 対象年月.End.ToIntYMD();
					int denno = initDen;

					// プラス分振替伝票データを作成 ------------------------------------------------------
					List<PCA仕入明細> outputList = new List<PCA仕入明細>();
					string sykd_jbmn = string.Empty;
					for (int i = 0; i < list仕入振替月次.Count; i++)
					{
						仕入振替月次 data = list仕入振替月次[i];
						if (sykd_jbmn != data.sykd_jbmn)
						{
							denno++;
						}
						PCA仕入明細 pca = new PCA仕入明細();
						pca.仕入日 = lastDay;// 3仕入日を月末日に変更
						pca.精算日 = lastDay;//4
						pca.伝票No = denno;//5
						pca.仕入先コード = "furi";// 6仕入先コードを "furi" に差し替え
						//pca.仕入先名 = "";//7
						//pca.先方担当者名 = "";//8
						pca.部門コード = data.部門コード;//9
						pca.担当者コード = data.担当者コード;//10
						pca.摘要コード = "0";//11
						//pca.摘要名 = "";//12
						pca.商品コード = data.sykd_scd;//13
						pca.商品名 = data.sykd_mei;//15
						if ("11" == data.部門コード)
						{
							pca.区 = 0;//16
							pca.倉庫コード = "11";//17
							pca.数量 = -(data.数量);//20
							pca.単価 = 0;//22
							pca.金額 = 0;//23
						}
						else
						{
							pca.区 = 2;// 16 区:2は単価修正
							pca.倉庫コード = "0";//17
							pca.数量 = data.数量;//20
							pca.単価 = data.評価単価;//22
							pca.金額 = data.金額;//23
						}
						//pca.単位 = "";//21
						pca.税区分 = 2;//26
						pca.税率 = data.sykd_rate;//39
						outputList.Add(pca);

						sykd_jbmn = data.sykd_jbmn;
					}

					// マイナス分振替伝票データを作成
					sykd_jbmn = string.Empty;
					for (int i = 0; i < list仕入振替月次.Count; i++)
					{
						仕入振替月次 data = list仕入振替月次[i];
						if ("11" == data.部門コード)
						{
							sykd_jbmn = data.sykd_jbmn;
							continue;
						}
						if (sykd_jbmn != data.sykd_jbmn)
						{
							denno++;
						}
						PCA仕入明細 pca = new PCA仕入明細();
						pca.仕入日 = lastDay;// 3仕入日を月末日に変更
						pca.精算日 = lastDay;//4
						pca.伝票No = denno;//5
						pca.仕入先コード = "furi";// 6仕入先コードを "furi" に差し替え
						//pca.仕入先名 = "";//7
						//pca.先方担当者名 = "";//8
						pca.部門コード = "11";//9
						pca.担当者コード = "0";//10 担当者コードを"0"
						pca.摘要コード = "0";//11
						//pca.摘要名 = "";//12
						pca.商品コード = data.sykd_scd;//13
						pca.商品名 = data.sykd_mei;//15
						pca.区 = 2;// 16 区:2は単価修正
						pca.倉庫コード = "11";//17
						pca.数量 = -(data.数量);//20
						//pca.単位 = "";//21
						pca.単価 = data.評価単価;//22
						pca.金額 = -(data.金額);//23
						pca.税区分 = 2;//26
						pca.税率 = data.sykd_rate;//39
						outputList.Add(pca);

						sykd_jbmn = data.sykd_jbmn;
					}
					using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
					{
						foreach (PCA仕入明細 pca in outputList)
						{
							writer.WriteLine(pca.ToCsvString(7));
						}
						writer.Close();
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// りすとん月額仕入振替月次データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void りすとん月額仕入振替月次データ作成(int denNo)
		{
			try
			{
				// りすとん月額振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.りすとん月額振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.MonthlyListonGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_suryo != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						MonthlyMasterGoods goods = Settings.MonthlyListonGoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 月額振替出力ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.End.ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Office365仕入振替月次データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void Office365仕入振替月次データ作成(int denNo)
		{
			try
			{
				// Office365振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.Office365振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.MonthlyOffice365GoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_suryo != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						MonthlyMasterGoods goods = Settings.MonthlyOffice365GoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 月額振替出力ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.Start.PlusMonths(1).ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 問心伝月額仕入振替月次データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void 問心伝月額仕入振替月次データ作成(int denNo)
		{
			try
			{
				// 問心伝月額振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.問心伝月額振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.MonthlyMonshindenGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_suryo != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						MonthlyMasterGoods goods = Settings.MonthlyMonshindenGoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 月額振替出力ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.End.ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ソフトバンク仕入振替月次データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void ソフトバンク仕入振替月次データ作成(int denNo)
		{
			try
			{
				// ソフトバンク振替出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.ソフトバンク振替出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.MonthlySoftbankGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_suryo != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						MonthlyMasterGoods goods = Settings.MonthlySoftbankGoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 月額振替出力ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.Start.PlusMonths(1).ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Curline本体アプリ仕入作成月次データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void Curline本体アプリ仕入作成月次データ作成(int denNo)
		{
			try
			{
				// Curline本体アプリ出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.Curline本体アプリ出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.MonthlyCurlineGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_suryo != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						MonthlyMasterGoods goods = Settings.MonthlyCurlineGoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = "075";
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 月額振替出力ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.End.ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// ナルコーム仕入データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void ナルコーム仕入データ作成(int denNo)
		{
			try
			{
				// ナルコーム出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.ナルコーム出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.NarcohmGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_kingaku != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						ExtraMasterGoods goods = Settings.NarcohmGoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.仕入フラグ = goods.仕入フラグ;
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 仕入ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.End.ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// クラウドデータバンク仕入データ作成
		/// </summary>
		/// <param name="denNo">伝票No</param>
		private void クラウドデータバンク仕入データ作成(int denNo)
		{
			try
			{
				// クラウドデータバンク出力ファイルの出力
				using (FileStream fileStream = new FileStream(Path.Combine(FileExportFolder, Settings.クラウドデータバンク出力ファイル名), FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					string whereGoods = string.Empty;
					foreach (var goods in Settings.CloudDataBankGoodsList)
					{
						if (0 < whereGoods.Length)
						{
							whereGoods += ",";
						}
						whereGoods += string.Format("'{0}'", goods.商品コード);
					}
					List<vMicPCA売上明細> list売上明細月次 = PurchaseTransferAccess.Get売上明細月次(whereGoods, 対象年月, Program.DATABASE_ACCESS_CT);
					if (0 == list売上明細月次.Count)
					{
						return;
					}
					var query = from 売上明細月次 in list売上明細月次
								where 売上明細月次.sykd_kingaku != 0
								orderby 売上明細月次.sykd_jbmn, 売上明細月次.sykd_scd
								group 売上明細月次 by new { 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 売上明細月次.sykd_scd, 売上明細月次.sykd_mkbn, 売上明細月次.sykd_tani, 売上明細月次.sykd_rate } into X
								select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

					List<仕入振替月次> list仕入振替月次 = new List<仕入振替月次>();
					foreach (var a in query)
					{
						仕入振替月次 data = new 仕入振替月次();
						ExtraMasterGoods goods = Settings.CloudDataBankGoodsList.Find(p => p.商品コード == a.sykd_scd);
						if (null != goods)
						{
							data.仕入フラグ = goods.仕入フラグ;
							data.sykd_scd = goods.仕入商品コード;
							data.評価単価 = goods.仕入価格;
							data.仕入先コード = goods.仕入先;
							List<vMicPCA商品マスタ> mst = PurchaseTransferAccess.GetPCA商品マスタ(goods.仕入商品コード, Program.DATABASE_ACCESS_CT);
							if (0 < mst.Count)
							{
								data.sykd_mei = mst.First().sms_mei;
							}
						}
						data.sykd_jbmn = a.sykd_jbmn;
						data.sykd_jtan = a.sykd_jtan;
						data.sykd_mkbn = a.sykd_mkbn;
						data.数量 = a.数量;
						data.sykd_tani = a.sykd_tani;
						data.sykd_rate = (int)a.sykd_rate;
						list仕入振替月次.Add(data);
					}
					if (0 == list仕入振替月次.Count)
					{
						return;
					}
					// 仕入ファイルデータの出力
					List<PCA仕入明細> outputList = OutputFurikaeData(list仕入振替月次, denNo, 対象年月.End.ToIntYMD());
					if (0 < outputList.Count)
					{
						using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
						{
							foreach (PCA仕入明細 pca in outputList)
							{
								writer.WriteLine(pca.ToCsvString(7));
							}
							writer.Close();
						}
					}
				}
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 月額振替出力ファイルデータの出力
		/// </summary>
		/// <param name="list"></param>
		/// <param name="initDen">伝票No初期値</param>
		/// <param name="seisanDate">清算日</param>
		/// <returns>月額振替出力ファイルデータ</returns>
		private List<PCA仕入明細> OutputFurikaeData(List<仕入振替月次> list, int initDen, int seisanDate)
		{
			List<PCA仕入明細> result = new List<PCA仕入明細>();
			int lastDay = 対象年月.End.ToIntYMD();

			int denno = initDen;
			string sykd_jbmn = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				仕入振替月次 data = list[i];
				if (sykd_jbmn != data.sykd_jbmn)
				{
					denno++;
				}
				PCA仕入明細 pca = new PCA仕入明細();
				pca.仕入日 = 対象年月.End.ToIntYMD();// 3仕入日を月末日に変更
				pca.精算日 = seisanDate;//4清算日 ソフトバンク仕入振替月次 or Office365仕入振替月次は翌月初日
				pca.伝票No = denno;//5
				pca.仕入先コード = data.仕入先コード;// 6仕入先コード
				//pca.仕入先名 = "";//7
				//pca.先方担当者名 = "";//8
				pca.部門コード = data.部門コード;//9
				pca.担当者コード = "0";//10 担当者コードを"0"
				pca.摘要コード = "0";//11
				//pca.摘要名 = "";//12
				pca.商品コード = data.sykd_scd;//13
				pca.商品名 = data.sykd_mei;//15
				pca.区 = 0;// 16 区:2は単価修正	2が正しいのでは?
				pca.倉庫コード = "0";//17
				pca.数量 = data.数量;//20
				//pca.単位 = "";//21
				pca.単価 = data.評価単価;//22
				pca.金額 = data.金額;//23
				pca.税区分 = 2;//26
				pca.備考 = "0";//28 たぶん間違い?
				pca.税率 = data.sykd_rate;//39

				result.Add(pca);

				sykd_jbmn = data.sykd_jbmn;
			}
			return result;
		}
	}
}
