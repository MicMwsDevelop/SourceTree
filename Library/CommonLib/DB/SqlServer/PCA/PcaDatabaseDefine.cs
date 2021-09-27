using CommonLib.Common;

namespace CommonLib.DB.SqlServer.PCA
{
	public static class PcaDatabaseDefine
	{
		/// <summary>
		/// データベース名
		/// </summary>
		static private string DatabaseName = "P10V01C001KON0001.dbo";

		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			JUCD = 1,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.JUCD, string.Format("{0}.JUCD", DatabaseName) },
		};
	}
}
