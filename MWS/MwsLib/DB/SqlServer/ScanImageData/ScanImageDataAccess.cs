using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.ScanImageData;

namespace MwsLib.DB.SqlServer.ScanImageData
{
	public static class ScanImageDataAccess
	{
		/// <summary>
		/// 顧客情報リストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>顧客情報Noリスト</returns>
		public static List<ScanImageDataFileInfo> GetCustomerInfoList(bool sqlsv2 = false)
		{
			DataTable table = ScanImageDataGetIO.GetCustomerInfoList(sqlsv2);
			return ScanImageDataController.ConvertCustomerInfoList(table);
		}
	}
}
