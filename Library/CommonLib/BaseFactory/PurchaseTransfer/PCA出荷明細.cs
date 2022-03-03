using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 2-1 社内使用分出荷明細
	/// </summary>
	public class PCA出荷明細
	{
		public short 出荷方法 { get; set; }
		public int 出荷日 { get; set; }
		public int 伝票No { get; set; }
		public string 出荷先コード { get; set; }
		public string 出荷先名 { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 商品コード { get; set; }
		public string 品名 { get; set; }
		public string 倉庫コード { get; set; }
		public int 入数 { get; set; }
		public int 箱数 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 出荷金額 { get; set; }
		public short 税区分 { get; set; }
		public short 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 税率 { get; set; }
		public string プロジェクトコード { get; set; }
		public string 商品名2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA出荷明細()
		{
			出荷方法 = 0;
			出荷日 = 0;
			伝票No = 0;
			出荷先コード = string.Empty;
			出荷先名 = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			商品コード = string.Empty;
			品名 = string.Empty;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			出荷金額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			税率 = 0;
			プロジェクトコード = string.Empty;
			商品名2 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<PCA出荷明細> DataTableToList(DataTable table)
		{
			List<PCA出荷明細> result = new List<PCA出荷明細>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					PCA出荷明細 data = new PCA出荷明細
					{
						出荷方法 = DataBaseValue.ConvObjectToShort(row["出荷方法"]),
						出荷日 = DataBaseValue.ConvObjectToInt(row["出荷日"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						出荷先コード = row["出荷先コード"].ToString().Trim(),
						出荷先名 = row["出荷先名"].ToString().Trim(),
						先方担当者名 = row["先方担当者名"].ToString().Trim(),
						部門コード = row["部門コード"].ToString().Trim(),
						担当者コード = row["担当者コード"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						品名 = row["品名"].ToString().Trim(),
						倉庫コード = row["倉庫コード"].ToString().Trim(),
						入数 = DataBaseValue.ConvObjectToInt(row["入数"]),
						箱数 = DataBaseValue.ConvObjectToInt(row["箱数"]),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						単位 = row["単位"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToInt(row["単価"]),
						出荷金額 = DataBaseValue.ConvObjectToInt(row["出荷金額"]),
						税区分 = DataBaseValue.ConvObjectToShort(row["税区分"]),
						税込区分 = DataBaseValue.ConvObjectToShort(row["税込区分"]),
						備考 = row["備考"].ToString().Trim(),
						税率 = DataBaseValue.ConvObjectToInt(row["税率"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
