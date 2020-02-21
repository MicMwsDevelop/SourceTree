using MwsLib.Common;
using System.Collections.Generic;

namespace PrintNouhin
{
	/// <summary>
	/// 納品情報
	/// </summary>
	public class NouhinData
	{
		private const int FIELD_COUNT = 21;

		/// <summary>
		/// 先頭
		/// </summary>
		public string TopFlag { get; set; }
		/// <summary>
		/// 受注番号
		/// </summary>
		public string JuchuBango { get; set; }

		/// <summary>
		/// お客様コードNo
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string Zipcode { get; set; }

		/// <summary>
		/// 住所
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string Tel { get; set; }

		/// <summary>
		/// 受注顧客No
		/// </summary>
		public string CustomerNo { get; set; }
		/// <summary>
		/// 年
		/// </summary>
		public string Year { get; set; }

		/// <summary>
		/// 月
		/// </summary>
		public string Month { get; set; }

		/// <summary>
		/// 日
		/// </summary>
		public string Day { get; set; }

		/// <summary>
		/// 担当
		/// </summary>
		public string Tanto { get; set; }

		/// <summary>
		/// 伝票番号
		/// </summary>
		public string DenNo { get; set; }

		/// <summary>
		/// 摘要
		/// </summary>
		public string Tekiyo { get; set; }

		/// <summary>
		/// 品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public string Count { get; set; }

		/// <summary>
		/// 単位
		/// </summary>
		public string Unit { get; set; }

		/// <summary>
		/// 単価
		/// </summary>
		public string Tanka { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public string Price { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string Biko { get; set; }

		/// <summary>
		/// 合計
		/// </summary>
		public string Total { get; set; }

		/// <summary>
		/// 先頭レコードかどうか？
		/// </summary>
		public bool IsTop
		{
			get
			{
				return ("1" == TopFlag)? true: false;
			}
		}


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NouhinData()
		{
			TopFlag = string.Empty;
			JuchuBango = string.Empty;
			TokuisakiNo = string.Empty;
			Zipcode = string.Empty;
			Address = string.Empty;
			ClinicName = string.Empty;
			Tel = string.Empty;
			CustomerNo = string.Empty;
			Year = string.Empty;
			Month = string.Empty;
			Day = string.Empty;
			Tanto = string.Empty;
			DenNo = string.Empty;
			Tekiyo = string.Empty;
			GoodsName = string.Empty;
			Count = string.Empty;
			Unit = string.Empty;
			Tanka = string.Empty;
			Price = string.Empty;
			Biko = string.Empty;
			Total = string.Empty;
		}

		public void SetCsvData(string str)
		{
			List<string> split = SplitString.CSVSplitLine(str);
			if (FIELD_COUNT == split.Count)
			{
				TopFlag = split[0];
				JuchuBango = split[1];
				TokuisakiNo = split[2];
				Zipcode = split[3];
				Address = split[4];
				ClinicName = split[5];
				Tel = split[6];
				CustomerNo = split[7];
				Year = split[8];
				Month = split[9];
				Day = split[10];
				Tanto = split[11];
				DenNo = split[12];
				Tekiyo = split[13];
				GoodsName = split[14];
				Count = split[15];
				Unit = split[16];
				Tanka = split[17];
				Price = split[18];
				Biko = split[19];
				Total = split[20];
			}
		}
	}
}
