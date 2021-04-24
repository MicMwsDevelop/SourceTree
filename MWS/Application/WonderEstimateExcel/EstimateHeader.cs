//
// EstimateHeader.cs
// 
// WonderWeb見積書ヘッダ行情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/03/31 勝呂)
//

namespace WonderEstimateExcel
{
	/// <summary>
	/// WonderWeb見積書ヘッダ行情報
	/// </summary>
	public class EstimateHeader
	{
		/// <summary>
		/// フィールド数
		/// </summary>
		public const short FieldCount = 21;

		public int 見積番号 { get; set; }
		public string 発行日 { get; set; }
		public string 担当支店名 { get; set; }
		public string 担当支店住所1 { get; set; }
		public string 担当支店住所2 { get; set; }
		public string 担当支店TEL { get; set; }
		public string 担当支店FAX { get; set; }
		public string 担当者名 { get; set; }
		public string 顧客名 { get; set; }
		public int 見積金額 { get; set; }
		public int リース金額 { get; set; }
		public string 件名 { get; set; }
		public string 納期 { get; set; }
		public string 支払条件 { get; set; }
		public string 納入場所 { get; set; }
		public string 有効期限 { get; set; }
		public string 顧客住所1 { get; set; }
		public string 顧客住所2 { get; set; }
		public string 顧客TEL { get; set; }
		public string 備考 { get; set; }
		public string リース期間 { get; set; }

		/// <summary>
		/// 見積書エクセルファイル名の取得
		/// </summary>
		public string GetFilename
		{
			get
			{
				return string.Format("見積書_{0}.xlsx", 見積番号);
			}
		}

		/// <summary>
		/// 医院名
		/// </summary>
		public string 医院名
		{
			get
			{
				return string.Format("{0} 御中", 顧客名);
			}
		}

		/// <summary>
		/// 見積金額合計
		/// </summary>
		public string 見積金額合計
		{
			get
			{
				return string.Format("{0}-", EstimateCsv.CommaEdit(EstimateCsv.GetOutsideTaxPrice(見積金額)));
			}
		}

		/// <summary>
		/// 消費税
		/// </summary>
		public string 消費税
		{
			get
			{
				return string.Format("{0}-", EstimateCsv.CommaEdit(EstimateCsv.GetTax(見積金額)));
			}
		}

		/// <summary>
		/// 月額リース金額
		/// </summary>
		public string 月額リース金額
		{
			get
			{
				int price = EstimateCsv.GetOutsideTaxPrice(リース金額);
				if (0 < リース金額)
				{
					if (0 < リース期間.Length)
					{
						return string.Format("{0}-／{1}", EstimateCsv.CommaEdit(price), リース期間);
					}
					else
					{
						return string.Format("{0}-／", EstimateCsv.CommaEdit(price));
					}
				}
				else
				{
					if (0 < リース期間.Length)
					{
						return string.Format("／{1}", リース期間);
					}
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// 顧客住所
		/// </summary>
		public string 顧客住所
		{
			get
			{
				if (0 < 顧客住所2.Length)
				{
					return string.Format("{0} {1}", 顧客住所1, 顧客住所2);
				}
				return 顧客住所1;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EstimateHeader()
		{
			見積番号 = 0;
			発行日 = string.Empty;
			担当支店名 = string.Empty;
			担当支店住所1 = string.Empty;
			担当支店住所2 = string.Empty;
			担当支店TEL = string.Empty;
			担当支店FAX = string.Empty;
			担当者名 = string.Empty;
			顧客名 = string.Empty;
			見積金額 = 0;
			リース金額 = 0;
			件名 = string.Empty;
			納期 = string.Empty;
			支払条件 = string.Empty;
			納入場所 = string.Empty;
			有効期限 = string.Empty;
			顧客住所1 = string.Empty;
			顧客住所2 = string.Empty;
			顧客TEL = string.Empty;
			備考 = string.Empty;
			リース期間 = string.Empty;
		}

		/// <summary>
		/// CSVデータの格納
		/// </summary>
		/// <param name="csv"></param>
		/// <returns></returns>
		public bool SetRecord(string csv)
		{
			string[] split = EstimateCsv.Split(csv);
			for (int i = 0; i < split.Length; i++)
				split[i] = split[i].Trim('"'); // 先頭と最後尾の '"' を削除


			//string[] split = csv.Split(',');
			if (FieldCount == split.Length)
			{
				if (0 == split[0].Length)
				{
					return false;
				}
				int work;
				if (int.TryParse(EstimateCsv.Trim(split[0]), out work))
				{
					見積番号 = work;
				}
				発行日 = EstimateCsv.Trim(split[1]);
				担当支店名 = EstimateCsv.Trim(split[2]);
				担当支店住所1 = EstimateCsv.Trim(split[3]);
				担当支店住所2 = EstimateCsv.Trim(split[4]);
				担当支店TEL = EstimateCsv.Trim(split[5]);
				担当支店FAX = EstimateCsv.Trim(split[6]);
				担当者名 = EstimateCsv.Trim(split[7]);
				顧客名 = EstimateCsv.Trim(split[8]);
				if (int.TryParse(EstimateCsv.Trim(split[9]), out work))
				{
					見積金額 = work;
				}
				if (int.TryParse(EstimateCsv.Trim(split[10]), out work))
				{
					リース金額 = work;
				}
				件名 = EstimateCsv.Trim(split[11]);
				納期 = EstimateCsv.Trim(split[12]);
				支払条件 = EstimateCsv.Trim(split[13]);
				納入場所 = EstimateCsv.Trim(split[14]);
				有効期限 = EstimateCsv.Trim(split[15]);
				顧客住所1 = EstimateCsv.Trim(split[16]);
				顧客住所2 = EstimateCsv.Trim(split[17]);
				顧客TEL = EstimateCsv.Trim(split[18]);
				備考 = EstimateCsv.Trim(split[19]);
				リース期間 = EstimateCsv.Trim(split[20]);
				return true;
			}
			return false;
		}
	}
}
