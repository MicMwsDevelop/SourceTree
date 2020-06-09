//
// CheckOrderSlipAccess.cs
//
// 受注伝票情報 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory.Junp.CheckOrderSlip;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CheckOrderSlip
{
	public static class CheckOrderSlipAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注伝票情報リストの取得(受注日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetOrderSlipList(Date date, List<string> goods, bool sqlsv2)
		{
			DataTable table = CheckOrderSlipGetIO.GetOrderSlipList(date, goods, sqlsv2);
			return CheckOrderSlipController.ConvertOrderSlipList(table);
		}

		/// <summary>
		/// 受注伝票情報リストの取得(受注承認日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetOrderAcceptSlipList(Date date, List<string> goods, bool sqlsv2)
		{
			DataTable table = CheckOrderSlipGetIO.GetOrderAcceptSlipList(date, goods, sqlsv2);
			return CheckOrderSlipController.ConvertOrderSlipList(table);
		}

		/// <summary>
		/// 受注伝票情報リストの取得(売上承認日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetSaleSlipList(Date date, List<string> goods, bool sqlsv2)
		{
			DataTable table = CheckOrderSlipGetIO.GetSaleSlipList(date, goods, sqlsv2);
			return CheckOrderSlipController.ConvertOrderSlipList(table);
		}
	}
}
