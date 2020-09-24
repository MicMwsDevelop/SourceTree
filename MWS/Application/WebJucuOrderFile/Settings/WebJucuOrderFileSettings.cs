//
// WebJucuOrderFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/09/23 勝呂)
//
using System;
using System.Collections.Generic;
using System.IO;

namespace WebJucuOrderFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class WebJucuOrderFileSettings : ICloneable
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string ExportDir;

		/// <summary>
		/// 受注日
		/// </summary>
		public int? OrderDate;

		/// <summary>
		/// WebJucu受注明細ファイル名の取得
		/// </summary>
		private static string OutputFilename
		{
			get
			{
				return string.Format("WebJucu-{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
			}
		}

		/// <summary>
		/// 出力ファイルパス名
		/// </summary>
		public string Pathname
		{
			get
			{
				return Path.Combine(ExportDir, OutputFilename);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebJucuOrderFileSettings()
        {
			ExportDir = string.Empty;
			OrderDate = null;
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
