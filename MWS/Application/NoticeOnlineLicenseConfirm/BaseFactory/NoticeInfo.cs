//
// NoticeInfo.cs
//
// 通知情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
// 
using ClosedXML.Excel;
using System.Collections.Generic;

namespace NoticeOnlineLicenseConfirm.BaseFactory
{
	/// <summary>
	/// 通知情報
	/// </summary>
	public class NoticeInfo
	{
		public string メール送信指示 { get; set; }
		public string MIC連絡担当者 { get; set; }
		public string 社員番号 { get; set; }
		public string MailAddress { get; set; }

		/// <summary>
		/// メール送信するかどうか？
		/// </summary>
		public bool IsSendMail
		{
			get
			{
				return ("●" == メール送信指示) ? true : false;
			}
		}

		/// <summary>
		/// メール送信するかどうか？
		/// </summary>
		public bool IsEnableSendMail
		{
			get
			{
				if (IsSendMail && 0 < MIC連絡担当者.Length && 0 < MailAddress.Length)
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// カラム数の取得
		/// </summary>
		public int GetColumn
		{
			get
			{
				return 4;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NoticeInfo()
		{
			Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Clear()
		{
			メール送信指示 = string.Empty;
			MIC連絡担当者 = string.Empty;
			社員番号 = string.Empty;
			MailAddress = string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<string> GetData()
		{
			List<string> ret = new List<string>();
			ret.Add(メール送信指示);
			ret.Add(MIC連絡担当者);
			ret.Add(社員番号);
			ret.Add(MailAddress);
			return ret;
		}

		/// <summary>
		/// ワークシートの設定
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void ReadWorksheet(IXLWorksheet ws, int row)
		{
			メール送信指示 = ws.Cell(row, 1).GetString();
			MIC連絡担当者 = ws.Cell(row, 2).GetString();
			社員番号 = ws.Cell(row, 3).GetString();
			MailAddress = ws.Cell(row, 4).GetString();
		}
	}
}
