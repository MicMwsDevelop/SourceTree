using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace CalcBusinessConsignCommission.BaseFactory
{
	/// <summary>
	/// 売上レコード
	/// </summary>
	[Serializable]
	public class SaleRecord : IEquatable<SaleRecord>
	{
		/// <summary>
		/// 伝票番号
		/// </summary>
		public int SlipCode { get; set; }

		/// <summary>
		/// 得意先コード
		/// </summary>
		public string TokuisakiCode { get; set; }

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
		/// 売上金額
		/// </summary>
		public int SalePrice { get; set; }

		/// <summary>
		/// 原単価
		/// </summary>
		public int OrgUnitPrice { get; set; }

		/// <summary>
		/// 原価金額
		/// </summary>
		public int OrgPrice { get; set; }

		/// <summary>
		/// 売上データ
		/// </summary>
		public string Record { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SaleRecord()
		{
			SlipCode = 0;
			TokuisakiCode = string.Empty;
			GoodsCode = string.Empty;
			GoodsName = string.Empty;
			UnitPrice = 0;
			SalePrice = 0;
			OrgUnitPrice = 0;
			OrgPrice = 0;
			Record = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">レコード</param>
		public SaleRecord(string record)
		{
			List<string> split = SplitString.CSVSplitLine(record);
			SlipCode = int.Parse(split[3]);
			TokuisakiCode = split[4];
			GoodsCode = split[14];
			GoodsName = split[16];
			UnitPrice = int.Parse(split[23]);
			SalePrice = int.Parse(split[24]);
			OrgUnitPrice = int.Parse(split[25]);
			OrgPrice = int.Parse(split[26]);
			Record = record;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するSaleRecord</param>
		/// <returns>判定</returns>
		public bool Equals(SaleRecord other)
		{
			if (null != other)
			{
				if (SlipCode != other.SlipCode)
					return false;
				if (TokuisakiCode != other.TokuisakiCode)
					return false;
				if (GoodsCode != other.GoodsCode)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (UnitPrice != other.UnitPrice)
					return false;
				if (SalePrice != other.SalePrice)
					return false;
				if (OrgUnitPrice != other.OrgUnitPrice)
					return false;
				if (OrgPrice != other.OrgPrice)
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
		/// <param name="obj">比較するSaleRecordオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is SaleRecord)
			{
				return this.Equals((SaleRecord)obj);
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
	/// 売上データ
	/// </summary>
	public class SaleData
	{
		/// <summary>
		/// ファイル名称
		/// </summary>
		public const string SALE_FILENAME = "PCA売上データ.csv";

		/// <summary>
		/// 伝票番号
		/// </summary>
		public int SlipCode { get; set; }

		/// <summary>
		/// 得意先コード
		/// </summary>
		public string TokuisakiCode { get; set; }

		/// <summary>
		/// 売上レコード
		/// </summary>
		public List<SaleRecord> RecordList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private SaleData()
		{
			SlipCode = 0;
			TokuisakiCode = string.Empty;
			RecordList = new List<SaleRecord>();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">レコード</param>
		public SaleData(string record)
		{
			List<string> split = SplitString.CSVSplitLine(record);
			SlipCode = int.Parse(split[3]);
			TokuisakiCode = split[4];
			RecordList = new List<SaleRecord>();
			RecordList.Add(new SaleRecord(record));
		}

		/// <summary>
		/// レコードから伝票番号の抽出
		/// </summary>
		/// <param name="record">レコード</param>
		/// <returns>伝票番号</returns>
		public static int ExtractionSlipCode(string record)
		{
			List<string> split = SplitString.CSVSplitLine(record);
			return int.Parse(split[3]);
		}

		/// <summary>
		/// 売上金額の取得
		/// </summary>
		/// <returns>売上金額</returns>
		public int GetSalePrice()
		{
			int price = 0;
			foreach (SaleRecord record in RecordList)
			{
				if ("000014" != record.GoodsCode && "800001" != record.GoodsCode && "800155" != record.GoodsCode && "800156" != record.GoodsCode && "800157" != record.GoodsCode)
				{
					// 記事データ、MIC WEB SERVICE(ﾌﾟﾗｯﾄﾌｫｰﾑ利用 月額)、MWS おまとめﾌﾟﾗﾝ12ケ月、MWS おまとめﾌﾟﾗﾝ24ケ月、MWS おまとめﾌﾟﾗﾝ36ケ月以外
					price += record.SalePrice;
				}
			}
			return price;
		}
	}
}
