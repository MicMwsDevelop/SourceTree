//
// OutputStockRecord.cs
//
// 出力用仕入データ情報
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
	/// 出力用仕入データ情報
	/// </summary>
	public class OutputStockRecord : StockRecord
	{
		/// <summary>
		/// 業務委託手数料対象金額
		/// </summary>
		public int TargetPrice { get; set; }

		/// <summary>
		/// 手数料率
		/// </summary>
		public int CommissionRate { get; set; }

		/// <summary>
		/// 単価（再計算）
		/// </summary>
		public int CalcUnitPrice { get; set; }

		/// <summary>
		/// 金額（再計算）
		/// </summary>
		public int CaclPrice { get; set; }

		/// <summary>
		/// 再計算対象レコードかどうか？
		/// </summary>
		public bool RecalcRecord { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private OutputStockRecord() : base()
		{
			TargetPrice = 0;
			CommissionRate = 0;
			CalcUnitPrice = 0;
			CaclPrice = 0;
			RecalcRecord = false;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">仕入データ情報</param>
		public OutputStockRecord(StockRecord record) : base(record)
		{
			TargetPrice = 0;
			CommissionRate = 0;
			CalcUnitPrice = base.UnitPrice;
			CaclPrice = base.Price;
			RecalcRecord = false;
		}

		/// <summary>
		/// 業務委託手数料対象金額と手数料率から金額を再計算する
		/// </summary>
		/// <param name="targetPrice">業務委託手数料対象金額</param>
		/// <param name="commissionRate">手数料率</param>
		public void Recalc(int targetPrice, int commissionRate)
		{
			TargetPrice = targetPrice;
			CommissionRate = commissionRate;
			if (0 < base.UnitPrice && 0 < CommissionRate)
			{
				if (0 < TargetPrice)
				{
					double work = TargetPrice;
					work = (work * CommissionRate) / 100;
					CalcUnitPrice = CaclPrice = (int)Math.Ceiling(work);
				}
				else
				{
					// プラットフォーム利用料を引いた結果、業務委託手数料対象金額が０円
					CalcUnitPrice = CaclPrice = 0;
				}
			}
			if (CalcUnitPrice != base.UnitPrice)
			{
				RecalcRecord = true;
			}
		}

		/// <summary>
		/// 仕入レコード文字列の出力
		/// </summary>
		/// <returns>仕入レコード文字列</returns>
		public string Output()
		{
			if (RecalcRecord)
			{
				// 再計算レコード
				string record = base.Record;
				List<string> split = SplitString.CSVSplitLine(record);
				
				// 単価
				split[22] = CalcUnitPrice.ToString();

				// 金額
				split[23] = CaclPrice.ToString();

				return string.Format("{0},{1},{2},\"{3}\",\"{4}\",{5},\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",{11},\"{12}\",\"{13}\",{14},\"{15}\",{16},\"{17}\",{18},{19},{20},\"{21}\",{22},{23},{24},{25},{26},{27},\"{28}\",\"{29}\",\"{30}\",\"{31}\",{32},{33},{34},{35},{36},{37},{38},{39},{40},\"{41}\",\"{42}\",{43},\"{44}\""
										, split[0], split[1], split[2], split[3], split[4], split[5], split[6], split[7], split[8], split[9]
										, split[10], split[11], split[12], split[13], split[14], split[15], split[16], split[17], split[18], split[19]
										, split[20], split[21], split[22], split[23], split[24], split[25], split[26], split[27], split[28], split[29]
										, split[30], split[31], split[32], split[33], split[34], split[35], split[36], split[37], split[38], split[39]
										, split[40], split[41], split[42], split[43], split[44]);
			}
			return base.Record;

		}
	}
}
