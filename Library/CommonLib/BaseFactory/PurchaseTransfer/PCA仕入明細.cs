using CommonLib.BaseFactory.Pca;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

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
		public short マスター区分 { get; set; }
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
		public int データ区分 { get; set; }
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
			マスター区分 = 0;
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
			データ区分 = 0;
			商品名2 = string.Empty;
		}

		/// <summary>
		/// PCA仕入明細汎用データにPCA仕入明細のデータを格納
		/// </summary>
		/// <returns>PCA仕入明細汎用データ</returns>
		public PCA仕入明細汎用データ SetPCA仕入明細汎用データ()
		{
			PCA仕入明細汎用データ data = new PCA仕入明細汎用データ();
			data.入荷方法 = 入荷方法;
			data.科目区分 = 科目区分;
			data.伝区 = 伝区;
			data.仕入日 = 仕入日;
			data.精算日 = 精算日;
			data.伝票No = 伝票No;
			data.仕入先コード = 仕入先コード;
			data.仕入先名 = 仕入先名;
			data.先方担当者名 = 先方担当者名;
			data.部門コード = 部門コード;
			data.担当者コード = 担当者コード;
			data.摘要コード = 摘要コード;
			data.摘要名 = 摘要名;
			data.商品コード = 商品コード;
			data.マスター区分 = マスター区分;
			data.商品名 = 品名;
			data.区 = 区;
			data.倉庫コード = 倉庫コード;
			data.入数 = 入数;
			data.箱数 = 箱数;
			data.数量 = 数量;
			data.単位 = 単位;
			data.単価 = 単価;
			data.金額 = 金額;
			data.外税額 = 外税額;
			data.内税額 = 内税額;
			data.税区分 = 税区分;
			data.税込区分 = 税込区分;
			data.備考 = 備考;
			data.規格型番 = 規格型番;
			data.色 = 色;
			data.サイズ = サイズ;
			data.計算式コード = 計算式コード;
			data.商品項目1 = 商品項目1;
			data.商品項目2 = 商品項目2;
			data.商品項目3 = 商品項目3;
			data.仕入項目1 = 仕入項目1;
			data.仕入項目2 = 仕入項目2;
			data.仕入項目3 = 仕入項目3;
			data.税率 = 税率;
			data.伝票消費税額 = 伝票消費税外税;
			data.ﾌﾟﾛｼﾞｪｸﾄコード = プロジェクトコード;
			data.伝票No2 = 伝票No2;
			data.データ区分 = データ区分;
			data.商品名2 = 商品名2;
			data.単位区分 = 0;
			data.ロットNo = string.Empty;
			data.ロット有効期限 = 0;
			data.仕入税種別 = 0;
			return data;
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
						マスター区分 = DataBaseValue.ConvObjectToShort(row["マスター区分"]),
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
						データ区分 = DataBaseValue.ConvObjectToInt(row["データ区分"]),
						商品名2 = row["商品名2"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
