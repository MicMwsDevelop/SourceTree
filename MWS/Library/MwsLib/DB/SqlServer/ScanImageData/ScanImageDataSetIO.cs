using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MwsLib.BaseFactory.ScanImageData;

namespace MwsLib.DB.SqlServer.ScanImageData
{
	public static class ScanImageDataSetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		///  [JunpDB].[dbo].[tMic文書インデクス]の追加
		/// </summary>
		/// <param name="data">製品サポート情報ソフト保守情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoDocumentIndexList(List<ScanImageDataFileInfo> list, bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							string sqlString = @"INSERT INTO tMic文書インデクス VALUES (@1, @2, @3, @4, @5, @6, @7, @8)";
							int i = 1;
							foreach (ScanImageDataFileInfo data in list)
							{
								SqlParameter[] param = { new SqlParameter("@1", i),					// [ID]
													new SqlParameter("@2", data.CustomerNo),		// [顧客Ｎｏ]
													new SqlParameter("@3", data.TokuisakiNo),		// [得意先Ｎｏ]
													new SqlParameter("@4", (int)data.Document),		// [文書種別]
													new SqlParameter("@5", data.FolderName),		// [フォルダ名]
													new SqlParameter("@6", data.FileName),			// [ファイル名]
													new SqlParameter("@7", (data.FileDateTime.HasValue) ? data.FileDateTime.Value.ToString() : System.Data.SqlTypes.SqlString.Null),	// [ファイル更新日時]
													new SqlParameter("@8", DateTime.Now) };			// [更新日]

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoDocumentIndexList() Error!");
								}
								i++;
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
		/// [JunpDB].[dbo].[tMic文書インデクス]の全削除
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int DeleteDocumentIndex(bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							string sqlString = @"DELETE FROM tMic文書インデクス";

							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString);
							if (rowCount <= -1)
							{
								throw new ApplicationException("DeleteUserRegister() Error!");
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


	}
}
