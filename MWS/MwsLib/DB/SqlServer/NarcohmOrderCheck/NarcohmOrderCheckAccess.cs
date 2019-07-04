using MwsLib.BaseFactory.NarcohmOrderCheck;
using System.Collections.Generic;
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
