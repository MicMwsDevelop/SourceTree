//
// OrderVoucher.cs
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

namespace MwsLib.BaseFactory.CheckVoucherPaletteES
{
	/// <summary>
	/// 受注伝票情報
	/// </summary>
	public class OrderVoucher
	{
		/// <summary>
		/// 判定結果
		/// </summary>
		public bool Result { get; set; }

		/// <summary>
		/// 受注番号
		/// </summary>
		public int 受注番号 { get; set; }

		/// <summary>
		/// 年度
		/// </summary>
		public int 年度 { get; set; }

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
		/// 販売種別
		/// </summary>
		public MwsDefine.ApplyType 販売種別 { get; set; }

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
		public OrderVoucher()
		{
			Result = false;
			受注番号 = 0;
			年度 = 0;
			受注日 = null;
			受注承認日 = null;
			売上承認日 = null;
			納期 = null;
			販売種別 = MwsDefine.ApplyType.Etc;
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
			string[] item = new string[17];
			item[0] = 0 < ErrorList.Count ? "×" : "〇";
			item[1] = 受注番号.ToString();
			item[2] = 受注日.HasValue ? 受注日.Value.ToDate().ToString() : "";
			item[3] = 受注承認日.HasValue ? 受注承認日.Value.ToDate().ToString() : "";
			item[4] = 売上承認日.HasValue ? 売上承認日.Value.ToDate().ToString() : "";
			item[5] = 納期.HasValue ? 納期.ToString() : "";
			item[6] = MwsDefine.ApplyTypeString[販売種別];
			item[7] = 顧客No.ToString();
			item[8] = 顧客名;
			item[9] = 商品名;
			item[10] = Span.Nothing == 利用期間 ? "" : 利用期間.Start.ToString();
			item[11] = Span.Nothing == 利用期間 ? "" : 利用期間.End.ToString();
			item[12] = 販売先コード.ToString();
			item[13] = 販売先;
			item[14] = 担当支店名;
			item[15] = 担当者名;
			item[16] = 件名;

			return item;
		}

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
		/// paletteESの販売種別がＶＰかどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsFormalApplyTypeByPaletteES()
		{
			if (PcaGoodsIDDefine.PaletteES_2019 == 商品コード)
			{
				if (MwsDefine.ApplyType.ValuePack == 販売種別)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金かどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsFormalApplyTypeByMainte72()
		{
			if (PcaGoodsIDDefine.PaletteES_Mainte72 == 商品コード)
			{
				if (MwsDefine.ApplyType.Monthly == 販売種別)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金かどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsFormalApplyTypeByMainte12()
		{
			if (PcaGoodsIDDefine.PaletteES_Mainte12 == 商品コード)
			{
				if (MwsDefine.ApplyType.Monthly == 販売種別)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// palette ESの利用期間が72ヵ月かどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsFormalSpanByPaletteES()
		{
			if (Span.Nothing != 利用期間)
			{
				if (PcaGoodsIDDefine.PaletteES_2019 == 商品コード)
				{
					if (72 == 利用期間.GetMonthCount())
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月かどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsFormalSpanByMainte72()
		{
			if (Span.Nothing != 利用期間)
			{
				if (PcaGoodsIDDefine.PaletteES_Mainte72 == 商品コード)
				{
					if (72 == 利用期間.GetMonthCount())
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月かどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsFormalSpanByMainte12()
		{
			if (Span.Nothing != 利用期間)
			{
				if (PcaGoodsIDDefine.PaletteES_Mainte12 == 商品コード)
				{
					if (12 == 利用期間.GetMonthCount())
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// palette ESのみ抽出
		/// </summary>
		/// <param name="list"></param>
		/// <returns>結果</returns>
		public static List<OrderVoucher> GetAllPaletteES(List<OrderVoucher> list)
		{
			return list.FindAll(p => PcaGoodsIDDefine.PaletteES_2019 == p.商品コード);
		}

		/// <summary>
		/// 同伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
		/// </summary>
		/// <param name="list"></param>
		/// <param name="target"></param>
		/// <returns>結果</returns>
		public static OrderVoucher GetSameVoucherMaite72(List<OrderVoucher> list, OrderVoucher target)
		{
			if (PcaGoodsIDDefine.PaletteES_2019 == target.商品コード)
			{
				return list.Find(p => target.年度 == p.年度 && target.受注番号 == p.受注番号 && PcaGoodsIDDefine.PaletteES_Mainte72 == p.商品コード);
			}
			return null;
		}

		/// <summary>
		/// 別伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
		/// </summary>
		/// <param name="list"></param>
		/// <param name="target"></param>
		/// <returns>結果</returns>
		public static OrderVoucher GetAnotherVoucherMaite72(List<OrderVoucher> list, OrderVoucher target)
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
		public static OrderVoucher GetAnotherVoucherMaite12(List<OrderVoucher> list, OrderVoucher target)
		{
			if (PcaGoodsIDDefine.PaletteES_2019 == target.商品コード)
			{
				return list.Find(p => target.顧客No == p.顧客No && PcaGoodsIDDefine.PaletteES_Mainte12 == p.商品コード);
			}
			return null;
		}
	}
}
