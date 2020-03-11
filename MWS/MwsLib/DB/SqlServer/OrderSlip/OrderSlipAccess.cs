//
// OrderSlipAccess.cs
//
// 受注伝票情報 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory.Junp;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.OrderSlip
{
	public static class OrderSlipAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注伝票情報リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetOrderSlipList(Date date, List<string> goods, bool sqlsv2)
		{
			DataTable table = OrderSlipGetIO.GetOrderSlipList(date, goods, sqlsv2);
			return OrderSlipController.ConvertOrderSlipList(table);
		}
	}
}
