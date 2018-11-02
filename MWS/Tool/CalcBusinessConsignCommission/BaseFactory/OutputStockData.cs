using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcBusinessConsignCommission.BaseFactory
{
	/// <summary>
	/// 出力用仕入データ
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
		/// デフォルトコンストラクタ
		/// </summary>
		public OutputStockRecord() : base()
		{
			TargetPrice = 0;
			CommissionRate = 0;
			CalcUnitPrice = 0;
			CaclPrice = 0;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="record">仕入データ情報</param>
		public OutputStockRecord(StockRecord record) : base(record)
		{
			TargetPrice = 0;
			CommissionRate = 0;
			CalcUnitPrice = 0;
			CaclPrice = 0;
		}

		/// <summary>
		/// 業務委託手数料対象金額と手数料率から金額を再計算する
		/// </summary>
		public void Recalc()
		{
			if (0 < UnitPrice && 0 < TargetPrice && 0 < CommissionRate)
			{
				double work = TargetPrice;
				work = (work * CommissionRate) / 100;
				CalcUnitPrice = CaclPrice = (int)Math.Ceiling(work);
			}
		}
	}
}
