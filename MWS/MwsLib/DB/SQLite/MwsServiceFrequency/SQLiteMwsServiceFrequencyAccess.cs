using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.MwsServiceFrequency;
using MwsLib.Common;

namespace MwsLib.DB.SQLite.MwsServiceFrequency
{
	public static class SQLiteMwsServiceFrequencyAccess
	{
		public static int SetMwsServiceFrequencyDataList(string dbPath, MwsServiceFrequencyDataList list)
		{
			return SQLiteMwsServiceFrequencySetIO.InsertIntoMwsServiceFrequencyDataList(dbPath, list);
		}

		public static int DeleteAllMwsServiceFrequencyData(string dbPath, YearMonth ym)
		{
			return SQLiteMwsServiceFrequencySetIO.DeleteAllMwsServiceFrequencyData(dbPath, ym);
		}
	}
}
