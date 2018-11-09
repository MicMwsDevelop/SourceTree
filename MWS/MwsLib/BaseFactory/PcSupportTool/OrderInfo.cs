using MwsLib.Common;

namespace MwsLib.BaseFactory.PcSupportTool
{
	/// <summary>
	/// 受注情報
	/// </summary>
	public class OrderInfo
	{
		/// <summary>
		/// 受注No
		/// </summary>
		public string OrderNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 医院名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 料金
		/// </summary>
		public string PriceStr { get; set; }

		/// <summary>
		/// 拠店ID
		/// </summary>
		public string BranchID { get; set; }

		/// <summary>
		/// 拠店名
		/// </summary>
		public string BranchName { get; set; }

		/// <summary>
		/// 担当者ID
		/// </summary>
		public string SalesmanID { get; set; }

		/// <summary>
		/// 担当者名
		/// </summary>
		public string SalesmanName { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public Date? OrderDate { get; set; }

		/// <summary>
		/// 受注承認日
		/// </summary>
		public Date? OrderApprovalDate { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 契約年数の取得
		/// </summary>
		public int AgreeYear
		{
			get
			{
				if (PcSupportGoodsInfo.PC_SUPPORT3_GOODS_ID == GoodsID)
				{
					return 3;
				}
				return 1;
			}
		}

		/// <summary>
		/// 料金の取得
		/// </summary>
		public int Price
		{
			get
			{
				double price = double.Parse(PriceStr);
				return (int)price;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OrderInfo()
		{
			OrderNo = string.Empty;
			CustomerNo = 0;
			ClinicName = string.Empty;
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			PriceStr = string.Empty;
			BranchID = string.Empty;
			BranchName = string.Empty;
			SalesmanID = string.Empty;
			SalesmanName = string.Empty;
			OrderDate = null;
			OrderApprovalDate = null;
			Remark = string.Empty;
		}
	}
}
