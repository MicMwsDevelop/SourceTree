namespace VariousDocumentOut.Settings
{
	/// <summary>
	/// オンライン資格確認関連商品
	/// </summary>
	public class OnlineGoods
	{
		public string 商品コード { get; set; }
		public string 項目 { get; set; }
		public string 内訳 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineGoods()
		{
			商品コード = string.Empty;
			項目 = string.Empty;
			内訳 = string.Empty;
		}
	}
}
