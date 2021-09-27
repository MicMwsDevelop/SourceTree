//
// ScanImageFile.cs
//
// 文書インデックス管理 スキャナーファイル情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace CommonLib.BaseFactory.ScanImageManager
{
	/// <summary>
	/// スキャナーファイル情報
	/// </summary>
	public class ScanImageFile
	{
		/// <summary>
		/// ファイル名
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 医院名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// フォルダ名
		/// </summary>
		public string FolderName { get; set; }

		/// <summary>
		/// ファイル更新日時
		/// </summary>
		public DateTime? FileDateTime { get; set; }

		/// <summary>
		/// 文書種別
		/// </summary>
		public ScanImageManagerDef.ScanDocumentType Document { get; set; }

		/// <summary>
		/// パス名の取得
		/// </summary>
		public string Pathname
		{
			get
			{
				return Path.Combine(FolderName, FileName);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ScanImageFile()
		{
			CustomerNo = 0;
			TokuisakiNo = string.Empty;
			ClinicName = string.Empty;
			FolderName = string.Empty;
			FileName = string.Empty;
			FileDateTime = null;
			Document = ScanImageManagerDef.ScanDocumentType.None;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="other">ユーザー登録情報</param>
		public ScanImageFile(ScanImageFile other)
		{
			CustomerNo = other.CustomerNo;
			TokuisakiNo = other.TokuisakiNo;
			ClinicName = other.ClinicName;
			FolderName = other.FolderName;
			FileName = other.FileName;
			FileDateTime = other.FileDateTime;
			Document = other.Document;
		}

		/// <summary>
		/// ファイル名称から得意先番号を取得
		/// </summary>
		/// <returns>得意先番号</returns>
		public string GetToluisakiNo()
		{
			string no = Regex.Replace(FileName, @"[^0-9]", "");
			if (6 <= no.Length)
			{
				return no.Substring(0, 6);
			}
			return string.Empty;
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic文書インデクス]格納用フォルダ名の取得
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public string GetFolderName(string path)
		{
			if (0 < FolderName.Length)
			{
				return FolderName.Replace(path + @"\", "");
			}
			return string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rootPath"></param>
		/// <param name="folder"></param>
		public void SetFolderName(string rootPath, string folder)
		{
			FolderName = folder.Replace(rootPath + @"\", "");
		}

		/// <summary>
		/// ログ出力文字列の取得
		/// </summary>
		/// <returns>得意先検索行</returns>
		public string LogOut()
		{
			string[] log = new string[4];
			log[0] = FileName;
			log[1] = CustomerNo.ToString();
			log[2] = TokuisakiNo;
			log[3] = ClinicName;
			return string.Join(",", log);
		}

		/// <summary>
		/// 得意先検索行の取得
		/// </summary>
		/// <returns>得意先検索行</returns>
		public string Output()
		{
			string[] log = new string[3];
			log[0] = CustomerNo.ToString();
			log[1] = "\"" + TokuisakiNo + "\"";
			log[2] = "\"" + ClinicName + "\"";
			return string.Join(",", log);
		}

		/// <summary>
		///  顧客情報リストの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>顧客情報リスト</returns>
		public static List<ScanImageFile> ConvertCustomerInfoList(DataTable table)
		{
			List<ScanImageFile> result = null;
			if (null != table)
			{
				result = new List<ScanImageFile>();
				foreach (DataRow row in table.Rows)
				{
					ScanImageFile data = new ScanImageFile();
					data.TokuisakiNo = row["得意先No"].ToString().Trim();
					data.CustomerNo = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.ClinicName = row["顧客名"].ToString().Trim();
					result.Add(data);
				}
			}
			return result;
		}
	}
}
