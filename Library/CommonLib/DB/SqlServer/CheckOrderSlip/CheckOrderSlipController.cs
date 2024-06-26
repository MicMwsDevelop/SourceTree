﻿//
// CheckOrderSlipController.cs
//
// 受注伝票情報 データ詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.CheckOrderSlip;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.CheckOrderSlip
{
	public static class CheckOrderSlipController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注伝票情報リストの詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>終了ユーザー情報リスト</returns>
		public static List<OrderSlipData> ConvertOrderSlipList(DataTable table)
		{
			List<OrderSlipData> result = null;
			if (null != table)
			{
				result = new List<OrderSlipData>();
				foreach (DataRow row in table.Rows)
				{
					OrderSlipData data = new OrderSlipData
					{
						受注番号 = DataBaseValue.ConvObjectToInt(row["f受注番号"]),
						受注日 = DataBaseValue.ConvObjectToDateTimeNull(row["f受注日"]),
						受注承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["f受注承認日"]),
						売上承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["f売上承認日"]),
						販売種別 = (MwsDefine.ApplyType)DataBaseValue.ConvObjectToInt(row["f販売種別"]),
						顧客No = DataBaseValue.ConvObjectToInt(row["fユーザーコード"]),
						顧客名 = row["fユーザー"].ToString(),
						商品コード = row["f商品コード"].ToString(),
						商品名 = row["f商品名"].ToString(),
						標準価格 = DataBaseValue.ConvObjectToInt(row["f標準価格"]),
						受注金額 = DataBaseValue.ConvObjectToInt(row["f受注金額"]),
						販売先コード = DataBaseValue.ConvObjectToInt(row["f販売先コード"]),
						販売先 = row["f販売先"].ToString(),
						支店コード = row["fBshCode3"].ToString(),
						担当支店名 = row["f担当支店名"].ToString(),
						担当者コード = row["f担当者コード"].ToString(),
						担当者名 = row["f担当者名"].ToString(),
						件名 = row["f件名"].ToString(),
						リプレース = row["fリプレース"].ToString()
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
					data.Type = OrderSlipData.GetOrderType(data.商品コード);

					result.Add(data);
				}
			}
			return result;
		}
	}
}
