//
// CloudBackupAccess.cs
//
// クラウドバックアップPCA売上明細情報 データアクセスクラス
// 
// Ver1.00 新規作成(2020/10/06 勝呂)
// 
using CommonLib.BaseFactory.CloudBackup;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.CloudBackup
{
	public static class CloudBackupAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 指定期間のクラウドバックアップPCA売上明細情報リストの取得
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>PCA売上明細情報リスト</returns>
		public static List<GroupMicPCA売上明細> GetCloudBackupEarningsList(string goods, Span span, string connectStr)
		{
			DataTable table = CloudBackupGetIO.GetCloudBackupEarningsList(goods, span, connectStr);
			return GroupMicPCA売上明細.DataTableToList(table);
		}
	}
}
