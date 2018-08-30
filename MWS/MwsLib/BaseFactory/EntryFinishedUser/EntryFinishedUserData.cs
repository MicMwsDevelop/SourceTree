using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace MwsLib.BaseFactory.EntryFinishedUser
{
	public class EntryFinishedUserData
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public string CostomerID { get; set; }

		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// レセコン名称
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// 拠点コード
		/// </summary>
		public string AreaCode { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// 都道府県名
		/// </summary>
		public string KenName { get; set; }

		/// <summary>
		/// 終了事由
		/// </summary>
		public string FinishedReason { get; set; }

		/// <summary>
		/// リプレース
		/// </summary>
		public string Replace { get; set; }

		/// <summary>
		/// 理由
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// コメント
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// 有効ユーザーフラグ
		/// </summary>
		public bool EnableUserFlag { get; set; }

		/// <summary>
		/// 除外
		/// </summary>
		public string Expcet { get; set; }

		/// <summary>
		/// 販売店ID
		/// </summary>
		public string HanbaitenID { get; set; }

		/// <summary>
		/// 販売店名称
		/// </summary>
		public string HanbaitenName { get; set; }

		/// <summary>
		/// 終了月
		/// </summary>
		public YearMonth? FinishedYearMonth { get; set; }

		/// <summary>
		/// 終了届受領日
		/// </summary>
		public Date? AcceptDate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EntryFinishedUserData()
		{
			CostomerID = string.Empty;
			TokuisakiNo = string.Empty;
			UserName = string.Empty;
			SystemName = string.Empty;
			AreaCode = string.Empty;
			AreaName = string.Empty;
			KenName = string.Empty;
			FinishedReason = string.Empty;
			Replace = string.Empty;
			Reason = string.Empty;
			Comment = string.Empty;
			EnableUserFlag = false;
			Expcet = string.Empty;
			HanbaitenID = string.Empty;
			HanbaitenName = string.Empty;
			FinishedYearMonth = null;
			AcceptDate = null;
		}
	}
}
