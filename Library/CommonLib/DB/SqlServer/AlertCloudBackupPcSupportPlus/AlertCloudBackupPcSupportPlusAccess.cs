//
// AlertCloudBackupPcSupportPlusAccess.cs
//
// クラウドバックアップPC安心サポートPlus同時申込アラート データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
// 
using CommonLib.Common;
using CommonLib.BaseFactory.AlertCloudBackupPcSupportPlus;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.AlertCloudBackupPcSupportPlus
{
	public class AlertCloudBackupPcSupportPlusAccess
	{
		//////////////////////////////////////////////////////////////////
		/// charlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// クラウドバックアップとクラウドバックアップ（PC安心サポートPlus）同時契約中リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<CloudBackupPcSupportPlus> GetCloudBackupPcSupportPlusList(Date date, string connectStr)
		{
			DataTable table = AlertCloudBackupPcSupportPlusGetIO.GetCloudBackupPcSupportPlusList(date, connectStr);
			return CloudBackupPcSupportPlus.DataTableToList(table);
		}
	}
}
