//
// Program.cs
// 
// アルメックス保守サービス仕入データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/01/20 勝呂)
//
using AlmexMaintePurchaseFile.Forms;
using AlmexMaintePurchaseFile.Mail;
using AlmexMaintePurchaseFile.Settings;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.BaseFactory.Pca;
using MwsLib.Common;
using MwsLib.DB.SqlServer.AlmexMainte;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.Log;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlmexMaintePurchaseFile
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		private const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AlmexMaintePurchaseFileSettings gSettings;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ｱﾙﾒｯｸｽ保守仕入ﾃﾞｰﾀ作成";

		/// <summary>
		/// 集計日
		/// </summary>
		public static Date CollectDate;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = AlmexMaintePurchaseFileSettingsIF.GetSettings();

#if DEBUG
			CollectDate = new Date(2021, 1, 1);
#else
			// 集計日を当月初日に設定
			CollectDate = Date.Today.FirstDayOfTheMonth();
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					string msg = OutputCsvFile(CollectDate);
					AlmexMaintePurchaseFileSettingsIF.SetSettings(gSettings);
					if (0 < msg.Length)
					{
						return 1;
					}
					return 0;
				}
			}
			Application.Run(new MainForm());
			return 0;
		}

		/// <summary>
		/// アルメックス保守仕入データ.csv
		/// </summary>
		/// <param name="settings">出力ファイルパス名</param>
		/// <param name="collectDate">集計日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date collectDate)
		{
			string msg = string.Empty;
			try
			{
				// アルメックス保守仕入データ.csvの出力
				using (var sw = new System.IO.StreamWriter(gSettings.Pathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					List<MakePurchaseData> stockList = new List<MakePurchaseData>();

					List<vMicPCA売上明細> pcaList = AlmexMainteAccess.GetAlmexMainteEarningsList(gSettings.GetAlmexMainteGoods(), collectDate.ToYearMonth().ToSpan(), DATABASE_ACCESS_CT);
					if (0 < pcaList.Count)
					{
						// 商品マスタ
						List<Tuple<string, string>> goodsList = new List<Tuple<string, string>>();

						// 仕入先マスタ
						List<Tuple<string, string>> stockMasterList = new List<Tuple<string, string>>();
						foreach (vMicPCA売上明細 pca in pcaList)
						{
							if (0 != pca.数量)
							{
								AlmexMainteGoods goods = gSettings.AlmexMainteGoodsList.Find(p => p.商品コード == pca.sykd_scd);
								if (null != goods)
								{
									MakePurchaseData stock = new MakePurchaseData();
									stock.f部門コード = pca.sykd_jbmn;
									stock.f担当者コード = pca.sykd_jtan;
									stock.f仕入商品コード = goods.仕入商品コード;
									stock.f単位 = pca.sykd_tani;
									stock.f仕入価格 = goods.仕入価格;
									stock.f売上日 = pca.sykd_uribi;
									stock.f仕入フラグ = goods.仕入フラグ;
									stock.f消費税率 = pca.消費税;
									stock.f数量 = pca.数量;

									Tuple<string, string> goodsName = goodsList.Find(p => p.Item1 == goods.仕入商品コード);
									if (null != goodsName)
									{
										stock.f商品名 = goodsName.Item2;
									}
									else
									{
										vMicPCA商品マスタ mst = JunpDatabaseAccess.Select_vMicPCA商品マスタ(goods.仕入商品コード, DATABASE_ACCESS_CT);
										if (null != mst)
										{
											stock.f商品名 = mst.sms_mei;
											goodsList.Add(new Tuple<string, string>(goods.仕入商品コード, mst.sms_mei));
										}
									}

									// アプリケーション情報のLicensedKeyから仕入先コードを取得する方法
									// 請求元と請求先が違うか確認
									string stockCode = pca.sykd_tcd;
									string whereStr = string.Format("sykd_uribi = {0} AND sykd_denno = {1} AND sykd_scd = '000014' AND sykd_mei like '%得意先No.%'"
																		, pca.sykd_uribi, pca.sykd_denno);
									List<vMicPCA売上明細> list = JunpDatabaseAccess.Select_vMicPCA売上明細(whereStr, "", DATABASE_ACCESS_CT);
									if (null != list && 0 < list.Count)
									{
										// 請求先が違う場合
										stockCode = list[0].sykd_mei.Replace("得意先No.", "").Trim();
									}
									List<tMik基本情報> basic = JunpDatabaseAccess.Select_tMik基本情報(string.Format("[fkj得意先情報] = '{0}'", stockCode), "", DATABASE_ACCESS_CT);
									whereStr = string.Format("faiCliMicID = {0} AND (faiアプリケーション名 = '{1}' OR faiアプリケーション名 = '{2}' OR faiアプリケーション名 = '{3}' OR faiアプリケーション名 = '{4}' OR faiアプリケーション名 = '{5}' OR faiアプリケーション名 = '{6}')"
																		, basic[0].fkjCliMicID
																		, tMikコードマスタ.fcmコード_AlmexMainteTex30_Cash
																		, tMikコードマスタ.fcmコード_AlmexMainteTex30_Credit
																		, tMikコードマスタ.fcmコード_AlmexMainteFitA_Cash
																		, tMikコードマスタ.fcmコード_AlmexMainteFitA_QR
																		, tMikコードマスタ.fcmコード_AlmexMainteFitA_QRCredit
																		, tMikコードマスタ.fcmコード_AlmexMainteFitA_Credit);
									List<tMikアプリケーション情報> apl = JunpDatabaseAccess.Select_tMikアプリケーション情報(whereStr, "faiアプリケーションNo, faiアプリケーション名", DATABASE_ACCESS_CT);
									if (null != apl && 0 < apl.Count)
									{
										stock.f仕入先コード = apl[0].faiLicensedKey;
									}
									if (6 != stock.f仕入先コード.Length && true == StringUtil.IsAllHankakuNumeral(stock.f仕入先コード))
									{
										// 仕入先コードが半角6桁でない
										msg = string.Format("アプリケーション情報 Licensed Key欄の仕入先コードが空欄もしくは正しくありません。(伝票No.{0})", pca.sykd_denno);
										ErrorLogger.Error(msg);
									}
									//// 住所から仕入先コードを取得する方法
									//List<tMik基本情報> basic = JunpDatabaseAccess.Select_tMik基本情報(string.Format("[fkj得意先情報] = '{0}'", pca.sykd_tcd), "", DATABASE_ACCESS_CT);
									//if (null != basic && 0 < basic.Count)
									//{
									//	stock.f仕入先コード = AlemxPurchase.仕入先コード(basic[0].県番号, basic[0].fkj住所１);
									//}

									if (0 < stock.f仕入先コード.Length)
									{
										Tuple<string, string> stockName = stockMasterList.Find(p => p.Item1 == stock.f仕入先コード);
										if (null != stockName)
										{
											stock.f仕入先名 = stockName.Item2;
										}
										else
										{
											whereStr = string.Format("rms_tcd = '{0}'", stock.f仕入先コード);
											List<vMicPCA仕入先マスタ> mst = JunpDatabaseAccess.Select_vMicPCA仕入先マスタ(whereStr, "", DATABASE_ACCESS_CT);
											if (null != mst && 0 < mst.Count)
											{
												stock.f仕入先名 = string.Format("{0} {1}", mst[0].rms_mei1.Trim(), mst[0].rms_mei2.Trim());
												stockMasterList.Add(new Tuple<string, string>(stock.f仕入先コード, stock.f仕入先名));
											}
										}
									}
									stockList.Add(stock);
								}
							}
						}
						int plusNo = gSettings.InitDenNo; // 20801番台
						foreach (MakePurchaseData stock in stockList)
						{
							string str = stock.ToPurchase(plusNo, gSettings.PcaVersion);
							sw.WriteLine(str);
							plusNo++;
						}
					}
					// 経理部にメール送信
					SendMailControl.AlmexMainteSendMail(stockList);
				}
			}
			catch (Exception ex)
			{
				ErrorLogger.Error(ex.Message);
				return ex.Message;
			}
			return msg;
		}
	}
}
