using MwsLib.Common;
using System;

namespace MwsLib.BaseFactory.PcSafetySupport
{
	/// <summary>
	/// PC安心サポート管理情報
	/// [Charlie].[dbo].[T_PC_SUPPORT_CONTORL]
	/// </summary>
	[Serializable]
	public class PcSupportControl : IEquatable<PcSupportControl>
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
		/// 備考１
		/// </summary>
		public string Remark1 { get; set; }

		/// <summary>
		/// 備考２
		/// </summary>
		public string Remark2 { get; set; }

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
		/// 作成日
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
			StartDate = null;
			EndDate = null;
			PeriodEndDate = null;
			AgreeYear = 0;
			Price = 0;
			SalesmanID = string.Empty;
			BranchID = string.Empty;
			OrderDate = null;
			OrderReportAccept = false;
			OrderApprovalDate = null;
			MailAddress = string.Empty;
			Remark1 = string.Empty;
			Remark2 = string.Empty;
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

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="other">PcSupportControl</param>

		public PcSupportControl(PcSupportControl other)
		{
			OrderNo = other.OrderNo;
			CustomerNo = other.CustomerNo;
			ClinicName = other.ClinicName;
			GoodsID = other.GoodsID;
			StartDate = other.StartDate;
			EndDate = other.EndDate;
			PeriodEndDate = other.PeriodEndDate;
			AgreeYear = other.AgreeYear;
			Price = other.Price;
			SalesmanID = other.SalesmanID;
			BranchID = other.BranchID;
			OrderDate = other.OrderDate;
			OrderReportAccept = other.OrderReportAccept;
			OrderApprovalDate = other.OrderApprovalDate;
			MailAddress = other.MailAddress;
			Remark1 = other.Remark1;
			Remark2 = other.Remark2;
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
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するPcSupportControl</param>
		/// <returns>判定</returns>
		public bool Equals(PcSupportControl other)
		{
			if (null != other)
			{
				if (OrderNo != other.OrderNo)
					return false;
				if (CustomerNo != other.CustomerNo)
					return false;
				if (ClinicName != other.ClinicName)
					return false;
				if (GoodsID != other.GoodsID)
					return false;
				if (StartDate != other.StartDate)
					return false;
				if (EndDate != other.EndDate)
					return false;
				if (PeriodEndDate != other.PeriodEndDate)
					return false;
				if (AgreeYear != other.AgreeYear)
					return false;
				if (Price != other.Price)
					return false;
				if (SalesmanID != other.SalesmanID)
					return false;
				if (BranchID != other.BranchID)
					return false;
				if (OrderDate != other.OrderDate)
					return false;
				if (OrderReportAccept != other.OrderReportAccept)
					return false;
				if (OrderApprovalDate != other.OrderApprovalDate)
					return false;
				if (MailAddress != other.MailAddress)
					return false;
				if (Remark1 != other.Remark1)
					return false;
				if (Remark2 != other.Remark2)
					return false;
				if (StartMailDateTime != other.StartMailDateTime)
					return false;
				if (GuideMailDateTime != other.GuideMailDateTime)
					return false;
				if (UpdateMailDateTime != other.UpdateMailDateTime)
					return false;
				if (CancelDate != other.CancelDate)
					return false;
				if (CancelReportAccept != other.CancelReportAccept)
					return false;
				if (CancelReason != other.CancelReason)
					return false;
				if (DisableFlag != other.DisableFlag)
					return false;
				if (WonderWebRenewalFlag != other.WonderWebRenewalFlag)
					return false;
				if (CreateDateTime != other.CreateDateTime)
					return false;
				if (CreatePerson != other.CreatePerson)
					return false;
				if (UpdateDateTime != other.UpdateDateTime)
					return false;
				if (UpdatePerson != other.UpdatePerson)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するPcSupportControlオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is PcSupportControl)
			{
				return this.Equals((PcSupportControl)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return OrderNo + CustomerNo.ToString() + ClinicName + GoodsID + SalesmanID + BranchID + MailAddress + Remark1 + Remark2 + CancelReason + CreatePerson + UpdatePerson;
		}

		/// <summary>
		/// 契約開始日と契約年数に対する契約終了日の取得
		/// </summary>
		/// <param name="start">契約開始日</param>
		/// <param name="agreeYear">契約年数</param>
		/// <returns>契約終了日</returns>
		public static Date GetEndDate(Date start, int agreeYear)
		{
			Date end = start.PlusMonths((agreeYear * 12) - 1);
			return new Date(end.Year, end.Month, end.GetDaysInMonth());
		}

		/// <summary>
		/// 申込情報が揃っているかどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsAcceptComputeData(out string msg)
		{
			msg = string.Empty;
			if (0 == OrderNo.Length)
			{
				msg = "受注Noが設定されていません。";
				return false;
			}
			if (0 == CustomerNo)
			{
				msg = "顧客Noが設定されていません。";
				return false;
			}
			if (0 == GoodsID.Length)
			{
				msg = "商品が設定されていません。";
				return false;
			}
			if (false == StartDate.HasValue)
			{
				msg = "契約開始日が設定されていません。";
				return false;
			}
			if (false == EndDate.HasValue)
			{
				msg = "契約終了日が設定されていません。";
				return false;
			}
			if (0 == AgreeYear)
			{
				msg = "契約年数が設定されていません。";
				return false;
			}
			if (0 == Price)
			{
				msg = "料金が設定されていません。";
				return false;
			}
			if (0 == BranchID.Length)
			{
				msg = "拠店名が設定されていません。";
				return false;
			}
			if (0 == SalesmanID.Length)
			{
				msg = "担当営業員が設定されていません。";
				return false;
			}
			if (false == OrderApprovalDate.HasValue)
			{
				msg = "受注承認日が設定されていません。";
				return false;
			}
			if (0 == MailAddress.Length)
			{
				msg = "メールアドレスが設定されていません。";
				return false;
			}
			return true;
		}

		/// <summary>
		/// 開始メール送信対象かどうか？
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>判定</returns>
		public bool IsSendStartMail(Date date)
		{
			if (DisableFlag)
				return false;
			if (OrderApprovalDate.HasValue)
				return false;
			if (StartMailDateTime.HasValue)
				return false;
			if (date < StartDate.Value)
				return false;
			string msg;
			return this.IsAcceptComputeData(out msg);
		}

		/// <summary>
		/// 契約更新案内メール送信対象かどうか？
		/// 送信条件：契約終了日の２か月前
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>判定</returns>
		public bool IsSendGuideMail(Date date)
		{
			if (DisableFlag)
				return false;
			if (false == PeriodEndDate.HasValue && false == GuideMailDateTime.HasValue)
			{
				if (EndDate.HasValue)
				{
					Date guideDate = EndDate.Value.PlusMonths(-2);
					guideDate = new Date(guideDate.Year, guideDate.Month, 1);
					if (guideDate <= date)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 契約更新メール送信対象かどうか？
		/// 送信条件：契約終了月の来月の初日以降
		/// </summary>
		/// <param name="date">当日</param>
		/// <returns>判定</returns>
		public bool IsSendUpdateMail(Date date)
		{
			if (DisableFlag)
				return false;
			if (false == PeriodEndDate.HasValue && false == UpdateMailDateTime.HasValue)
			{
				if (EndDate.HasValue)
				{
					Date updateDate = EndDate.Value.PlusMonths(1);
					updateDate = new Date(updateDate.Year, updateDate.Month, 1);
					if (updateDate <= date)
					{
						return true;
					}
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
			if (Remark1 != prev.Remark1)
				return true;
			if (Remark2 != prev.Remark2)
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
			contract.Remark1 = Remark1;
			contract.Remark2 = Remark2;
			return contract;
		}
	}
}

