using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.VariousDocumentOut
{
	public class オンライン資格確認対象商品売上明細
	{
		public string 得意先コード { get; set; }
		public int 売上日 { get; set; }
		public int 伝票No { get; set; }
		public string sykd_ocd { get; set; }
		public string 部門コード { get; set; }
		public string 摘要 { get; set; }
		public int 枝番 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public int 標準価格 { get; set; }
		public int 数量 { get; set; }
		public int 金額 { get; set; }
		public int 税額 { get; set; }
		public int 補助対象金額 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public オンライン資格確認対象商品売上明細()
		{
			得意先コード = string.Empty;
			売上日 = 0;
			伝票No = 0;
			sykd_ocd = string.Empty;
			部門コード = string.Empty;
			摘要 = string.Empty;
			枝番 = 0;
			商品コード = string.Empty;
			商品名 = string.Empty;
			標準価格 = 0;
			数量 = 0;
			金額 = 0;
			税額 = 0;
			補助対象金額 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<オンライン資格確認対象商品売上明細> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<オンライン資格確認対象商品売上明細> result = new List<オンライン資格確認対象商品売上明細>();
				foreach (DataRow row in table.Rows)
				{
					オンライン資格確認対象商品売上明細 data = new オンライン資格確認対象商品売上明細();
					data.得意先コード = row["得意先コード"].ToString().Trim();
					data.売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]);
					data.伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]);
					data.sykd_ocd = row["sykd_ocd"].ToString().Trim();
					data.部門コード = row["部門コード"].ToString().Trim();
					data.摘要 = row["摘要"].ToString().Trim();
					data.枝番 = DataBaseValue.ConvObjectToInt(row["枝番"]);
					data.商品コード = row["商品コード"].ToString().Trim();
					data.商品名 = row["商品名"].ToString().Trim();
					data.標準価格 = DataBaseValue.ConvObjectToInt(row["標準価格"]);
					data.数量 = DataBaseValue.ConvObjectToInt(row["数量"]);
					data.金額 = DataBaseValue.ConvObjectToInt(row["金額"]);
					data.税額 = DataBaseValue.ConvObjectToInt(row["税額"]);
					data.補助対象金額 = DataBaseValue.ConvObjectToInt(row["補助対象金額"]);
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
