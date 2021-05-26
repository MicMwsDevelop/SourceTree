//
// CheckVoucherPaletteESAccess.cs
//
// 受注伝票情報 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory.CheckVoucherPaletteES;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CheckVoucherPaletteES
{
	public static class CheckVoucherPaletteESAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// paletteES 受注伝票情報リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<OrderVoucher> GetPaletteESOrderVoucherList(Date date, bool sqlsv2)
		{
			DataTable table = CheckVoucherPaletteESGetIO.GetPaletteESOrderVoucherList(date, sqlsv2);
			return CheckVoucherPaletteESController.ConvertOrderVoucherList(table);
		}
	}
}
