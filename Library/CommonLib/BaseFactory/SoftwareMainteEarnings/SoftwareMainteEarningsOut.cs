//
// SoftwareMainteEarningsOut.cs
//
// ソフトウェア保守料売上出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;

namespace CommonLib.BaseFactory.SoftwareMainteEarnings
{
	public class SoftwareMainteEarningsOut
	{
		/// <summary>
		/// vMic全ユーザー2 顧客No
		/// </summary>
		public int f顧客No { get; set; }

		/// <summary>
		/// vMic全ユーザー2 顧客名
		/// </summary>
		public string f顧客名 { get; set; }

		/// <summary>
		/// vMic全ユーザー2 得意先コード
		/// </summary>
		public string f得意先コード { get; set; }

		/// <summary>
		/// vMic全ユーザー2 請求先コード
		/// </summary>
		public string f請求先コード { get; set; }

		/// <summary>
		/// tMih支店情報 fPca部門コード
		/// </summary>
		public short? fPca部門コード { get; set; }

		/// <summary>
		/// tMih支店情報 fPca担当者コード
		/// </summary>
		public string fPca担当者コード { get; set; }

		/// <summary>
		/// tMih支店情報 fPca倉庫コード
		/// </summary>
		public short? fPca倉庫コード { get; set; }

		/// <summary>
		/// PCA商品マスタ
		/// </summary>
		public string f商品コード { get; set; }

		/// <summary>
		/// PCA商品マスタ
		/// </summary>
		public string f商品名 { get; set; }

		/// <summary>
		/// PCA商品マスタ
		/// </summary>
		public int f標準価格 { get; set; }

		/// <summary>
		/// PCA商品マスタ
		/// </summary>
		public int f原単価 { get; set; }

		/// <summary>
		/// PCA商品マスタ
		/// </summary>
		public string f単位 { get; set; }

		/// <summary>
		/// 利用期間
		/// </summary>
		public Span f利用期間 { get; set; }

		/// <summary>
		/// 摘要名の取得
		/// ｢利用年月分｣
		/// </summary>
		public string 摘要名
		{
			get
			{
				if (null != f利用期間)
				{
					return string.Format("{0}年{1}月～{2}年{3}月", f利用期間.Start.Year, f利用期間.Start.Month, f利用期間.End.Year, f利用期間.End.Month);
				}
				return string.Empty;
			}
		}

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
		/// デフォルトコンストラクタ
		/// </summary>
		public SoftwareMainteEarningsOut()
		{
			f顧客No = 0;
			f顧客名 = string.Empty;
			f得意先コード = string.Empty;
			f請求先コード = string.Empty;
			fPca部門コード = null;
			fPca担当者コード = string.Empty;
			fPca倉庫コード = null;
			f商品コード = string.Empty;
			f商品名 = string.Empty;
			f標準価格 = 0;
			f原単価 = 0;
			f単位 = string.Empty;
			f利用期間 = null;
		}

		/// <summary>
		/// 利用期間を１年後に設定
		/// </summary>
		/// <param name="end">終了日</param>
		public void Set利用期間(Date end)
		{
			// 開始：翌月初日、終了：１年後の当月末日
			f利用期間 = new Span(end.FirstDayOfNextMonth(), end.PlusYears(1).LastDayOfTheMonth());
		}

		/// <summary>
		/// ソフトウェア保守料売上データCSV文字列の取得
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="saleDate">売上日 </param>
		/// <param name="tax">税率</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToEarnings(int no, string hanbaisakiCode, Date saleDate, int tax, int pcaVer)
		{
			PCA売上明細汎用データ pca = new PCA売上明細汎用データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = fPca部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPca担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名;//12:摘要名(30)｢利用年月分｣
			pca.商品コード = f商品コード;// 15:商品コード(13)
			pca.マスター区分 = 0;// 16:マスタ区分
			pca.商品名 = 商品名;// 17:品名(36)
			pca.倉庫コード = fPca倉庫コード.Value.ToString();// 19:倉庫コード(6)
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
			/*
						sPCAデータ = """" & "0" & """"                                    '1伝区
						sPCAデータ = sPCAデータ & "," & """" & sSaleDate & """"           '2売上年月日
						sPCAデータ = sPCAデータ & "," & """" & sAccountDate & """"        '3請求年月日
						sPCAデータ = sPCAデータ & "," & """" & s伝票番号 & """"           '4伝票番号
						sPCAデータ = sPCAデータ & "," & """" & s売上得意先コード & """"   '5得意先コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '6得意先名
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '7直送先コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '8先方担当者名
						sPCAデータ = sPCAデータ & "," & """" & s売上部門 & """"           '9部門コード
						sPCAデータ = sPCAデータ & "," & """" & s売上担当 & """"           '10担当者コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '11摘要コード
						sPCAデータ = sPCAデータ & "," & """" & s摘要名 & """"             '12摘要名
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '13分類コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '14伝票区分
						sPCAデータ = sPCAデータ & "," & """" & s商品コード & """"         '15商品コード
						sPCAデータ = sPCAデータ & "," & """" & sマスタ区分 & """"         '16マスタ区分
						sPCAデータ = sPCAデータ & "," & """" & s商品名 & """"             '17品名
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '18区
						sPCAデータ = sPCAデータ & "," & """" & s売上倉庫 & """"           '19倉庫コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '20入数
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '21箱数
						sPCAデータ = sPCAデータ & "," & """" & "1" & """"                 '22数量
						sPCAデータ = sPCAデータ & "," & """" & s単位 & """"               '23単位
						sPCAデータ = sPCAデータ & "," & """" & CStr(l売上単価) & """"     '24単価
						sPCAデータ = sPCAデータ & "," & """" & CStr(l売上金額) & """"     '25売上金額
						sPCAデータ = sPCAデータ & "," & """" & CStr(d原単価) & """"       '26原単価
						sPCAデータ = sPCAデータ & "," & """" & CStr(d原価額) & """"       '27原価額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '28粗利額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '29外税額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '30内税額
						sPCAデータ = sPCAデータ & "," & """" & "2" & """"                 '31税区分
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '32税込区分
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '33備考
						sPCAデータ = sPCAデータ & "," & """" & CStr(l売上単価) & """"     '34標準価格
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '35同時入荷区分
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '36売単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '37売価金額
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '38規格・型番
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '39色
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '40サイズ
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '41計算式コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '42商品項目1
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '43商品項目2
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '44商品項目3
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '45売上項目1
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '46売上項目2
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '47売上項目3
						sPCAデータ = sPCAデータ & "," & """" & CStr(i消費税率) & """"     '48税率
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '49伝票消費税（外税）
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '50プロジェクトコード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '51伝票No２
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"                 '52データ区分
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                  '53商品名２
			*/
		}

		/// <summary>
		/// 売上データ記事行１ CSV文字列の取得
		/// ○○○○様分
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="saleDate">売上日 </param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle1(int no, string hanbaisakiCode, Date saleDate, int pcaVer)
		{
			PCA売上明細汎用データ pca = new PCA売上明細汎用データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = fPca部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPca担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名;//12:摘要名(30)｢利用年月分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行1_品名;// 17:品名 ○○○○様分(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
			/*
						sPCAデータ = """" & "0" & """"                                  '1伝区
						sPCAデータ = sPCAデータ & "," & """" & sSaleDate & """"         '2売上年月日
						sPCAデータ = sPCAデータ & "," & """" & sAccountDate & """"      '3請求年月日
						sPCAデータ = sPCAデータ & "," & """" & s伝票番号 & """"         '4伝票番号
						sPCAデータ = sPCAデータ & "," & """" & s売上得意先コード & """" '5得意先コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '6得意先名
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '7直送先コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '8先方担当者名
						sPCAデータ = sPCAデータ & "," & """" & s売上部門 & """"         '9部門コード
						sPCAデータ = sPCAデータ & "," & """" & s売上担当 & """"         '10担当者コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '11摘要コード
						sPCAデータ = sPCAデータ & "," & """" & s摘要名 & """"           '12摘要名
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '13分類コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '14伝票区分
						sPCAデータ = sPCAデータ & "," & """" & s商品コード & """"       '15商品コード
						sPCAデータ = sPCAデータ & "," & """" & sマスタ区分 & """"       '16マスタ区分
						sPCAデータ = sPCAデータ & "," & """" & LeftB(sユーザー名, 32) & "様分" & """"           '17品名
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '18区
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '19倉庫コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '20入数
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '21箱数
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '22数量
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '23単位
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '24単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '25売上金額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '26原単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '27原価額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '28粗利額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '29外税額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '30内税額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '31税区分
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '32税込区分
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '33備考
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '34標準価格
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '35同時入荷区分
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '36売単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '37売価金額
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '38規格・型番
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '39色
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '40サイズ
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '41計算式コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '42商品項目1
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '43商品項目2
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '44商品項目3
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '45売上項目1
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '46売上項目2
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '47売上項目3
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '48税率
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '49伝票消費税（外税）
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '50プロジェクトコード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '51伝票No２
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '52データ区分
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '53商品名２
			*/
		}

		/// <summary>
		/// 売上データ記事行２ CSV文字列の取得
		/// 得意先No. XXXXXX
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="hanbaisakiCode">販売先コード</param>
		/// <param name="saleDate">売上日 </param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToArticle2(int no, string hanbaisakiCode, Date saleDate, int pcaVer)
		{
			PCA売上明細汎用データ pca = new PCA売上明細汎用データ();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = hanbaisakiCode;// 5:得意先コード(13)
			pca.部門コード = fPca部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPca担当者コード;// 10:担当者コード(13)
			pca.摘要コード = "0";// 11:摘要コード(6)
			pca.摘要名 = 摘要名;//12:摘要名(30)｢利用年月分｣
			pca.商品コード = PcaGoodsIDDefine.ArticleCode;// 15:000014(13) 
			pca.マスター区分 = 4;// 16:マスタ区分
			pca.商品名 = 記事行2_品名;// 17:品名 得意先No. XXXXXX(36)
			pca.倉庫コード = "0";// 19:倉庫コード(6)
			return pca.ToCsvString(pcaVer);
			/*
						sPCAデータ = """" & "0" & """"                                  '1伝区
						sPCAデータ = sPCAデータ & "," & """" & sSaleDate & """"         '2売上年月日
						sPCAデータ = sPCAデータ & "," & """" & sAccountDate & """"      '3請求年月日
						sPCAデータ = sPCAデータ & "," & """" & s伝票番号 & """"         '4伝票番号
						sPCAデータ = sPCAデータ & "," & """" & s売上得意先コード & """" '5得意先コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '6得意先名
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '7直送先コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '8先方担当者名
						sPCAデータ = sPCAデータ & "," & """" & s売上部門 & """"         '9部門コード
						sPCAデータ = sPCAデータ & "," & """" & s売上担当 & """"         '10担当者コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '11摘要コード
						sPCAデータ = sPCAデータ & "," & """" & s摘要名 & """"           '12摘要名
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '13分類コード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '14伝票区分
						sPCAデータ = sPCAデータ & "," & """" & s商品コード & """"       '15商品コード
						sPCAデータ = sPCAデータ & "," & """" & sマスタ区分 & """"       '16マスタ区分
						sPCAデータ = sPCAデータ & "," & """" & "得意先No. " & sユーザー得意先コード & """"           '17品名
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '18区
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '19倉庫コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '20入数
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '21箱数
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '22数量
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '23単位
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '24単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '25売上金額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '26原単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '27原価額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '28粗利額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '29外税額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '30内税額
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '31税区分
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '32税込区分
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '33備考
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '34標準価格
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '35同時入荷区分
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '36売単価
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '37売価金額
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '38規格・型番
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '39色
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '40サイズ
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '41計算式コード
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '42商品項目1
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '43商品項目2
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '44商品項目3
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '45売上項目1
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '46売上項目2
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '47売上項目3
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '48税率
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '49伝票消費税（外税）
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '50プロジェクトコード
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '51伝票No２
						sPCAデータ = sPCAデータ & "," & """" & "0" & """"               '52データ区分
						sPCAデータ = sPCAデータ & "," & """" & "" & """"                '53商品名２
			*/
		}
	}
}
