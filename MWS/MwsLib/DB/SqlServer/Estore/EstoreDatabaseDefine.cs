using MwsLib.Common;

namespace MwsLib.DB.SqlServer.Estore
{
	public static class EstoreDatabaseDefine
	{
		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			tMICestore_log = 1,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.tMICestore_log, "tMICestore_log" },
		};
	}
}
