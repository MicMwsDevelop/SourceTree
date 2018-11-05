//
// StockData.cs
//
// 仕入データ情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/05 勝呂)
// 
using MwsLib.Common;
using System;
using System.Collections.Generic;

namespace CalcBusinessConsignCommission.BaseFactory
{
	/// <summary>
	/// 仕入データ情報
	/// </summary>
	[Serializable]
	public class StockRecord : IEquatable<StockRecord>
	{
		/// <summary>
		/// 伝票番号
		/// </summary>
		public int SlipCode { get; set; }

		/// <summary>
		/// 仕入先コード
		/// </summary>
		public string VenderCode { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsCode { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 単価
		/// </summary>
		public int UnitPrice { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 仕入データ
		/// </summary>
		public string Record { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public StockRecord()
		{
			SlipCode = 0;
			VenderCode = string.Empty;
			GoodsCode = string.Empty;
			GoodsName = string.Empty;
			UnitPrice = 0;
			Price = 0;
			Record = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">レコード</param>
		public StockRecord(string record)
		{
			List<string> split = SplitString.CSVSplitLine(record);
			SlipCode = int.Parse(split[5]);
			VenderCode = split[6];
			GoodsCode = split[13];
			GoodsName = split[15];
			UnitPrice = int.Parse(split[22]);
			Price = int.Parse(split[23]);
			Record = record;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">仕入データ情報</param>
		public StockRecord(StockRecord record)
		{
			SlipCode = record.SlipCode;
			VenderCode = record.VenderCode;
			GoodsCode = record.GoodsCode;
			GoodsName = record.GoodsName;
			UnitPrice = record.UnitPrice;
			Price = record.Price;
			Record = record.Record;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するStockRecord</param>
		/// <returns>判定</returns>
		public bool Equals(StockRecord other)
		{
			if (null != other)
			{
				if (SlipCode != other.SlipCode)
					return false;
				if (VenderCode != other.VenderCode)
					return false;
				if (GoodsCode != other.GoodsCode)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (UnitPrice != other.UnitPrice)
					return false;
				if (Price != other.Price)
					return false;
				if (Record != other.Record)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するStockRecordオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is StockRecord)
			{
				return this.Equals((StockRecord)obj);
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
			return Record;
		}
	}

	/// <summary>
	/// 仕入データ情報
	/// </summary>
	public class StockData
	{
		/// <summary>
		/// ファイル名称
		/// </summary>
		public const string STOCK_FILENAME = "PCA仕入データ.csv";

		/// <summary>
		/// 伝票番号
		/// </summary>
		public int SlipCode { get; set; }

		/// <summary>
		/// 仕入先コード
		/// </summary>
		public string VenderCode { get; set; }

		/// <summary>
		/// 仕入レコード
		/// </summary>
		public List<StockRecord> RecordList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private StockData()
		{
			SlipCode = 0;
			VenderCode = string.Empty;
			RecordList = new List<StockRecord>();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">レコード</param>
		public StockData(string record)
		{
			List<string> split = SplitString.CSVSplitLine(record);
			SlipCode = int.Parse(split[5]);
			VenderCode = split[6];
			RecordList = new List<StockRecord>();
			RecordList.Add(new StockRecord(record));
		}

		/// <summary>
		/// レコードから伝票番号の抽出
		/// </summary>
		/// <param name="record">レコード</param>
		/// <returns>伝票番号</returns>
		public static int ExtractionSlipCode(string record)
		{
			List<string> split = SplitString.CSVSplitLine(record);
			return int.Parse(split[5]);
		}
	}
}
