//
// AlertCloudBackupPcSupportPlusAccess.cs
//
// クラウドバックアップPC安心サポートPlus同時申込アラート データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
// 
using MwsLib.BaseFactory.AlertCloudBackupPcSupportPlus;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.AlertCloudBackupPcSupportPlus
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
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<CloudBackupPcSupportPlus> GetCloudBackupPcSupportPlusList(Date date, bool ct)
		{
			DataTable table = AlertCloudBackupPcSupportPlusGetIO.GetCloudBackupPcSupportPlusList(date, ct);
			return CloudBackupPcSupportPlus.DataTableToList(table);
		}
	}
}
