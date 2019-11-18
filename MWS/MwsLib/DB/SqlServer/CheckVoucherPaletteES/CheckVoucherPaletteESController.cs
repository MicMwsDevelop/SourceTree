//
// CheckVoucherPaletteESController.cs
//
// 受注伝票情報 データ詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.CheckVoucherPaletteES;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CheckVoucherPaletteES
{
	public static class CheckVoucherPaletteESController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注伝票情報リストの詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>終了ユーザー情報リスト</returns>
		public static List<OrderVoucher> ConvertOrderVoucherList(DataTable table)
		{
			List<OrderVoucher> result = null;
			if (null != table)
			{
				result = new List<OrderVoucher>();
				foreach (DataRow row in table.Rows)
				{
					OrderVoucher data = new OrderVoucher
					{
						受注番号 = DataBaseValue.ConvObjectToInt(row["f受注番号"]),
						年度 = DataBaseValue.ConvObjectToInt(row["f年度"]),
						受注日 = DataBaseValue.ConvObjectToDateTimeNull(row["f受注日"]),
						受注承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["f受注承認日"]),
						売上承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["f売上承認日"]),
						販売種別 = (MwsDefine.ApplyType)DataBaseValue.ConvObjectToInt(row["f販売種別"]),
						販売先コード = DataBaseValue.ConvObjectToInt(row["f販売先コード"]),
						販売先 = row["f販売先"].ToString(),
						顧客No = DataBaseValue.ConvObjectToInt(row["fユーザーコード"]),
						顧客名 = row["fユーザー"].ToString(),
						商品コード = row["f商品コード"].ToString(),
						商品名 = row["f商品名"].ToString(),
						受注金額 = DataBaseValue.ConvObjectToInt(row["f受注金額"]),
						支店コード = row["fBshCode3"].ToString(),
						担当支店名 = row["f担当支店名"].ToString(),
						担当者コード = row["f担当者コード"].ToString(),
						担当者名 = row["f担当者名"].ToString()
					};
					string buf = row["f納期"].ToString();
					if (null != buf && 10 == buf.Length)
					{
						data.納期 = Date.Parse(buf);
					}
					Date start = Span.Nothing.Start;
					buf = row["fSV利用開始年月"].ToString();
					if (null != buf && 7 == buf.Length)
					{
						start = YearMonth.Parse(buf).ToDate(1);
					}
					Date end = Span.Nothing.End;
					buf = row["fSV利用終了年月"].ToString();
					if (null != buf && 7 == buf.Length)
					{
						end = YearMonth.Parse(buf).ToDate(-1);
					}
					data.利用期間 = new Span(start, end);

					result.Add(data);
				}
			}
			return result;
		}
	}
}
