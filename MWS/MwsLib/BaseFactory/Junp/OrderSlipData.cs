//
// OrderSlipData.cs
//
// 受注伝票情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.Common;
using System;
using System.Collections.Generic;

namespace MwsLib.BaseFactory.Junp
{
	/// <summary>
	/// 受注伝票情報
	/// </summary>
	public class OrderSlipData
	{
		/// <summary>
		/// 伝票種別定義
		/// </summary>
		public enum OrderType
		{
			/// <summary>
			/// なし
			/// </summary>
			None,

			/// <summary>
			/// palette ES
			/// </summary>
			PaletteES,

			/// <summary>
			/// palette ES ソフトウェア保守料
			/// </summary>
			MainteES,

			/// <summary>
			/// PC安心サポート
			/// </summary>
			PcSupport,

			/// <summary>
			/// おまとめプラン
			/// </summary>
			Matome,
		}

		/// <summary>
		/// 判定結果
		/// </summary>
		public bool Result { get; set; }

		/// <summary>
		/// 伝票種別
		/// </summary>
		public OrderType Type { get; set; }

		/// <summary>
		/// 販売種別
		/// </summary>
		public MwsDefine.ApplyType 販売種別 { get; set; }

		/// <summary>
		/// 受注番号
		/// </summary>
		public int 受注番号 { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public DateTime? 受注日 { get; set; }

		/// <summary>
		/// 受注承認日
		/// </summary>
		public DateTime? 受注承認日 { get; set; }

		/// <summary>
		/// 売上承認日
		/// </summary>
		public DateTime? 売上承認日 { get; set; }

		/// <summary>
		/// 納期
		/// </summary>
		public Date? 納期 { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string 商品コード { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string 商品名 { get; set; }

		/// <summary>
		/// 受注金額
		/// </summary>
		public int 受注金額 { get; set; }

		/// <summary>
		/// 利用期間
		/// </summary>
		public Span 利用期間 { get; set; }

		/// <summary>
		/// 販売先コード
		/// </summary>
		public int 販売先コード { get; set; }

		/// <summary>
		/// 販売先
		/// </summary>
		public string 販売先 { get; set; }

		/// <summary>
		/// 支店コード
		/// </summary>
		public string 支店コード { get; set; }

		/// <summary>
		/// 担当支店名
		/// </summary>
		public string 担当支店名 { get; set; }

		/// <summary>
		/// 担当者コード
		/// </summary>
		public string 担当者コード { get; set; }

		/// <summary>
		/// 担当者名
		/// </summary>
		public string 担当者名 { get; set; }

		/// <summary>
		/// 件名
		/// </summary>
		public string 件名 { get; set; }

		/// <summary>
		/// エラー情報
		/// </summary>
		public List<string> ErrorList { get; set; }

		/// <summary>
		/// 受注日文字列の取得
		/// </summary>
		public string 受注日Str
		{
			get
			{
				if (受注日.HasValue)
				{
					return new Date(受注日.Value.Year, 受注日.Value.Month, 受注日.Value.Day).ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// 受注承認日文字列の取得
		/// </summary>
		public string 受注承認日Str
		{
			get
			{
				if (受注承認日.HasValue)
				{
					return new Date(受注承認日.Value.Year, 受注承認日.Value.Month, 受注承認日.Value.Day).ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// 受注承認日文字列の取得
		/// </summary>
		public string 売上承認日Str
		{
			get
			{
				if (売上承認日.HasValue)
				{
					return new Date(売上承認日.Value.Year, 売上承認日.Value.Month, 売上承認日.Value.Day).ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OrderSlipData()
		{
			Result = false;
			Type = OrderType.None;
			販売種別 = MwsDefine.ApplyType.Etc;
			受注番号 = 0;
			受注日 = null;
			受注承認日 = null;
			売上承認日 = null;
			納期 = null;
			顧客No = 0;
			顧客名 = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			受注金額 = 0;
			利用期間 = Span.Nothing;
			販売先コード = 0;
			販売先 = string.Empty;
			支店コード = string.Empty;
			担当支店名 = string.Empty;
			担当者コード = string.Empty;
			担当者名 = string.Empty;
			件名 = string.Empty;
			ErrorList = new List<string>();
		}

		/// <summary>
		/// ListView設定値の取得
		/// </summary>
		/// <returns>ListView設定値</returns>
		public string[] GetListViewItem()
		{
			string[] item = new string[16];
			item[0] = 0 < ErrorList.Count ? "×" : "〇";
			item[1] = OrderSlipData.OrderTypeString[Type];
			item[2] = MwsDefine.ApplyTypeString[販売種別];
			item[3] = 受注番号.ToString();
			item[4] = 受注日.HasValue ? 受注日.Value.ToDate().ToString() : "";
			item[5] = 受注承認日.HasValue ? 受注承認日.Value.ToDate().ToString() : "";
			item[6] = 売上承認日.HasValue ? 売上承認日.Value.ToDate().ToString() : "";
			item[7] = 納期.HasValue ? 納期.ToString() : "";
			item[8] = 顧客No.ToString();
			item[9] = 顧客名;
			item[10] = 商品名;
			item[11] = Span.Nothing == 利用期間 ? "" : 利用期間.Start.ToString();
			item[12] = Span.Nothing == 利用期間 ? "" : 利用期間.End.ToString();
			item[13] = 担当支店名;
			item[14] = 担当者名;
			item[15] = 件名;
			return item;
		}

		static public string[] GetExcelTitle()
		{
			string[] title = new string[23];
			title[0] = "判定";
			title[1] = "伝票種別";
			title[2] = "販売種別";
			title[3] = "受注番号";
			title[4] = "受注日";
			title[5] = "受注承認日";
			title[6] = "売上承認日";
			title[7] = "納期";
			title[8] = "顧客No";
			title[9] = "顧客名";
			title[10] = "商品コード";
			title[11] = "商品名";
			title[12] = "受注金額";
			title[13] = "利用開始日";
			title[14] = "利用終了日";
			title[15] = "販売先コード";
			title[16] = "販売先";
			title[17] = "支店コード";
			title[18] = "担当支店名";
			title[19] = "担当者コード";
			title[20] = "担当者名";
			title[21] = "件名";
			title[22] = "不備内容";
			return title;
		}


		/// <summary>
		/// Excel出力行の取得
		/// </summary>
		/// <returns>ListView設定値</returns>
		public List<string> GetExcelRow()
		{
			List<string> list = new List<string>();
			list.Add(0 < ErrorList.Count ? "×": "〇");
			list.Add(OrderSlipData.OrderTypeString[Type]);
			list.Add(MwsDefine.ApplyTypeString[販売種別]);
			list.Add(受注番号.ToString());
			list.Add(受注日.HasValue ? 受注日.Value.ToDate().ToString() : "");
			list.Add(受注承認日.HasValue ? 受注承認日.Value.ToDate().ToString() : "");
			list.Add(売上承認日.HasValue ? 売上承認日.Value.ToDate().ToString() : "");
			list.Add(納期.HasValue ? 納期.ToString() : "");
			list.Add(顧客No.ToString());
			list.Add(顧客名);
			list.Add(商品コード);
			list.Add(商品名);
			list.Add(受注金額.ToString());
			list.Add(Span.Nothing == 利用期間 ? "" : 利用期間.Start.ToString());
			list.Add(Span.Nothing == 利用期間 ? "" : 利用期間.End.ToString());
			list.Add(販売先コード.ToString());
			list.Add(販売先);
			list.Add(支店コード);
			list.Add(担当支店名);
			list.Add(担当者コード);
			list.Add(担当者名);
			list.Add(件名);
			list.AddRange(ErrorList);
			return list;
		}

		/// <summary>
		/// 商品コードから伝票種別を格納
		/// </summary>
		public static OrderType GetOrderType(string goodID)
		{
			switch (goodID)
			{
				case PcaGoodsIDDefine.PaletteES_2019:
					return OrderType.PaletteES;
				case PcaGoodsIDDefine.PaletteES_Mainte72:
				case PcaGoodsIDDefine.PaletteES_Mainte12:
					return OrderType.MainteES;
				case PcaGoodsIDDefine.PcSafetySupport3:
				case PcaGoodsIDDefine.PcSafetySupport1:
					return OrderType.PcSupport;
				case PcaGoodsIDDefine.Matome12:
				case PcaGoodsIDDefine.Matome24:
				case PcaGoodsIDDefine.Matome36:
				case PcaGoodsIDDefine.Matome48:
				case PcaGoodsIDDefine.Matome60:
					return OrderType.Matome;
			}
			return OrderType.None;
		}

		/// <summary>
		/// 申込種別/販売種別文字列
		/// </summary>
		public static readonly EnumDictionary<OrderType, string> OrderTypeString = new EnumDictionary<OrderType, string>()
		{
			{ OrderType.None, "なし" },
			{ OrderType.PaletteES, "ES" },
			{ OrderType.MainteES, "保守料" },
			{ OrderType.PcSupport, "PC安心" },
			{ OrderType.Matome, "まとめ" },
		};

		/// <summary>
		/// ログ出力文字列の取得
		/// </summary>
		/// <returns>ログ出力文字列</returns>
		public string GetLogString()
		{
			List<string> list = new List<string>();
			list.AddRange(this.GetListViewItem());
			list.RemoveAt(0);
			return string.Join(", ", list.ToArray());
		}

		/// <summary>
		/// palette ESのみ抽出
		/// </summary>
		/// <param name="list"></param>
		/// <returns>結果</returns>
		public static List<OrderSlipData> SelectPaletteES(List<OrderSlipData> list)
		{
			return list.FindAll(p => PcaGoodsIDDefine.PaletteES_2019 == p.商品コード);
		}

		/// <summary>
		/// 同伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
		/// </summary>
		/// <param name="list"></param>
		/// <param name="target"></param>
		/// <returns>結果</returns>
		public static OrderSlipData GetSameMainte72(List<OrderSlipData> list, OrderSlipData target)
		{
			if (PcaGoodsIDDefine.PaletteES_2019 == target.商品コード)
			{
				return list.Find(p => target.受注番号 == p.受注番号 && PcaGoodsIDDefine.PaletteES_Mainte72 == p.商品コード);
			}
			return null;
		}

		/// <summary>
		/// 別伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
		/// </summary>
		/// <param name="list"></param>
		/// <param name="target"></param>
		/// <returns>結果</returns>
		public static OrderSlipData GetAnotherMainte72(List<OrderSlipData> list, OrderSlipData target)
		{
			if (PcaGoodsIDDefine.PaletteES_2019 == target.商品コード)
			{
				return list.Find(p => target.顧客No == p.顧客No && PcaGoodsIDDefine.PaletteES_Mainte72 == p.商品コード);
			}
			return null;
		}

		/// <summary>
		/// 別伝票にｿﾌﾄｳｪｱ保守料12ケ月が存在するか？
		/// </summary>
		/// <param name="list"></param>
		/// <param name="target"></param>
		/// <returns>結果</returns>
		public static OrderSlipData GetAnotherMainte12(List<OrderSlipData> list, OrderSlipData target)
		{
			if (PcaGoodsIDDefine.PaletteES_2019 == target.商品コード)
			{
				return list.Find(p => target.顧客No == p.顧客No && PcaGoodsIDDefine.PaletteES_Mainte12 == p.商品コード);
			}
			return null;
		}

		/// <summary>
		/// PC安心サポートのみ抽出
		/// </summary>
		/// <param name="list"></param>
		/// <returns>結果</returns>
		public static List<OrderSlipData> SelectPcSupport(List<OrderSlipData> list)
		{
			return list.FindAll(p => PcaGoodsIDDefine.PcSafetySupport3 == p.商品コード || PcaGoodsIDDefine.PcSafetySupport1 == p.商品コード);
		}

		/// <summary>
		/// おまとめプランのみ抽出
		/// </summary>
		/// <param name="list"></param>
		/// <returns>結果</returns>
		public static List<OrderSlipData> SelectMatome(List<OrderSlipData> list)
		{
			return list.FindAll(p => PcaGoodsIDDefine.Matome12 == p.商品コード || PcaGoodsIDDefine.Matome24 == p.商品コード || PcaGoodsIDDefine.Matome36 == p.商品コード || PcaGoodsIDDefine.Matome48 == p.商品コード || PcaGoodsIDDefine.Matome60 == p.商品コード);
		}
	}
}
