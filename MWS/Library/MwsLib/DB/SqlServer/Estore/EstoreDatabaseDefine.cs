using MwsLib.Common;

namespace MwsLib.DB.SqlServer.Estore
{
	public static class EstoreDatabaseDefine
	{
		/// <summary>
		/// データベース名
		/// </summary>
		static private string DatabaseName = "estoreDB.dbo";

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
			{ TableType.tMICestore_log, string.Format("{0}.tMICestore_log", DatabaseName) },
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			vMic部門コード = 1,
			vMic受注最大番号 = 2,
			vMicOrder_accept = 3,
			vMic顧客マスタ = 4,
			vMic商品マスタ = 5,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.vMic部門コード, string.Format("{0}.vMic部門コード", DatabaseName) },
			{ ViewType.vMic受注最大番号, string.Format("{0}.vMic受注最大番号", DatabaseName) },
			{ ViewType.vMicOrder_accept, string.Format("{0}.vMicOrder_accept", DatabaseName) },
			{ ViewType.vMic顧客マスタ, string.Format("{0}.vMic顧客マスタ", DatabaseName) },
			{ ViewType.vMic商品マスタ, string.Format("{0}.vMic商品マスタ", DatabaseName) },
		};
	}
}
