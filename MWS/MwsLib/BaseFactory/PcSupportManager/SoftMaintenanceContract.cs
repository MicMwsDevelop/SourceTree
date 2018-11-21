//
// SoftMaintenanceContract.cs
//
// 製品サポート情報ソフト保守情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using MwsLib.Common;

namespace MwsLib.BaseFactory.PcSupportManager
{
	/// <summary>
	/// 製品サポート情報ソフト保守
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
		public SoftMaintenanceContract(PcSupportControl control)
		{
			CustomerNo = control.CustomerNo;
			Subscription = false;
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
			// 文字列を400で切る
			if (400 < control.Remark.GetMultiByteLength())
			{
				Remark1 = control.Remark.CutByMultiByteLength(400);
			}
			else
			{
				Remark1 = control.Remark;
			}
		}

		/// <summary>
		/// PC安心サポート管理情報から格納
		/// </summary>
		/// <param name="PcSupportControl">PC安心サポート管理情報</param>
		/// <returns>変更の可否</returns>
		public bool SetPcSupportControl(PcSupportControl control)
		{
			bool ret = false;
			if (CollectionDate != control.OrderDate)
			{
				CollectionDate = control.OrderDate;
				ret = true;
			}
			if (AgreeYear != control.AgreeYear)
			{
				AgreeYear = control.AgreeYear;
				ret = true;
			}
			if (Price != control.Price)
			{
				Price = control.Price;
				ret = true;
			}
			if (StartYM != control.StartDate.Value.ToYearMonth())
			{
				StartYM = control.StartDate.Value.ToYearMonth();
				ret = true;
			}
			if (control.PeriodEndDate.HasValue)
			{
				if (EndYM != control.PeriodEndDate.Value.ToYearMonth())
				{
					EndYM = control.PeriodEndDate.Value.ToYearMonth();
					ret = true;
				}
			}
			else
			{
				if (EndYM != control.EndDate.Value.ToYearMonth())
				{
					EndYM = control.EndDate.Value.ToYearMonth();
					ret = true;
				}
			}
			if (400 < control.Remark.GetMultiByteLength())
			{
				string remark = control.Remark.CutByMultiByteLength(400);
				if (Remark1 != remark)
				{
					Remark1 = remark;
					ret = true;
				}
			}
			else
			{
				if (Remark1 != control.Remark)
				{
					Remark1 = control.Remark;
					ret = true;
				}
			}
			return ret;
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
		/// 製品サポート情報ソフト保守情報を初期化
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
