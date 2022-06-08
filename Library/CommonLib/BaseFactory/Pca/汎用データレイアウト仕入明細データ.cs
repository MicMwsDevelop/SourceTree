//
// 汎用データレイアウト仕入明細データ.cs
//
// PCA汎用データレイアウト 仕入明細データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(DX-Rev3.00)に対応(2022/05/25 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Pca
{
	/// <summary>
	/// PCA汎用データレイアウト仕入明細データ
	/// </summary>
	[Serializable]
	public class 汎用データレイアウト仕入明細データ
	{
		/// <summary>
		/// 1 0:通常仕入、1:製品完成
		/// </summary>
		public short 入荷方法 { get; set; }
		/// <summary>
		/// 2 0:仕入、1:仕入以外
		/// </summary>
		public short 科目区分 { get; set; }
		/// <summary>
		/// 3 0:掛買、1:現金、2:カード、3:そ の他、5:内製、6:契約
		/// </summary>
		public string 伝区 { get; set; }
		/// <summary>
		/// 4
		/// </summary>
		public int 仕入日 { get; set; }
		/// <summary>
		/// 5
		/// </summary>
		public int 精算日 { get; set; }
		/// <summary>
		/// 6
		/// </summary>
		public int 伝票No { get; set; }
		/// <summary>
		/// 7
		/// </summary>
		public string 仕入先コード { get; set; }
		/// <summary>
		/// 8
		/// </summary>
		public string 仕入先名 { get; set; }
		/// <summary>
		/// 9
		/// </summary>
		public string 先方担当者名 { get; set; }
		/// <summary>
		/// 10
		/// </summary>
		public string 部門コード { get; set; }
		/// <summary>
		/// 11
		/// </summary>
		public string 担当者コード { get; set; }
		/// <summary>
		/// 12
		/// </summary>
		public string 摘要コード { get; set; }
		/// <summary>
		/// 13
		/// </summary>
		public string 摘要名 { get; set; }
		/// <summary>
		/// 14
		/// </summary>
		public string 商品コード { get; set; }
		/// <summary>
		/// 15 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
		/// </summary>
		public short マスター区分 { get; set; }
		/// <summary>
		/// 16
		/// </summary>
		public string 商品名 { get; set; }
		/// <summary>
		/// 17 0:仕入、1:返品、2:単価訂正、9: 一般商品以外を示す
		/// </summary>
		public short 区 { get; set; }
		/// <summary>
		/// 18
		/// </summary>
		public string 倉庫コード { get; set; }
		/// <summary>
		/// 19
		/// </summary>
		public decimal 入数 { get; set; }
		/// <summary>
		/// 20
		/// </summary>
		public decimal 箱数 { get; set; }
		/// <summary>
		/// 21
		/// </summary>
		public decimal 数量 { get; set; }
		/// <summary>
		/// 22
		/// </summary>
		public string 単位 { get; set; }
		/// <summary>
		/// 23
		/// </summary>
		public decimal 単価 { get; set; }
		/// <summary>
		/// 24
		/// </summary>
		public decimal 金額 { get; set; }
		/// <summary>
		/// 25
		/// </summary>
		public decimal 外税額 { get; set; }
		/// <summary>
		/// 26
		/// </summary>
		public decimal 内税額 { get; set; }
		/// <summary>
		/// 27 0:非課税、1～9:会社基本情報の税 率ﾃｰﾌﾞﾙ
		/// </summary>
		public short 税区分 { get; set; }
		/// <summary>
		/// 28 0:税抜き、1:税込み
		/// </summary>
		public short 税込区分 { get; set; }
		/// <summary>
		/// 29
		/// </summary>
		public string 備考 { get; set; }
		/// <summary>
		/// 30
		/// </summary>
		public string 規格型番 { get; set; }
		/// <summary>
		/// 31
		/// </summary>
		public string 色 { get; set; }
		/// <summary>
		/// 32
		/// </summary>
		public string サイズ { get; set; }
		/// <summary>
		/// 33
		/// </summary>
		public int 計算式コード { get; set; }
		/// <summary>
		/// 34
		/// </summary>
		public int 商品項目1 { get; set; }
		/// <summary>
		/// 35
		/// </summary>
		public int 商品項目2 { get; set; }
		/// <summary>
		/// 36
		/// </summary>
		public int 商品項目3 { get; set; }
		/// <summary>
		/// 37
		/// </summary>
		public int 仕入項目1 { get; set; }
		/// <summary>
		/// 38
		/// </summary>
		public int 仕入項目2 { get; set; }
		/// <summary>
		/// 39
		/// </summary>
		public int 仕入項目3 { get; set; }
		/// <summary>
		/// 40
		/// </summary>
		public decimal 税率 { get; set; }
		/// <summary>
		/// 41
		/// </summary>
		public int 伝票消費税額 { get; set; }
		/// <summary>
		/// 42
		/// </summary>
		public string ﾌﾟﾛｼﾞｪｸﾄコード { get; set; }
		/// <summary>
		/// 43
		/// </summary>
		public string 伝票No2 { get; set; }
		/// <summary>
		/// 44
		/// </summary>
		public int データ区分 { get; set; }
		/// <summary>
		/// 45
		/// </summary>
		public string 商品名2 { get; set; }
		/// <summary>
		/// 46
		/// </summary>
		public int 単位区分 { get; set; }
		/// <summary>
		/// 47
		/// </summary>
		public string ロットNo { get; set; }
		/// <summary>
		/// 48
		/// </summary>
		public int ロット有効期限 { get; set; }
		/// <summary>
		/// 49
		/// </summary>
		public int 仕入税種別 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 汎用データレイアウト仕入明細データ()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		protected void Clear()
		{
			入荷方法 = 0;
			科目区分 = 0;
			伝区 = "0";
			仕入日 = 0;
			精算日 = 0;
			伝票No = 0;
			仕入先コード = string.Empty;
			仕入先名 = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			商品コード = string.Empty;
			マスター区分 = 0;
			商品名 = string.Empty;
			区 = 0;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			金額 = 0;
			外税額 = 0;
			内税額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = 0;
			商品項目1 = 0;
			商品項目2 = 0;
			商品項目3 = 0;
			仕入項目1 = 0;
			仕入項目2 = 0;
			仕入項目3 = 0;
			税率 = 0;
			伝票消費税額 = 0;
			ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
			伝票No2 = string.Empty;
			データ区分 = 0;
			商品名2 = string.Empty;
			単位区分 = 0;
			ロットNo = string.Empty;
			ロット有効期限 = 0;
			仕入税種別 = 0;
		}

		/// <summary>
		/// CSV文字列の取得(ダブルクォーテーションあり)
		/// </summary>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToCsvStringDoubleQuotation(int pcaVer)
		{
			// 汎用データレイアウト指定 7: Rev4.20
			List<string> list = new List<string>();
			/*01*/list.Add(入荷方法.ToString());
			/*02*/list.Add(科目区分.ToString());
			/*03*/list.Add(伝区);
			/*04*/list.Add("\"" + 仕入日.ToString() + "\"");
			/*05*/list.Add("\"" + 精算日.ToString() + "\"");
			/*06*/list.Add(伝票No.ToString());
			/*07*/list.Add("\"" + 仕入先コード + "\"");
			/*08*/list.Add("\"" + 仕入先名 + "\"");
			/*09*/list.Add("\"" + 先方担当者名 + "\"");
			/*10*/list.Add("\"" + 部門コード + "\"");
			/*11*/list.Add("\"" + 担当者コード + "\"");
			/*12*/list.Add(摘要コード);
			/*13*/list.Add("\"" + 摘要名 + "\"");
			/*14*/list.Add("\"" + 商品コード + "\"");
			/*15*/list.Add(マスター区分.ToString());
			/*16*/list.Add("\"" + 商品名 + "\"");
			/*17*/list.Add(区.ToString());
			/*18*/list.Add("\"" + 倉庫コード + "\"");
			/*19*/list.Add(((int)入数).ToString());
			/*20*/list.Add(((int)箱数).ToString());
			/*21*/list.Add(((int)数量).ToString());
			/*22*/list.Add("\"" + 単位 + "\"");
			/*23*/list.Add(((int)単価).ToString());
			/*24*/list.Add(((int)金額).ToString());
			/*25*/list.Add(((int)外税額).ToString());
			/*26*/list.Add(((int)内税額).ToString());
			/*27*/list.Add(税区分.ToString());
			/*28*/list.Add(税込区分.ToString());
			/*29*/list.Add("\"" + 備考 + "\"");
			/*30*/list.Add("\"" + 規格型番 + "\"");
			/*31*/list.Add("\"" + 色 + "\"");
			/*32*/list.Add("\"" + サイズ + "\"");
			/*33*/list.Add(計算式コード.ToString());
			/*34*/list.Add(商品項目1.ToString());
			/*35*/list.Add(商品項目2.ToString());
			/*36*/list.Add(商品項目2.ToString());
			/*37*/list.Add(仕入項目1.ToString());
			/*38*/list.Add(仕入項目2.ToString());
			/*39*/list.Add(仕入項目3.ToString());
			/*40*/list.Add(税率.ToString());
			/*41*/list.Add(伝票消費税額.ToString());
			/*42*/list.Add("\"" + ﾌﾟﾛｼﾞｪｸﾄコード + "\"");
			/*43*/list.Add("\"" + 伝票No2 + "\"");
			/*44*/list.Add(データ区分.ToString());
			/*45*/list.Add("\"" + 商品名2 + "\"");
			if (8 <= pcaVer)
			{
				// 汎用データレイアウト指定 8: Rev4.50
				/*46*/list.Add(単位区分.ToString());
				/*47*/list.Add("\"" + ロットNo + "\"");
				/*48*/list.Add(ロット有効期限.ToString());
			}
			// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(DX-Rev3.00)に対応(2022/05/25 勝呂)
			if (9 <= pcaVer)
			{
				// 汎用データレイアウト指定 9: DX-Rev3.00
				/*49*/list.Add(仕入税種別.ToString());
			}
			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// CSV文字列の取得(ダブルクォーテーションなし)
		/// </summary>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToCsvString(int pcaVer)
		{
			// 汎用データレイアウト指定 7: Rev4.20
			List<string> list = new List<string>();
			/*01*/list.Add(入荷方法.ToString());
			/*02*/list.Add(科目区分.ToString());
			/*03*/list.Add(伝区);
			/*04*/list.Add(仕入日.ToString());
			/*05*/list.Add(精算日.ToString());
			/*06*/list.Add(伝票No.ToString());
			/*07*/list.Add(仕入先コード);
			/*08*/list.Add(仕入先名);
			/*09*/list.Add(先方担当者名);
			/*10*/list.Add(部門コード);
			/*11*/list.Add(担当者コード);
			/*12*/list.Add(摘要コード);
			/*13*/list.Add(摘要名);
			/*14*/list.Add(商品コード);
			/*15*/list.Add(マスター区分.ToString());
			/*16*/list.Add(商品名);
			/*17*/list.Add(区.ToString());
			/*18*/list.Add(倉庫コード);
			/*19*/list.Add(((int)入数).ToString());
			/*20*/list.Add(((int)箱数).ToString());
			/*21*/list.Add(((int)数量).ToString());
			/*22*/list.Add(単位);
			/*23*/list.Add(((int)単価).ToString());
			/*24*/list.Add(((int)金額).ToString());
			/*25*/list.Add(((int)外税額).ToString());
			/*26*/list.Add(((int)内税額).ToString());
			/*27*/list.Add(税区分.ToString());
			/*28*/list.Add(税込区分.ToString());
			/*29*/list.Add(備考);
			/*30*/list.Add(規格型番);
			/*31*/list.Add(色);
			/*32*/list.Add(サイズ);
			/*33*/list.Add(計算式コード.ToString());
			/*34*/list.Add(商品項目1.ToString());
			/*35*/list.Add(商品項目2.ToString());
			/*36*/list.Add(商品項目2.ToString());
			/*37*/list.Add(仕入項目1.ToString());
			/*38*/list.Add(仕入項目2.ToString());
			/*39*/list.Add(仕入項目3.ToString());
			/*40*/list.Add(税率.ToString());
			/*41*/list.Add(伝票消費税額.ToString());
			/*42*/list.Add(ﾌﾟﾛｼﾞｪｸﾄコード);
			/*43*/list.Add(伝票No2);
			/*44*/list.Add(データ区分.ToString());
			/*45*/list.Add(商品名2);
			if (8 <= pcaVer)
			{
				// 汎用データレイアウト指定 8: Rev4.50
				/*46*/list.Add(単位区分.ToString());
				/*47*/list.Add(ロットNo);
				/*48*/list.Add(ロット有効期限.ToString());
			}
			// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(DX-Rev3.00)に対応(2022/05/25 勝呂)
			if (9 <= pcaVer)
			{
				// 汎用データレイアウト指定 9: DX-Rev3.00
				/*49*/list.Add(仕入税種別.ToString());
			}
			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<汎用データレイアウト仕入明細データ> DataTableToList(DataTable table)
		{
			List<汎用データレイアウト仕入明細データ> result = new List<汎用データレイアウト仕入明細データ>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					汎用データレイアウト仕入明細データ data = new 汎用データレイアウト仕入明細データ
					{
						入荷方法 = DataBaseValue.ConvObjectToShort(row["nykd_hoho"]),
						科目区分 = DataBaseValue.ConvObjectToShort(row["nykd_flid"]),
						伝区 = row["nykd_denku"].ToString().Trim(),
						仕入日 = DataBaseValue.ConvObjectToInt(row["nykd_uribi"]),
						精算日 = DataBaseValue.ConvObjectToInt(row["nykd_seibi"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["nykd_denno"]),
						仕入先コード = row["nykd_tcd"].ToString().Trim(),
						仕入先名 = row["rms_mei1"].ToString().Trim(),
						先方担当者名 = row["rms_tanmei"].ToString().Trim(),
						部門コード = row["nykd_jbmn"].ToString().Trim(),
						担当者コード = row["nykd_jtan"].ToString().Trim(),
						摘要コード = row["nykd_tekcd"].ToString().Trim(),
						摘要名 = row["nykd_tekmei"].ToString().Trim(),
						商品コード = row["nykd_scd"].ToString().Trim(),
						マスター区分 = DataBaseValue.ConvObjectToShort(row["nykd_mkbn"]),
						商品名 = row["nykd_mei"].ToString().Trim(),
						区 = DataBaseValue.ConvObjectToShort(row["nykd_ku"]),
						倉庫コード = row["nykd_souko"].ToString().Trim(),
						入数 = DataBaseValue.ConvObjectToDecimal(row["nykd_iri"]),
						箱数 = DataBaseValue.ConvObjectToDecimal(row["nykd_hako"]),
						数量 = DataBaseValue.ConvObjectToDecimal(row["nykd_suryo"]),
						単位 = row["nykd_tani"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToDecimal(row["nykd_tanka"]),
						金額 = DataBaseValue.ConvObjectToDecimal(row["nykd_kingaku"]),
						外税額 = DataBaseValue.ConvObjectToDecimal(row["nykd_zei"]),
						内税額 = DataBaseValue.ConvObjectToDecimal(row["nykd_uchi"]),
						税区分 = DataBaseValue.ConvObjectToShort(row["nykd_tax"]),
						税込区分 = DataBaseValue.ConvObjectToShort(row["nykd_komi"]),
						備考 = row["nykd_biko"].ToString().Trim(),
						税率 = DataBaseValue.ConvObjectToInt(row["nykd_rate"]),
					};
					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// 在庫単価不明リスト出力用テキストファイル 出力文字列
		/// </summary>
		/// <returns>出力文字列</returns>
		public string ToFumeiList()
		{
			//Print #2, Trim(Ostr(5)) & vbTab & Ostr(9) & vbTab & Trim(Ostr(13)) & vbTab & Trim(Ostr(20)) & vbTab & Trim(Ostr(15))
			List<string> buf = new List<string>();
			buf.Add(仕入日.ToString());
			buf.Add(部門コード);
			buf.Add(商品コード);
			buf.Add(((int)数量).ToString());
			buf.Add(商品名);
			return string.Join("\t", buf.ToArray());
		}
	}
}
