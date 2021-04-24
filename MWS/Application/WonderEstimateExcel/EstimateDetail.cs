//
// EstimateDetail.cs
// 
// WonderWeb見積書明細行情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/03/31 勝呂)
//
using System.Linq;

namespace WonderEstimateExcel
{
	/// <summary>
	/// WonderWeb見積書明細行情報
	/// </summary>
	public class EstimateDetail
	{
		/// <summary>
		/// フィールド数
		/// </summary>
		public const short FieldCount = 7;

		public string 区分名 { get; set; }
		public string 商品名 { get; set; }
		public int 数量 { get; set; }
		public int 標準価格 { get; set; }
		public int 標準価格計 { get; set; }
		public int 提供価格 { get; set; }
		public string 商品備考 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EstimateDetail()
		{
			区分名 = string.Empty;
			商品名 = string.Empty;
			数量 = 0;
			標準価格 = 0;
			標準価格計 = 0;
			提供価格 = 0;
			商品備考 = string.Empty;
		}

		/// <summary>
		/// CSVデータの格納
		/// </summary>
		/// <param name="csv"></param>
		/// <returns></returns>
		public bool SetRecord(string csv)
		{
			string[] split = EstimateCsv.Split(csv);
			if (FieldCount == split.Count())
			{
				if (0 == split[0].Length)
				{
					return false;
				}
				区分名 = EstimateCsv.Trim(split[0]);
				商品名 = EstimateCsv.Trim(split[1]);
				int work;
				if (int.TryParse(EstimateCsv.Trim(split[2]), out work))
				{
					数量 = work;
				}
				if (int.TryParse(EstimateCsv.Trim(split[3]), out work))
				{
					標準価格 = work;
				}
				if (int.TryParse(EstimateCsv.Trim(split[4]), out work))
				{
					標準価格計 = work;
				}
				if (int.TryParse(EstimateCsv.Trim(split[5]), out work))
				{
					提供価格 = work;
				}
				商品備考 = EstimateCsv.Trim(split[6]);
				return true;
			}
			return false;
		}
	}
}
