//
// PCA仕入明細.cs
//
// PCA汎用データ 仕入明細データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Pca
{
	/// <summary>
	/// PCA汎用データ 仕入明細データ
	/// </summary>
	[Serializable]
	public class PCA仕入明細
	{
		/// <summary>
		/// 0:通常仕入、1:製品完成
		/// </summary>
		public short 入荷方法 { get; set; }
		/// <summary>
		/// 0:仕入、1:仕入以外
		/// </summary>
		public short 科目区分 { get; set; }
		/// <summary>
		/// 0:掛買、1:現金、2:カード、3:そ の他、5:内製、6:契約
		/// </summary>
		public string 伝区 { get; set; }
		public int 仕入日 { get; set; }
		public int 精算日 { get; set; }
		public int 伝票No { get; set; }
		public string 仕入先コード { get; set; }
		public string 仕入先名 { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string 商品コード { get; set; }
		/// <summary>
		/// 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
		/// </summary>
		public short マスター区分 { get; set; }
		public string 商品名 { get; set; }
		/// <summary>
		/// 0:仕入、1:返品、2:単価訂正、9: 一般商品以外を示す
		/// </summary>
		public short 区 { get; set; }
		public string 倉庫コード { get; set; }
		public decimal 入数 { get; set; }
		public decimal 箱数 { get; set; }
		public decimal 数量 { get; set; }
		public string 単位 { get; set; }
		public decimal 単価 { get; set; }
		public decimal 金額 { get; set; }
		public decimal 外税額 { get; set; }
		public decimal 内税額 { get; set; }
		/// <summary>
		/// 0:非課税、1～9:会社基本情報の税 率ﾃｰﾌﾞﾙ
		/// </summary>
		public short 税区分 { get; set; }
		/// <summary>
		/// 0:税抜き、1:税込み
		/// </summary>
		public short 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 計算式コード { get; set; }
		public int 商品項目1 { get; set; }
		public int 商品項目2 { get; set; }
		public int 商品項目3 { get; set; }
		public int 仕入項目1 { get; set; }
		public int 仕入項目2 { get; set; }
		public int 仕入項目3 { get; set; }
		public decimal 税率 { get; set; }
		public int 伝票消費税額 { get; set; }
		public string ﾌﾟﾛｼﾞｪｸﾄコード { get; set; }
		public string 伝票No2 { get; set; }
		public int データ区分 { get; set; }
		public string 商品名2 { get; set; }
		public int 単位区分 { get; set; }
		public string ロットNo { get; set; }
		public int ロット有効期限 { get; set; }
		public int 仕入税種別 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA仕入明細()
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
		/// CSV文字列の取得
		/// </summary>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToCsvString(int pcaVer)
		{
			List<string> list = new List<string>();
			list.Add(入荷方法.ToString());
			list.Add(科目区分.ToString());
			list.Add(伝区);
			list.Add(仕入日.ToString());
			list.Add(精算日.ToString());
			list.Add(伝票No.ToString());
			list.Add(仕入先コード);
			list.Add(仕入先名);
			list.Add(先方担当者名);
			list.Add(部門コード);
			list.Add(担当者コード);
			list.Add(摘要コード);
			list.Add(摘要名);
			list.Add(商品コード);
			list.Add(マスター区分.ToString());
			list.Add(商品名);
			list.Add(区.ToString());
			list.Add(倉庫コード);
			list.Add(((int)入数).ToString());
			list.Add(((int)箱数).ToString());
			list.Add(((int)数量).ToString());
			list.Add(単位);
			list.Add(((int)単価).ToString());
			list.Add(((int)金額).ToString());
			list.Add(((int)外税額).ToString());
			list.Add(((int)内税額).ToString());
			list.Add(税区分.ToString());
			list.Add(税込区分.ToString());
			list.Add(備考);
			list.Add(規格型番);
			list.Add(色);
			list.Add(サイズ);
			list.Add(計算式コード.ToString());
			list.Add(商品項目1.ToString());
			list.Add(商品項目2.ToString());
			list.Add(商品項目2.ToString());
			list.Add(仕入項目1.ToString());
			list.Add(仕入項目2.ToString());
			list.Add(仕入項目3.ToString());
			list.Add(税率.ToString());
			list.Add(伝票消費税額.ToString());
			list.Add(ﾌﾟﾛｼﾞｪｸﾄコード);
			list.Add(伝票No2);
			list.Add(データ区分.ToString());
			list.Add(商品名2);
			if (8 == pcaVer)
			{
				// 汎用データレイアウト指定 7: Rev4.20、8: Rev4.50
				list.Add(単位区分.ToString());
				list.Add(ロットNo);
				list.Add(ロット有効期限.ToString());
				list.Add(仕入税種別.ToString());
			}
			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<PCA仕入明細> DataTableToList(DataTable table)
		{
			List<PCA仕入明細> result = new List<PCA仕入明細>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					PCA仕入明細 data = new PCA仕入明細
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
