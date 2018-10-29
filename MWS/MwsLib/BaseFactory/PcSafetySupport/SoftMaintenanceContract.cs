using MwsLib.Common;

namespace MwsLib.BaseFactory.PcSafetySupport
{
	/// <summary>
	/// ソフト保守メンテナンス情報
	/// [JunpDB].[dbo].[tMik保守契約]
	/// </summary>
	public class SoftMaintenanceContract
	{
		/// <summary>
		/// 顧客ID
		/// </summary>
		public string CustomerID { get; set; }

		/// <summary>
		/// fhsS保守
		/// </summary>
		public bool Subscription { get; set; }

		/// <summary>
		/// fhsS契約書回収年月
		/// </summary>
		public Date? CollectionDate { get; set; }

		/// <summary>
		/// fhsS契約年数
		/// </summary>
		public int AgreeYear { get; set; }

		/// <summary>
		/// fhsSメンテ料金
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// fhsSメンテ契約開始
		/// </summary>
		public Date? StartDate { get; set; }

		/// <summary>
		/// fhsSメンテ契約終了
		/// </summary>
		public Date? EndDate { get; set; }

		/// <summary>
		/// fhsSメンテ契約備考1
		/// </summary>
		public string Remark1 { get; set; }

		/// <summary>
		/// fhsSメンテ契約備考2
		/// </summary>
		public string Remark2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SoftMaintenanceContract()
		{
			CustomerID = string.Empty;
			Subscription = false;
			CollectionDate = null;
			AgreeYear = 0;
			Price = 0;
			StartDate = null;
			EndDate = null;
			Remark1 = string.Empty;
			Remark2 = string.Empty;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するEstimate</param>
		/// <returns>判定</returns>
		public bool Equals(SoftMaintenanceContract other)
		{
			if (null != other)
			{
				if (CustomerID != other.CustomerID)
					return false;
				if (Subscription != other.Subscription)
					return false;
				if (CollectionDate != other.CollectionDate)
					return false;
				if (AgreeYear != other.AgreeYear)
					return false;
				if (Price != other.Price)
					return false;
				if (StartDate != other.StartDate)
					return false;
				if (EndDate != other.EndDate)
					return false;
				if (Remark1 != other.Remark1)
					return false;
				if (Remark2 != other.Remark2)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するSoftMaintenanceContractオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is SoftMaintenanceContract)
			{
				return this.Equals((SoftMaintenanceContract)obj);
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
			return CustomerID + Subscription.ToString() + AgreeYear.ToString() + Price.ToString() + Remark1 + Remark2;
		}
	}
}
