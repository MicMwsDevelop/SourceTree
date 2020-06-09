using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.ShipmentActing;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using System.Data.SqlClient;
using MwsLib.BaseFactory.ShipmentActing;
using MwsLib.DB.SqlServer.PCA;
using MwsLib.DB.SqlServer.Estore;
using ShipmentActing.BaseFactory;
using ShipmentActing.Settings;

namespace ShipmentActing.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 送料商品コードリスト
		/// </summary>
		private List<string> PostageCodeList;

		/// <summary>
		/// PCA商品マスタリスト
		/// </summary>
		private List<vMicPCA商品マスタ> PcaGoodsList;

		/// <summary>
		/// 代引き手数料(000610)の商品名と金額
		/// </summary>
		private Tuple<string, int> DaibikiTesuryo;

		/// <summary>
		/// 佐川急便の代引き発送の取扱い不能地域住所
		/// </summary>
		private List<string> RitoAddressList;

		/// <summary>
		/// 出荷日付
		/// </summary>
		private Date ShipmentDate;

		/// <summary>
		/// 消費税率
		/// </summary>
		private int TaxRate;

		/// <summary>
		/// vMicPCA担当者マスタリスト
		/// </summary>
		private List<vMicPCA担当者マスタ> PcaTantoList;

		/// <summary>
		/// 発送用データファイル(1)
		/// </summary>
		private List<string> HassouFileList;

		/// <summary>
		/// 納品書用データファイル(2)
		/// </summary>
		private List<string> NouhinFileList;

		/// <summary>
		/// ＰＣＡ商魂・商管用汎用売上明細データファイル(3)
		/// </summary>
		private List<string> HsykdFileList;

		/// <summary>
		/// 離島代引き発送用データファイル(6)
		/// </summary>
		private List<string> RitoHassouFile;

		/// <summary>
		/// 着荷日付指定発送情報
		/// </summary>
		private List<ShiteiHassou> CkakubiList;

		/// <summary>
		/// 航空便指定発送情報
		/// </summary>
		private List<ShiteiHassou> AirCargoList;

		/// <summary>
		/// 環境設定
		/// </summary>
		private ShipmentActingSettings Settings;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			PostageCodeList = null;
			PcaGoodsList = null;
			DaibikiTesuryo = null;
			RitoAddressList = null;
			ShipmentDate = Date.Today;
			TaxRate = 10;
			PcaTantoList = null;
			HassouFileList = new List<string>();
			NouhinFileList = new List<string>();
			HsykdFileList = new List<string>();
			RitoHassouFile = new List<string>();
			CkakubiList = new List<ShiteiHassou>();
			AirCargoList = new List<ShiteiHassou>();
			Settings = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 消費税率を取得
			TaxRate = JunpDatabaseAccess.GetTaxRate(ShipmentDate, Program.DATABASE_ACCESS_CT);

			// 送料の商品コードを取得
			PostageCodeList = ShipmentActingAccess.Select_tMih送料商品コード(Program.DATABASE_ACCESS_CT);

			// PCA商品マスタの取得
			PcaGoodsList = ShipmentActingAccess.Select_vMicPCA商品マスタ(Program.DATABASE_ACCESS_CT);
			if (0 < PcaGoodsList.Count)
			{
				vMicPCA商品マスタ goods = PcaGoodsList.Find(p => p.sms_scd == Settings.DaibikiTesuryoCode);
				if (null != goods)
				{
					// 代引き手数料の商品名と金額を設定
					DaibikiTesuryo = new Tuple<string, int>(goods.sms_mei, goods.sms_hyo);
				}
			}
			// tMic離島から佐川急便の代引き発送の取扱い不能地域住所を取得
			RitoAddressList = ShipmentActingAccess.Select_tMic離島(Program.DATABASE_ACCESS_CT);

			// vMicPCA担当者マスタの取得
			PcaTantoList = ShipmentActingAccess.Select_vMicPCA担当者マスタ(Program.DATABASE_ACCESS_CT);

			{
				/*
				// 着日正常抽出確認テストモジュール
				sql = string.Format("SELECT * FROM {0} where jucd_mei like '%月%'", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA受注明細]);
				List<vMicPCA受注明細> aaaList = ShipmentActingAccess.Select_vMicPCA受注明細(sql, Program.DATABASE_ACCESS_CT);
				foreach (vMicPCA受注明細 aaa in aaaList)
				{
					Date? arrivalDate = aaa.ArrivalDate(Date.Today);
					System.Console.WriteLine(arrivalDate.ToString());
				}
				*/
			}

			// 環境設定の読み込み
			Settings = ShipmentActingSettingsIF.GetSettings();
/*
			{
				// 環境設定作成
				Settings = new ShipmentActingSettings();
				Settings.HassouDir = @"C:\ShipmentActing\発送用データ";
				Settings.HanDir = @"C:\ShipmentActing\汎用データ";
				Settings.HassouFile = "Hassou.csv";
				Settings.NouhinFile = "Nouhin.csv";
				Settings.HsykdFile = "Uriage.txt";
				Settings.SuridFile = "Syuka.txt";
				Settings.SnykdFile = "Shiire.txt";
				Settings.RitoHassouFile = "HassouY.csv";
				Settings.DaibikiTesuryoCode = "000610";
				Settings.ArchiveFile = "HASSOU.ZIP";
				Settings.ChakubiShiteiCode = "000020";
				Settings.AirCargoShippingCode = "000605";
				Settings.ZaikoListFile = "ZAIKOLST.xls";
				Settings.PCAReadDataDir = @"C:\ShipmentActing\pcadata";
				Settings.ShukkaDataHozonDir = @"C:\ShipmentActing\bk";
				Settings.MailFromAddress = "ミック業務部<suguro@mic.jp>";
				Settings.MailToAddress = "test<suguro@mic.jp>";
				Settings.MailCcAddress = "ミックtest<suguro@mic.jp>";
				ShipmentActingSettingsIF.SetSettings(Settings);
			}
*/
		}

		/// <summary>
		/// 送料を除くWhere文の取得
		/// </summary>
		/// <returns>Where文</returns>
		private string ExcludePostageGoodsWhereString()
		{
			string where = string.Empty;
			foreach (string code in PostageCodeList)
			{
				if (0 < where.Length)
				{
					where += " AND ";
				}
				where += string.Format("jucd_scd <> '{0}'", code);
			}
			return where;
		}

		/// <summary>
		/// 送料のみWhere文の取得
		/// </summary>
		/// <returns>Where文</returns>
		private string OnlyPostageGoodsWhereString()
		{
			string where = string.Empty;
			foreach (string code in PostageCodeList)
			{
				if (0 < where.Length)
				{
					where += " OR ";
				}
				where += string.Format("jucd_scd = '{0}'", code);
			}
			return where;
		}

		/// <summary>
		/// 実行ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			// tMihPca在庫引当表Jの引当在庫数をクリアする
			string sql = string.Format("UPDATE {0} SET f引当在庫数 = 0", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]);
			//ShipmentActingAccess.UpdateSet_tMihPca在庫引当表J(sql, null, Program.DATABASE_ACCESS_CT);

			int CntCommit = 0;	// 出荷品目数を初期化

			// 未出荷受注グループリストの取得（送料除く）
			sql = string.Format("SELECT jucd_jucbi, jucd_jno, jucd_tcd FROM {0} WHERE jucd_flg = '0' AND ({1}) GROUP BY jucd_jucbi, jucd_jno, jucd_tcd ORDER BY jucd_jucbi, jucd_jno"
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA受注明細]
									, ExcludePostageGoodsWhereString());
			List<vMicPCA受注明細> orderHeaderList = ShipmentActingAccess.Select_vMicPCA受注明細_GroupList(sql, Program.DATABASE_ACCESS_CT);
			foreach (vMicPCA受注明細 header in orderHeaderList)
			{
				bool bKannou = true;    // 完納可能判定フラグとりあえず 初期値 = 完納可能

				//  PCA受注明細から未出荷伝票の抽出（送料のみ）
				sql = string.Format("SELECT * FROM {0} WHERE jucd_flg = '0' AND jucd_tcd = '{1}' AND jucd_jucbi = {2} AND jucd_jno = {3} AND ({4}) ORDER BY jucd_seq"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA受注明細]
										, header.jucd_tcd
										, header.jucd_jucbi
										, header.jucd_jno
										, OnlyPostageGoodsWhereString());
				List<vMicPCA受注明細> postageList = ShipmentActingAccess.Select_vMicPCA受注明細(sql, Program.DATABASE_ACCESS_CT);
				if (null != postageList)
				{
					// 着日指定がある場合、出荷日より１週後の着日の伝票は出荷対象外とする
					foreach (vMicPCA受注明細 postage in postageList)
					{
						Date? arrivalDate = postage.ArrivalDate(ShipmentDate);
						if (arrivalDate.HasValue)
						{
							// 着日指定がある場合、出荷日より１週以内の日付かを判定しそれ以後の着日の伝票は出荷対象外とする
							if (ShipmentDate + 6 < arrivalDate)
							{
								// 出荷対象外
								bKannou = false;
							}
						}
					}
				}
				// 受注伝票単位で受注品目全てを完納出荷出来るか判定
				sql = string.Format("SELECT * FROM {0} WHERE jucd_flg = '0' AND jucd_tcd = '{1}' AND jucd_jucbi = {2} AND jucd_jno = {3} AND ({4}) ORDER BY jucd_seq"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA受注明細]
										, header.jucd_tcd
										, header.jucd_jucbi
										, header.jucd_jno
										, ExcludePostageGoodsWhereString());
				List<vMicPCA受注明細> orderList = ShipmentActingAccess.Select_vMicPCA受注明細(sql, Program.DATABASE_ACCESS_CT);
				if (null != orderList)
				{
					if (bKannou)
					{
						foreach (vMicPCA受注明細 order in orderList)
						{
							sql = string.Format("SELECT * FROM {0} WHERE f商品コード = '{1}'"
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]
													, order.jucd_scd);
							List<tMihPca在庫引当表J> zaikoList = ShipmentActingAccess.Select_tMihPca在庫引当表J(sql, Program.DATABASE_ACCESS_CT);
							if (null != zaikoList)
							{
								foreach (tMihPca在庫引当表J zaiko in zaikoList)
								{
									// 有効在庫数 = fPCA現在庫数 - f引当数
									// 受注残数 = jucd_suryo - jucd_ruikei
									if (zaiko.有効在庫数 < order.受注残数)
									{
										bKannou = false;    // 完納不可
									}
								}
								if (!bKannou)
								{
									break;  // 以降の明細は検査しない
								}
							}
						}
					}
					// 得意先情報を取得
					sql = string.Format("SELECT * FROM {0} WHERE 得意先No = '{1}'"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー２]
										, header.jucd_tcd);
					List <vMic全ユーザー2> userList = ShipmentActingAccess.Select_vMic全ユーザー2(sql, Program.DATABASE_ACCESS_CT);

					if (!bKannou)
					{
						// 出荷対象外 - 引当在庫数に受注数を加算して更新
						foreach (vMicPCA受注明細 order in orderList)
						{
							sql = string.Format("SELECT * FROM {0} WHERE f商品コード = '{1}'"
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]
													, order.jucd_scd);
							List<tMihPca在庫引当表J> zaikoList = ShipmentActingAccess.Select_tMihPca在庫引当表J(sql, Program.DATABASE_ACCESS_CT);
							if (null != zaikoList)
							{
								foreach (tMihPca在庫引当表J zaiko in zaikoList)
								{
									sql = string.Format("UPDATE {0} SET f引当在庫数 = @1 WHERE f商品コード = '{1}'"
														, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]
														, order.jucd_scd);
									SqlParameter[] param = { new SqlParameter("@1", zaiko.f引当在庫数 + order.jucd_suryo) };	// f引当在庫数
									//ShipmentActingAccess.UpdateSet_tMihPca在庫引当表J(sql, param, Program.DATABASE_ACCESS_CT);
								}
							}
						}
					}
					else
					{
						// 出荷代行リスト
						List<ShipmentActingData> shipmentList = new List<ShipmentActingData>();

						// 出荷対象 - 受注伝票単位で受注品目全てを完納出荷可能
						int lShkCount = 0;    // 出荷明細件数の初期化

						// 代金引換 or 後請求 を決定
						bool bDaibiki = false;
						if (userList.First().Is別途請求先)
						{
							if (1 == userList.First().代引配送)
							{
								// 顧客区分が「ユーザー」で、請求書発行方法が「コンビニ・郵便局請求書」なら、消耗品は代引き発送
								bDaibiki = true;
							}
						}


						foreach (vMicPCA受注明細 order in orderList)
						{
							sql = string.Format("SELECT * FROM {0} WHERE f商品コード = '{1}'"
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]
													, order.jucd_scd);
							List<tMihPca在庫引当表J> zaikoList = ShipmentActingAccess.Select_tMihPca在庫引当表J(sql, Program.DATABASE_ACCESS_CT);
							if (null != zaikoList)
							{
								foreach (tMihPca在庫引当表J zaiko in zaikoList)
								{
									int hikiate = 0;
									int ruikei = 0;
									int zan = 0;
									if (order.受注残数 <= zaiko.有効在庫数)
									{
										hikiate = order.受注残数;	// 引当数 = 受注数 - 出荷済数・・・受注残数を全て引当
										ruikei = order.jucd_suryo;	// 出荷済数 = 受注数・・・受注数全てを出荷済み
										zan = 0;					// 受注残 = 0・・・受注残なし
									}
									else if (0 < zaiko.有効在庫数)
									{
										hikiate = zaiko.有効在庫数;				// 引当数 = 有効在庫数・・・有効在庫数を全て引当
										ruikei = order.jucd_ruikei + hikiate;	// 出荷済数 = 出荷済数 + 引当数・・・引当数を加算
										zan = order.jucd_suryo - ruikei;		// 受注残 = 受注数 - 出荷済数・・・受注残
									}
									else
									{
										hikiate = 0;						// 引当数 = 0・・・引当なし
										ruikei = order.jucd_ruikei;			// 出荷済数 = 出荷済数・・・出荷済み数はそのまま
										zan = order.jucd_suryo - ruikei;	// 受注残 = 受注数 - 出荷済数・・・受注残
									}
									if (0 < ruikei)
									{
										// 引当可能
										lShkCount++;
										ShipmentActingData ship = new ShipmentActingData(order);
										ship.jucd_ruikei = ruikei;   // 出荷済数を更新
										ship.jucd_zan = zan * order.jucd_tanka;
										if (0 == zan)
										{
											ship.jucd_flg = 1;  // 出荷済フラグをON
										}
										ship.MeiType = 0;   // 0:商品
										ship.Hikiatesu = hikiate;   // 引当数を更新
										ship.HassouDate = Date.Today;   // 出荷日付
										vMicPCA商品マスタ goods = PcaGoodsList.Find(p => p.sms_scd == order.jucd_scd);
										if (null != goods)
										{
											ship.jucd_gentan = goods.sms_gen;     // 商品マスタの原単価をコピー
										}
										if (1 == lShkCount)
										{
											ship.First = 1; // 1行目フラグをON
										}
										shipmentList.Add(ship);
									}
									sql = string.Format("UPDATE {0} SET f引当数 = @1, f引当在庫数 = @2 WHERE f商品コード = '{1}'"
															, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]
															, order.jucd_scd);
									SqlParameter[] param = { new SqlParameter("@1", zaiko.f引当数 + hikiate),	// f引当数
															new SqlParameter("@2", zaiko.f引当在庫数 + zan) };   // f引当在庫数
									//ShipmentActingAccess.UpdateSet_tMihPca在庫引当表J(sql, param, Program.DATABASE_ACCESS_CT);
								}
							}
						}
						if (0 < lShkCount)
						{
							foreach (vMicPCA受注明細 postage in postageList)
							{
								////////////////////////////////////
								// 送料を明細に追加

								lShkCount++;    // 出荷明細件数に１件追加

								ShipmentActingData ship = new ShipmentActingData(postage);
								//ship.First = 0;
								//ship.Commit = 0
								ship.jucd_flg = 1;  // 出荷済フラグ ON
								ship.MeiType = 1;   // 1:送料
								ship.HassouDate = ShipmentDate; // 出荷日付
								ship.jucd_ruikei = postage.jucd_suryo;
								//ship.jucd_zan = 0;
								//ship.Hikiatesu = 0;
								shipmentList.Add(ship);
							}
							if (bDaibiki)
							{
								// 合計金額
								int lSumKingaku = ShipmentActingData.GetTotalKingaku(shipmentList);
								if (0 < lSumKingaku)
								{
									////////////////////////////////////
									// 代引き手数料を明細に追加

									lShkCount++;	// 出荷明細件数に１件追加

									ShipmentActingData ship = new ShipmentActingData(shipmentList.First() as vMicPCA受注明細);
									//ship.First = 0;
									//ship.Commit = 1 '出荷明細出力品目とする
									ship.Hikiatesu = 0;
									ship.HassouDate = ShipmentDate; // 出荷日付
									ship.jucd_scd = Settings.DaibikiTesuryoCode;       // 代引き手数料の商品コード(000610)
									ship.jucd_mkbn = 1;
									ship.jucd_tax = 2;
									ship.jucd_mei = DaibikiTesuryo.Item1;       // 代引き手数料の商品名
									ship.jucd_kingaku = DaibikiTesuryo.Item2;    // 代引き手数料の金額
									ship.jucd_zei = shipmentList[0].jucd_kingaku * TaxRate; // 消費税
									{
										//ship.jucd_bketa = "1";
										//gaOrder(i).jucd_komi = 0;
										//gaOrder(i).jucd_iri = 0
										//gaOrder(i).jucd_hako = 0
										//gaOrder(i).jucd_suryo = 0
										//gaOrder(i).jucd_tani = ""
										//gaOrder(i).jucd_tanka = 0
										//gaOrder(i).jucd_gentan = 0
										//gaOrder(i).jucd_zan = 0 
										//gaOrder(i).jucd_genka = 0
										//gaOrder(i).jucd_biko = ""
										//gaOrder(i).jucd_souko = 0
										//gaOrder(i).jucd_arari = 0
										//gaOrder(i).jucd_uchi = 0
										//gaOrder(i).jucd_hyo = 0
										//gaOrder(i).jucd_baitan = 0
										//gaOrder(i).jucd_baika = 0
										//gaOrder(i).jucd_kikaku = ""
										//gaOrder(i).jucd_color = ""
										//gaOrder(i).jucd_size = ""
									}
									shipmentList.Add(ship);
								}
								else
								{
									// 金額が0円なので代引き発送ではなく通常発送に切り換える
									bDaibiki = false;
								}
							}
							// PCA受注明細の更新、出荷累計、受注残金額、出荷済フラグ
							foreach (ShipmentActingData ship in shipmentList)
							{
								if ("000610" != ship.jucd_scd)
								{
									// 代引き手数料以外
									sql = string.Format("UPDATE {0} SET jucd_flg = @1, jucd_ruikei = @2, jucd_zan = @3 WHERE jucd_flg = '0' AND jucd_hid = {1} AND jucd_seq = {2}"
															, PcaDatabaseDefine.TableName[PcaDatabaseDefine.TableType.JUCD]
															, ship.jucd_hid
															, ship.jucd_seq);
									SqlParameter[] param = { new SqlParameter("@1", ship.jucd_flg),
															new SqlParameter("@2", ship.jucd_ruikei),
															new SqlParameter("@3", ship.jucd_zan) };
									//ShipmentActingAccess.UpdateSet_JUCD(sql, param, Program.DATABASE_ACCESS_CT);

									//ship.Commit = 1;    // 出荷明細出力品目とする
									CntCommit++;		// 出荷品目数に１品目追加
								}
							}
							if (0 < CntCommit)
							{
								// 出荷品目数あり

								// 合計金額の計算
								int lSumKingaku = 0;
								foreach (ShipmentActingData ship in shipmentList)
								{
									//if (1 == ship.Commit)
									{
										// 出荷明細出力品目
										if (0 == ship.MeiType)
										{
											// 0:商品
											lSumKingaku += ship.jucd_tanka * ship.Hikiatesu;
										}
										else if (1 == ship.MeiType)
										{
											// 1:送料
											lSumKingaku += ship.jucd_kingaku;
										}
									}
								}
								// 消費税額の計算
								int lSumTax = lSumKingaku * TaxRate;

								// 請求先が異なる場合は、得意先情報を明細に追加する
								if (userList.First().Is別途請求先)
								{
									////////////////////////////////////
									// 記事１（"～～～～～様分"）を明細に追加

									lShkCount++;	// 出荷明細件数に１件追加
									CntCommit++;    // 出荷品目数に１品目追加

									ShipmentActingData ship = new ShipmentActingData(shipmentList.First() as vMicPCA受注明細);
									//ship.First = 0
									//ship.Commit = 1;    // 出荷明細出力品目とする
									ship.MeiType = 2;   // 記事
									ship.HassouDate = ShipmentDate; // 出荷日付
									ship.jucd_scd = "000014";
									ship.jucd_mkbn = 4; // 4:記事
									ship.jucd_mei = userList.First().顧客名;
									{
										//gaOrder(i).jucd_bketa = "1";
										//gaOrder(i).hikiatesu = 0;
										//gaOrder(i).jucd_tax = "0";
										//gaOrder(i).jucd_komi = "0";
										//gaOrder(i).jucd_iri = 0;
										//gaOrder(i).jucd_hako = 0;
										//gaOrder(i).jucd_suryo = 0;
										//gaOrder(i).jucd_tani = "";
										//gaOrder(i).jucd_tanka = 0;
										//gaOrder(i).jucd_gentan = 0;
										//gaOrder(i).jucd_kingaku = 0;
										//gaOrder(i).jucd_zan = 0;
										//gaOrder(i).jucd_genka = 0;
										//gaOrder(i).jucd_biko = "";
										//gaOrder(i).jucd_souko = 0;
										//gaOrder(i).jucd_arari = 0;
										//gaOrder(i).jucd_zei = 0;
										//gaOrder(i).jucd_uchi = 0;
										//gaOrder(i).jucd_hyo = 0;
										//gaOrder(i).jucd_baitan = 0;
										//gaOrder(i).jucd_baika = 0;
										//gaOrder(i).jucd_kikaku = "";
										//gaOrder(i).jucd_color = "";
										//gaOrder(i).jucd_size = "";
										//gaOrder(i).jucd_ncd = gaOrder(1).jucd_ncd;
										//gaOrder(i).jucd_jucbi = gaOrder(1).jucd_jucbi;
										//gaOrder(i).jucd_jno = gaOrder(1).jucd_jno;
										//gaOrder(i).jucd_tcd = gaOrder(1).jucd_tcd;
										//gaOrder(i).jucd_tbmn = gaOrder(1).jucd_tbmn;
										//gaOrder(i).jucd_ttan = gaOrder(1).jucd_ttan;
										//gaOrder(i).jucd_jbmn = gaOrder(1).jucd_jbmn;
										//gaOrder(i).jucd_jtan = gaOrder(1).jucd_jtan;
										//gaOrder(i).jucd_tekcd = gaOrder(1).jucd_tekcd;
										//gaOrder(i).jucd_tekmei = gaOrder(1).jucd_tekmei;
										//gaOrder(i).jucd_eda = gaOrder(1).jucd_eda;
									}
									shipmentList.Add(ship);

									////////////////////////////////////
									// 記事２（"得意先No xxxxxx"）を明細に追加

									lShkCount++;    // 出荷明細件数に１件追加
									CntCommit++;    // 出荷品目数に１品目追加

									ship = new ShipmentActingData(shipmentList.First() as vMicPCA受注明細);
									//ship.First = 0
									//ship.Commit = 1;  // 出荷明細出力品目とする
									ship.MeiType = 2;	// 記事
									ship.HassouDate = ShipmentDate; // 出荷日付
									ship.jucd_scd = "000014";
									ship.jucd_mkbn = 4;     // 4:記事
									ship.jucd_mei = "得意先No" + shipmentList.First().jucd_tcd;
									{
										//gaOrder(i).hikiatesu = 0;
										//gaOrder(i).jucd_jucbi = gaOrder(1).jucd_jucbi;
										//gaOrder(i).jucd_jno = gaOrder(1).jucd_jno;
										//gaOrder(i).jucd_tcd = gaOrder(1).jucd_tcd;
										//gaOrder(i).jucd_tbmn = gaOrder(1).jucd_tbmn;
										//gaOrder(i).jucd_ttan = gaOrder(1).jucd_ttan;
										//gaOrder(i).jucd_jbmn = gaOrder(1).jucd_jbmn;
										//gaOrder(i).jucd_jtan = gaOrder(1).jucd_jtan;
										//gaOrder(i).jucd_tekcd = gaOrder(1).jucd_tekcd;
										//gaOrder(i).jucd_tekmei = gaOrder(1).jucd_tekmei;
										//gaOrder(i).jucd_eda = gaOrder(1).jucd_eda;
										//gaOrder(i).jucd_tax = "0";
										//gaOrder(i).jucd_komi = "0";
										//gaOrder(i).jucd_iri = 0;
										//gaOrder(i).jucd_hako = 0;
										//gaOrder(i).jucd_suryo = 0;
										//gaOrder(i).jucd_tani = "";
										//gaOrder(i).jucd_tanka = 0;
										//gaOrder(i).jucd_gentan = 0;
										//gaOrder(i).jucd_kingaku = 0;
										//gaOrder(i).jucd_zan = 0;
										//gaOrder(i).jucd_genka = 0;
										//gaOrder(i).jucd_bketa = "1";
										//gaOrder(i).jucd_biko = "";
										//gaOrder(i).jucd_ncd = gaOrder(1).jucd_ncd;
										//gaOrder(i).jucd_souko = 0;
										//gaOrder(i).jucd_arari = 0;
										//gaOrder(i).jucd_zei = 0;
										//gaOrder(i).jucd_uchi = 0;
										//gaOrder(i).jucd_hyo = 0;
										//gaOrder(i).jucd_baitan = 0;
										//gaOrder(i).jucd_baika = 0;
										//gaOrder(i).jucd_kikaku = "";
										//gaOrder(i).jucd_color = "";
										//gaOrder(i).jucd_size = "";
									}
									shipmentList.Add(ship);

									////////////////////////////////////
									// 消費税行を明細に追加

									lShkCount++;    // 出荷明細件数に１件追加
									CntCommit++;    // 出荷品目数に１品目追加

									ship = new ShipmentActingData(shipmentList.First() as vMicPCA受注明細);
									//ship.First = 0;
									//ship.Commit = 1;	// 出荷明細出力品目とする
									ship.MeiType = 3;   // 消費税行
									ship.HassouDate = ShipmentDate; // 出荷日付
									ship.jucd_scd = "";
									ship.jucd_mei = "消費税";
									ship.jucd_tanka = lSumTax;
									ship.jucd_kingaku = lSumTax;
									{
										//gaOrder(i).hikiatesu = 0
										//gaOrder(i).jucd_jucbi = gaOrder(1).jucd_jucbi : gaOrder(i).jucd_jno = gaOrder(1).jucd_jno
										//gaOrder(i).jucd_tcd = gaOrder(1).jucd_tcd : gaOrder(i).jucd_tbmn = gaOrder(1).jucd_tbmn
										//gaOrder(i).jucd_ttan = gaOrder(1).jucd_ttan : gaOrder(i).jucd_jbmn = gaOrder(1).jucd_jbmn
										//gaOrder(i).jucd_jtan = gaOrder(1).jucd_jtan : gaOrder(i).jucd_tekcd = gaOrder(1).jucd_tekcd
										//gaOrder(i).jucd_tekmei = gaOrder(1).jucd_tekmei : gaOrder(i).jucd_eda = gaOrder(1).jucd_eda
										//gaOrder(i).jucd_mkbn = ""
										//gaOrder(i).jucd_tax = "0"
										//gaOrder(i).jucd_komi = "0"
										//gaOrder(i).jucd_iri = 0
										//gaOrder(i).jucd_hako = 0
										//gaOrder(i).jucd_suryo = 1
										//gaOrder(i).jucd_tani = ""
										//gaOrder(i).jucd_gentan = 0
										//gaOrder(i).jucd_zan = 0 : gaOrder(i).jucd_genka = 0 : gaOrder(i).jucd_bketa = "1" : gaOrder(i).jucd_biko = ""
										//gaOrder(i).jucd_ncd = gaOrder(1).jucd_ncd
										//gaOrder(i).jucd_souko = 0 : gaOrder(i).jucd_arari = 0 : gaOrder(i).jucd_zei = 0 : gaOrder(i).jucd_uchi = 0
										//gaOrder(i).jucd_hyo = 0 : gaOrder(i).jucd_baitan = 0 : gaOrder(i).jucd_baika = 0
										//gaOrder(i).jucd_kikaku = "" : gaOrder(i).jucd_color = "" : gaOrder(i).jucd_size = ""
									}
									shipmentList.Add(ship);

									// 担当者名と合計金額を設定
									vMicPCA担当者マスタ pcaMaster = PcaTantoList.Find(p => p.emst_kbn == shipmentList.First().jucd_jtan);
									if (null != pcaMaster)
									{
										foreach (ShipmentActingData workShip in shipmentList)
										{
											workShip.tanto = pcaMaster.emst_str;            // 担当者名
											workShip.SumKingaku = lSumKingaku + lSumTax;    // 合計金額 + 消費税額
										}
									}
									int lDaibikiKaishugaku = 0; // 代引き回収金額
									if (bDaibiki)
									{
										// 代引き
										lDaibikiKaishugaku = lSumKingaku + lSumTax; // 代引き回収金額 ← (合計金額 + 消費税額)
									}
									// 伝票番号  採番	[dbo].[pMic売上伝票番号取得]ストアドプロシージャの実行
									int lNo = 1;

									// 発送先名記憶
									;

									// 佐川急便の「代引き発送の取扱い不能地域」の判定
									bool bRitoDaibiki = false;
									foreach (string rito in RitoAddressList)
									{
										if (rito == userList.First().住所.Substring(rito.Length))
										{
											bRitoDaibiki = true;
											break;
										}
									}
									if (bRitoDaibiki)
									{
										// 離島代引き発送は別ファイルに出力
										RitoHassouFile.Add(shipmentList.First().OutHassou(userList.First(), lDaibikiKaishugaku, true));
									}
									else
									{
										HassouFileList.Add(shipmentList.First().OutHassou(userList.First(), lDaibikiKaishugaku, false));
									}
									foreach (ShipmentActingData ship2 in shipmentList)
									{
										//if (1 == ship2.Commit)
										{
											// 出荷明細出力品目

											// 業務委託先で出力するための納品書用データを書き込む
											NouhinFileList.Add(ship2.OutNouhin(userList.First(), lNo));

											// PCA売上明細汎用データを書き込む
											HsykdFileList.Add(ship2.OutHanUriage(userList.First(), lDaibikiKaishugaku, lNo, TaxRate));

											// 着荷日付指定発送情報を配列に保存
											if (ship2.jucd_scd == Settings.ChakubiShiteiCode)
											{
												// 着荷日指定記事の商品コード(000020)
												ShiteiHassou chakubi = new ShiteiHassou();
												if (0 < userList.First().発送先名.Length)
												{
													chakubi.顧客名 = userList.First().発送先名;
												}
												else
												{
													chakubi.顧客名 = userList.First().顧客名;
												}
												chakubi.伝票番号 = lNo; // 伝票番号
												chakubi.品名 = ship2.jucd_mei;     // 着日指定文字列「着日指定：　月　日」
												chakubi.得意先No = userList.First().得意先No;		// 得意先No
												CkakubiList.Add(chakubi);
											}
											// 航空便指定発送情報を配列に保存
											if (ship2.jucd_scd == Settings.AirCargoShippingCode)
											{
												// 航空便指定発送の商品コード(000605)
												ShiteiHassou airCargo = new ShiteiHassou();
												if (0 < userList.First().発送先名.Length)
												{
													airCargo.顧客名 = userList.First().発送先名;
												}
												else
												{
													airCargo.顧客名 = userList.First().顧客名;
												}
												airCargo.伝票番号 = lNo;    // '伝票番号
												airCargo.品名 = ship2.jucd_mei;   // 航空便指定送料商品名
												AirCargoList.Add(airCargo);
											}
										}
									}
									// Web受注品目は出荷日を登録
									sql = string.Format("UPDATE {0} SET 出荷日 = @1 WHERE PCA受注No = '{1}'"
															, EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]
															, shipmentList.First().jucd_jno);
									SqlParameter[] param = { new SqlParameter("@1", ShipmentDate.ToDateTime().ToShortDateString()) };   // 出荷日
																																		//ShipmentActingAccess.UpdateSet_tMICestore_log(sql, param, Program.DATABASE_ACCESS_CT);

									// 出荷指示テーブルに PCA受注No, 出荷日, 受注先顧客No, 運送会社 を記録
									SqlParameter[] param2 = { new SqlParameter("@1", shipmentList.First().jucd_jno),
															new SqlParameter("@2", ShipmentDate.ToIntYMD()),
															new SqlParameter("@3", userList.First().顧客No),
															new SqlParameter("@4", bRitoDaibiki ? "Y" : "S"),	// 離島代引きは「ヤマト運輸」離島代引き以外は「佐川急便」
															new SqlParameter("@5", 0) };
									//ShipmentActingAccess.InsertInto_t_MicSyukkashiji(param2, Program.DATABASE_ACCESS_CT);

								}
							}
						}
					}
				}
			}
		}
	}
}
