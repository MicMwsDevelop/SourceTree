//
// ProspectProgressAutoAggregateAccess.cs
//
// 見込進捗自動集計 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
// 
using CommonLib.BaseFactory.ProspectProgressAutoAggregate;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.ProspectProgressAutoAggregate
{
	public static class ProspectProgressAutoAggregateAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 売上予想の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<売上予想> Select_売上予想(Date today, string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_売上予想(today, connectStr);
			return 売上予想.DataTableToList(dt, today);
		}

		/// <summary>
		/// 売上予想ESの取得
		/// </summary>
		/// <param name="start">計上開始月</param>
		/// <param name="end">計上終了月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<売上予想ES> Select_売上予想ES(Date start, Date end, string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_売上予想ES(start, end, connectStr);
			return 売上予想ES.DataTableToList(dt);
		}

		/// <summary>
		/// 売上進捗ESの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<売上進捗ES> Select_売上進捗ES(string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_売上進捗ES(connectStr);
			return 売上進捗ES.DataTableToList(dt);
		}

		/// <summary>
		/// 売上進捗まとめの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<売上進捗まとめ> Select_売上進捗まとめ(string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_売上進捗まとめ_WW(connectStr);
			List<売上進捗まとめ> wwList = 売上進捗まとめ.DataTableToList(dt);
			dt = ProspectProgressAutoAggregateGetIO.Select_売上進捗まとめ_契約情報(connectStr);
			List<売上進捗まとめ> agreeList = 売上進捗まとめ.DataTableToList(dt);
			foreach (売上進捗まとめ agree in agreeList)
			{
				if (-1 == wwList.FindIndex(p => p.顧客No == agree.顧客No && p.課金開始日 == agree.課金開始日 && p.課金終了日 == agree.課金終了日))
				{
					wwList.Add(agree);
				}
			}
			return wwList;
		}
	}
}
