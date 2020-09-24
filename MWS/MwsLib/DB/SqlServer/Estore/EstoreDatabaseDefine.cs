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
			{ ViewType.vMic部門コード, "vMic部門コード" },
			{ ViewType.vMic受注最大番号, "vMic受注最大番号" },
			{ ViewType.vMicOrder_accept, "vMicOrder_accept" },
			{ ViewType.vMic顧客マスタ, "vMic顧客マスタ" },
			{ ViewType.vMic商品マスタ, "vMic商品マスタ" },
		};
	}
}
