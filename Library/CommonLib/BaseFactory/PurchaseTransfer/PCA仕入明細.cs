using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommonLib.DB;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 2-3 当月仕入単価
	/// </summary>
	public class PCA仕入明細
	{
		public short 入荷方法 { get; set; }
		public short 科目区分 { get; set; }
		public string 伝区 { get; set; }
		public int 仕入日 { get; set; }
		public int 精算日 { get; set; }
		public int 伝票No { get; set; }
		public string 仕入先コード { get; set; }
		public string 仕入先名 { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string 商品コード { get; set; }
		public string マスター区分 { get; set; }
		public string 品名 { get; set; }
		public short 区 { get; set; }
		public string 倉庫コード { get; set; }
		public int 入数 { get; set; }
		public int 箱数 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 金額 { get; set; }
		public int 外税額 { get; set; }
		public int 内税額 { get; set; }
		public short 税区分 { get; set; }
		public short 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 計算式コード { get; set; }
		public int 商品項目1 { get; set; }
		public int 商品項目2 { get; set; }
		public int 商品項目3 { get; set; }
		public int 仕入項目1 { get; set; }
		public int 仕入項目2 { get; set; }
		public int 仕入項目3 { get; set; }
		public int 税率 { get; set; }
		public int 伝票消費税外税 { get; set; }
		public string プロジェクトコード { get; set; }
		public string 伝票No2 { get; set; }
		public string データ区分 { get; set; }
		public string 商品名2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA仕入明細()
		{
			入荷方法 = 0;
			科目区分 = 0;
			伝区 = string.Empty;
			仕入日 = 0;
			精算日 = 0;
			伝票No = 0;
			仕入先コード = string.Empty;
			仕入先名 = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			商品コード = string.Empty;
			マスター区分 = string.Empty;
			品名 = string.Empty;
			区 = 0;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			金額 = 0;
			外税額 = 0;
			内税額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = 0;
			商品項目1 = 0;
			商品項目2 = 0;
			商品項目3 = 0;
			仕入項目1 = 0;
			仕入項目2 = 0;
			仕入項目3 = 0;
			税率 = 0;
			伝票消費税外税 = 0;
			プロジェクトコード = string.Empty;
			伝票No2 = string.Empty;
			データ区分 = "0";
			商品名2 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<PCA仕入明細> DataTableToList(DataTable table)
		{
			List<PCA仕入明細> result = new List<PCA仕入明細>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					PCA仕入明細 data = new PCA仕入明細
					{
						入荷方法 = DataBaseValue.ConvObjectToShort(row["入荷方法"]),
						科目区分 = DataBaseValue.ConvObjectToShort(row["科目区分"]),
						伝区 = row["伝区"].ToString().Trim(),
						仕入日 = DataBaseValue.ConvObjectToInt(row["仕入日"]),
						精算日 = DataBaseValue.ConvObjectToInt(row["精算日"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						仕入先コード = row["仕入先コード"].ToString().Trim(),
						仕入先名 = row["仕入先名"].ToString().Trim(),
						先方担当者名 = row["先方担当者名"].ToString().Trim(),
						部門コード = row["部門コード"].ToString().Trim(),
						担当者コード = row["担当者コード"].ToString().Trim(),
						摘要コード = row["摘要コード"].ToString().Trim(),
						摘要名 = row["摘要名"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						マスター区分 = row["マスター区分"].ToString().Trim(),
						品名 = row["品名"].ToString().Trim(),
						区 = DataBaseValue.ConvObjectToShort(row["区"]),
						倉庫コード = row["倉庫コード"].ToString().Trim(),
						入数 = DataBaseValue.ConvObjectToInt(row["入数"]),
						箱数 = DataBaseValue.ConvObjectToInt(row["箱数"]),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						単位 = row["単位"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToInt(row["単価"]),
						金額 = DataBaseValue.ConvObjectToInt(row["金額"]),
						外税額 = DataBaseValue.ConvObjectToInt(row["外税額"]),
						内税額 = DataBaseValue.ConvObjectToInt(row["内税額"]),
						税区分 = DataBaseValue.ConvObjectToShort(row["税区分"]),
						税込区分 = DataBaseValue.ConvObjectToShort(row["税込区分"]),
						備考 = row["備考"].ToString().Trim(),
						規格型番 = row["規格型番"].ToString().Trim(),
						色 = row["色"].ToString().Trim(),
						サイズ = row["サイズ"].ToString().Trim(),
						計算式コード = DataBaseValue.ConvObjectToInt(row["計算式コード"]),
						商品項目1 = DataBaseValue.ConvObjectToInt(row["商品項目1"]),
						商品項目2 = DataBaseValue.ConvObjectToInt(row["商品項目2"]),
						商品項目3 = DataBaseValue.ConvObjectToInt(row["商品項目3"]),
						仕入項目1 = DataBaseValue.ConvObjectToInt(row["仕入項目1"]),
						仕入項目2 = DataBaseValue.ConvObjectToInt(row["仕入項目2"]),
						仕入項目3 = DataBaseValue.ConvObjectToInt(row["仕入項目3"]),
						税率 = DataBaseValue.ConvObjectToInt(row["税率"]),
						伝票消費税外税 = DataBaseValue.ConvObjectToInt(row["伝票消費税外税"]),
						プロジェクトコード = row["プロジェクトコード"].ToString().Trim(),
						伝票No2 = row["伝票No2"].ToString().Trim(),
						データ区分 = row["データ区分"].ToString().Trim(),
						商品名2 = row["商品名2"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
