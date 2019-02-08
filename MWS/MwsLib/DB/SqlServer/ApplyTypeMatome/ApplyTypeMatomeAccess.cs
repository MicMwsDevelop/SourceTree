//
// ApplyTypeMatomeAccess.cs
//
// 申込種別まとめ情報データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// 
using MwsLib.BaseFactory.ApplyTypeMatome;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.ApplyTypeMatome
{
	public static class ApplyTypeMatomeAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 申込種別まとめ情報リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<ApplyTypeMatomeData> GetApplyMatomeList(Date date, bool sqlsv2)
		{
			DataTable table = ApplyTypeMatomeGetIO.GetApplyMatomeList(date, sqlsv2);
			return ApplyTypeMatomeController.ConvertApplyMatomeList(table);
		}

		/// <summary>
		/// 申込種別の更新
		/// </summary>
		/// <param name="list">申込種別まとめ情報リスト</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetApplyMatomeType(List<ApplyTypeMatomeData> list, bool sqlsv2 = false)
		{
			return ApplyTypeMatomeSetIO.UpdateApplyMatomeType(list, sqlsv2);
		}
	}
}
