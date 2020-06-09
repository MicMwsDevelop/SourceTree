//
// CloudDataBankPurchaseOutputSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/06 勝呂)
//
using System;
using System.Collections.Generic;
using System.IO;

namespace CloudDataBankPurchaseOutput.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class CloudDataBankPurchaseOutputSettings : ICloneable
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string ExportDir;

		/// <summary>
		/// 仕入データ出力ファイル名
		/// </summary>
		public string ExportFilename;

		/// <summary>
		/// PCAバージョン情報
		/// </summary>
		public int PcaVersion;

		/// <summary>
		/// クラウドデータバンク商品情報
		/// </summary>
		public List<CloudDataBankGoods> CloudDataBankGoodsList;

		/// <summary>
		/// 仕入データ出力ファイルパス名
		/// </summary>
		public string Pathname
		{
			get
			{
				return Path.Combine(ExportDir, ExportFilename);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CloudDataBankPurchaseOutputSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			CloudDataBankGoodsList = new List<CloudDataBankGoods>();
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

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetCloudDataBankGoods()
		{
			string str = string.Empty;
			foreach (CloudDataBankGoods goods in CloudDataBankGoodsList)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}
	}
}
