using MwsLib.Common;
using System;

namespace MwsLib.BaseFactory.PcSupportManager
{
	/// <summary>
	/// PC安心サポート管理情報
	/// [Charlie].[dbo].[T_PC_SUPPORT_CONTORL]
	/// </summary>
	public class PcSupportControl
	{
		/// <summary>
		/// 作成者
		/// </summary>
		public const string PERSON_NAME = "PC安心サポート管理";

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
		public int Price { get; set; }

		/// <summary>
		/// 契約年数
		/// </summary>
		public int AgreeYear { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public Date? StartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public Date? EndDate { get; set; }

		/// <summary>
		/// 利用期限日
		/// </summary>
		public Date? PeriodEndDate { get; set; }

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
		/// 申込用紙有無
		/// </summary>
		public bool OrderReportAccept { get; set; }

		/// <summary>
		/// 受注承認日
		/// </summary>
		public Date? OrderApprovalDate { get; set; }

		/// <summary>
		/// メールアドレス
		/// </summary>
		public string MailAddress { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 開始メール送信日時
		/// </summary>
		public DateTime? StartMailDateTime { get; set; }

		/// <summary>
		/// 契約更新案内メール送信日時
		/// </summary>
		public DateTime? GuideMailDateTime { get; set; }

		/// <summary>
		/// 契約更新メール送信日時
		/// </summary>
		public DateTime? UpdateMailDateTime { get; set; }

		/// <summary>
		/// 解約日
		/// </summary>
		public Date? CancelDate { get; set; }

		/// <summary>
		/// 解約届有無
		/// </summary>
		public bool CancelReportAccept { get; set; }

		/// <summary>
		/// 解約事由
		/// </summary>
		public string CancelReason { get; set; }

		/// <summary>
		/// 無効フラグ
		/// </summary>
		public bool DisableFlag { get; set; }

		/// <summary>
		/// WonderWeb更新フラグ
		/// </summary>
		public bool WonderWebRenewalFlag { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CreateDateTime { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? UpdateDateTime { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportControl()
		{
			OrderNo = string.Empty;
			CustomerNo = 0;
			ClinicName = string.Empty;
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			Price = 0;
			AgreeYear = 0;
			StartDate = null;
			EndDate = null;
			PeriodEndDate = null;
			BranchID = string.Empty;
			BranchName = string.Empty;
			SalesmanID = string.Empty;
			SalesmanName = string.Empty;
			OrderDate = null;
			OrderReportAccept = false;
			OrderApprovalDate = null;
			MailAddress = string.Empty;
			Remark = string.Empty;
			StartMailDateTime = null;
			GuideMailDateTime = null;
			UpdateMailDateTime = null;
			CancelDate = null;
			CancelReportAccept = false;
			CancelReason = string.Empty;
			DisableFlag = false;
			WonderWebRenewalFlag = false;
			CreateDateTime = null;
			CreatePerson = string.Empty;
			UpdateDateTime = null;
			UpdatePerson = string.Empty;
		}

		/// </summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="other">PC安心サポート管理情報</param>

		public PcSupportControl(PcSupportControl other)
		{
			OrderNo = other.OrderNo;
			CustomerNo = other.CustomerNo;
			ClinicName = other.ClinicName;
			GoodsID = other.GoodsID;
			GoodsName = other.GoodsName;
			Price = other.Price;
			AgreeYear = other.AgreeYear;
			StartDate = other.StartDate;
			EndDate = other.EndDate;
			PeriodEndDate = other.PeriodEndDate;
			BranchID = other.BranchID;
			BranchName = other.BranchName;
			SalesmanID = other.SalesmanID;
			SalesmanName = other.SalesmanName;
			OrderDate = other.OrderDate;
			OrderReportAccept = other.OrderReportAccept;
			OrderApprovalDate = other.OrderApprovalDate;
			MailAddress = other.MailAddress;
			Remark = other.Remark;
			StartMailDateTime = other.StartMailDateTime;
			GuideMailDateTime = other.GuideMailDateTime;
			UpdateMailDateTime = other.UpdateMailDateTime;
			CancelDate = other.CancelDate;
			CancelReportAccept = other.CancelReportAccept;
			CancelReason = other.CancelReason;
			DisableFlag = other.DisableFlag;
			WonderWebRenewalFlag = other.WonderWebRenewalFlag;
			CreateDateTime = other.CreateDateTime;
			CreatePerson = other.CreatePerson;
			UpdateDateTime = other.UpdateDateTime;
			UpdatePerson = other.UpdatePerson;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="order">受注情報</param>
		/// <param name="mailAddress">メールアドレス</param>
		/// <param name="now">更新日時</param>
		public PcSupportControl(OrderInfo order, string mailAddress, Date now)
		{
			OrderNo = order.OrderNo;
			CustomerNo = order.CustomerNo;
			ClinicName = order.ClinicName;
			GoodsID = order.GoodsID;
			GoodsName = order.GoodsName;
			Price = order.Price;
			AgreeYear = order.AgreeYear;
			BranchID = order.BranchID;
			BranchName = order.BranchName;
			SalesmanID = order.SalesmanID;
			SalesmanName = order.SalesmanName;
			OrderDate = order.OrderDate;
			OrderApprovalDate = order.OrderApprovalDate;
			MailAddress = mailAddress;
			Remark = order.Remark;
			StartDate = null;
			EndDate = null;
			PeriodEndDate = null;
			OrderReportAccept = false;
			StartMailDateTime = null;
			GuideMailDateTime = null;
			UpdateMailDateTime = null;
			CancelDate = null;
			CancelReportAccept = false;
			CancelReason = string.Empty;
			DisableFlag = false;
			WonderWebRenewalFlag = false;
			CreateDateTime = now.ToDateTime();
			CreatePerson = PERSON_NAME;
			UpdateDateTime = null;
			UpdatePerson = string.Empty;
		}

		/// <summary>
		/// 受注情報からの変更があるかどうか？
		/// </summary>
		/// <param name="order">受注情報</param>
		/// <param name="mailAddress">メールアドレス</param>
		/// <returns>判定</returns>
		public bool IsUpdateOrderData(OrderInfo order, string mailAddress)
		{
			if (OrderNo != order.OrderNo)
				return true;
			if (CustomerNo != order.CustomerNo)
				return true;
			if (ClinicName != order.ClinicName)
				return true;
			if (GoodsID != order.GoodsID)
				return true;
			if (GoodsName != order.GoodsName)
				return true;
			if (Price != order.Price)
				return true;
			if (AgreeYear != order.AgreeYear)
				return true;
			if (BranchID != order.BranchID)
				return true;
			if (BranchName != order.BranchName)
				return true;
			if (SalesmanID != order.SalesmanID)
				return true;
			if (SalesmanName != order.SalesmanName)
				return true;
			if (OrderDate != order.OrderDate)
				return true;
			if (OrderApprovalDate != order.OrderApprovalDate)
				return true;
			if (0 == MailAddress.Length)
			{
				if (MailAddress != mailAddress)
					return true;
			}
			if (0 == Remark.Length)
			{
				if (Remark != order.Remark)
					return true;
			}
			return false;
		}

		/// <summary>
		/// 受注情報の格納
		/// </summary>
		/// <param name="order">受注情報</param>
		/// <param name="mailAddress">メールアドレス</param>
		/// <param name="now">更新日時</param>
		public void SetOrderInfo(OrderInfo order, string mailAddress, Date now)
		{
			bool wwFlag = false;
			if (GoodsID != order.GoodsID)
				wwFlag = true;
			if (OrderDate != order.OrderDate)
				wwFlag = true;
			if (Remark != order.Remark)
				wwFlag = true;
			OrderNo = order.OrderNo;
			CustomerNo = order.CustomerNo;
			ClinicName = order.ClinicName;
			GoodsID = order.GoodsID;
			GoodsName = order.GoodsName;
			Price = order.Price;
			AgreeYear = order.AgreeYear;
			BranchID = order.BranchID;
			BranchName = order.BranchName;
			SalesmanID = order.SalesmanID;
			SalesmanName = order.SalesmanName;
			OrderDate = order.OrderDate;
			OrderApprovalDate = order.OrderApprovalDate;
			if (null == MailAddress)
			{
				MailAddress = mailAddress;
			}
			else if (0 == MailAddress.Length)
			{
				MailAddress = mailAddress;
			}
			if (null == Remark)
			{
				Remark = order.Remark;
			}
			else if (0 == Remark.Length)
			{
				Remark = order.Remark;
			}
			if (false == WonderWebRenewalFlag)
			{
				WonderWebRenewalFlag = wwFlag;
			}
			UpdateDateTime = now.ToDateTime();
			UpdatePerson = PERSON_NAME;
		}

		/// <summary>
		/// 契約開始日と契約年数に対する契約終了日の取得
		/// </summary>
		/// <param name="start">契約開始日</param>
		/// <param name="agreeYear">契約年数</param>
		/// <returns>契約終了日</returns>
		public static Date GetEndDate(Date start, int agreeYear)
		{
			return start.PlusMonths((agreeYear * 12) - 1).ToYearMonth().Last;
		}

		/// <summary>
		/// 申込情報が揃っているかどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsOrderInfoCompleted(bool sendMail)
		{
			if (0 == OrderNo.Length)
				return false;
			if (0 == CustomerNo)
				return false;
			if (0 == GoodsID.Length)
				return false;
			if (false == StartDate.HasValue)
				return false;
			if (false == EndDate.HasValue)
				return false;
			if (0 == AgreeYear)
				return false;
			if (0 == Price)
				return false;
			if (0 == BranchID.Length)
				return false;
			if (0 == SalesmanID.Length)
				return false;
			if (false == OrderApprovalDate.HasValue)
				return false;
			if (sendMail)
			{
				if (0 == MailAddress.Length)
					return false;
			}
			return true;
		}

		/// <summary>
		/// 開始メール送信対象かどうか？
		/// 送信条件：契約開始日が当月末以前
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>判定</returns>
		public bool IsSendStartMail(Date date)
		{
			if (this.IsOrderInfoCompleted(true))
			{
				if (DisableFlag)
					return false;
				if (StartMailDateTime.HasValue)
					return false;
				if (PeriodEndDate.HasValue)
					return false;
				if (StartDate <= date.ToYearMonth().Last)
				{
					// 契約開始日が当月末まで
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 契約更新案内メール送信対象かどうか？
		/// 送信条件：契約終了月が当月の翌月
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>判定</returns>
		public bool IsSendGuideMail(Date date)
		{
			if (DisableFlag)
				return false;
			if (GuideMailDateTime.HasValue)
				return false;
			if (PeriodEndDate.HasValue)
				return false;
			if (EndDate.HasValue)
			{
				if (EndDate.Value.ToYearMonth() == date.PlusMonths(1).ToYearMonth())
				{
					// 契約終了月が当月の翌月
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 契約更新メール送信対象かどうか？
		/// 送信条件：契約終了月が先月
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>判定</returns>
		public bool IsSendUpdateMail(Date date)
		{
			if (DisableFlag)
				return false;
			if (UpdateMailDateTime.HasValue)
				return false;
			if (PeriodEndDate.HasValue)
				return false;
			if (EndDate.HasValue)
			{
				if (EndDate.Value.ToYearMonth() == date.PlusMonths(-1).ToYearMonth())
				{
					// 契約終了月が先月
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// WonderWebソフト保守メンテナンス情報の更新が必要かどうか？
		/// </summary>
		/// <param name="prev">過去データ</param>
		/// <returns>判定</returns>
		public bool IsWonderWebRenewal(PcSupportControl prev)
		{
			if (GoodsID != prev.GoodsID)
				return true;
			if (StartDate != prev.StartDate)
				return true;
			if (EndDate != prev.EndDate)
				return true;
			if (PeriodEndDate != prev.PeriodEndDate)
				return true;
			if (OrderDate != prev.OrderDate)
				return true;
			if (Remark != prev.Remark)
				return true;
			if (DisableFlag != prev.DisableFlag)
				return true;
			return false;
		}

		/// <summary>
		/// PC安心サポート管理情報からソフト保守メンテナンス情報を取得
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>ソフト保守メンテナンス情報</returns>
		public SoftMaintenanceContract GetSoftMaintenanceContract(Date date)
		{
			SoftMaintenanceContract contract = new SoftMaintenanceContract();
			contract.CustomerNo = CustomerNo;
			contract.Subscription = true;
			if (PeriodEndDate.HasValue)
			{
				if (PeriodEndDate <= date)
				{
					contract.Subscription = false;
				}
			}
			if (EndDate.HasValue)
			{
				if (EndDate <= date)
				{
					contract.Subscription = false;
				}
			}
			contract.CollectionDate = OrderDate;
			contract.AgreeYear = AgreeYear;
			contract.Price = Price;
			if (StartDate.HasValue)
			{
				contract.StartYM = StartDate.Value.ToYearMonth();
			}
			if (EndDate.HasValue)
			{
				contract.EndYM = EndDate.Value.ToYearMonth();
			}
			contract.Remark1 = Remark;
			return contract;
		}
	}
}

