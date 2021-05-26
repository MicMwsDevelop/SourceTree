using MwsLib.Common;
using System.Collections.Generic;

namespace MwsLib.BaseFactory.MwsServiceFrequency
{
	public class MwsServiceFrequencyData
	{
		/// <summary>
		/// 利用年月
		/// </summary>
		public YearMonth UsedMonth { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public string CostomerID { get; set; }

		/// <summary>
		/// サービスコード
		/// </summary>
		public int ServiceCode { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 利用回数
		/// </summary>
		public int UsedCount { get; set; }

		/// <summary>
		/// 県番号
		/// </summary>
		public KenNumDef.KenNumber Ken { get; set; }

		/// <summary>
		/// 支店コード
		/// </summary>
		public int BranchCode { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MwsServiceFrequencyData()
		{
			UsedMonth = new YearMonth();
			CostomerID = string.Empty;
			ServiceCode = 0;
			ServiceName = string.Empty;
			UsedCount = 0;
			Ken = KenNumDef.KenNumber.None;
			BranchCode = 0;
		}

		public MwsServiceFrequencyData(string[] list)
		{
			if (0 < list[21].Length)
			{
				YearMonth ym;
				if (YearMonth.TryParse(list[21], out ym))
				{
					UsedMonth = ym;
				}
			}
			CostomerID = list[5];
			ServiceCode = int.Parse(list[11]);
			ServiceName = list[12];
			if (0 < list[15].Length)
			{
				UsedCount = int.Parse(list[15]);
			}
			Ken = (KenNumDef.KenNumber)int.Parse(list[19]);
			BranchCode = int.Parse(list[3]);
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
			if (0 < this.Count)
			{
				return this[0].UsedMonth.GetJapaneseANString();
			}
			return "";
		}
	}
}
