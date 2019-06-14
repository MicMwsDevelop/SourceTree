using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.NarcohmOrderCheck;
using System.Data;

namespace MwsLib.DB.SqlServer.NarcohmOrderCheck
{
    public static class NarcohmOrderCheckAccess
    {
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 電話番号に該当する医院情報の取得
		/// </summary>
		/// <param name="tel">電話番号</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客情報リスト</returns>
		public static CustomerInfo GetCustomerInfo(string tel, bool sqlsv2)
        {
            DataTable table = NarcohmOrderCheckGetIO.GetCustomerInfo(tel, sqlsv2);
            List<CustomerInfo> list =  NarcohmOderCheckController.ConvertCustomerInfo(table);
            if (0 < list.Count)
            {
                return list[0];
            }
            return null;
        }

		/// <summary>
		/// ナルコーム製品受注情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="goodsID">商品コード</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客情報リスト</returns>
		public static List<NarcohmOrderInfo> GetNarcohmOrderInfo(int customerNo, string goodsID, bool sqlsv2)
		{
			DataTable table = NarcohmOrderCheckGetIO.GetNarcohmOrderInfo(customerNo, goodsID, sqlsv2);
			return NarcohmOderCheckController.ConvertNarcohmOrderInfo(table);
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ナルコーム申込情報番号の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>ナルコーム申込情報番号</returns>
		public static int GetNarcohmApplicateID(int customerNo, bool sqlsv2)
		{
			DataTable table = NarcohmOrderCheckGetIO.GetNarcohmApplicateID(customerNo, sqlsv2);
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					return DataBaseValue.ConvObjectToInt(table.Rows[0]["ApplicateID"]);
				}
			}
			return -1;
		}

		/// <summary>
		/// ナルコーム申込情報の追加
		/// </summary>
		/// <param name="data">ナルコーム申込情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>判定</returns>
		public static int AddNewNarcohmApplicate(NarcohmApplicate data, bool sqlsv2)
		{
			return NarcohmOrderCheckSetIO.InsertIntoNarcohmApplicate(data, sqlsv2);
		}

		/// <summary>
		/// ナルコーム申込情報の変更
		/// </summary>
		/// <param name="data">ナルコーム申込情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>判定</returns>
		public static int UpdateNarcohmApplicate(NarcohmApplicate data, bool sqlsv2)
		{
			return NarcohmOrderCheckSetIO.UpdateNarcohmApplicate(data, sqlsv2);
		}

		/// <summary>
		/// ナルコーム申込情報リストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>ナルコーム申込情報リスト</returns>
		public static List<NarcohmApplicate> GetNarcohmApplicateList(bool sqlsv2)
		{
			DataTable tableHeader = NarcohmOrderCheckGetIO.GetNarcohmApplicateHeaderList(sqlsv2);
			if (null != tableHeader)
			{
				if (0 < tableHeader.Rows.Count)
				{
					DataTable tableDetail = NarcohmOrderCheckGetIO.GetNarcohmApplicateDetailList(sqlsv2);
					if (0 < tableDetail.Rows.Count)
					{
						List<NarcohmApplicate> headerList = NarcohmOderCheckController.ConvertNarcohmApplicateHeader(tableHeader);
						List<NarcohmApplicateDetail> detailList = NarcohmOderCheckController.ConvertNarcohmApplicateDetail(tableDetail);
						foreach (NarcohmApplicate header in headerList)
						{
							header.DetailList = detailList.FindAll(p => header.ApplicateID == p.ApplicateID);
						}
						return headerList;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// ナルコーム製品標準価格の取得
		/// </summary>
		/// <param name="goodsCode">商品ID</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>標準価格</returns>
		public static int GetNarcohmProductPrice(string goodsCode, bool sqlsv2)
		{
			DataTable table = NarcohmOrderCheckGetIO.GetNarcohmProductPrice(goodsCode, sqlsv2);
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					return DataBaseValue.ConvObjectToInt(table.Rows[0]["標準価格"]);
				}
			}
			return 0;
		}
	}
}
