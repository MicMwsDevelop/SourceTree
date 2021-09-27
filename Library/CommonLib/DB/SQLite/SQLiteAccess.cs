//
// SQLiteAccess.cs
// 
// SQLiteデータベース接続基本情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Data.SQLite;

namespace CommonLib.DB.SQLite
{
	/// <summary>
	/// SQLiteデータベース接続基本情報
	/// </summary>
	public static class SQLiteAccess
    {

        // メソッド ///////////////////////////////////////

        /// <summary>
        /// SQLite接続文字列を作成する
        /// </summary>
        /// <returns>接続文字列</returns>
        public static string CreateConnectionString(string databasePathname)
        {
			SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
			builder.DataSource = databasePathname;
			builder.Pooling = true;
			//builder.ReadOnly = true;

			return builder.ToString();
		}


		//  接続タイムアウト回避対応  ----------------------------------------------------->

		/// <summary>Connection Openリトライ回数</summary>
		private static readonly int retryCount = 1000;
        /// <summary>リトライ待ち時間(秒)</summary>
        //private static readonly int WaitTime = 10;
        /// <summary>
        /// SQLiteConnection作成(Openした状態でSQLiteConnectionを返却)
        /// C++に合わせて1000回までリトライする
        /// using (SQLiteConnection con = new SQLiteConnectionの代替処理
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        /// <returns></returns>
        internal static SQLiteConnection CreateConnectionRetryOpen(string connectionString)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    con.Open();
                    break;
                }
                catch
                {
                    //  コネクションプールクリア
                    SQLiteConnection.ClearPool(con);
                    con.Dispose();

                    con = new SQLiteConnection(connectionString);
                    //  10秒待つ 1000をかけてミリセカンド
                    // Thread.Sleep(WaitTime * 1000);
                }
            }
            return con;
        }

        //  タイムアウト回避対応  -----------------------------------------------------<
    }
}
