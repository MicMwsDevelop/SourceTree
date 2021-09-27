//
// SQLiteController.cs
// 
// SQLiteデータベースI/O管理クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace CommonLib.DB.SQLite
{
	/// <summary>
	/// データベースI/O管理クラス(SQLite版)
	/// </summary>
	public static class SQLiteController
    {
        /// <summary>
        /// SQLコマンドの実行
        /// </summary>
        /// <param name="con">SqlConnection</param>
        /// <param name="tran">SqlTransaction</param>
        /// <param name="table">DataTable</param>
        /// <param name="sqlString">クエリ</param>
        /// <returns>実行結果</returns>
        public static int SqlExecuteCommand(SQLiteConnection con, SQLiteTransaction tran, string sqlString, SQLiteParameter[] param = null)
        {
            int result = -1;
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, con, tran))
                {
                    cmd.Parameters.Clear();
					if (null != param)
					{
						cmd.Parameters.AddRange(param);
					}
					try
                    {
                        //実行
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        // SQLエラー
                        switch (ex.ErrorCode)
                        {
                            //// 外部キー制約違反
                            //case SqlNumber.FK_ERROR:
                            //    break;
                            //// 一意キー制約違反
                            //// PK制約違反
                            //case SqlNumber.KY_ERROR:
                            //case SqlNumber.PK_ERROR:
                            //    break;
                            default:
                                throw;
                        }
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
		/// </summary>
		/// <param name="table">型付きDataTable</param>
		/// <returns>クエリ</returns>
		public static string CreateUpdateQuery(DataTable table)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            int colCount = table.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                //  PKの場合はwhereに設定
                if (Array.LastIndexOf(table.PrimaryKey, table.Columns[i]) >= 0)
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
            sbWhere.Remove(sbWhere.Length - 5, 5);

            return string.Format("UPDATE {0} SET {1} Where {2};", table.TableName, sbColumns.ToString(), sbWhere.ToString());
        }

		/// <summary>
		/// Deleteクエリ作成　PK用
		/// </summary>
		/// <param name="table">型付きDataTable</param>
		/// <returns>クエリ</returns>
		public static string CreateDeleteQuery(DataTable table)
        {
            // パラメータ
            StringBuilder sbWheres = new StringBuilder();

            int colCount = table.PrimaryKey.Length;
            for (int i = 0; i < colCount; i++)
            {
                sbWheres.AppendFormat(" {0}=@{0} and ", table.PrimaryKey[i].ColumnName);
            }
            // 末尾の"and "を除去
            sbWheres.Remove(sbWheres.Length - 5, 5);

            return string.Format("DELETE FROM {0} Where {1};", table.TableName, sbWheres.ToString());
        }

		/// <summary>
		/// SqlParameter配列の作成
		/// </summary>
		/// <param name="columns">テーブルカラム</param>
		/// <param name="row">データ</param>
		/// <returns>SqlParameter配列</returns>
		public static SQLiteParameter[] CreateSqlParameters(DataColumnCollection columns, DataRow row)
        {
            List<SQLiteParameter> paramList = new List<SQLiteParameter>();

            int colCount = columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                paramList.Add(new SQLiteParameter("@" + columns[i].ColumnName, row[columns[i]]));
            }
            return paramList.ToArray();
        }

		/// <summary>
		/// Updateパラメータ作成
		/// </summary>
		/// <param name="columns"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		public static SQLiteParameter[] CreateUpdateParameter(DataTable table, DataRow row)
        {
            List<SQLiteParameter> paramList = new List<SQLiteParameter>();
            List<SQLiteParameter> paramListPk = new List<SQLiteParameter>();

            int colCount = table.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                if (Array.LastIndexOf(table.PrimaryKey, table.Columns[i]) >= 0)
                {
                    // PKの場合
                    paramListPk.Add(new SQLiteParameter("@" + table.Columns[i].ColumnName, row[table.Columns[i]]));
                }
                else
                {
                    paramList.Add(new SQLiteParameter("@" + table.Columns[i].ColumnName, row[table.Columns[i]]));
                }
            }
            // PKの条件文パラメータを加える
            foreach (var pk in paramListPk)
            {
                paramList.Add(pk);
            }

            return paramList.ToArray();
        }

		/// <summary>
		/// Deleteパラメータ作成
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
            sbWheres.Remove(sbWheres.Length - 5, 5);

            return sbWheres.ToString();
        }
    }
}
