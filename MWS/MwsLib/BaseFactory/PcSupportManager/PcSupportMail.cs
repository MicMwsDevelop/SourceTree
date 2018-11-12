using MwsLib.Common;
using System;

namespace MwsLib.BaseFactory.PcSupportManager
{
	/// <summary>
	/// PC安心サポート送信メール情報
	/// [Charlie].[dbo].[T_PC_SUPPORT_MAIL]
	/// </summary>
	public class PcSupportMail
	{
		/// <summary>
		/// 送信メール種別
		/// </summary>
		public enum MailType
		{
			/// <summary>初期値</summary>
			None = 0,
			/// <summary>開始メール</summary>
			Start,
			/// <summary>契約更新案内メール</summary>
			Guide,
			/// <summary>契約更新メール</summary>
			Update,
		};

		/// <summary>
		/// メールNo
		/// </summary>
		public int MailNo { get; set; }

		/// <summary>
		/// 送信メール種別
		/// </summary>
		public MailType SendMailType { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 受注No
		/// </summary>
		public string OrderNo { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public Date? StartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public Date? EndDate { get; set; }

		/// <summary>
		/// 契約年数
		/// </summary>
		public int AgreeYear { get; set; }

		/// <summary>
		/// 料金
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 営業担当者ID
		/// </summary>
		public string SalesmanID { get; set; }

		/// <summary>
		/// 支店ID
		/// </summary>
		public string BranchID { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public Date? OrderDate { get; set; }

		/// <summary>
		/// メールアドレス
		/// </summary>
		public string MailAddress { get; set; }

		/// <summary>
		/// 送信日時
		/// </summary>
		public DateTime? SendDateTime { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportMail()
		{
			MailNo = 0;
			SendMailType = MailType.None;
			CustomerNo = 0;
			OrderNo = string.Empty;
			GoodsID = string.Empty;
			StartDate = null;
			EndDate = null;
			AgreeYear = 0;
			Price = 0;
			SalesmanID = string.Empty;
			BranchID = string.Empty;
			OrderDate = null;
			MailAddress = string.Empty;
			SendDateTime = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="mailType">メール種別</param>
		/// <param name="pc">PC安心サポート管理情報</param>
		public PcSupportMail(MailType mailType, PcSupportControl pc)
		{
			MailNo = 0;
			SendMailType = mailType;
			CustomerNo = pc.CustomerNo;
			OrderNo = pc.OrderNo;
			GoodsID = pc.GoodsID;
			StartDate = pc.StartDate;
			EndDate = pc.EndDate;
			AgreeYear = pc.AgreeYear;
			Price = pc.Price;
			SalesmanID = pc.SalesmanID;
			BranchID = pc.BranchID;
			OrderDate = pc.OrderDate;
			MailAddress = pc.MailAddress;
			SendDateTime = null;
		}
	}
}
