//
// NouhinData.cs
// 
// 配送センター納品書情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
//
using System.Collections.Generic;

namespace DeliveryCenterPrintNouhin.Print
{
	/// <summary>
	/// 配送センター納品書情報
	/// </summary>
	public class NouhinData
	{
		/// <summary>
		/// フィールド数
		/// </summary>
		static public readonly int FIELD_COUNT = 21;

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
		/// 納品物リスト
		/// </summary>
		public List<NouhinGoods> GoodsList { get; set; }

		/// <summary>
		/// 先頭レコードかどうか？
		/// </summary>
		public bool IsTop
		{
			get
			{
				return ("1" == TopFlag) ? true : false;
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
			GoodsList = new List<NouhinGoods>();
		}

		/// <summary>
		/// CSVレコードの設定
		/// </summary>
		/// <param name="split">CSVレコード</param>
		public void SetTopRecord(List<string> split)
		{
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
				SetGoodsRecord(split);
			}
		}

		/// <summary>
		/// CSVレコードの設定
		/// </summary>
		/// <param name="split"></param>
		public void SetGoodsRecord(List<string> split)
		{
			if (FIELD_COUNT == split.Count)
			{
				NouhinGoods goods = new NouhinGoods();
				goods.SetGoodsData(split);
				GoodsList.Add(goods);
			}
		}

		/// <summary>
		/// 印刷枚数の取得
		/// </summary>
		/// <returns>印刷枚数</returns>
		public int GetMaxPage()
		{
			int max = GoodsList.Count / PrintNouhinControl.PRINT_GOODS_MAX;
			int amari = GoodsList.Count % PrintNouhinControl.PRINT_GOODS_MAX;
			return (0 == amari) ? max : max + 1;
		}

		/// <summary>
		/// テスト印刷
		/// </summary>
		/// <returns></returns>
		public static NouhinData GetTestData()
		{
			NouhinData test = new NouhinData();
			test.TopFlag = "1";
			test.JuchuBango = "9999999";
			test.TokuisakiNo = "999999";
			test.Zipcode = "ⅩⅩⅩ―ⅩⅩⅩⅩ";
			test.Address = "ⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩ";
			test.ClinicName = "ⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩ";
			test.Tel = "(XXX)XXX-XXXX";
			test.CustomerNo = "99999999";
			test.Year = "9999";
			test.Month = "99";
			test.Day = "99";
			test.Tanto = "ⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩ";
			test.DenNo = "999999";
			test.Tekiyo = "ⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩ";
			for (int i = 0; i < PrintNouhinControl.PRINT_GOODS_MAX; i++)
			{
				test.GoodsList.Add(NouhinGoods.GetTestData());
			}
			return test;
		}
	}
}
