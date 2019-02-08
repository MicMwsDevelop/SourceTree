//
// ApplyTypeMatomeGetIO.cs
//
// 申込種別まとめ情報 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// 
using MwsLib.Common;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.ApplyTypeMatome
{
	public static class ApplyTypeMatomeGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 申込種別まとめ情報リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetApplyMatomeList(Date date, bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					Date nextMonth = date.PlusMonths(1);
					YearMonth ym = nextMonth.ToYearMonth();
					nextMonth = new Date(ym.Year, ym.Month, ym.GetDays());

					string strSQL = string.Format(@"SELECT HD.fCustomerID, MWS.顧客名, MWS.支店名, MWS.営業担当者名, HD.fTotalAmount, HD.fApplyDate, HD.fMonths, HD.fContractType, HD.fContractStartDate, CF.APPLY_TYPE"
									+ " FROM T_USE_CONTRACT_HEADER AS HD"
									+ " LEFT JOIN T_CUSTOMER_FOUNDATIONS AS CF"
									+ " ON HD.fCustomerID = CF.CUSTOMER_ID"
									+ " LEFT JOIN view_MWSユーザー AS MWS"
									+ " ON HD.fCustomerID = MWS.顧客ID"
									+ " WHERE fContractType = 'まとめ' AND CONVERT(date, fContractStartDate) <= CONVERT(date, '{0}', 112) AND fEndFlag = '0' AND APPLY_TYPE <> '4'"
									+ " ORDER BY fContractStartDate ASC, fCustomerID ASC", nextMonth.ToIntYMD()); 

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
