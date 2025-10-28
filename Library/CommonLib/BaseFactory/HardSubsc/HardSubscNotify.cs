//
// HardSubscNotify.cs
//
// ハードサブスク利用期限通知クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2025/04/15 勝呂):新規作成
// 
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.HardSubsc
{
	public class HardSubscNotify
	{
		/// <summary>
		/// 内部契約番号
		/// </summary>
		public int 内部契約番号 { get; set; }

		/// <summary>
		/// 契約番号
		/// </summary>
		public string 契約番号 { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public DateTime? 受注日 { get; set; }

		/// <summary>
		/// 月額利用料
		/// </summary>
		public int 月額利用料 { get; set; }

		/// <summary>
		/// 利用月数
		/// </summary>
		public short 利用月数 { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public DateTime? 契約開始日 { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public DateTime? 契約終了日 { get; set; }

		/// <summary>
		/// 解約日
		/// </summary>
		public DateTime? 解約日 { get; set; }

		/// <summary>
		/// サービス終了フラグ
		/// </summary>
		public bool サービス終了フラグ { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string 電話番号 { get; set; }

		/// <summary>
		/// 得意先コード
		/// </summary>
		public string 得意先コード { get; set; }

		/// <summary>
		/// 請求先コード
		/// </summary>
		public string 請求先コード { get; set; }

		/// <summary>
		/// 請求先顧客No
		/// </summary>
		public string 請求先No { get; set; }

		/// <summary>
		/// 請求先名
		/// </summary>
		public string 請求先名 { get; set; }

		/// <summary>
		/// 支店コード
		/// </summary>
		public string 支店コード { get; set; }

		/// <summary>
		/// オフィス名
		/// </summary>
		public string オフィス名 { get; set; }

		/// <summary>
		/// オフィスメールアドレス
		/// </summary>
		public string オフィスメールアドレス { get; set; }

		/// <summary>
		/// 契約期間文字列の取得
		/// </summary>
		public string 契約期間
		{
			get
			{
				if (契約開始日.HasValue && 契約終了日.HasValue)
				{
					return string.Format("{0}年{1}月～{2}年{3}月", 契約開始日.Value.Year, 契約開始日.Value.Month, 契約終了日.Value.Year, 契約終了日.Value.Month);
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public HardSubscNotify()
		{
			内部契約番号 = 0;
			契約番号 = string.Empty;
			顧客No = 0;
			受注日 = null;
			月額利用料 = 0;
			利用月数 = 0;
			契約開始日 = null;
			契約終了日 = null;
			解約日 = null;
			サービス終了フラグ = false;
			顧客名 = string.Empty;
			電話番号 = string.Empty;
			得意先コード = string.Empty;
			請求先コード = string.Empty;
			請求先No = string.Empty;
			請求先名 = string.Empty;
			支店コード = string.Empty;
			オフィス名 = string.Empty;
			オフィスメールアドレス = string.Empty;
		}

		/// <summary>
		/// 利用期限日の取得
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>利用期限日</returns>
		public static Date GetLimitDate(Date date)
		{
			// 当日から半年後の末日
			// 2027/12/1→2028/5/31
			return date.PlusMonths(5).LastDayOfTheMonth();
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>HardSubscNotify</returns>
		public static List<HardSubscNotify> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<HardSubscNotify> result = new List<HardSubscNotify>();
				foreach (DataRow row in table.Rows)
				{
					HardSubscNotify data = new HardSubscNotify
					{
						内部契約番号 = DataBaseValue.ConvObjectToInt(row["内部契約番号"]),
						契約番号 = row["契約番号"].ToString().Trim(),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						受注日 = DataBaseValue.ConvObjectToDateTimeNull(row["受注日"]),
						月額利用料 = DataBaseValue.ConvObjectToInt(row["月額利用料"]),
						利用月数 = DataBaseValue.ConvObjectToShort(row["利用月数"]),
						契約開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約開始日"]),
						契約終了日 = DataBaseValue.ConvObjectToDateTimeNull(row["契約終了日"]),
						解約日 = DataBaseValue.ConvObjectToDateTimeNull(row["解約日"]),
						サービス終了フラグ = ("0" == row["サービス終了フラグ"].ToString()) ? false : true,
						顧客名 = row["顧客名"].ToString().Trim(),
						電話番号 = row["電話番号"].ToString().Trim(),
						得意先コード = row["得意先コード"].ToString().Trim(),
						請求先コード = row["請求先コード"].ToString().Trim(),
						請求先No = row["請求先No"].ToString().Trim(),
						請求先名 = row["請求先名"].ToString().Trim(),
						支店コード = row["支店コード"].ToString().Trim(),
						オフィス名 = row["オフィス名"].ToString().Trim(),
						オフィスメールアドレス = row["オフィスメールアドレス"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
