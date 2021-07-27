//
// ScanImageManagerAccess.cs
//
// 文書インデックス管理 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// 
using MwsLib.BaseFactory.ScanImageManager;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.ScanImageManager
{
	public static class ScanImageManagerAccess
	{
		/// <summary>
		/// 得意先番号に対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先番号</param>
		/// <returns>顧客情報Noリスト</returns>
		public static ScanImageFile GetCustomerInfo(string tokuisakiNo, bool ct = false)
		{
			DataTable table = ScanImageManagerGetIO.GetCustomerInfo(tokuisakiNo, ct);
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
		/// <param name="ct">CT環境</param>
		/// <returns>顧客情報Noリスト</returns>
		public static List<ScanImageFile> GetCustomerInfoList(bool ct = false)
		{
			DataTable table = ScanImageManagerGetIO.GetCustomerInfoList(ct);
			return ScanImageFile.ConvertCustomerInfoList(table);
		}
	}
}
