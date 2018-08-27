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
		public static List<EntryFinishedUserData> GetEntryFinishedUserDataList()
		{
			DataTable table = EntryFinishedUserDataGetIO.GetEntryFinishedUserDataList();
			return EntryFinishedUserDataController.ConvertEntryFinishedUserDataList(table);
		}

		public static EntryFinishedUserData GetEntryFinishedUserData(string tokuisakiID)
		{
			DataTable table = EntryFinishedUserDataGetIO.GetEntryFinishedUserDataList(tokuisakiID);
			List<EntryFinishedUserData> list = EntryFinishedUserDataController.ConvertEntryFinishedUserDataList(table);
			if (0 < list.Count)
			{
				return list[0];
			}
			return null;
		}
	}
}
