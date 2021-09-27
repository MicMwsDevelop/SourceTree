//
// ScanImageManagerAccess.cs
//
// 文書インデックス管理 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// Ver1.01 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
// 
using CommonLib.BaseFactory.ScanImageManager;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.ScanImageManager
{
	public static class ScanImageManagerAccess
	{
		/// <summary>
		/// 得意先番号に対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先番号</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客情報Noリスト</returns>
		public static ScanImageFile GetCustomerInfo(string tokuisakiNo, string connectStr)
		{
			DataTable table = ScanImageManagerGetIO.GetCustomerInfo(tokuisakiNo, connectStr);
			List<ScanImageFile> list = ScanImageFile.ConvertCustomerInfoList(table);
			if (0 < list.Count)
			{
				return list[0];
			}
			return null;
		}

		/// <summary>
		/// 顧客情報リストの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客情報Noリスト</returns>
		public static List<ScanImageFile> GetCustomerInfoList(string connectStr)
		{
			DataTable table = ScanImageManagerGetIO.GetCustomerInfoList(connectStr);
			return ScanImageFile.ConvertCustomerInfoList(table);
		}
	}
}
