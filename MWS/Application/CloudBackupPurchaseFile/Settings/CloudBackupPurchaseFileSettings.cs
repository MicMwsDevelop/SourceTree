﻿//
// CloudBackupPurchaseFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/06 勝呂)
//
using MwsLib.BaseFactory.Mail;
using System;
using System.Collections.Generic;
using System.IO;

namespace CloudBackupPurchaseFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class CloudBackupPurchaseFileSettings : ICloneable
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
		public List<CloudBackupGoods> CloudBackupGoodsList;

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
		public CloudBackupPurchaseFileSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			InitDenNo = 20501;
			CloudBackupGoodsList = new List<CloudBackupGoods>();
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
		public string GetCloudBackupGoods()
		{
			string str = string.Empty;
			foreach (CloudBackupGoods goods in CloudBackupGoodsList)
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