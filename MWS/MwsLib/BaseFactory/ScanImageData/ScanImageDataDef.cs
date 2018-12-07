using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.ScanImageData
{
	public class ScanImageDataDef
	{
		/// <summary>
		/// 文書インデックスデータベース名
		/// </summary>
		public const string SCAN_IMAGE_DATA_DATABASE_NAME = "ScanImageData.db";

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
			User = 10,

			/// <summary>
			/// 保守契約
			/// </summary>
			Mainte = 20,

			/// <summary>
			/// 口座振替
			/// </summary>
			AccountTransfer = 30,

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
			Consent = 60,

			/// <summary>
			/// その他
			/// </summary>
			Etc = 99,
		}

		/// <summary>
		/// 登録・変更フォルダ名
		/// </summary>
		public static readonly string FolderUser = "toroku";

		/// <summary>
		/// 保守契約
		/// </summary>
		public static readonly string FolderMainte = "hosyu";

		/// <summary>
		/// 口座振替
		/// </summary>
		public static readonly string FolderAccountTransfer = "kofuri";

		/// <summary>
		/// 取引条件確認書
		/// </summary>
		public static readonly string FolderTransaction = "取引条件確認書";

		/// <summary>
		/// リモートサービス利用規約同意書
		/// </summary>
		public static readonly string FolderConsent = "リモートサービス利用規約同意書";
	}
}
