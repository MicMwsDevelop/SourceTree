//
// PcaDatabaseAccess.cs
//
// P20V01C001KON0001 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/04/15 勝呂)
// 
using CommonLib.DB.SqlServer;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.PCA
{
	public static class PcaDatabaseAccess
	{
		/// <summary>
		/// [P20V01C001KON0001] レコードの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetPcaDatabase(string sqlStr, SqlParameter[] param, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(sqlStr, param, connectStr);
		}
	}
}
