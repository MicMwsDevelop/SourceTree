using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.EntryFinishedUser;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserDataAccess
	{
		public static List<EntryFinishedUserData> GetEntryFinishedUserDataList(bool sqlsv2)
		{
			DataTable table = EntryFinishedUserDataGetIO.GetEntryFinishedUserDataList(sqlsv2);
			return EntryFinishedUserDataController.ConvertEntryFinishedUserDataList(table);
		}

		public static EntryFinishedUserData GetEntryFinishedUserData(string tokuisakiID, bool sqlsv2)
		{
			DataTable table = EntryFinishedUserDataGetIO.GetEntryFinishedUserDataList(sqlsv2, tokuisakiID);
			List<EntryFinishedUserData> list = EntryFinishedUserDataController.ConvertEntryFinishedUserDataList(table);
			if (0 < list.Count)
			{
				return list[0];
			}
			return null;
		}
	}
}
