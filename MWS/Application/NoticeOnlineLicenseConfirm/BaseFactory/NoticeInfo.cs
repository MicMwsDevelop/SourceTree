//
// NoticeInfo.cs
//
// 通知情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/19 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Sales.View;
using CommonLib.DB.SqlServer.Junp;
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

		/// <summary>
		/// MIC連絡担当者の通知情報を取得
		/// </summary>
		/// <param name="webHS">Webヒアリングシート</param>
		/// <param name="病院ID">病院ID</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>通知情報</returns>
		public static NoticeInfo GetNoticeInfo(List<vオンライン資格確認ユーザー> webHS, int 病院ID, string connectStr)
		{
			vオンライン資格確認ユーザー hs = webHS.Find(p => p.顧客No == 病院ID);
			if (null != hs)
			{
				NoticeInfo notice = new NoticeInfo();
				if (0 < hs.MIC連絡担当者社員番号.Length)
				{
					List<tUser> userList = JunpDatabaseAccess.Select_tUser(string.Format("fUsrID = '{0}'", hs.MIC連絡担当者社員番号), "", connectStr);
					if (null != userList && 0 < userList.Count)
					{
						notice.メール送信指示 = "●";
						notice.社員番号 = userList[0].fUsrID;
						notice.MIC連絡担当者 = userList[0].fUsrName;
						notice.MailAddress = userList[0].fUsrEmail;
						return notice;
					}
				}
				if (0 < hs.MIC連絡担当者.Length)
				{
					string name = hs.MIC連絡担当者.Replace(" ", "").Replace("　", "");
					List<tUser> userList = JunpDatabaseAccess.Select_tUser(string.Format("[fUsrBusho1] = '40' AND REPLACE([fUsrName], ' ', '') LIKE '{0}%'", name), "", connectStr);
					if (null != userList && 0 < userList.Count)
					{
						notice.メール送信指示 = "●";
						notice.社員番号 = userList[0].fUsrID;
						notice.MIC連絡担当者 = userList[0].fUsrName;
						notice.MailAddress = userList[0].fUsrEmail;
						return notice;
					} 
					notice.MIC連絡担当者 = hs.MIC連絡担当者;
					return notice;
				}
				else
				{
					notice.MIC連絡担当者 = hs.更新者;
					return notice;
				}
			}
			return null;
		}
	}
}
