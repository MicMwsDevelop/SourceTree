using MwsLib.Common;
using System;

namespace MwsLib.BaseFactory.PcSafetySupport
{
	/// <summary>
	/// PC安心サポート管理情報（T_PC_SUPPORT_CONTORL）
	/// </summary>
	[Serializable]
	public class PcSupportControl : IEquatable<PcSupportControl>
	{
		/// <summary>
		/// 顧客ID
		/// </summary>
		public string CustomerID { get; set; }

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
		/// 申込日時
		/// </summary>
		public Date? ApplyDate { get; set; }

		/// <summary>
		/// 申込用紙有無
		/// </summary>
		public bool ApplyReportAccept { get; set; }

		/// <summary>
		/// メールアドレス
		/// </summary>
		public string MaleAddress { get; set; }

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
		public Date? StartMaleDate { get; set; }

		/// <summary>
		/// 契約更新案内メール送信日時
		/// </summary>
		public Date? GuideMaleDate { get; set; }

		/// <summary>
		/// 契約更新メール送信日時
		/// </summary>
		public Date? UpdateMaleDate { get; set; }

		/// <summary>
		/// 解約日時
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
		/// 作成日時
		/// </summary>
		public Date? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreaatePerson { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public Date? UpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// WonderWeb更新フラグ
		/// </summary>
		public bool WonderWebRenewalFlag { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportControl()
		{
			CustomerID = string.Empty;
			GoodsID = string.Empty;
			StartDate = null;
			EndDate = null;
			PeriodEndDate = null;
			AgreeYear = 0;
			Price = 0;
			SalesmanID = string.Empty;
			BranchID = string.Empty;
			ApplyDate = null;
			ApplyReportAccept = false;
			MaleAddress = string.Empty;
			Remark1 = string.Empty;
			Remark2 = string.Empty;
			StartMaleDate = null;
			GuideMaleDate = null;
			UpdateMaleDate = null;
			CancelDate = null;
			CancelReportAccept = false;
			CancelReason = string.Empty;
			CreateDate = null;
			CreaatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
			WonderWebRenewalFlag = false;
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
				if (CustomerID != other.CustomerID)
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
				if (ApplyDate != other.ApplyDate)
					return false;
				if (ApplyReportAccept != other.ApplyReportAccept)
					return false;
				if (MaleAddress != other.MaleAddress)
					return false;
				if (Remark1 != other.Remark1)
					return false;
				if (Remark2 != other.Remark2)
					return false;
				if (StartMaleDate != other.StartMaleDate)
					return false;
				if (GuideMaleDate != other.GuideMaleDate)
					return false;
				if (UpdateMaleDate != other.UpdateMaleDate)
					return false;
				if (CancelDate != other.CancelDate)
					return false;
				if (CancelReportAccept != other.CancelReportAccept)
					return false;
				if (CancelReason != other.CancelReason)
					return false;
				if (CreateDate != other.CreateDate)
					return false;
				if (CreaatePerson != other.CreaatePerson)
					return false;
				if (UpdateDate != other.UpdateDate)
					return false;
				if (UpdatePerson != other.UpdatePerson)
					return false;
				if (WonderWebRenewalFlag != other.WonderWebRenewalFlag)
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
			return CustomerID + GoodsID + SalesmanID + BranchID + MaleAddress + Remark1 + Remark2 + CancelReason + CreaatePerson + UpdatePerson;
		}
	}
}

