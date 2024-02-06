//
// OnlineLicenseMainteEarningsOut.cs
//
// オンライン資格保守サービス売上情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/23 勝呂):新規作成
// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
// 
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.OnlineLicenseMainte
{
	public class OnlineLicenseMainteEarningsOut
	{
		public int f顧客No { get; set; }

		public string f顧客名 { get; set; }

		public string f得意先コード { get; set; }

		public string f請求先コード { get; set; }

		public YearMonth? f保守開始月 { get; set; }

		public YearMonth? f保守終了月 { get; set; }

		public string fアプリケーションNo { get; set; }

		public string fアプリケーション名 { get; set; }

		public string fcm名称 { get; set; }

		public string f更新単位 { get; set; }

		public string f商品コード { get; set; }

		public string f商品名 { get; set; }

		public int f標準価格 { get; set; }

		public int f原単価 { get; set; }

		public string f単位 { get; set; }

		public short? fPCA部門コード { get; set; }

		public short? fPCA倉庫コード { get; set; }

		public string fPCA担当者コード { get; set; }

		public bool f終了フラグ { get; set; }

		/// <summary>
		/// 商品名の取得
		/// </summary>
		public string 商品名
		{
			get
			{
				return StringUtil.GetSubstringByByte(f商品名, 36);
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
				return string.Format("{0}様分", StringUtil.GetSubstringByByte(f顧客名, 32));
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
				return string.Format("得意先No.{0}", f得意先コード);
			}
		}

		/// <summary>
		/// 摘要名の取得
		/// ｢利用年月分｣
		/// </summary>
		public string 摘要名(YearMonth ym)
		{
			// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
			//return string.Format("{0}年{1}月分", ym.Year, ym.Month);
			return string.Format("{0}年{1}月更新分", ym.Year, ym.Month);
		}

		/// <summary>
		/// 最終徴収年月の取得
		/// 保守開始月から４年後
		/// </summary>
		public YearMonth? 最終徴収年月
		{
			get
			{
				if (f保守開始月.HasValue)
				{
					return f保守開始月.Value.PlusYears(4);
				}
				return null;
			}
		}

		/// <summary>
		/// 最終徴収年月かどうか？
		/// 保守開始月から４年後かどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool Is最終徴収年月
		{
			get
			{
				if (f保守終了月.HasValue)
				{
					return (f保守終了月.Value == 最終徴収年月) ? true : false;
				}
				return false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineLicenseMainteEarningsOut()
		{
			f顧客No = 0;
			f顧客名 = string.Empty;
			f得意先コード = string.Empty;
			f請求先コード = string.Empty;
			f保守開始月 = null;
			f保守終了月 = null;
			fアプリケーションNo = string.Empty;
			fアプリケーション名 = string.Empty;
			fcm名称 = string.Empty;
			f更新単位 = string.Empty;
			f商品コード = string.Empty;
			f商品名 = string.Empty;
			f標準価格 = 0;
			f原単価 = 0;
			f単位 = string.Empty;
			fPCA部門コード = null;
			fPCA倉庫コード = null;
			fPCA担当者コード = string.Empty;
			f終了フラグ = false;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<OnlineLicenseMainteEarningsOut> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<OnlineLicenseMainteEarningsOut> result = new List<OnlineLicenseMainteEarningsOut>();
				foreach (DataRow row in table.Rows)
				{
					OnlineLicenseMainteEarningsOut data = new OnlineLicenseMainteEarningsOut();
					data.f顧客No = DataBaseValue.ConvObjectToInt(row["f顧客No"]);
					data.f顧客名 = row["f顧客名"].ToString().Trim();
					data.f得意先コード = row["f得意先コード"].ToString().Trim();
					data.f請求先コード = row["f請求先コード"].ToString().Trim();
					string startYM = row["f保守開始月"].ToString().Trim();
					string endYM = row["f保守終了月"].ToString().Trim();
					YearMonth ym;
					if (YearMonth.TryParse(startYM, out ym))
					{
						data.f保守開始月 = ym;
					}
					if (YearMonth.TryParse(endYM, out ym))
					{
						data.f保守終了月 = ym;
					}
					data.fアプリケーションNo = row["fアプリケーションNo"].ToString().Trim();
					data.fアプリケーション名 = row["fアプリケーション名"].ToString().Trim();
					data.fcm名称 = row["fcm名称"].ToString().Trim();
					data.f更新単位 = row["f更新単位"].ToString().Trim();
					data.f商品コード = row["f商品コード"].ToString().Trim();
					data.f商品名 = row["f商品名"].ToString().Trim();
					data.f標準価格 = DataBaseValue.ConvObjectToInt(row["f標準価格"]);
					data.f原単価 = DataBaseValue.ConvObjectToInt(row["f原単価"]);
					data.f単位 = row["f単位"].ToString().Trim();
					data.fPCA部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPCA部門コード"]);
					data.fPCA倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPCA倉庫コード"]);
					data.fPCA担当者コード = row["fPCA担当者コード"].ToString().Trim();

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
		/// <param name="tekiyoDate">摘要利用年月日（ yyyy年MM月更新分）</param>
		/// <param name="tax">税率</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToEarnings(int no, string hanbaisakiCode, Date saleDate, Date tekiyoDate, int tax, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = fPCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(tekiyoDate.ToYearMonth());//12:摘要名(30)｢利用年月分｣
			pca.商品コード = f商品コード;// 15:商品コード(13)
			pca.マスター区分 = 0;// 16:マスタ区分
			pca.商品名 = 商品名;// 17:品名(36)
			pca.倉庫コード = fPCA倉庫コード.Value.ToString();// 19:倉庫コード(6)
			pca.数量 = 1;// 22:数量
			pca.単位 = f単位;// 23:単位
			pca.単価 = f標準価格;// 24:単価
			pca.売上金額 = f標準価格;// 25:売上金額
			pca.原単価 = f原単価;// 26:原単価
			pca.原価金額 = f原単価;// 27:原価額
			pca.税区分 = 2;// 31:税区分
			pca.税込区分 = 0;// 32:税込区分
			pca.標準価格 = f標準価格;// 34:標準価格
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
		/// <param name="tekiyoDate">摘要利用年月日（ yyyy年MM月更新分）</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle1(int no, string hanbaisakiCode, Date saleDate, Date tekiyoDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = fPCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(tekiyoDate.ToYearMonth());//12:摘要名(30)｢利用年月分｣
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
		/// <param name="tekiyoDate">摘要利用年月日（ yyyy年MM月更新分）</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle2(int no, string hanbaisakiCode, Date saleDate, Date tekiyoDate, int pcaVer)
		{
			汎用データレイアウト売上明細データ pca = new 汎用データレイアウト売上明細データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = fPCA部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPCA担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名(tekiyoDate.ToYearMonth());//12:摘要名(30)｢利用年月分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行2_品名;// 17:品名 得意先No. XXXXXX(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
		}
	}
}
