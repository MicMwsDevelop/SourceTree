//
// DataBaseTableSQL.cs
// 
// SQL SERVER データベース 共通テーブルSQLクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer
{
	/// <summary>
	/// 共通テーブルSQLクラス
	/// </summary>
	internal static class DataBaseTableSQL
    {
        /// <summary>
        /// Insertコマンド
        /// </summary>
        /// <param name="con">コネクション</param>
        /// <param name="tran">トランザクション</param>
        /// <param name="table">テーブル</param>
        public static int InsertData(SqlConnection con, SqlTransaction tran, DataTable table)
        {
            int result = -1;
            //  コマンド実行
            string sqlString = DataBaseController.CreateInsertQuery(table);
            int rowCount = table.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                //  SqlParameter配列作成
                SqlParameter[] param = DataBaseController.CreateSqlParameters(table.Columns, table.Rows[i]);
                //  実行
                result = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
                if (result <= -1)
                {
                    throw new ApplicationException("");
                }
            }
            return result;
        }

        /// <summary>
        /// Updateコマンド
        /// </summary>
        /// <param name="con">コネクション</param>
        /// <param name="tran">トランザクション</param>
        /// <param name="table">テーブル</param>
        /// <param name="whereColumns">指定有 tableのpkカラムではなく、指定カラムでwhere句を作成</param>
        public static int UpdateData(SqlConnection con, SqlTransaction tran, DataTable table, DataColumn[] whereColumns = null)
        {
            int result = -1;
            //  コマンド実行
            string sqlString = DataBaseController.CreateUpdateQuery(table, whereColumns);
            int rowCount = table.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                //  SqlParameter配列作成
                SqlParameter[] param = DataBaseController.CreateSqlParameters(table.Columns, table.Rows[i]);
                //  実行
                result = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
                if (result <= -1)
                {
                    throw new ApplicationException("");
                }
            }
            return result;
        }

        /// <summary>
        /// Deleteコマンド
        /// </summary>
        /// <remarks>
        /// PrimaryKeyを指定して削除する場合、複数のレコードを個々に指定して削除する場合に使用する
        /// </remarks>
        /// <param name="con">コネクション</param>
        /// <param name="tran">トランザクション</param>
        /// <param name="table">テーブル</param>
        /// <param name="whereColumns">指定有 tableのpkカラムではなく、指定カラムでwhere句を作成</param>
        public static int DeleteData(SqlConnection con, SqlTransaction tran, DataTable table, DataColumn[] whereColumns = null)
        {
            int result = -1;

            // コマンド実行
            string sqlString = DataBaseController.CreateDeleteQuery(table, whereColumns);
            int rowCount = table.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                // SqlParameter配列作成
                SqlParameter[] param = DataBaseController.CreateDeleteSqlParameters(table, i, whereColumns);

                // 実行
                result = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
                if (result <= -1)
                {
                    throw new ApplicationException("");
                }
            }
            return result;
        }

        /// <summary>
        /// Deleteコマンド(データ直接指定)
        /// テーブルの行データを使用しない
        /// </summary>
        /// <remarks>
        /// PrimaryKey以外のフィールド値を条件にする場合、複数のレコードを同一の条件で削除する場合に使用する
        /// </remarks>
        /// <param name="con">コネクション</param>
        /// <param name="tran">トランザクション</param>
        /// <param name="table">テーブル(テーブル名のみ使用)</param>
        /// <param name="whereColumns">指定カラムでwhere句を作成</param>
        /// <param name="specify">指定カラムのデータ(列順) unll値の場合は対象箇所のパラメーターを作成しない</param>
        public static int DeleteDataSpecify(SqlConnection con, SqlTransaction tran, DataTable table, DataColumn[] whereColumns, params string[] specify)
        {
            int result = -1;
            // コマンド作成
            string sqlString = DataBaseController.CreateDeleteQuery(table, whereColumns);

            // SqlParameter配列作成
            List<SqlParameter> paramList = new List<SqlParameter>();

            int colCount = whereColumns.Length;
            for (int i = 0; i < colCount; i++)
            {
                if (specify != null
                    && !string.IsNullOrEmpty(specify[i]))
                {
                    paramList.Add(new SqlParameter("@" + whereColumns[i].ColumnName, specify[i]));
                }
            }

            // 実行
            result = DataBaseController.SqlExecuteCommand(con, tran, sqlString, paramList.ToArray());
            if (result <= -1)
            {
                throw new ApplicationException("");
            }
            return result;
        }

        /// <summary>
        /// BulkCopyコマンド
        /// </summary>
        /// <remarks>
        /// 100行以上のデータを一括でInsertするとき用
        /// </remarks>
        /// <param name="con">コネクション</param>
        /// <param name="tran">トランザクション</param>
        /// <param name="table">テーブル</param>
        /// <returns>実行結果</returns>
        public static int BulkCopyData(SqlConnection con, SqlTransaction tran, DataTable table)
        {
            int result = -1;

            // BulkCopy実行
            result = DataBaseController.SqlExecuteBulkCopy(con, tran, table);
            if (result <= -1)
            {
                throw new ApplicationException("");
            }

            return result;
        }

        /// <summary>
        /// DeleteTableコマンド
        /// </summary>
        /// <remarks>
        /// Tableのデータを全て削除するとき用
        /// </remarks>
        /// <param name="con">コネクション</param>
        /// <param name="tran">トランザクション</param>
        /// <returns>実行結果</returns>
        public static int DeleteTable(SqlConnection con, SqlTransaction tran, string tableName)
        {
            int result = -1;

            // コマンド実行
            string sqlString = string.Format("DELETE FROM {0};", tableName);
            SqlParameter[] param = new List<SqlParameter>().ToArray();
            result = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
            if (result <= -1)
            {
                throw new ApplicationException("");
            }

            return result;
        }
    }
}
