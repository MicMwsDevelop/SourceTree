//
// OnlineDemandEarningsOut.cs
// 
// 各種作業料売上データクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/12/01 勝呂):新規作成
//
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.OnlineDemand
{
	/// <summary>
	/// 各種作業料売上データクラス
	/// </summary>
	public class OnlineDemandEarningsOut
	{
		public int 受付No { get; set; }

		public DateTime? 申請日時 { get; set; }

		public int 顧客No { get; set; }

		public string 顧客名 { get; set; }

		public string 得意先コード { get; set; }

		public string 請求先コード { get; set; }

		public string 商品コード { get; set; }

		public string 商品名 { get; set; }

		public int 標準価格 { get; set; }

		public int 原単価 { get; set; }

		public string 単位 { get; set; }

		public short? PCA部門コード { get; set; }

		public short? PCA倉庫コード { get; set; }

		public string PCA担当者コード { get; set; }

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
		/// 摘要名の取得
		/// yyyy年MM月dd日 オンライン請求作業
		/// </summary>
		public string 摘要名(Date date)
		{
			return string.Format("{0} オンライン請求作業", date.GetNormalString());
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineDemandEarningsOut()
		{
			受付No = 0;
			申請日時 = null;
			顧客No = 0;
			顧客名 = string.Empty;
			得意先コード = string.Empty;
			請求先コード = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			標準価格 = 0;
			原単価 = 0;
			単位 = string.Empty;
			PCA部門コード = null;
			PCA倉庫コード = null;
			PCA担当者コード = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<OnlineDemandEarningsOut> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<OnlineDemandEarningsOut> result = new List<OnlineDemandEarningsOut>();
				foreach (DataRow row in table.Rows)
				{
					OnlineDemandEarningsOut data = new OnlineDemandEarningsOut();
					data.受付No = DataBaseValue.ConvObjectToInt(row["受付No"]);
					data.申請日時 = DataBaseValue.ConvObjectToDateTimeNull(row["申請日時"]);
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.顧客名 = row["顧客名"].ToString().Trim();
					data.得意先コード = row["得意先コード"].ToString().Trim();
					data.請求先コード = row["請求先コード"].ToString().Trim();
					data.商品コード = row["商品コード"].ToString().Trim();
					data.商品名 = row["商品名"].ToString().Trim();
					data.標準価格 = DataBaseValue.ConvObjectToInt(row["標準価格"]);
					data.原単価 = DataBaseValue.ConvObjectToInt(row["原単価"]);
					data.単位 = row["単位"].ToString().Trim();
					data.PCA部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["PCA部門コード"]);
					data.PCA倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["PCA倉庫コード"]);
					data.PCA担当者コード = row["PCA担当者コード"].ToString().Trim();
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 売上明細データCSV文字列の取得
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="requestDate">申請日 </param>
		/// <param name="tax">税率</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToEarnings(int no, string hanbaisakiCode, Date requestDate, int tax, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = requestDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = requestDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(requestDate);//12:摘要名(30)｢yyyy年MM月dd日 オンライン請求作業｣
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
		/// 売上明細データ記事行１ CSV文字列の取得
		/// ○○○○様分
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="requestDate">申請日 </param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle1(int no, string hanbaisakiCode, Date requestDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = requestDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = requestDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(requestDate);//12:摘要名(30)｢yyyy年MM月dd日 オンライン請求作業｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行1_品名;// 17:品名 ○○○○様分(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}

		/// <summary>
		/// 売上明細データ記事行２ CSV文字列の取得
		/// 得意先No. XXXXXX
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="requestDate">申請日 </param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle2(int no, string hanbaisakiCode, Date requestDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = requestDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = requestDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = PCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = PCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(requestDate);//12:摘要名(30)｢yyyy年MM月dd日 オンライン請求作業｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行2_品名;// 17:品名 得意先No. XXXXXX(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}
	}
}
