using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.BaseFactory.Junp.View;
using System.Data.SqlClient;
using MwsLib.DB.SqlServer.PCA;
using MwsLib.DB.SqlServer.Estore;

namespace MwsLib.DB.SqlServer.ShipmentActing
{
	public static class ShipmentActingAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 送料商品コードリストの取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<string> Select_tMih送料商品コード(bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih送料商品コード], "", "f商品コード ASC", ct);
			return tMih送料商品コード.DataTableToList(table);
		}

		/// <summary>
		/// PCA商品マスタリストの取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<vMicPCA商品マスタ> Select_vMicPCA商品マスタ(bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ], "", "", ct);
			return vMicPCA商品マスタ.DataTableToList(table);
		}

		/// <summary>
		/// tMic離島リストの取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<string> Select_tMic離島(bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic離島], "", "", ct);
			return tMic離島.DataTableToStringList(table);
		}

		/// <summary>
		/// tMihPca在庫引当表Jリストの取得
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<tMihPca在庫引当表J> Select_tMihPca在庫引当表J(string sqlStr, bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, ct);
			return tMihPca在庫引当表J.DataTableToList(table);
		}

		/// <summary>
		/// tMihPca在庫引当表Jの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメタ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tMihPca在庫引当表J(string sqlStr, SqlParameter[] param, bool ct)
		{
			return JunpDatabaseAccess.UpdateSetJunpDatabase(sqlStr, param, ct);
		}

		/// <summary>
		/// vMicPCA受注明細グループリストの取得
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<vMicPCA受注明細> Select_vMicPCA受注明細_GroupList(string sqlStr, bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, ct);
			return vMicPCA受注明細.DataTableToGroupList(table);
		}

		/// <summary>
		/// vMicPCA受注明細リストの取得
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<vMicPCA受注明細> Select_vMicPCA受注明細(string sqlStr, bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, ct);
			return vMicPCA受注明細.DataTableToList(table);
		}

		/// <summary>
		/// vMic全ユーザー2リストの取得
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<vMic全ユーザー2> Select_vMic全ユーザー2(string sqlStr, bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, ct);
			return vMic全ユーザー2.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA担当者マスタ]の取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<vMicPCA担当者マスタ> Select_vMicPCA担当者マスタ(bool ct)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA担当者マスタ], "", "", ct);
			return vMicPCA担当者マスタ.DataTableToList(table);
		}

		/// <summary>
		/// t_MicSyukkashijiの新規追加
		/// </summary>
		/// <param name="param">パラメタ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_t_MicSyukkashiji(SqlParameter[] param, bool ct)
		{
			return JunpDatabaseAccess.InsertIntoJunpDatabase(t_MicSyukkashiji.InsertIntoSqlString, param, ct);
		}


		//////////////////////////////////////////////////////////////////
		/// estoreDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// tMICestore_logの更新
		/// </summary>
		/// <param name="sql">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tMICestore_log(string sqlStr, SqlParameter[] param, bool ct)
		{
			return EstoreDatabaseAccess.UpdateSetEstoreDatabase(sqlStr, param, ct);
		}


		//////////////////////////////////////////////////////////////////
		/// P10V01C001KON0001
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// JUCDの更新
		/// </summary>
		/// <param name="sql">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_JUCD(string sqlStr, SqlParameter[] param, bool ct)
		{
			return PcaDatabaseAccess.UpdateSetPcaDatabase(sqlStr, param, ct);
		}
	}
}
