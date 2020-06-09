using MwsLib.Common;

namespace MwsLib.DB.SqlServer.PCA
{
	public static class PcaDatabaseDefine
	{
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
			{ TableType.JUCD, "JUCD" },
		};
	}
}
