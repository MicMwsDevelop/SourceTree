using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace MwsLib.BaseFactory.MwsServiceFrequency
{
	public class MwsServiceFrequencyData
	{
		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool FinishedFlag { get; set; }

		/// <summary>
		/// 有効ユーザーフラグ
		/// </summary>
		public bool EnableUserFlag { get; set; }

		/// <summary>
		/// 営業部名
		/// </summary>
		public string SectionName { get; set; }

		/// <summary>
		/// 支店コード
		/// </summary>
		public int BranchCode { get; set; }

		/// <summary>
		/// 支店名
		/// </summary>
		public string BranchName { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public string CostomerID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CostomerName { get; set; }

		/// <summary>
		/// 売上月
		/// </summary>
		public YearMonth EarningsMonth { get; set; }

		/// <summary>
		/// MWS_申込種別
		/// </summary>
		public int MwsAcceptType { get; set; }

		/// <summary>
		/// サービス種別ID
		/// </summary>
		public int ServiceTypeID { get; set; }

		/// <summary>
		/// サービス種別名称
		/// </summary>
		public string ServiceTypeName { get; set; }

		/// <summary>
		/// サービスコード
		/// </summary>
		public int ServiceCode { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public Date UsedStartDate { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public Date UsedEndDate { get; set; }

		/// <summary>
		/// 利用回数
		/// </summary>
		public int UsedCount { get; set; }

		/// <summary>
		/// システム名
		/// </summary>
		public int SystemID { get; set; }

		/// <summary>
		/// システム名称
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// 前システム名称
		/// </summary>
		public string BeforeSystemName { get; set; }

		/// <summary>
		/// 県番号
		/// </summary>
		public KenNumDef.KenNumber Ken { get; set; }

		/// <summary>
		/// 利用年月
		/// </summary>
		public YearMonth UsedMonth { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MwsServiceFrequencyData()
		{
			FinishedFlag = false;
			EnableUserFlag = false;
			SectionName = string.Empty;
			BranchCode = 0;
			BranchName = string.Empty;
			CostomerID = string.Empty;
			CostomerName = string.Empty;
			EarningsMonth = new YearMonth();
			MwsAcceptType = 0;
			ServiceTypeID = 0;
			ServiceTypeName = string.Empty;
			ServiceCode = 0;
			ServiceName = string.Empty;
			UsedStartDate = new Date();
			UsedEndDate = new Date();
			UsedCount = 0;
			SystemID = 0;
			SystemName = string.Empty;
			BeforeSystemName = string.Empty;
			Ken = KenNumDef.KenNumber.None;
			UsedMonth = new YearMonth();
		}

		public MwsServiceFrequencyData(string[] list)
		{
			FinishedFlag = ("0" == list[0])? false: true;
			EnableUserFlag = ("0" == list[1]) ? false : true;
			SectionName = list[2];
			BranchCode = int.Parse(list[3]);
			BranchName = list[4];
			CostomerID = list[5];
			CostomerName = list[6];
			if (0 < list[7].Length)
			{
				YearMonth ym;
				if (YearMonth.TryParse(list[7], out ym))
				{
					EarningsMonth = ym;
				}
			}
			MwsAcceptType = int.Parse(list[8]);
			ServiceTypeID = int.Parse(list[9]);
			ServiceTypeName = list[10];
			ServiceCode = int.Parse(list[11]);
			ServiceName = list[12];
			if (0 < list[13].Length)
			{
				Date d;
				if (Date.TryParse(list[13], out d))
				{
					UsedStartDate = d;
				}
			}
			if (0 < list[14].Length)
			{
				Date d;
				if (Date.TryParse(list[14], out d))
				{
					UsedEndDate = d;
				}
			}
			if (0 < list[15].Length)
			{
				UsedCount = int.Parse(list[15]);
			}
			SystemID = int.Parse(list[16]);
			SystemName = list[17];
			BeforeSystemName = list[18];
			Ken = (KenNumDef.KenNumber)int.Parse(list[19]);
			if (0 < list[21].Length)
			{
				YearMonth ym;
				if (YearMonth.TryParse(list[21], out ym))
				{
					UsedMonth = ym;
				}
			}
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return UsedMonth.GetJapaneseANString();
		}
	}

	public class MwsServiceFrequencyDataList : List<MwsServiceFrequencyData>
	{
		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			if (0 < Count)
			{
				return this[0].UsedMonth.GetJapaneseANString();
			}
			return "";
		}
	}
}
