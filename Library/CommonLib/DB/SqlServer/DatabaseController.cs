﻿//
// DatabaseController.cs
// 
// SQL SERVER データベースI/O管理クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CommonLib.DB.SqlServer
{
	/// <summary>
	/// データベースI/O管理クラス
	/// </summary>
	public static class DatabaseController
    {
		/// <summary>
		/// SQLコマンドの実行
		/// クエリが単一の値を返すときに使用。結果は最初の行の最初の列
		/// </summary>
		/// <param name="con">SqlConnection</param>
		/// <param name="sqlString">クエリ</param>
		/// <returns>実行結果</returns>
		public static object SqlExecuteScalar(SqlConnection con, string sqlString)
		{
			object result = null;
			{
				using (SqlCommand cmd = new SqlCommand(sqlString, con))
				{
					try
					{
						// 実行
						result = cmd.ExecuteScalar();
					}
					catch
					{
						throw;
					}
				}
			}
			return result;
		}

		/// <summary>
		/// SQLコマンドの実行
		/// クエリが単一の値を返すときに使用。結果は最初の行の最初の列（UPDATE、INSERTなど）に使用
		/// </summary>
		/// <param name="con">SqlConnection</param>
		/// <param name="sqlString">クエリ</param>
		/// <param name="param">引数</param>
		/// <returns>実行結果</returns>
		public static object SqlExecuteScalar(SqlConnection con, string sqlString, SqlParameter[] param)
		{
			object result = null;
			{
				using (SqlCommand cmd = new SqlCommand(sqlString, con))
				{
					cmd.Parameters.Clear();
					if (null != param)
					{
						cmd.Parameters.AddRange(param);
					}
					try
					{
						// 実行
						result = cmd.ExecuteScalar();
					}
					catch
					{
						throw;
					}
				}
			}
			return result;
		}

		/// <summary>
		/// SQLコマンドの実行
		/// 結果セットの取得
		/// </summary>
		/// <param name="con">SQL接続情報</param>
		/// <param name="sqlString">SQL文</param>
		/// <returns>結果セット</returns>
		public static DataTable SqlExcuteDataAdapter(SqlConnection con, string sqlString)
		{
			DataTable result = null;
			try
			{
				using (SqlCommand cmd = new SqlCommand(sqlString, con))
				{
					// タイムアウト300秒
					cmd.CommandTimeout = 300;

					using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						result = new DataTable();
						da.Fill(result);
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		/// <summary>
		/// SQLコマンドの実行
		/// 結果セットの取得
		/// </summary>
		/// <param name="con">SQL接続情報</param>
		/// <param name="sqlString">SQL文</param>
		/// <param name="timeOutSecond">タイムアウト(秒数)</param>
		/// <returns>結果セット</returns>
		public static DataTable SqlExcuteDataAdapterTimeOut(SqlConnection con, string sqlString, int timeOutSecond)
		{
			DataTable result = null;
			try
			{
				using (SqlCommand cmd = new SqlCommand(sqlString, con))
				{
					// タイムアウト
					cmd.CommandTimeout = timeOutSecond;

					using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						result = new DataTable();
						da.Fill(result);
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		/// <summary>
		/// SQLコマンドの実行
		/// 結果のないSQLステートメント（UPDATE、INSERTなど）に使用
		/// </summary>
		/// <param name="con">SQL接続情報</param>
		/// <param name="sqlString">SQL文</param>
		/// <param name="param">引数</param>
		/// <returns>影響行数</returns>
		public static int SqlExecuteNonQuery(SqlConnection con, string sqlString, SqlParameter[] param = null)
		{
			int result = -1;
			using (SqlCommand cmd = new SqlCommand(sqlString, con))
			{
				cmd.Parameters.Clear();
				if (null != param)
				{
					cmd.Parameters.AddRange(param);
				}
				try
				{
					// 実行
					result = cmd.ExecuteNonQuery();
				}
				catch
				{
					throw;
				}
			}
			return result;
		}

		/// <summary>
		/// SQLコマンドの実行
		/// 結果のないSQLステートメント（UPDATE、INSERTなど）に使用
		/// </summary>
		/// <param name="con">SQL接続情報</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="sqlString">SQL文</param>
		/// <param name="param">引数</param>
		/// <returns>影響行数</returns>
		public static int SqlExecuteNonQueryTran(SqlConnection con, SqlTransaction tran, string sqlString, SqlParameter[] param = null)
		{
			int result = -1;
			using (SqlCommand cmd = new SqlCommand(sqlString, con, tran))
			{
				cmd.Parameters.Clear();
				if (null != param)
				{
					cmd.Parameters.AddRange(param);
				}
				try
				{
					// 実行
					result = cmd.ExecuteNonQuery();
				}
				catch
				{
					throw;
				}
			}
			return result;
		}

		/// <summary>
		/// ストアドプロシージャの実行
		/// </summary>
		/// <param name="con">SQL接続情報</param>
		/// <param name="sqlString">SQL文</param>
		/// <param name="param">引数</param>
		/// <returns>実行結果</returns>
		public static int SqlExecuteStoredProcedure(SqlConnection con, string sqlString, SqlParameter[] param = null)
		{
			int result = -1;

			// 任意のストアドプロシージャを指定
			using (SqlCommand cmd = new SqlCommand(sqlString, con))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Clear();
				if (null != param)
				{
					cmd.Parameters.AddRange(param);
				}
				try
				{
					// ストアドプロシージャ実行
					using (SqlDataReader sdr = cmd.ExecuteReader())
					{
						//// 行を読み込む
						//while (sdr.Read())
						//{
						//	// 列を読み込む
						//	for (int i = 0; i < sdr.FieldCount; i++)
						//	{
						//		Console.Write(sdr[i] + " ");
						//	}
						//	Console.WriteLine();
						//}

						// SqlDataReaderをクローズ
						sdr.Close();
					}
					result = 0;
				}
				catch
				{
					throw;
				}
			}
			return result;
		}

		/// <summary>
		/// BulkCopyの実行
		/// （100行以上のInsertの時のみ使用する）
		/// </summary>
		/// <param name="con">SQL接続情報</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="table">DataTable</param>
		/// <returns>実行結果</returns>
		public static int SqlExecuteBulkCopy(SqlConnection con, SqlTransaction tran, DataTable table)
        {
            int result = -1;
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran))
                {
                    try
                    {
                        // 実行
                        bulkCopy.BulkCopyTimeout = 30;
                        bulkCopy.DestinationTableName = table.TableName;
                        bulkCopy.WriteToServer(table);

                        result = 0;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return result;
        }

		/// <summary>
		/// Insertクエリ作成
		/// </summary>
		/// <param name="table">型付きDataTable</param>
		/// <returns>クエリ</returns>
		public static string CreateInsertQuery(DataTable table)
        {
            // カラム名
            StringBuilder sbCol = new StringBuilder();
            // パラメータ
            StringBuilder sbVal = new StringBuilder();

            int colCount = table.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                sbCol.AppendFormat("{0}, ", table.Columns[i].ColumnName);
                sbVal.AppendFormat("@{0}, ", table.Columns[i].ColumnName);
            }
            // 末尾の", "を除去
            sbCol.Remove(sbCol.Length - 2, 2);
            sbVal.Remove(sbVal.Length - 2, 2);

            return string.Format("INSERT INTO {0} ({1}) Values ({2});", table.TableName, sbCol.ToString(), sbVal.ToString());
        }

		/// <summary>
		/// Updateクエリ作成　PK用
		/// tableの全pkカラム=でwhere句を作成する。
		/// </summary>
		/// <param name="table">型付きDataTable</param>
		/// <param name="whereColumns">指定有 tableのpkカラムではなく、指定カラムでwhere句を作成</param>
		/// <returns>クエリ</returns>
		public static string CreateUpdateQuery(DataTable table, DataColumn[] whereColumns = null)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            DataColumn[] pkColumns;
            if (whereColumns == null)
            {
                pkColumns = table.PrimaryKey;
            }
            else
            {
                pkColumns = whereColumns;
            }

            int colCount = table.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                //  項目がPKの場合はwhereに設定                
                if (Array.LastIndexOf(pkColumns, table.Columns[i]) >= 0)
                {
                    sbWhere.AppendFormat(" {0}=@{0} and ", table.Columns[i].ColumnName);
                }
                else
                {
                    sbColumns.AppendFormat("{0}=@{0}, ", table.Columns[i].ColumnName);
                }
            }
            // 末尾の", "を除去
            sbColumns.Remove(sbColumns.Length - 2, 2);
            // 末尾の"and "を除去
            sbWhere.Remove(sbWhere.Length - 5, 4);

            return string.Format("UPDATE {0} SET {1} Where {2};", table.TableName, sbColumns.ToString(), sbWhere.ToString());
        }

		/// <summary>
		/// Updateクエリ作成
		/// </summary>
		/// <param name="table">型付きDataTable</param>
		/// <param name="columns">指定有 指定された項目の値のみ更新/ 指定無 tableの全項目の値を更新</param>
		/// <param name="whereColumns">指定有 tableのpkカラムではなく、指定カラムでwhere句を作成</param>
		/// <returns>クエリ</returns>
		public static string CreateUpdateItemQuery(DataTable table, DataColumn[] columns = null, DataColumn[] whereColumns = null)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            DataColumn[] pkColumns;
            if (whereColumns == null)
            {
                pkColumns = table.PrimaryKey;
            }
            else
            {
                pkColumns = whereColumns;
            }

            if (columns == null)
            {
                int colCount = table.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    //  項目がPKの場合はwhereに設定                
                    if (Array.LastIndexOf(pkColumns, table.Columns[i]) >= 0)
                    {
                        sbWhere.AppendFormat(" {0}=@{0} and ", table.Columns[i].ColumnName);
                    }
                    else
                    {
                        sbColumns.AppendFormat("{0}=@{0}, ", table.Columns[i].ColumnName);
                    }
                }
            }
            else
            {
                foreach (DataColumn col in columns)
                {
                    sbColumns.AppendFormat("{0}=@{0}, ", col.ColumnName);
                }
                foreach (DataColumn col in pkColumns)
                {
                    sbWhere.AppendFormat(" {0}=@{0} and ", col.ColumnName);
                }
            }
            // 末尾の", "を除去
            sbColumns.Remove(sbColumns.Length - 2, 2);
            // 末尾の"and "を除去
            sbWhere.Remove(sbWhere.Length - 5, 4);

            return string.Format("UPDATE {0} SET {1} Where {2};", table.TableName, sbColumns.ToString(), sbWhere.ToString());
        }

		/// <summary>
		/// Deleteクエリ作成　PK用
		/// tableの全pkカラム=でwhere句を作成する。
		/// </summary>
		/// <param name="table">型付きDataTable</param>
		/// <param name="whereColumns">指定有 tableのpkカラムではなく、指定カラムでwhere句を作成</param>
		/// <returns>クエリ</returns>
		public static string CreateDeleteQuery(DataTable table, DataColumn[] whereColumns = null)
        {
            // パラメータ
            StringBuilder sbWheres = new StringBuilder();

            DataColumn[] pkColumns;
            if (whereColumns == null)
            {
                pkColumns = table.PrimaryKey;
            }
            else
            {
                pkColumns = whereColumns;
            }

            int colCount = pkColumns.Length;
            for (int i = 0; i < colCount; i++)
            {
                sbWheres.AppendFormat(" {0}=@{0} and ", pkColumns[i].ColumnName);
            }
            // 末尾の"and "を除去
            sbWheres.Remove(sbWheres.Length - 5, 4);

            return string.Format("DELETE FROM {0} Where {1};", table.TableName, sbWheres.ToString());
        }

		/// <summary>
		/// SqlParameter配列の作成
		/// </summary>
		/// <param name="columns">テーブルカラム</param>
		/// <param name="row">データ</param>
		/// <returns>SqlParameter配列</returns>
		public static SqlParameter[] CreateSqlParameters(DataColumnCollection columns, DataRow row)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            int colCount = columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                paramList.Add(new SqlParameter("@" + columns[i].ColumnName, row[columns[i]]));
            }
            return paramList.ToArray();
        }

		/// <summary>
		/// 削除用のSqlParameter配列の作成
		/// </summary>
		/// <param name="table">テーブル</param>
		/// <param name="rowIndex">データ位置</param>
		/// <param name="whereColumns">指定無：tableのpkカラムを使用、指定有：指定カラムを使用</param>
		/// <returns>SqlParameter配列</returns>
		public static SqlParameter[] CreateDeleteSqlParameters(DataTable table, int rowIndex, DataColumn[] whereColumns = null)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            DataColumn[] pkColumns;
            if (whereColumns == null)
            {
                pkColumns = table.PrimaryKey;
            }
            else
            {
                pkColumns = whereColumns;
            }

            int colCount = pkColumns.Length;
            DataRow row = table.Rows[rowIndex];
            for (int i = 0; i < colCount; i++)
            {
                paramList.Add(new SqlParameter("@" + pkColumns[i].ColumnName, row[pkColumns[i]]));
            }
            return paramList.ToArray();
        }

		/// <summary>
		/// Insertパラメータ作成
		/// </summary>
		/// <param name="pkColumns">テーブルカラム情報</param>
		/// <param name="strColumns">項目文字列</param>
		/// <param name="strColumns">パラメータ文字列</param>
		public static void CreateInsertParameter(DataColumnCollection columns, ref string strColumns, ref string strValues)
        {
            // カラム名            
            StringBuilder sbCol = new StringBuilder();
            // パラメータ
            StringBuilder sbVal = new StringBuilder();

            int colCount = columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                sbCol.AppendFormat("{0}, ", columns[i].ColumnName);
                sbVal.AppendFormat("@{0}, ", columns[i].ColumnName);
            }
            // 末尾の", "を除去
            sbCol.Remove(sbCol.Length - 2, 2);
            sbVal.Remove(sbVal.Length - 2, 2);

            strColumns = sbCol.ToString();
            strValues = sbVal.ToString();
        }

		/// <summary>
		/// Updateパラメータ作成
		/// </summary>
		/// <param name="pkColumns">テーブルカラム情報</param>
		/// <param name="primaryKey">テーブルPK情報</param>
		/// <param name="strColumns">項目文字列</param>
		/// <param name="strWhere">Where文字列</param>
		public static void CreateUpdateParameter(DataColumnCollection columns, DataColumn[] primaryKey, ref string strColumns, ref string strWhere)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            int colCount = columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                //  PKの場合はwhereに設定
                if (Array.LastIndexOf(primaryKey, columns[i]) >= 0)
                {
                    sbWhere.AppendFormat(" {0}=@{0} and ", columns[i].ColumnName);
                }
                else
                {
                    sbColumns.AppendFormat("{0}=@{0}, ", columns[i].ColumnName);
                }
            }
            // 末尾の", "を除去
            sbColumns.Remove(sbColumns.Length - 2, 2);
            // 末尾の"and "を除去
            sbWhere.Remove(sbWhere.Length - 5, 4);

            strColumns = sbColumns.ToString();
            strWhere = sbWhere.ToString();
        }

		/// <summary>
		///     Deleteパラメータ作成
		/// </summary>
		/// <param name="primaryKey">テーブルPK情報</param>
		/// <param name="strWhere">Where文字列</param>
		public static string CreateDeleteParameter(DataColumn[] primaryKey)
        {
            // パラメータ
            StringBuilder sbWheres = new StringBuilder();

            int colCount = primaryKey.Length;
            for (int i = 0; i < colCount; i++)
            {
                sbWheres.AppendFormat(" {0}=@{0} and ", primaryKey[i].ColumnName);
            }
            // 末尾の"and "を除去
            sbWheres.Remove(sbWheres.Length - 5, 4);

            return sbWheres.ToString();
        }

		/// <summary>
		/// Insertクエリ作成
		/// </summary>
		/// <remarks>
		/// 自動採番されるカラムを除く
		/// </remarks>
		/// <param name="table">型付きDataTable</param>
		/// <returns>クエリ</returns>
		public static string CreateInsertQueryExceptAutoIncrementColumn(DataTable table)
        {
            // カラム名
            StringBuilder sbCol = new StringBuilder();
            // パラメータ
            StringBuilder sbVal = new StringBuilder();

            int colCount = table.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                if (table.Columns[i].AutoIncrement != true)
                {
                    sbCol.AppendFormat("{0}, ", table.Columns[i].ColumnName);
                    sbVal.AppendFormat("@{0}, ", table.Columns[i].ColumnName);
                }
            }
            // 末尾の", "を除去
            sbCol.Remove(sbCol.Length - 2, 2);
            sbVal.Remove(sbVal.Length - 2, 2);

            return string.Format("INSERT INTO {0} ({1}) Values ({2});", table.TableName, sbCol.ToString(), sbVal.ToString());
        }

		/// <summary>
		/// SqlParameter配列の作成
		/// </summary>
		/// <remarks>
		/// 自動採番されるカラムを除く
		/// </remarks>
		/// <param name="pkColumns">テーブルカラム</param>
		/// <param name="row">データ</param>
		/// <returns>SqlParameter配列</returns>
		public static SqlParameter[] CreateSqlParametersExceptAutoIncrementColumn(DataColumnCollection columns, DataRow row)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            int colCount = columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                if (columns[i].AutoIncrement != true)
                {
                    paramList.Add(new SqlParameter("@" + columns[i].ColumnName, row[columns[i]]));
                }
            }
            return paramList.ToArray();
        }
	}
}
