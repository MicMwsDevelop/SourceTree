//
// Prescription.cs
// 
// 電子処方箋契約情報 売上明細データ出力情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.PrescriptionManager
{
	public class PrescriptionEarnings
	{
		public int 申込No { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public string 得意先コード { get; set; }
		public string 請求先コード { get; set; }
		public string 請求先名 { get; set; }
		public DateTime? 運用開始日 { get; set; }
		public DateTime? 契約開始日 { get; set; }
		public DateTime? 契約終了日 { get; set; }
		public string 更新単位 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public int 標準価格 { get; set; }
		public int 原単価 { get; set; }
		public string 単位 { get; set; }
		public short 部門コード { get; set; }
		public short 倉庫コード { get; set; }
		public string 担当者コード { get; set; }

		/// <summary>
		/// 契約期間の取得
		/// </summary>
		public Span 契約期間
		{
			get
			{
				if (契約開始日.HasValue && 契約終了日.HasValue)
				{
					return new Span(契約開始日.Value.ToDate(), 契約終了日.Value.ToDate());
				}
				return null;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PrescriptionEarnings()
		{
			申込No = 0;
			顧客No = 0;
			顧客名 = string.Empty;
			得意先コード = string.Empty;
			請求先コード = string.Empty;
			請求先名 = string.Empty;
			運用開始日 = null;
			契約開始日 = null;
			契約終了日 = null;
			更新単位 = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			標準価格 = 0;
			原単価 = 0;
			単位 = string.Empty;
			部門コード = 0;
			倉庫コード = 0;
			担当者コード = string.Empty;
		}

		/// <summary>
		/// 摘要名の取得
		/// ｢yyyy年mm月～yyyy年mm月利用分｣
		/// </summary>
		public string 摘要名取得(Span span)
		{
			return string.Format("{0}年{1}月～{2}年{3}月利用分", span.Start.Year, span.Start.Month, span.End.Year, span.End.Month);
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<PrescriptionEarnings> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<PrescriptionEarnings> result = new List<PrescriptionEarnings>();
				foreach (DataRow row in table.Rows)
				{
					PrescriptionEarnings data = new PrescriptionEarnings();
					data.申込No = DataBaseValue.ConvObjectToInt(row["申込No"]);
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.顧客名 = row["顧客名"].ToString().Trim();
					data.得意先コード = row["得意先コード"].ToString().Trim();
					data.請求先コード = row["請求先コード"].ToString().Trim();
					data.請求先名 = row["請求先名"].ToString().Trim();
					data.運用開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["運用開始日"]);
					data.契約開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約開始日"]);
					data.契約終了日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約終了日"]);
					data.更新単位 = row["更新単位"].ToString().Trim();
					data.商品コード = row["商品コード"].ToString().Trim();
					data.商品名 = row["商品名"].ToString().Trim();
					data.標準価格 = DataBaseValue.ConvObjectToInt(row["標準価格"]);
					data.原単価 = DataBaseValue.ConvObjectToInt(row["原単価"]);
					data.単位 = row["単位"].ToString().Trim();
					data.部門コード = DataBaseValue.ConvObjectToShort(row["部門コード"]);
					data.倉庫コード = DataBaseValue.ConvObjectToShort(row["倉庫コード"]);
					data.担当者コード = row["担当者コード"].ToString().Trim();

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
		/// <param name="hanbaisakiName">販売先名</param>
		/// <param name="saleDate">売上日（当月初日）</param>
		/// <param name="term">契約期間</param>
		/// <param name="tax">税率</param>
		/// <param name="pcaVer">PCAバージョン情報</param>
		/// <returns>CSV文字列</returns>
		public string ToEarnings(int no, string hanbaisakiCode, string hanbaisakiName, Date saleDate, Span term, int tax, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.得意先名 = hanbaisakiName; // 6:得意先名(40)
			pca.部門コード = 部門コード.ToString();// 9:部門コード(6)
			pca.担当者コード = 担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名取得(term);//12:摘要名(30)｢｢yyyy年mm月～yyyy年mm月利用分｣
			pca.商品コード = 商品コード;// 15:商品コード(13)
			pca.マスター区分 = 0;// 16:マスタ区分
			pca.商品名 = StringUtil.GetSubstringByByte(商品名, 36);	// 17:品名(36)
			pca.倉庫コード = 倉庫コード.ToString();// 19:倉庫コード(6)
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
		/// <param name="saleDate">売上日（当月初日）</param>
		/// <param name="term">契約期間</param>
		/// <param name="pcaVer">PCAバージョン情報</param>
		/// <returns>CSV文字列</returns>
		public string ToEarningsArticle1(int no, Date saleDate, Span term, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = 請求先コード;// 5:得意先コード(13)
			pca.部門コード = 部門コード.ToString();// 9:部門コード(6)
			pca.担当者コード = 担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名取得(term);//12:摘要名(30)｢｢yyyy年mm月～yyyy年mm月利用分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = string.Format("{0}様分", StringUtil.GetSubstringByByte(顧客名, 32));// 17:品名(36) ○○○○様分
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}

		/// <summary>
		/// 売上明細データ記事行２ CSV文字列の取得
		/// 得意先No.XXXXXX
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="saleDate">売上日（当月初日）</param>
		/// <param name="term">契約期間</param>
		/// <param name="pcaVer">PCAバージョン情報</param>
		/// <returns>CSV文字列</returns>
		public string ToEarningsArticle2(int no, Date saleDate, Span term, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = 請求先コード;// 5:得意先コード(13)
			pca.部門コード = 部門コード.ToString();// 9:部門コード(6)
			pca.担当者コード = 担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名取得(term);//12:摘要名(30)｢｢yyyy年mm月～yyyy年mm月利用分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = string.Format("得意先No.{0}", 得意先コード);// 17:品名(36) 得意先No. XXXXXX
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}
	}
}
