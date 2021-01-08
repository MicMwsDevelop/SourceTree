//
// AlmexMaintePurchaseFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/01/06 勝呂)
//
using MwsLib.BaseFactory.Mail;
using System;
using System.Collections.Generic;
using System.IO;

namespace AlmexMaintePurchaseFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class AlmexMaintePurchaseFileSettings : ICloneable
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
		/// 伝票番号初期値
		/// </summary>
		public int InitDenNo;

		/// <summary>
		/// クラウドデータバンク商品情報
		/// </summary>
		public List<AlmexMainteGoods> AlmexMainteGoodsList;

		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

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
		public AlmexMaintePurchaseFileSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			InitDenNo = 20801;
			AlmexMainteGoodsList = new List<AlmexMainteGoods>();
			Mail = new MailSettings();
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
		public string GetAlmexMainteGoods()
		{
			string str = string.Empty;
			foreach (AlmexMainteGoods goods in AlmexMainteGoodsList)
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
