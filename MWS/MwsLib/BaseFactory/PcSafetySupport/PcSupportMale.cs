using MwsLib.Common;
using System;

namespace MwsLib.BaseFactory.PcSafetySupport
{
	/// <summary>
	/// PC安心サポート送信メール情報
	/// [Charlie].[dbo].[T_PC_SUPPORT_MALE]
	/// </summary>
	[Serializable]
	public class PcSupportMale : IEquatable<PcSupportMale>
	{
		/// <summary>
		/// 送信メール種別
		/// </summary>
		public enum MaleType
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
		public int MaleNo { get; set; }

		/// <summary>
		/// 送信メール種別
		/// </summary>
		public MaleType SendMaleType { get; set; }

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
		/// メールアドレス
		/// </summary>
		public string MaleAddress { get; set; }

		/// <summary>
		/// 送信日時
		/// </summary>
		public Date? SendDate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportMale()
		{
			MaleNo = 0;
			SendMaleType = MaleType.None;
			CustomerID = string.Empty;
			GoodsID = string.Empty;
			StartDate = null;
			EndDate = null;
			AgreeYear = 0;
			Price = 0;
			SalesmanID = string.Empty;
			BranchID = string.Empty;
			ApplyDate = null;
			MaleAddress = string.Empty;
			SendDate = null;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するPcSupportMale</param>
		/// <returns>判定</returns>
		public bool Equals(PcSupportMale other)
		{
			if (null != other)
			{
				if (MaleNo != other.MaleNo)
					return false;
				if (SendMaleType != other.SendMaleType)
					return false;
				if (CustomerID != other.CustomerID)
					return false;
				if (GoodsID != other.GoodsID)
					return false;
				if (StartDate != other.StartDate)
					return false;
				if (EndDate != other.EndDate)
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
				if (MaleAddress != other.MaleAddress)
					return false;
				if (SendDate != other.SendDate)
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
			return CustomerID + GoodsID + SalesmanID + BranchID + MaleAddress;
		}
	}
}
