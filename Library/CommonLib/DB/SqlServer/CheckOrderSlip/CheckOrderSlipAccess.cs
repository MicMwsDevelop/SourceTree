//
// CheckOrderSlipAccess.cs
//
// 受注伝票情報 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/04/17 勝呂)
// Ver1.12 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
// 
using CommonLib.BaseFactory.Junp.CheckOrderSlip;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.CheckOrderSlip
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
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetOrderSlipList(Date date, List<string> goods, string connectStr)
		{
			DataTable table = CheckOrderSlipGetIO.GetOrderSlipList(date, goods, connectStr);
			return CheckOrderSlipController.ConvertOrderSlipList(table);
		}

		/// <summary>
		/// 受注伝票情報リストの取得(受注承認日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetOrderAcceptSlipList(Date date, List<string> goods, string connectStr)
		{
			DataTable table = CheckOrderSlipGetIO.GetOrderAcceptSlipList(date, goods, connectStr);
			return CheckOrderSlipController.ConvertOrderSlipList(table);
		}

		/// <summary>
		/// 受注伝票情報リストの取得(売上承認日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderSlipData> GetSaleSlipList(Date date, List<string> goods, string connectStr)
		{
			DataTable table = CheckOrderSlipGetIO.GetSaleSlipList(date, goods, connectStr);
			return CheckOrderSlipController.ConvertOrderSlipList(table);
		}
	}
}
