namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 3-2 当月仕入単価貯蔵品
	/// </summary>
	public class PCA仕入明細貯蔵品
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

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA仕入明細貯蔵品()
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
		}
	}
}
