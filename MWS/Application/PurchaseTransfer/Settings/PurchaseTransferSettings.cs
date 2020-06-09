//
// PurchaseTransferSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/26 勝呂)
//
using System;
using System.Collections.Generic;

namespace PurchaseTransfer.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PurchaseTransferSettings : ICloneable//, IEquatable<PurchaseTransferSettings>
	{
		/// <summary>
		/// 
		/// </summary>
		public string 在庫一覧表入力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string 仕入振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string 社内使用分振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string 貯蔵品社内使用分振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string りすとん振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string りすとん出荷ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string りすとん月額振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string りすとん月額出荷ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string Office365振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string Office365出荷ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string 問心伝振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string 問心伝出荷ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string 問心伝月額振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string ソフトバンク振替出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string Curline本体アプリ出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string ナルコーム出力ファイル名;

		/// <summary>
		/// 
		/// </summary>
		public string クラウドデータバンク出力ファイル名;

		/// <summary>
		/// りすとん商品コード
		/// </summary>
		public List<MasterGoods> ListonGoodsList;

		/// <summary>
		/// 問心伝商品コード
		/// </summary>
		public List<MasterGoods> MonshindenGoodsList;

		/// <summary>
		/// りすとん月額商品コード 
		/// </summary>
		public List<MonthlyMasterGoods> MonthlyListonGoodsList;

		/// <summary>
		/// Office365商品コード 
		/// </summary>
		public List<MonthlyMasterGoods> MonthlyOffice365GoodsList;

		/// <summary>
		/// 問心伝月額商品コード  
		/// </summary>
		public List<MonthlyMasterGoods> MonthlySoftbankGoodsList;

		/// <summary>
		/// ソフトバンク商品コード 
		/// </summary>
		public List<MonthlyMasterGoods> MonthlyMonshindenGoodsList;

		/// <summary>
		/// Curline本体アプリ商品コード 
		/// </summary>
		public List<MonthlyMasterGoods> MonthlyCurlineGoodsList;

		/// <summary>
		/// ナルコーム商品コード
		/// </summary>
		public List<ExtraMasterGoods> NarcohmGoodsList;

		/// <summary>
		/// クラウドデータバンク商品コード
		/// </summary>
		public List<ExtraMasterGoods> CloudDataBankGoodsList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PurchaseTransferSettings()
        {
			在庫一覧表入力ファイル名= string.Empty;
			仕入振替出力ファイル名 = string.Empty;
			社内使用分振替出力ファイル名 = string.Empty;
			貯蔵品社内使用分振替出力ファイル名 = string.Empty;
			りすとん振替出力ファイル名 = string.Empty;
			りすとん出荷ファイル名 = string.Empty;
			りすとん月額振替出力ファイル名 = string.Empty;
			りすとん月額出荷ファイル名 = string.Empty;
			Office365振替出力ファイル名 = string.Empty;
			Office365出荷ファイル名 = string.Empty;
			問心伝振替出力ファイル名 = string.Empty;
			問心伝出荷ファイル名 = string.Empty;
			問心伝月額振替出力ファイル名 = string.Empty;
			ソフトバンク振替出力ファイル名 = string.Empty;
			Curline本体アプリ出力ファイル名 = string.Empty;
			ナルコーム出力ファイル名 = string.Empty;
			クラウドデータバンク出力ファイル名 = string.Empty;
			ListonGoodsList = new List<MasterGoods>();
			MonshindenGoodsList = new List<MasterGoods>();
			MonthlyListonGoodsList = new List<MonthlyMasterGoods>();
			MonthlyOffice365GoodsList = new List<MonthlyMasterGoods>();
			MonthlyMonshindenGoodsList = new List<MonthlyMasterGoods>();
			MonthlySoftbankGoodsList = new List<MonthlyMasterGoods>();
			MonthlyCurlineGoodsList = new List<MonthlyMasterGoods>();
			NarcohmGoodsList = new List<ExtraMasterGoods>();
			CloudDataBankGoodsList = new List<ExtraMasterGoods>();
		}

		/// <summary>
		/// メンバーのクローンを作成する
		/// （ICloneableの実装）
		/// </summary>
		/// <returns>クローンオブジェクト</returns>
		public Object Clone()
		{
			return MemberwiseClone();
		}
	}
}
