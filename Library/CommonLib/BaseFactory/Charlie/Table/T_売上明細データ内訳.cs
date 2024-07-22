//
// T_売上明細データ内訳.cs
//
// [charlieDB].[dbo].[T_売上明細データ内訳]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/06/27 勝呂):新規作成
//
using CommonLib.BaseFactory.Pca;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// T_売上明細データ内訳クラス
	/// </summary>
	public class T_売上明細データ内訳
	{
		public int 枝番 { get; set; }
		public int 伝区 { get; set; }
		public int 売上日 { get; set; }
		public int 請求日 { get; set; }
		public int 伝票No { get; set; }
		public string 得意先コード { get; set; }
		public string 得意先名 { get; set; }
		public string 直送先コード { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string 分類コード { get; set; }
		public string 伝票区分 { get; set; }
		public string 商品コード { get; set; }
		public int マスター区分 { get; set; }
		public string 商品名 { get; set; }
		public int 区 { get; set; }
		public string 倉庫コード { get; set; }
		public int 入数 { get; set; }
		public int 箱数 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 売上金額 { get; set; }
		public int 原単価 { get; set; }
		public int 原価金額 { get; set; }
		public int 粗利益 { get; set; }
		public int 外税額 { get; set; }
		public int 内税額 { get; set; }
		public int 税区分 { get; set; }
		public int 税込区分 { get; set; }
		public string 備考 { get; set; }
		public int 標準価格 { get; set; }
		public int 同時入荷区分 { get; set; }
		public int 売単価 { get; set; }
		public int 売価金額 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 計算式コード { get; set; }
		public int 商品項目1 { get; set; }
		public int 商品項目2 { get; set; }
		public int 商品項目3 { get; set; }
		public int 売上項目1 { get; set; }
		public int 売上項目2 { get; set; }
		public int 売上項目3 { get; set; }
		public int 税率 { get; set; }
		public int 伝票消費税額 { get; set; }
		public string ﾌﾟﾛｼﾞｪｸﾄコード { get; set; }
		public string 伝票No2 { get; set; }
		public int データ区分 { get; set; }
		public string 商品名2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_売上明細データ内訳()
		{
			枝番 = 0;
			伝区 = 0;
			売上日 = 0;
			請求日 = 0;
			伝票No = 0;
			得意先コード = string.Empty;
			得意先名 = string.Empty;
			直送先コード = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			分類コード = string.Empty;
			伝票区分 = string.Empty;
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
			売上金額 = 0;
			原単価 = 0;
			原価金額 = 0;
			粗利益 = 0;
			外税額 = 0;
			内税額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			標準価格 = 0;
			同時入荷区分 = 0;
			売単価 = 0;
			売価金額 = 0;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = 0;
			商品項目1 = 0;
			商品項目2 = 0;
			商品項目3 = 0;
			売上項目1 = 0;
			売上項目2 = 0;
			売上項目3 = 0;
			税率 = 0;
			伝票消費税額 = 0;
			ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
			伝票No2 = string.Empty;
			データ区分 = 0;
			商品名2 = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_売上明細データ内訳2]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_売上明細データ内訳</returns>
		public static List<T_売上明細データ内訳> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_売上明細データ内訳> result = new List<T_売上明細データ内訳>();
				foreach (DataRow row in table.Rows)
				{
					T_売上明細データ内訳 data = new T_売上明細データ内訳
					{
						枝番 = DataBaseValue.ConvObjectToInt(row["枝番"]),
						伝区 = DataBaseValue.ConvObjectToInt(row["伝区"]),
						売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]),
						請求日 = DataBaseValue.ConvObjectToInt(row["請求日"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						得意先コード = row["得意先コード"].ToString().Trim(),
						得意先名 = row["得意先名"].ToString().Trim(),
						直送先コード = row["直送先コード"].ToString().Trim(),
						先方担当者名 = row["先方担当者名"].ToString().Trim(),
						部門コード = row["部門コード"].ToString().Trim(),
						担当者コード = row["担当者コード"].ToString().Trim(),
						摘要コード = row["摘要コード"].ToString().Trim(),
						摘要名 = row["摘要名"].ToString().Trim(),
						分類コード = row["分類コード"].ToString().Trim(),
						伝票区分 = row["伝票区分"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						マスター区分 = DataBaseValue.ConvObjectToInt(row["マスター区分"]),
						商品名 = row["商品名"].ToString().Trim(),
						区 = DataBaseValue.ConvObjectToInt(row["区"]),
						倉庫コード = row["倉庫コード"].ToString().Trim(),
						入数 = DataBaseValue.ConvObjectToInt(row["入数"]),
						箱数 = DataBaseValue.ConvObjectToInt(row["箱数"]),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						単位 = row["単位"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToInt(row["単価"]),
						売上金額 = DataBaseValue.ConvObjectToInt(row["売上金額"]),
						原単価 = DataBaseValue.ConvObjectToInt(row["原単価"]),
						原価金額 = DataBaseValue.ConvObjectToInt(row["原価金額"]),
						粗利益 = DataBaseValue.ConvObjectToInt(row["粗利益"]),
						外税額 = DataBaseValue.ConvObjectToInt(row["外税額"]),
						内税額 = DataBaseValue.ConvObjectToInt(row["内税額"]),
						税区分 = DataBaseValue.ConvObjectToInt(row["税区分"]),
						税込区分 = DataBaseValue.ConvObjectToInt(row["税込区分"]),
						備考 = row["備考"].ToString().Trim(),
						標準価格 = DataBaseValue.ConvObjectToInt(row["標準価格"]),
						同時入荷区分 = DataBaseValue.ConvObjectToInt(row["同時入荷区分"]),
						売単価 = DataBaseValue.ConvObjectToInt(row["売単価"]),
						売価金額 = DataBaseValue.ConvObjectToInt(row["売価金額"]),
						規格型番 = row["規格型番"].ToString().Trim(),
						色 = row["色"].ToString().Trim(),
						サイズ = row["サイズ"].ToString().Trim(),
						計算式コード = DataBaseValue.ConvObjectToInt(row["計算式コード"]),
						商品項目1 = DataBaseValue.ConvObjectToInt(row["商品項目1"]),
						商品項目2 = DataBaseValue.ConvObjectToInt(row["商品項目2"]),
						商品項目3 = DataBaseValue.ConvObjectToInt(row["商品項目3"]),
						売上項目1 = DataBaseValue.ConvObjectToInt(row["売上項目1"]),
						売上項目2 = DataBaseValue.ConvObjectToInt(row["売上項目2"]),
						売上項目3 = DataBaseValue.ConvObjectToInt(row["売上項目3"]),
						税率 = DataBaseValue.ConvObjectToInt(row["税率"]),
						伝票消費税額 = DataBaseValue.ConvObjectToInt(row["伝票消費税額"]),
						ﾌﾟﾛｼﾞｪｸﾄコード = row["ﾌﾟﾛｼﾞｪｸﾄコード"].ToString().Trim(),
						伝票No2 = row["伝票No2"].ToString().Trim(),
						データ区分 = DataBaseValue.ConvObjectToInt(row["データ区分"]),
						商品名2 = row["商品名2"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 汎用データレイアウト売上明細データの設定
		/// </summary>
		/// <param name="eda">枝番</param>
		/// <param name="data">汎用データレイアウト売上明細データ</param>
		public void SetData(int eda, 汎用データレイアウト売上明細データ data)
		{
			枝番 = eda;
			伝区 = data.伝区;
			売上日 = data.売上日;
			請求日 = data.請求日;
			伝票No = data.伝票No;
			得意先コード = data.得意先コード;
			得意先名 = data.得意先名;
			直送先コード = data.直送先コード;
			先方担当者名 = data.先方担当者名;
			部門コード = data.部門コード;
			担当者コード = data.担当者コード;
			摘要コード = data.摘要コード;
			摘要名 = data.摘要名;
			分類コード = data.分類コード;
			伝票区分 = data.伝票区分;
			商品コード = data.商品コード;
			マスター区分 = data.マスター区分;
			商品名 = data.商品名;
			区 = data.区;
			倉庫コード = data.倉庫コード;
			入数 = data.入数;
			箱数 = data.箱数;
			数量 = data.数量;
			単位 = data.単位;
			単価 = data.単価;
			売上金額 = data.売上金額;
			原単価 = data.原単価;
			原価金額 = data.原価金額;
			粗利益 = data.粗利益;
			外税額 = data.外税額;
			内税額 = data.内税額;
			税区分 = data.税区分;
			税込区分 = data.税込区分;
			備考 = data.備考;
			標準価格 = data.標準価格;
			同時入荷区分 = data.同時入荷区分;
			売単価 = data.売単価;
			売価金額 = data.売価金額;
			規格型番 = data.規格型番;
			色 = data.色;
			サイズ = data.サイズ;
			計算式コード = data.計算式コード;
			商品項目1 = data.商品項目１;
			商品項目2 = data.商品項目２;
			商品項目3 = data.商品項目３;
			売上項目1 = data.売上項目１;
			売上項目2 = data.売上項目２;
			売上項目3 = data.売上項目３;
			税率 = data.税率;
			伝票消費税額 = data.伝票消費税額;
			ﾌﾟﾛｼﾞｪｸﾄコード = data.ﾌﾟﾛｼﾞｪｸﾄコード;
			伝票No2 = data.伝票No2;
			データ区分 = data.データ区分;
			商品名2 = data.商品名２;
		}

		/// <summary>
		/// CSV文字列の取得
		/// </summary>
		/// <returns>CSV文字列</returns>
		public string ToCsvString()
		{
			List<string> list = new List<string>();
			list.Add(枝番.ToString());
			list.Add(伝区.ToString());
			list.Add(売上日.ToString());
			list.Add(請求日.ToString());
			list.Add(伝票No.ToString());
			list.Add(得意先コード);
			list.Add(得意先名);
			list.Add(直送先コード);
			list.Add(先方担当者名);
			list.Add(部門コード);
			list.Add(担当者コード);
			list.Add(摘要コード);
			list.Add(摘要名);
			list.Add(分類コード);
			list.Add(伝票区分);
			list.Add(商品コード);
			list.Add(マスター区分.ToString());
			list.Add(商品名);
			list.Add(区.ToString());
			list.Add(倉庫コード);
			list.Add(入数.ToString());
			list.Add(箱数.ToString());
			list.Add(数量.ToString());
			list.Add(単位);
			list.Add(単価.ToString());
			list.Add(売上金額.ToString());
			list.Add(原単価.ToString());
			list.Add(原価金額.ToString());
			list.Add(粗利益.ToString());
			list.Add(外税額.ToString());
			list.Add(内税額.ToString());
			list.Add(税区分.ToString());
			list.Add(税込区分.ToString());
			list.Add(備考);
			list.Add(標準価格.ToString());
			list.Add(同時入荷区分.ToString());
			list.Add(売単価.ToString());
			list.Add(売価金額.ToString());
			list.Add(規格型番);
			list.Add(色);
			list.Add(サイズ);
			list.Add(計算式コード.ToString());
			list.Add(商品項目1.ToString());
			list.Add(商品項目2.ToString());
			list.Add(商品項目3.ToString());
			list.Add(売上項目1.ToString());
			list.Add(売上項目2.ToString());
			list.Add(売上項目3.ToString());
			list.Add(税率.ToString());
			list.Add(伝票消費税額.ToString());
			list.Add(ﾌﾟﾛｼﾞｪｸﾄコード);
			list.Add(伝票No2);
			list.Add(データ区分.ToString());
			list.Add(商品名2);
			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// タイトル行の取得
		/// </summary>
		/// <returns>タイトル行</returns>
		public static string ToTitle()
		{
			List<string> list = new List<string>();
			list.Add("枝番");
			list.Add("伝区");
			list.Add("売上日");
			list.Add("請求日");
			list.Add("伝票No");
			list.Add("得意先コード");
			list.Add("得意先名");
			list.Add("直送先コード");
			list.Add("先方担当者名");
			list.Add("部門コード");
			list.Add("担当者コード");
			list.Add("摘要コード");
			list.Add("摘要名");
			list.Add("分類コード");
			list.Add("伝票区分");
			list.Add("商品コード");
			list.Add("マスター区分");
			list.Add("商品名");
			list.Add("区");
			list.Add("倉庫コード");
			list.Add("入数");
			list.Add("箱数");
			list.Add("数量");
			list.Add("単位");
			list.Add("単価");
			list.Add("売上金額");
			list.Add("原単価");
			list.Add("原価金額");
			list.Add("粗利益");
			list.Add("外税額");
			list.Add("内税額");
			list.Add("税区分");
			list.Add("税込区分");
			list.Add("備考");
			list.Add("標準価格");
			list.Add("同時入荷区分");
			list.Add("売単価");
			list.Add("売価金額");
			list.Add("規格型番");
			list.Add("色");
			list.Add("サイズ");
			list.Add("計算式コード");
			list.Add("商品項目1");
			list.Add("商品項目2");
			list.Add("商品項目3");
			list.Add("売上項目1");
			list.Add("売上項目2");
			list.Add("売上項目3");
			list.Add("税率");
			list.Add("伝票消費税額");
			list.Add("ﾌﾟﾛｼﾞｪｸﾄコード");
			list.Add("伝票No2");
			list.Add("データ区分");
			list.Add("商品名2");
			return ";" + String.Join(",", list.ToArray());
		}
	}
}
