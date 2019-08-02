//
// EntryFinishedUserData.cs
//
// 終了ユーザー情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using System;

namespace MwsLib.BaseFactory.EntryFinishedUser
{
	/// <summary>
	/// 終了ユーザー情報
	/// </summary>
	public class EntryFinishedUserData
	{
		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 終了月
		/// </summary>
		public YearMonth? FinishedYearMonth { get; set; }

		/// <summary>
		/// 終了届受領日
		/// </summary>
		public Date? AcceptDate { get; set; }

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
		/// 非paletteユーザー
		/// </summary>
		public bool NonPaletteUser { get; set; }

		/// <summary>
		/// システム名
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// システムコード
		/// </summary>
		public string SystemCode { get; set; }

		/// <summary>
		/// リプレース先コード
		/// </summary>
		public string ReplaceCode { get; set; }

		/// <summary>
		/// 拠点コード
		/// </summary>
		public string AreaCode { get; set; }

		/// <summary>
		/// 都道府県名
		/// </summary>
		public string KenName { get; set; }

		/// <summary>
		/// 販売店ID
		/// </summary>
		public string HanbaitenID { get; set; }

		/// <summary>
		/// 販売店名称
		/// </summary>
		public string HanbaitenName { get; set; }

		/// <summary>
		/// 有効ユーザーフラグ
		/// </summary>
		public bool EnableUserFlag { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool FinishedUser { get; set; }

		/// <summary>
		/// 除外
		/// </summary>
		public string Expcet { get; set; }

		/// <summary>
		/// コメント
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// 終了ユーザー対象システムかどうか？
		/// </summary>
		/// <returns>判定</returns>
		public bool IsEnableSystem
		{
			get {
				if (MwsDefine.SystemCodePaletteNetwork == SystemCode)
				{
					return true;
				}
				if (MwsDefine.SystemCodePaletteStandalone == SystemCode)
				{
					return true;
				}
				if (MwsDefine.SystemCodeEtc == SystemCode)
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// フィールド名の取得
		/// </summary>
		public static string[] FieldName
		{
			get
			{
				string[] ret = new string[17];
				ret[0] = "顧客No";
				ret[1] = "得意先No";
				ret[2] = "顧客名";
				ret[3] = "システム名";
				ret[4] = "拠点コード";
				ret[5] = "拠点名";
				ret[6] = "都道府県名";
				ret[7] = "終了事由";
				ret[8] = "リプレース";
				ret[9] = "理由";
				ret[10] = "コメント";
				ret[11] = "有効ユーザーフラグ";
				ret[12] = "除外";
				ret[13] = "販売店ID";
				ret[14] = "販売店名称";
				ret[15] = "終了月";
				ret[16] = "非paletteユーザー";
				return ret;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EntryFinishedUserData()
		{
			TokuisakiNo = string.Empty;
			CustomerID = 0;
			UserName = string.Empty;
			FinishedYearMonth = null;
			AcceptDate = null;
			FinishedReason = string.Empty;
			Replace = string.Empty;
			Reason = string.Empty;
			NonPaletteUser = false;
			SystemCode = string.Empty;
			SystemName = string.Empty;
			AreaName = string.Empty;
			ReplaceCode = string.Empty;
			AreaCode = string.Empty;
			KenName = string.Empty;
			HanbaitenID = string.Empty;
			HanbaitenName = string.Empty;
			EnableUserFlag = false;
			FinishedUser = false;
			Expcet = string.Empty;
			Comment = string.Empty;
		}

		/// <summary>
		/// 使用終了予定メモ文字列の取得
		/// </summary>
		/// <returns>メモ文字列</returns>
		public string GetMemoPlanString()
		{
			if (0 < Replace.Length)
			{
				if (0 < Reason.Length)
				{
					return string.Format("【使用終了予定】\r\n終了事由:{0}({1})\r\n終了月:{2}\r\n理由:{3}", FinishedReason, Replace, FinishedYearMonth.ToString(), Reason);
				}
				return string.Format("【使用終了予定】\r\n終了事由:{0}({1})\r\n終了月:{2}", FinishedReason, Replace, FinishedYearMonth.ToString());
			}
			if (0 < Reason.Length)
			{
				return string.Format("【使用終了予定】\r\n終了事由:{0}\r\n終了月:{1}\r\n\r\n理由:{2}", FinishedReason, FinishedYearMonth.ToString(), Reason);
			}
			return string.Format("【使用終了予定】\r\n終了事由:{0}\r\n終了月:{1}", FinishedReason, FinishedYearMonth.ToString());
		}

		/// <summary>
		/// 使用終了メモ文字列の取得
		/// </summary>
		/// <returns>メモ文字列</returns>
		public string GetMemoFinishedString()
		{
			if (0 < Replace.Length)
			{
				if (0 < Reason.Length)
				{
					return string.Format("【使用終了】\r\n終了事由:{0}({1})\r\n終了月:{2}\r\n\r\n理由:{3}", FinishedReason, Replace, FinishedYearMonth.ToString(), Reason);
				}
				return string.Format("【使用終了】\r\n終了事由:{0}({1})\r\n終了月:{2}", FinishedReason, Replace, FinishedYearMonth.ToString());
			}
			if (0 < Reason.Length)
			{
				return string.Format("【使用終了】\r\n終了事由:{0}\r\n終了月:{1}\r\n\r\n理由:{2}", FinishedReason, FinishedYearMonth.ToString(), Reason);
			}
			return string.Format("【使用終了】\r\n終了事由:{0}\r\n終了月:{1}", FinishedReason, FinishedYearMonth.ToString());
		}

		/// <summary>
		/// 前月終了月ユーザーかどうか？
		/// </summary>
		/// <param name="ym">当月</param>
		/// <returns>判定</returns>
		public bool IsPrevMonthFinishedUser(YearMonth ym)
		{
			//if (false == FinishedUser)
			{
				if (FinishedYearMonth.HasValue)
				{
					if (FinishedYearMonth.Value == ym - 1)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// tMic終了ユーザーリストに変換
		/// </summary>
		/// <returns>tMic終了ユーザーリスト</returns>
		public tMic終了ユーザーリスト To_tMic終了ユーザーリスト()
		{
			tMic終了ユーザーリスト ret = new tMic終了ユーザーリスト
			{
				得意先No = TokuisakiNo,
				終了月 = FinishedYearMonth,
				終了届受領日 = AcceptDate,
				終了事由 = FinishedReason,
				リプレース = Replace,
				理由 = Reason,
				非paletteユーザー = NonPaletteUser
			};
			return ret;
		}

		/// <summary>
		/// tMemoに変換
		/// </summary>
		/// <returns>tMemo</returns>
		public tMemo To_tMemo()
		{
			tMemo ret = new tMemo
			{
				fMemKey = CustomerID,
				fMemTable = "tClient",
				fMemType = string.Format("{0} {1:D2}:{2:D2} 営業管理部", new Date(DateTime.Now).ToString(), DateTime.Now.Hour, DateTime.Now.Minute),
				fMemUpdate = DateTime.Now,
				fMemUpdateMan = "営業管理部"
			};
			return ret;
		}
	}
}
