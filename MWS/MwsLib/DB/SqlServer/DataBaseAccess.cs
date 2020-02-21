//
// DataBaseAccess.cs
// 
// SQL SERVER データベース接続基本情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace MwsLib.DB.SqlServer
{
	/// <summary>
	/// SQL SERVER DB接続基本情報
	/// </summary>
	public static class DataBaseAccess
    {
        /// <summary>
        /// ロック用クラス
        /// </summary>
        private static ReaderWriterLock rwLock = new ReaderWriterLock();


		//// メソッド ///////////////////////////////////////

		/// <summary>
		/// Charileデータベース接続文字列を作成する
		/// </summary>
		/// <returns>Charlieデータベース接続文字列</returns>
		public static string CreateCharlieConnectionString(bool sqlsv2)
		{
			try
			{
				// リーダーロックを取得
				rwLock.AcquireReaderLock(Timeout.Infinite);
			}
			finally
			{
				// リーダーロックを解放
				rwLock.ReleaseReaderLock();
			}
			// Charlie
			return DatabaseConnect.CharlieWebConnectionString(sqlsv2);
		}

		/// <summary>
		/// Charlieデータベース接続文字列を作成する
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>Charlieデータベース接続文字列</returns>
		public static string CreateCharlieWebConnectionString(bool sqlsv2)
		{
			return DatabaseConnect.CharlieWebConnectionString(sqlsv2);
		}

		/// <summary>
		/// Junpデータベース接続文字列を作成する
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>Junpデータベース接続文字列</returns>
		public static string CreateJunpConnectionString(bool sqlsv2)
		{
			try
			{
				// リーダーロックを取得
				rwLock.AcquireReaderLock(Timeout.Infinite);
			}
			finally
			{
				// リーダーロックを解放
				rwLock.ReleaseReaderLock();
			}
			// Junp
			return DatabaseConnect.JunpWebConnectionString(sqlsv2);
		}

		/// <summary>
		/// Junpデータベース接続文字列を作成する
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>Junpデータベース接続文字列</returns>
		public static string CreateJunpWebConnectionString(bool sqlsv2)
		{
			return DatabaseConnect.JunpWebConnectionString(sqlsv2);
		}

		/// <summary>
		/// TCCSVデータベース接続文字列を作成する
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>Junpデータベース接続文字列</returns>
		public static string CreateCouplerTccsvConnectionString()
		{
			return DatabaseConnect.CouplerTccsvConnectionString();
		}


		//  接続タイムアウト回避対応  ----------------------------------------------------->

		/// <summary>
		/// Connection Openリトライ回数
		/// </summary>
		public const int RETRY_OPEN_COUNT = 2;

		/// <summary>
		/// リトライ待ち時間(秒)
		/// </summary>
		private static readonly int WaitTime = 5;

		/// <summary>
		/// SqlConnection作成(Openした状態でSqlConnectionを返却)
		/// C++に合わせて1000回までリトライする
		/// using (SqlConnection con = new SqlConnectionの代替処理
		/// </summary>
		/// <remarks>待ち時間が長くなるので2回に減らした
		/// openできなかった場合メッセージボックスを表示するようにした
		/// </remarks>
		/// <param name="connectionString">connectionString</param>
		/// <param name="retryCount">リトライ回数</param>
		/// <returns></returns>
		public static SqlConnection CreateConnectionRetryOpen(string connectionString, int retryCount = RETRY_OPEN_COUNT)
		{
			SqlConnection sqlCon = new SqlConnection(connectionString);
			for (int i = 0; i < retryCount + 1; i++)
			{
				try
				{
					sqlCon.Open();
					break;
				}
				catch
				{
					//  コネクションプールクリア
					SqlConnection.ClearPool(sqlCon);
					sqlCon.Dispose();

					sqlCon = new SqlConnection(connectionString);
					//  5秒待つ 1000をかけてミリセカンド
					Thread.Sleep(WaitTime * 1000);
				}
			}
			if (sqlCon.State != System.Data.ConnectionState.Open)
			{
				MessageBox.Show("データベースのオープンに失敗しました。", "データベース", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return sqlCon;
		}

		/// <summary>
		/// SqlConnection作成(Openした状態でSqlConnectionを返却)
		/// ※接続確認専用(メッセージボックスを表示しない)
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="retryCount"></param>
		/// <returns></returns>
		public static SqlConnection CreateConnectionSilentRetryOpen(string connectionString, int retryCount = RETRY_OPEN_COUNT)
		{
			SqlConnection sqlCon = new SqlConnection(connectionString);
			for (int i = 0; i < retryCount + 1; i++)
			{
				try
				{
					sqlCon.Open();
					break;
				}
				catch
				{
					//  コネクションプールクリア
					SqlConnection.ClearPool(sqlCon);
					sqlCon.Dispose();

					sqlCon = new SqlConnection(connectionString);
					//  5秒待つ 1000をかけてミリセカンド
					Thread.Sleep(WaitTime * 1000);
				}
			}
			return sqlCon;
		}
		//  タイムアウト回避対応  -----------------------------------------------------<
	}
}