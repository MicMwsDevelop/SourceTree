//
// PcSupportEarningsGetIO.cs
//
// PC安心サポート継続売上データ作成 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/02 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.PcSupportEarnings
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成  データ取得クラス
	/// </summary>
	public static class PcSupportEarningsGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客Noに売上データ必須情報の取得
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportEarningsOut(int customerID, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
							+ " U.顧客No as f顧客No"
							+ ", U.顧客名１ + U.顧客名２ as f顧客名"
							+ ", U.得意先No as f得意先コード"
							+ ", U.請求先コード as f請求先コード"
							+ ", S.sms_scd as f商品コード"
							+ ", S.sms_mei as f商品名"
							+ ", convert(int, S.sms_hyo) as f標準価格"
							+ ", convert(int, S.sms_gen) as f原単価"
							+ ", S.sms_tani as f単位"
							+ ", B.fPca部門コード as fPCA部門コード"
							+ ", B.fPca倉庫コード as fPCA倉庫コード"
							+ ", B.f担当者コード as fPCA担当者コード"
							+ " FROM {0} as U"
							+ " INNER JOIN {1} as S on S.sms_scd = '{2}'"
							+ " INNER JOIN {3} as B on B.fBshCode3 = U.支店コード"
							+ " WHERE U.顧客No = {4}"
						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー２]
						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
						, PcaGoodsIDDefine.PcSupport1Continue
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
						, customerID);

					using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
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
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return result;
		}
	}
}
