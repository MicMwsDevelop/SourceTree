using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using MwsLib.BaseFactory.WonderWebMemo;
using System.IO;

namespace MwsLib.DB.SQLite.WonderWebMemo
{
	public static class SQLiteWonderWebMemoAccess
	{
		/// <summary>
		/// 銀行振込請求書発行先メモ情報の取得
		/// </summary>
		/// <returns>銀行振込請求書発行先メモ情報</returns>
		public static List<MemoBankTransfer> GetMemoBankTransfer()
		{
			DataTable table = SQLiteWonderWebMemoGetIO.GetMemoBankTransfer(Directory.GetCurrentDirectory());
			return SQLiteWonderWebMemoController.ConvertBankTransfer(table);
		}
	}
}
