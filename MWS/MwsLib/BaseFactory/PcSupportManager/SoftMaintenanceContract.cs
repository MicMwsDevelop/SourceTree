using MwsLib.Common;

namespace MwsLib.BaseFactory.PcSupportManager
{
	/// <summary>
	/// ソフト保守メンテナンス情報
	/// [JunpDB].[dbo].[tMik保守契約]
	/// </summary>
	public class SoftMaintenanceContract
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

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
		public YearMonth? StartYM { get; set; }

		/// <summary>
		/// fhsSメンテ契約終了
		/// </summary>
		public YearMonth? EndYM { get; set; }

		/// <summary>
		/// fhsSメンテ契約備考1
		/// </summary>
		public string Remark1 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SoftMaintenanceContract()
		{
			CustomerNo = 0;
			Subscription = false;
			CollectionDate = null;
			AgreeYear = 0;
			Price = 0;
			StartYM = null;
			EndYM = null;
			Remark1 = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="control">PC安心サポート管理情報</param>
		/// <param name="subscription">fhsS保守</param>
		public SoftMaintenanceContract(PcSupportControl control, bool subscription)
		{
			CustomerNo = control.CustomerNo;
			Subscription = subscription;
			CollectionDate = control.OrderDate;
			AgreeYear = control.AgreeYear;
			Price = control.Price;
			StartYM = control.StartDate.Value.ToYearMonth();
			if (control.PeriodEndDate.HasValue)
			{
				EndYM = control.PeriodEndDate.Value.ToYearMonth();
			}
			else
			{
				EndYM = control.EndDate.Value.ToYearMonth();
			}
			Remark1 = control.Remark;
		}

		/// <summary>
		/// PC安心サポート管理情報から格納
		/// </summary>
		/// <param name="PcSupportControl">PC安心サポート管理情報</param>
		/// <param name="subscription">fhsS保守</param>
		public void SetPcSupportControl(PcSupportControl control, bool subscription)
		{
			Subscription = subscription;
			CollectionDate = control.OrderDate;
			AgreeYear = control.AgreeYear;
			Price = control.Price;
			StartYM = control.StartDate.Value.ToYearMonth();
			if (control.PeriodEndDate.HasValue)
			{
				EndYM = control.PeriodEndDate.Value.ToYearMonth();
			}
			else
			{
				EndYM = control.EndDate.Value.ToYearMonth();
			}
			Remark1 = control.Remark;
		}

		/// <summary>
		/// ログ出力文字列の取得
		/// </summary>
		/// <param name="pc">PC安心サポート管理情報</param>
		/// <returns>ログ出力文字列</returns>
		public string ToLog(PcSupportControl pc)
		{
			string[] log = new string[10];
			log[0] = CustomerNo.ToString();
			log[1] = pc.ClinicName;
			log[2] = (Subscription) ? "保守": "未保守";
			log[4] = (CollectionDate.HasValue) ? CollectionDate.ToString() : "";
			log[5] = AgreeYear.ToString();
			log[6] = Price.ToString();
			log[7] = (StartYM.HasValue) ? StartYM.ToString() : "";
			log[8] = (EndYM.HasValue) ? EndYM.ToString() : "";
			log[9] = Remark1;
			return string.Join(",", log);
		}

		/// <summary>
		/// ソフト保守メンテナンス情報を初期化
		/// </summary>
		public void Reset()
		{
			Subscription = false;
			CollectionDate = null;
			AgreeYear = 0;
			Price = 0;
			StartYM = null;
			EndYM = null;
			Remark1 = string.Empty;
		}
	}
}
