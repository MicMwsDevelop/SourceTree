using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.ScanImageData;

namespace MwsLib.DB.SqlServer.ScanImageData
{
	public static class ScanImageDataController
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		///  顧客情報リストの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>顧客情報リスト</returns>
		public static List<ScanImageDataFileInfo> ConvertCustomerInfoList(DataTable table)
		{
			List<ScanImageDataFileInfo> result = null;
			if (null != table)
			{
				result = new List<ScanImageDataFileInfo>();
				foreach (DataRow row in table.Rows)
				{
					ScanImageDataFileInfo data = new ScanImageDataFileInfo();
					data.TokuisakiNo = row["得意先No"].ToString().Trim();
					data.CustomerNo = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.ClinicName = row["顧客名"].ToString().Trim();
					result.Add(data);
				}
			}
			return result;
		}
	}
}
