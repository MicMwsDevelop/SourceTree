//
// ScanImageManagerDef.cs
// 
// 文書インデックス管理 定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MwsLib.BaseFactory.ScanImageManager
{
	/// <summary>
	/// 文書インデックス定義関連
	/// </summary>
	public class ScanImageManagerDef
	{
		/// <summary>
		/// 文書種別
		/// </summary>
		public enum ScanDocumentType
		{
			/// <summary>
			/// 初期値
			/// </summary>
			None = 0,

			/// <summary>
			/// 登録・変更
			/// </summary>
			Toroku = 10,

			/// <summary>
			/// 保守契約
			/// </summary>
			Hosyu = 20,

			/// <summary>
			/// 口座振替
			/// </summary>
			Kofuri = 30,

			/// <summary>
			/// 取引条件確認書
			/// </summary>
			Transaction = 40,

			/// <summary>
			/// 電レセ登録
			/// </summary>
			RezeptCompute = 50,

			/// <summary>
			/// リモートサービス利用規約同意書
			/// </summary>
			Remote = 60,

			/// <summary>
			/// PC安心サポート
			/// </summary>
			PcSupport = 70,

			/// <summary>
			/// その他
			/// </summary>
			Etc = 99,
		}

		/// <summary>
		/// サムネイルファイル名
		/// </summary>
		public static readonly string TempThumbnailFile = @"Thumbs.db";

		/// <summary>
		/// 登録・変更フォルダ名
		/// </summary>
		public static readonly string FolderToroku = @"Toroku";

		/// <summary>
		/// 保守契約フォルダ名
		/// </summary>
		public static readonly string FolderHoshu = @"Hosyu";

		/// <summary>
		/// 保守契約解約フォルダ名
		/// </summary>
		public static readonly string FolderHoshuKaiyaku = Path.Combine(FolderHoshu, @"Kaiyaku");

		/// <summary>
		/// 保守契約加入フォルダ名
		/// </summary>
		public static readonly string FolderHoshuKanyu = Path.Combine(FolderHoshu, @"Kanyu");

		/// <summary>
		/// 口座振替フォルダ名
		/// </summary>
		public static readonly string FolderKofuri = @"Kofuri";

		/// <summary>
		/// 取引条件確認書フォルダ名
		/// </summary>
		public static readonly string FolderTransaction = @"取引条件確認書";

		/// <summary>
		/// リモートサービス利用規約同意書フォルダ名
		/// </summary>
		public static readonly string FolderRemote = @"リモートサービス利用規約同意書";

		/// <summary>
		/// PC安心サポートフォルダ名
		/// </summary>
		public static readonly string FolderPcSupport = @"PC安心サポート";

		/// <summary>
		/// 得意先検索
		/// </summary>
		public static readonly string TokuisakiSearch = @"得意先検索";
	}
}
