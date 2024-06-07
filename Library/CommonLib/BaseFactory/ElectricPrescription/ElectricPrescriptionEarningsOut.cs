//
// ElectricPrescriptionEarningsOut.cs
//
// 電子処方箋管理サービス売上情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/07/01 勝呂):新規作成
// 
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.ElectricPrescription
{
	/// <summary>
	/// 電子処方箋管理サービス売上情報
	/// </summary>
	public class ElectricPrescriptionEarningsOut
	{
		/// <summary>
		/// XXXXXX 電子処方箋管理サービス（院外処方）
		/// </summary>
		public const string 院外処方_商品コード = "999998";

		/// <summary>
		/// XXXXXX 電子処方箋管理サービス（院内処方）
		/// </summary>
		public const string 院内処方_商品コード = "999999";

		public int 受付番号 { get; set; }

		public int 顧客No { get; set; }

		public string 顧客名 { get; set; }

		public string 得意先コード { get; set; }

		public string 請求先コード { get; set; }

		public string 請求先名 { get; set; }

		public string 商品コード { get; set; }

		public string 商品名 { get; set; }

		public int 標準価格 { get; set; }

		public int 原単価 { get; set; }

		public string 単位 { get; set; }

		public short? PCA部門コード { get; set; }

		public short? PCA倉庫コード { get; set; }

		public string PCA担当者コード { get; set; }

		public DateTime? 申込日時 { get; set; }

		public DateTime? 契約開始日 { get; set; }

		public DateTime? 契約終了日 { get; set; }

		public DateTime? 売上日時 { get; set; }

		/// <summary>
		/// 商品名の取得
		/// </summary>
		public string 商品名称
		{
			get
			{
				return StringUtil.GetSubstringByByte(商品名, 36);
			}
		}

		/// <summary>
		/// 記事行１ 顧客名の取得
		/// ○○○○様分
		/// </summary>
		public string 品名_記事行1
		{
			get
			{
				return string.Format("{0}様分", StringUtil.GetSubstringByByte(顧客名, 32));
			}
		}

		/// <summary>
		/// 記事行２ 得意先コードの取得
		/// 得意先No. XXXXXX
		/// </summary>
		public string 品名_記事行2
		{
			get
			{
				return string.Format("得意先No.{0}", 得意先コード);
			}
		}

		/// <summary>
		/// 院外処方かどうか？
		/// </summary>
		public bool Is院外処方
		{
			get
			{
				return (院外処方_商品コード == 商品コード) ? true : false;
			}
		}

		/// <summary>
		/// 商品コードに対応するサービスIDの取得
		/// </summary>
		public ServiceCodeDefine.ServiceCode GetServiceCode
		{
			get
			{
				return (Is院外処方) ? ServiceCodeDefine.ServiceCode.ElectricPrescriptionOutside : ServiceCodeDefine.ServiceCode.ElectricPrescriptionInside;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ElectricPrescriptionEarningsOut()
		{
			受付番号 = 0;
			顧客No = 0;
			顧客名 = string.Empty;
			得意先コード = string.Empty;
			請求先コード = string.Empty;
			請求先名 = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			標準価格 = 0;
			原単価 = 0;
			単位 = string.Empty;
			PCA部門コード = null;
			PCA倉庫コード = null;
			PCA担当者コード = string.Empty;
			申込日時 = null;
			契約開始日 = null;
			契約終了日 = null;
			売上日時 = null;
		}

		/// <summary>
		/// 摘要名の取得
		/// yyyy/MM～yyyy/MM 月額利用料
		/// <param name="startDate">契約開始日</param>
		/// <param name="endDate">契約終了日</param>
		/// </summary>
		public string 摘要名(DateTime? startDate, DateTime? endDate)
		{
			if (startDate.HasValue && endDate.HasValue)
			{
				return string.Format("{0}～{1} 月額利用料", startDate.Value.ToDate().ToYearMonth().GetNormalString(), endDate.Value.ToDate().ToYearMonth().GetNormalString());
			}
			return string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<ElectricPrescriptionEarningsOut> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<ElectricPrescriptionEarningsOut> result = new List<ElectricPrescriptionEarningsOut>();
				foreach (DataRow row in table.Rows)
				{
					ElectricPrescriptionEarningsOut data = new ElectricPrescriptionEarningsOut();
					data.受付番号 = DataBaseValue.ConvObjectToInt(row["ApplyNo"]);
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.顧客名 = row["顧客名"].ToString().Trim();
					data.得意先コード = row["得意先コード"].ToString().Trim();
					data.請求先コード = row["請求先コード"].ToString().Trim();
					data.請求先名 = row["請求先名"].ToString().Trim();
					data.商品コード = row["商品コード"].ToString().Trim();
					data.商品名 = row["商品名"].ToString().Trim();
					data.標準価格 = DataBaseValue.ConvObjectToInt(row["標準価格"]);
					data.原単価 = DataBaseValue.ConvObjectToInt(row["原単価"]);
					data.単位 = row["単位"].ToString().Trim();
					data.PCA部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["PCA部門コード"]);
					data.PCA倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["PCA倉庫コード"]);
					data.PCA担当者コード = row["PCA担当者コード"].ToString().Trim();
					data.申込日時 = DataBaseValue.ConvObjectToDateTimeNull(row["申込日時"]);
					data.契約開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約開始日"]);
					data.契約終了日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約終了日"]);
					data.売上日時 = DataBaseValue.ConvObjectToDateTimeNull(row["売上日時"]);
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 売上データCSV文字列の取得
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="saleDate">売上日</param>
		/// <param name="startDate">契約開始日</param>
		/// <param name="endDate">契約終了日</param>
		/// <param name="tax">税率</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToEarnings(int no, string hanbaisakiCode, Date saleDate, DateTime? startDate, DateTime? endDate, int tax, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(startDate, endDate);//12:摘要名(30)
			pca.商品コード = 商品コード;// 15:商品コード(13)
			pca.マスター区分 = 0;// 16:マスタ区分
			pca.商品名 = 商品名称;// 17:品名(36)
			pca.倉庫コード = PCA倉庫コード.Value.ToString();// 19:倉庫コード(6)
			pca.数量 = 1;// 22:数量
			pca.単位 = 単位;// 23:単位
			pca.単価 = 標準価格;// 24:単価
			pca.売上金額 = 標準価格;// 25:売上金額
			pca.原単価 = 原単価;// 26:原単価
			pca.原価金額 = 原単価;// 27:原価額
			pca.税区分 = 2;// 31:税区分
			pca.税込区分 = 0;// 32:税込区分
			pca.標準価格 = 標準価格;// 34:標準価格
			pca.税率 = tax;// 48:税率
			return pca.ToCsvString(pcaVer);
		}

		/// <summary>
		/// 売上データ記事行１ CSV文字列の取得
		/// ○○○○様分
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="saleDate">売上日 </param>
		/// <param name="startDate">契約開始日</param>
		/// <param name="endDate">契約終了日</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle1(int no, string hanbaisakiCode, Date saleDate, DateTime? startDate, DateTime? endDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(startDate, endDate);//12:摘要名(30)
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 品名_記事行1;// 17:品名 ○○○○様分(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}

		/// <summary>
		/// 売上データ記事行２ CSV文字列の取得
		/// 得意先No. XXXXXX
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="saleDate">売上日 </param>
		/// <param name="startDate">契約開始日</param>
		/// <param name="endDate">契約終了日</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle2(int no, string hanbaisakiCode, Date saleDate, DateTime? startDate, DateTime? endDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(startDate, endDate);//12:摘要名(30)
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 品名_記事行2;// 17:品名 得意先No. XXXXXX(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}
	}
}
