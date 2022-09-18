//
// StatusJudgement.cs
// 
// ステータス判定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
//
using CommonLib.Common;
using OnlineLicenseIntroductionStatus.Settings;
using System.Collections.Generic;

namespace OnlineLicenseIntroductionStatus.BaseFactory
{
	/// <summary>
	/// ステータス判定定義
	/// </summary>
	public class StatusJudgement
	{
		/// <summary>
		/// 導入意志定義リスト
		/// </summary>
		public List<string> 導入意志 { get; set; }

		/// <summary>
		/// 工事種別定義リスト
		/// </summary>
		public List<string> 工事種別 { get; set; }

		/// <summary>
		/// ステータス定義リスト
		/// </summary>
		public List<string> ステータス { get; set; }

		/// <summary>
		/// 導入意志あり定義
		/// </summary>
		public List<string> 導入意志あり_導入意思 { get; set; }

		/// <summary>
		/// 未確認_反応無し定義
		/// </summary>
		public List<string> 未確認_反応無し_導入意思 { get; set; }

		/// <summary>
		/// NTT_外注_依頼数定義
		/// </summary>
		public List<string> NTT_外注_依頼数_工事種別 { get; set; }

		/// <summary>
		/// IPSEC依頼提出数定義
		/// </summary>
		public List<string> IPSEC依頼提出数_工事種別 { get; set; }

		/// <summary>
		/// ヒアリングシート提出数定義
		/// </summary>
		public List<string> ヒアリングシート提出数_ステータス { get; set; }

		/// <summary>
		/// NTT案件納品数_工事種別定義
		/// </summary>
		public List<string> NTT案件納品数_工事種別 { get; set; }

		/// <summary>
		/// NTT案件納品数_ステータス定義
		/// </summary>
		public List<string> NTT案件納品数_ステータス { get; set; }

		/// <summary>
		/// IPSEC納品数_工事種別定義
		/// </summary>
		public List<string> IPSEC納品数_工事種別 { get; set; }

		/// <summary>
		/// IPSEC納品数_ステータス定義
		/// </summary>
		public List<string> IPSEC納品数_ステータス { get; set; }

		/// <summary>
		/// MIC自力_その他納品数_工事種別定義
		/// </summary>
		public List<string> MIC自力_その他納品数_工事種別 { get; set; }

		/// <summary>
		/// MIC自力_その他納品数_ステータス定義
		/// </summary>
		public List<string> MIC自力_その他納品数_ステータス { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public StatusJudgement()
		{
			導入意志 = null;
			工事種別 = null;
			ステータス = null;
			導入意志あり_導入意思 = null;
			未確認_反応無し_導入意思 = null;
			NTT_外注_依頼数_工事種別 = null;
			IPSEC依頼提出数_工事種別 = null;
			ヒアリングシート提出数_ステータス = null;
			NTT案件納品数_工事種別 = null;
			NTT案件納品数_ステータス = null;
			IPSEC納品数_工事種別 = null;
			IPSEC納品数_ステータス = null;
			MIC自力_その他納品数_工事種別 = null;
			MIC自力_その他納品数_ステータス = null;
		}


		/// <summary>
		/// ステータス判定定義への反映
		/// </summary>
		/// <param name="xml">環境設定</param>
		public void SetStatusJudgement(OnlineLicenseIntroductionStatusSettings xml)
		{
			導入意志 = SplitString.CSVSplitLine2(xml.導入意志);
			工事種別 = SplitString.CSVSplitLine2(xml.工事種別);
			ステータス = SplitString.CSVSplitLine2(xml.ステータス);
			導入意志あり_導入意思 = SplitString.CSVSplitLine2(xml.導入意志あり_導入意思);
			未確認_反応無し_導入意思 = SplitString.CSVSplitLine2(xml.未確認_反応無し_導入意思);
			NTT_外注_依頼数_工事種別 = SplitString.CSVSplitLine2(xml.NTT_外注_依頼数_工事種別);
			IPSEC依頼提出数_工事種別 = SplitString.CSVSplitLine2(xml.IPSEC依頼提出数_工事種別);
			ヒアリングシート提出数_ステータス = SplitString.CSVSplitLine2(xml.ヒアリングシート提出数_ステータス);
			NTT案件納品数_工事種別 = SplitString.CSVSplitLine2(xml.NTT案件納品数_工事種別);
			NTT案件納品数_ステータス = SplitString.CSVSplitLine2(xml.NTT案件納品数_ステータス);
			IPSEC納品数_工事種別 = SplitString.CSVSplitLine2(xml.IPSEC納品数_工事種別);
			IPSEC納品数_ステータス = SplitString.CSVSplitLine2(xml.IPSEC納品数_ステータス);
			MIC自力_その他納品数_工事種別 = SplitString.CSVSplitLine2(xml.MIC自力_その他納品数_工事種別);
			MIC自力_その他納品数_ステータス = SplitString.CSVSplitLine2(xml.MIC自力_その他納品数_ステータス);
		}

		/// <summary>
		/// 導入意志定義文字列が間違っているか？
		/// </summary>
		/// <param name="str">導入意思文字列</param>
		/// <returns>判定</returns>
		public bool IsMistake導入意志(string str)
		{
			if (0 == str.Length)
			{
				// 不明
				return false;
			}
			return (-1 != 導入意志.FindIndex(p => p == str)) ? false : true;
		}

		/// <summary>
		/// 工事種別定義文字列が間違っているか？
		/// </summary>
		/// <param name="str">工事種別文字列</param>
		/// <returns>判定</returns>
		public bool IsMistake工事種別(string str)
		{
			if (0 == str.Length)
			{
				// 不明
				return false;
			}
			return (-1 != 工事種別.FindIndex(p => p == str)) ? false : true;
		}

		/// <summary>
		/// ステータス定義文字列が間違っているか？
		/// </summary>
		/// <param name="str">ステータス文字列</param>
		/// <returns>判定</returns>
		public bool IsMistakeステータス(string str)
		{
			if (0 == str.Length)
			{
				// 不明
				return false;
			}
			return (-1 != ステータス.FindIndex(p => p == str)) ? false : true;
		}

		/// <summary>
		/// 導入意志ありかどうか？
		/// 導入意思：導入する
		/// </summary>
		/// <param name="str">導入意思文字列</param>
		/// <returns>判定</returns>
		public bool Is導入意志あり(string str)
		{
			return (-1 != 導入意志あり_導入意思.FindIndex(p => p == str)) ? true : false;
		}

		/// <summary>
		/// 未確認_反応無しかどうか？
		/// 導入意思：わからない
		/// </summary>
		/// <param name="str">導入意思文字列</param>
		/// <returns>判定</returns>
		public bool Is未確認_反応無し(string str)
		{
			return (-1 != 未確認_反応無し_導入意思.FindIndex(p => p == str)) ? true : false;
		}

		/// <summary>
		/// NTT_外注_依頼数かどうか？
		/// 工事種別：NTT（通常）、NTT（現調パック）
		/// </summary>
		/// <param name="construction">工事種別文字列</param>
		/// <returns>判定</returns>
		public bool IsNTT_外注_依頼数(string construction)
		{
			return (-1 != NTT_外注_依頼数_工事種別.FindIndex(p => p == construction)) ? true : false;
		}

		/// <summary>
		/// IPSEC依頼提出数かどうか？
		/// 工事種別：IPsec（菱洋）、IPsec（その他）
		/// </summary>
		/// <param name="construction">工事種別文字列</param>
		/// <returns>判定</returns>
		public bool IsIPSEC依頼提出数(string construction)
		{
			return (-1 != IPSEC依頼提出数_工事種別.FindIndex(p => p == construction)) ? true : false;
		}

		/// <summary>
		/// ヒアリングシート提出数かどうか？
		/// ステータス：ヒアリングシート提出済
		/// </summary>
		/// <param name="status">ステータス文字列</param>
		/// <returns>判定</returns>
		public bool Isヒアリングシート提出数(string status)
		{
			return (-1 != ヒアリングシート提出数_ステータス.FindIndex(p => p == status)) ? true : false;
		}

		/// <summary>
		/// NTT案件納品数かどうか？
		/// 工事種別：NTT（通常）、NTT（現調パック）
		/// ステータス：納品完了
		/// </summary>
		/// <param name="construction">工事種別文字列</param>
		/// <param name="status">ステータス文字列</param>
		/// <returns>判定</returns>
		public bool IsNTT案件納品数(string construction, string status)
		{
			if (-1 != NTT案件納品数_ステータス.FindIndex(p => p == status))
			{
				return (-1 != NTT案件納品数_工事種別.FindIndex(p => p == construction)) ? true : false;
			}
			return false;
		}

		/// <summary>
		/// IPSEC納品数かどうか？
		/// 工事種別：IPsec（菱洋）、IPsec（その他）
		/// ステータス：納品完了
		/// </summary>
		/// <param name="construction">工事種別文字列</param>
		/// <param name="status">ステータス文字列</param>
		/// <returns>判定</returns>
		public bool IsIPSEC納品数(string construction, string status)
		{
			if (-1 != IPSEC納品数_ステータス.FindIndex(p => p == status))
			{
				return (-1 != IPSEC納品数_工事種別.FindIndex(p => p == construction)) ? true : false;
			}
			return false;
		}

		/// <summary>
		/// MIC自力_その他納品数かどうか？
		/// 工事種別：ミック独力、外注（その他）
		/// ステータス：納品完了、納品完了（コニュファ）、納品完了（アイネット）、納品完了（BBIQ）
		/// </summary>
		/// <param name="construction">工事種別文字列</param>
		/// <param name="status">ステータス文字列</param>
		/// <returns>判定</returns>
		public bool IsMIC自力_その他納品数(string construction, string status)
		{
			if (-1 != MIC自力_その他納品数_ステータス.FindIndex(p => p == status))
			{
				return (-1 != MIC自力_その他納品数_工事種別.FindIndex(p => p == construction)) ? true : false;
			}
			return false;
		}
	}
}
