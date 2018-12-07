using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.ScanImageData;
using System.Data;
using System.Data.SQLite;
using System.IO;


namespace MwsLib.DB.SQLite.ScanImageData
{
	public static class SQLiteScanImageDataSetIO
	{
		/// <summary>
		/// スキャンデータ登録情報リストの追加
		/// </summary>
		/// <param name="dbPath">データベースパス</param>
		/// <param name="list">スキャンデータ情報リスト</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoUserScanDataList(string dbPath, List<ScanImageDataFileInfo> list)
		{
			int rowCount = -1;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, ScanImageDataDef.SCAN_IMAGE_DATA_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							string sqlString = @"INSERT INTO SCAN_DATA VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)";
							foreach (ScanImageDataFileInfo user in list)
							{
								SQLiteParameter[] param = { new SQLiteParameter("@1", 0),			// SCAN_ID
												new SQLiteParameter("@2", user.Document),			// DOCUMENT_TYPE
												new SQLiteParameter("@3", user.CustomerNo),			// CUSTOMER_NO
												new SQLiteParameter("@4", user.TokuisakiNo),		// TOKUISAKI_NO
												new SQLiteParameter("@5", user.ClinicName),			// CLINIC_NAME
												new SQLiteParameter("@6", user.FolderName),			// FOLDER_NAME
												new SQLiteParameter("@7", user.FileName),			// FILE_NAME
												new SQLiteParameter("@8", user.FileDateTime),	// FILE_DATETIME
												new SQLiteParameter("@9", user.Method) };			// METHOD_TYPE
								// 実行
								rowCount = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoUserScanData() Error!");
								}
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// スキャンデータ登録情報リストの全削除
		/// </summary>
		/// <param name="dbPath">データベースパス</param>
		/// <returns>影響行数</returns>
		public static int DeleteScanData(string dbPath)
		{
			int rowCount = -1;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, ScanImageDataDef.SCAN_IMAGE_DATA_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							string sqlString = @"DELETE FROM SCAN_DATA";

							// 実行
							rowCount = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
							if (rowCount <= -1)
							{
								throw new ApplicationException("DeleteUserRegister() Error!");
							}
							// autoincrementをリセット
							sqlString = @"DELETE FROM sqlite_sequence WHERE NAME = 'SCAN_DATA'";

							// 実行
							SQLiteController.SqlExecuteCommand(con, tran, sqlString);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}
	}
}
