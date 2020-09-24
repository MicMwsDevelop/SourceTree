using MwsLib.BaseFactory.Estore.Table;
using MwsLib.BaseFactory.Estore.View;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.BaseFactory.Pca;
using MwsLib.BaseFactory.WebJucuOrderFile;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Estore;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.WebJucuOrderFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WebJucuOrderFile.Settings;

namespace WebJucuOrderFile
{
	static class Program
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static WebJucuOrderFileSettings gSettings;

		/// <summary>
		/// データベース接続先
		/// </summary>
		public static bool DATABASE_ACCESS_CT = true;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = WebJucuOrderFileSettingsIF.GetSettings();
			if (false == Program.gSettings.OrderDate.HasValue)
			{
				Program.gSettings.OrderDate = Date.Today.ToIntYMD();
			}
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					string msg = OutputCsvFile();

					WebJucuOrderFileSettingsIF.SetSettings(gSettings);
					if (0 < msg.Length)
					{
						return 1;
					}
				}
			}
			Application.Run(new Forms.MainForm());
			return 0;
		}

		/// <summary>
		/// WebJucu.txtの出力
		/// </summary>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile()
		{
			try
			{
				using (var sw = new System.IO.StreamWriter(gSettings.Pathname, false))
				{
					// 11:00 前日の17:00～当日の11:00のweb受注データを読み込む
					// 17:00 当日の11:00～17:00までのweb受注データを読み込む
					Date date = Date.Parse(gSettings.OrderDate.Value);
					List<WebJucu> jucuList = WebJucuOrderFileAccess.Select_WebJucu(date, DATABASE_ACCESS_CT);
					if (0 < jucuList.Count)
					{
						List<vMic部門コード> bumonList = EstoreDatabaseAccess.Select_vMic部門コード(DATABASE_ACCESS_CT);
						List<vMic顧客マスタ> customerList = EstoreDatabaseAccess.Select_vMic顧客マスタ(DATABASE_ACCESS_CT);
						List<vMic商品マスタ> goodsList = EstoreDatabaseAccess.Select_vMic商品マスタ(DATABASE_ACCESS_CT);
						int taxRate = JunpDatabaseAccess.GetTaxRate(date, DATABASE_ACCESS_CT);

						// PCA受注最大番号の取得
						int pcaMax = EstoreDatabaseAccess.Select_vMic受注最大番号(DATABASE_ACCESS_CT);

						List<tMICestore_log> logList = new List<tMICestore_log>();
						int prevOrderNo = 0;
						foreach (WebJucu jucu in jucuList)
						{
							if (jucu.Order.order_no != prevOrderNo)
							{
								prevOrderNo = jucu.Order.order_no;
								pcaMax++;
							}
							jucu.PCA受注No = pcaMax;
							logList.Add(jucu.TotMICestore_log());
						}
						if (0 < logList.Count)
						{
							// tMICestore_logの追加
							//EstoreDatabaseAccess.InsertInto_tMICestore_log(logList, DATABASE_ACCESS_CT);
						}
						// 送料の取得
						vMicPCA商品マスタ pcaShipping = JunpDatabaseAccess.Select_vMicPCA商品マスタ(PCA受注明細汎用データ.ShippingGoodsCode, Program.DATABASE_ACCESS_CT);

						// PCA受注明細データの作成
						List<PCA受注明細汎用データ> layoutList = new List<PCA受注明細汎用データ>();
						WebJucu prevJucu = null;
						foreach (WebJucu jucu in jucuList)
						{
							if (null != prevJucu && jucu.Order.order_no != prevJucu.Order.order_no)
							{
								// 送料の追加
								layoutList.Add(PCA受注明細汎用データ.ShippingData(layoutList.Last(), pcaShipping));

								// 着日指定の追加
								if (prevJucu.Order.pref_arrival_date.HasValue)
								{
									layoutList.Add(PCA受注明細汎用データ.ArrivalDateData(layoutList.Last(), prevJucu.Order.pref_arrival_date.Value));
								}
							}
							vMic部門コード bumon = bumonList.Find(p => p.顧客No == jucu.Order.customer_no);
							vMic顧客マスタ customer = customerList.Find(p => p.顧客No == jucu.Order.customer_no);
							vMic商品マスタ goods = goodsList.Find(p => p.商品コード == jucu.Order.goods_code);
							vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(jucu.Order.goods_code, Program.DATABASE_ACCESS_CT);

							PCA受注明細汎用データ layout = new PCA受注明細汎用データ();
							layout.Initial();
							layout.受注No = jucu.PCA受注No;
							layout.受注日 = jucu.Order.order_dt.ToString("yyyyMMdd");
							layout.得意先No = customer.得意先No;
							layout.顧客名 = customer.顧客名;
							layout.PCA部門No = bumon.PCA部門No;
							layout.PCA主担当No = bumon.PCA主担当No;
							layout.摘要コード = "031";
							layout.摘要名 = PCA受注明細汎用データ.WebJucuTekimei;
							layout.goods_code = goods.商品コード;
							layout.マスター区分 = "0";
							layout.商品名 = goods.商品名;
							layout.数量 = jucu.Order.order_num.ToString();
							layout.単価 = jucu.Order.web_price.ToString();
							layout.受注金額 = (jucu.Order.order_num * jucu.Order.web_price).ToString();
							layout.原単価 = pca.sms_gen.ToString();
							layout.原価額 = (pca.sms_gen * jucu.Order.order_num).ToString();
							layout.税区分 = "2";
							layout.税率 = taxRate;
							layoutList.Add(layout);

							prevJucu = jucu;
						}
						WebJucu lastJucu = jucuList.Last();
						if (null != lastJucu)
						{
							// 送料の追加
							layoutList.Add(PCA受注明細汎用データ.ShippingData(layoutList.Last(), pcaShipping));

							// 着日指定の追加
							if (lastJucu.Order.pref_arrival_date.HasValue)
							{
								layoutList.Add(PCA受注明細汎用データ.ArrivalDateData(layoutList.Last(), lastJucu.Order.pref_arrival_date.Value));
							}
						}
						foreach (PCA受注明細汎用データ layout in layoutList)
						{
							sw.WriteLine(layout.ToCsvString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
