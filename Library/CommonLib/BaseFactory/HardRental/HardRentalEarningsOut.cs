//
// HardSubscriptEarningsOut.cs
//
// ハードレンタル売上データクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2025/04/15 勝呂):新規作成
// 
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.HardRental
{
	public class HardRentalEarningsOut
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 得意先コード
		/// </summary>
		public string 得意先コード { get; set; }

		/// <summary>
		/// 請求先コード
		/// </summary>
		public string 請求先コード { get; set; }

		/// <summary>
		/// PCA部門コード
		/// </summary>
		public short? PCA部門コード { get; set; }

		/// <summary>
		/// PCA倉庫コード
		/// </summary>
		public short? PCA倉庫コード { get; set; }

		/// <summary>
		/// PCA担当者コード
		/// </summary>
		public string PCA担当者コード { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string 商品コード { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string 商品名 { get; set; }

		/// <summary>
		/// 標準価格
		/// </summary>
		public int 標準価格 { get; set; }

		/// <summary>
		/// 原単価
		/// </summary>
		public int 原単価 { get; set; }

		/// <summary>
		/// 単位
		/// </summary>
		public string 単位 { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool 終了フラグ { get; set; }

		/// <summary>
		/// 内部契約番号
		/// </summary>
		public int 内部契約番号 { get; set; }

		/// <summary>
		/// 契約番号
		/// </summary>
		public string 契約番号 { get; set; }

		/// <summary>
		/// 契約日
		/// </summary>
		public DateTime? 契約日 { get; set; }

		/// <summary>
		/// 月額利用料
		/// </summary>
		public int 月額利用料 { get; set; }

		/// <summary>
		/// 利用月数
		/// </summary>
		public short 利用月数 { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime?  利用開始日 { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? 利用終了日 { get; set; }

		/// <summary>
		/// 課金開始日
		/// </summary>
		public DateTime? 課金開始日 { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public DateTime? 課金終了日 { get; set; }

		/// <summary>
		/// 解約日
		/// </summary>
		public DateTime? 解約日 { get; set; }

		/// <summary>
		/// サービス終了フラグ
		/// </summary>
		public bool サービス終了フラグ { get; set; }

		/// <summary>
		/// 記事行１ 品名の取得
		/// ○○○○様分
		/// </summary>
		public string 記事行1_品名
		{
			get
			{
				return string.Format("{0}様分", StringUtil.GetSubstringByByte(顧客名, 32));
			}
		}

		/// <summary>
		/// 記事行２ 品名の取得
		/// 得意先No. XXXXXX
		/// </summary>
		public string 記事行2_品名
		{
			get
			{
				return string.Format("得意先No.{0}", 得意先コード);
			}
		}

		/// <summary>
		/// 利用期間文字列の取得
		/// </summary>
		public string 利用期間
		{
			get
			{
				if (利用開始日.HasValue && 利用終了日.HasValue)
				{
					return string.Format("{0}年{1}月～{2}年{3}月", 利用開始日.Value.Year, 利用開始日.Value.Month, 利用終了日.Value.Year, 利用終了日.Value.Month);
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public HardRentalEarningsOut()
		{
			顧客No = 0;
			顧客名 = string.Empty;
			得意先コード = string.Empty;
			請求先コード = string.Empty;
			PCA部門コード = null;
			PCA倉庫コード = null;
			PCA担当者コード = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			標準価格 = 0;
			原単価 = 0;
			単位 = string.Empty;
			終了フラグ = false;
			内部契約番号 = 0;
			契約番号 = string.Empty;
			契約日 = null;
			月額利用料 = 0;
			利用月数 = 0;
			利用開始日 = null;
			利用終了日 = null;
			課金開始日 = null;
			課金終了日 = null;
			解約日 = null;
			サービス終了フラグ = false;
		}

		/// <summary>
		/// 商品名の取得
		/// </summary>
		/// <returns>商品名</returns>
		public string GetGoodsName()
		{
			return StringUtil.GetSubstringByByte(商品名, 36);
		}

		/// <summary>
		/// 摘要名の取得
		/// ｢利用年月分｣
		/// </summary>
		/// <returns>摘要名</returns>
		public string 摘要名(YearMonth ym)
		{
			return string.Format("{0}年{1}月分", ym.Year, ym.Month);
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<HardRentalEarningsOut> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<HardRentalEarningsOut> result = new List<HardRentalEarningsOut>();
				foreach (DataRow row in table.Rows)
				{
					HardRentalEarningsOut data = new HardRentalEarningsOut();
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.顧客名 = row["顧客名"].ToString().Trim();
					data.得意先コード = row["得意先コード"].ToString().Trim();
					data.請求先コード = row["請求先コード"].ToString().Trim();
					data.PCA部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["PCA部門コード"]);
					data.PCA倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["PCA倉庫コード"]);
					data.PCA担当者コード = row["PCA担当者コード"].ToString().Trim();
					data.商品コード = row["商品コード"].ToString().Trim();
					data.商品名 = row["商品名"].ToString().Trim();
					data.標準価格 = DataBaseValue.ConvObjectToInt(row["標準価格"]);
					data.原単価 = DataBaseValue.ConvObjectToInt(row["原単価"]);
					data.単位 = row["単位"].ToString().Trim();
					data.終了フラグ = DataBaseValue.ConvObjectToBool(row["終了フラグ"]);
					data.内部契約番号 = DataBaseValue.ConvObjectToInt(row["内部契約番号"]);
					data.契約番号 = row["契約番号"].ToString().Trim();
					data.契約日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約日"]);
					data.月額利用料 = DataBaseValue.ConvObjectToInt(row["月額利用料"]);
					data.利用月数 = DataBaseValue.ConvObjectToShort(row["利用月数"]);
					data.利用開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["利用開始日"]);
					data.利用終了日 = DataBaseValue.ConvObjectToDateTimeNull(row["利用終了日"]);
					data.課金開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["課金開始日"]);
					data.課金終了日 = DataBaseValue.ConvObjectToDateTimeNull(row["課金終了日"]);
					data.解約日 = DataBaseValue.ConvObjectToDateTimeNull(row["解約日"]);
					data.サービス終了フラグ = DataBaseValue.ConvObjectToBool(row["サービス終了フラグ"]);
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
		/// <param name="billingDate">請求日</param>
		/// <param name="useDate">利用日</param>
		/// <param name="tax">税率</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToEarnings(int no, string hanbaisakiCode, Date saleDate, Date billingDate, Date useDate, int tax, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(useDate.ToYearMonth());//12:摘要名(30)｢利用年月分｣
			pca.商品コード = 商品コード;// 15:商品コード(13)
			pca.マスター区分 = 0;// 16:マスタ区分
			pca.商品名 = GetGoodsName();// 17:品名(36)
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
		/// <param name="billingDate">請求日</param>
		/// <param name="useDate">利用日</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle1(int no, string hanbaisakiCode, Date saleDate, Date billingDate, Date useDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = billingDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(saleDate.ToYearMonth());//12:摘要名(30)｢利用年月分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行1_品名;// 17:品名 ○○○○様分(36)
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
		/// <param name="billingDate">請求日</param>
		/// <param name="useDate">利用日</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle2(int no, string hanbaisakiCode, Date saleDate, Date billingDate, Date useDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = billingDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(useDate.ToYearMonth());//12:摘要名(30)｢利用年月分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行2_品名;// 17:品名 得意先No. XXXXXX(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}
	}
}
