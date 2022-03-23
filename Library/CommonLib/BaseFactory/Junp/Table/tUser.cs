//
// tUser.cs
//
// MIC社員情報クラス
// [JunpDB].[dbo].[tUser]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2022/03/09 勝呂)
//
using System;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tUser
	{
		public string fUsrID { get; set; }
		public string fUsrName { get; set; }
		public string fUsrNameYomi { get; set; }
		public string fUsrYaku { get; set; }
		public string fUsrBusho1 { get; set; }
		public string fUsrBusho2 { get; set; }
		public string fUsrBusho3 { get; set; }
		public string fUsrLoginID { get; set; }
		public string fUsrLoginPwd { get; set; }
		public string fUsrEmail { get; set; }
		public string fUsrPriority { get; set; }
		public string fUsrMailSetting1 { get; set; }
		public string fUsrMailSetting2 { get; set; }
		public string fUsrMailSetting3 { get; set; }
		public string fUsrMailSetting4 { get; set; }
		public string fUsrMailSetting5 { get; set; }
		public DateTime? fUsrUpdate { get; set; }
		public string fUsrUpdateMan { get; set; }
		public string fUsrNTLoginID { get; set; }
		public string fUsrTel { get; set; }
		public string fUsrTeianAcc { get; set; }
		public string fUsrGWFlg { get; set; }
		public string fUsrBbsAcc { get; set; }
		public string fUsrMobileFlg { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tUser()
		{
			fUsrID = string.Empty;
			fUsrName = string.Empty;
			fUsrNameYomi = string.Empty;
			fUsrYaku = string.Empty;
			fUsrBusho1 = string.Empty;
			fUsrBusho2 = string.Empty;
			fUsrBusho3 = string.Empty;
			fUsrLoginID = string.Empty;
			fUsrLoginPwd = string.Empty;
			fUsrEmail = string.Empty;
			fUsrPriority = string.Empty;
			fUsrMailSetting1 = string.Empty;
			fUsrMailSetting2 = string.Empty;
			fUsrMailSetting3 = string.Empty;
			fUsrMailSetting4 = string.Empty;
			fUsrMailSetting5 = string.Empty;
			fUsrUpdate = null;
			fUsrUpdateMan = string.Empty;
			fUsrNTLoginID = string.Empty;
			fUsrTel = string.Empty;
			fUsrTeianAcc = string.Empty;
			fUsrGWFlg = string.Empty;
			fUsrBbsAcc = string.Empty;
			fUsrMobileFlg = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tUser> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tUser> result = new List<tUser>();
				foreach (DataRow row in table.Rows)
				{
					tUser data = new tUser
					{
						fUsrID = row["fUsrID"].ToString().Trim(),
						fUsrName = row["fUsrName"].ToString().Trim(),
						fUsrNameYomi = row["fUsrNameYomi"].ToString().Trim(),
						fUsrYaku = row["fUsrYaku"].ToString().Trim(),
						fUsrBusho1 = row["fUsrBusho1"].ToString().Trim(),
						fUsrBusho2 = row["fUsrBusho2"].ToString().Trim(),
						fUsrBusho3 = row["fUsrBusho3"].ToString().Trim(),
						fUsrLoginID = row["fUsrLoginID"].ToString().Trim(),
						fUsrLoginPwd = row["fUsrLoginPwd"].ToString().Trim(),
						fUsrEmail = row["fUsrEmail"].ToString().Trim(),
						fUsrPriority = row["fUsrPriority"].ToString().Trim(),
						fUsrMailSetting1 = row["fUsrMailSetting1"].ToString().Trim(),
						fUsrMailSetting2 = row["fUsrMailSetting2"].ToString().Trim(),
						fUsrMailSetting3 = row["fUsrMailSetting3"].ToString().Trim(),
						fUsrMailSetting4 = row["fUsrMailSetting4"].ToString().Trim(),
						fUsrMailSetting5 = row["fUsrMailSetting5"].ToString().Trim(),
						fUsrUpdate = DataBaseValue.ConvObjectToDateTimeNull(row["fUsrUpdate"]),
						fUsrUpdateMan = row["fUsrUpdateMan"].ToString().Trim(),
						fUsrNTLoginID = row["fUsrNTLoginID"].ToString().Trim(),
						fUsrTel = row["fUsrTel"].ToString().Trim(),
						fUsrTeianAcc = row["fUsrTeianAcc"].ToString().Trim(),
						fUsrGWFlg = row["fUsrGWFlg"].ToString().Trim(),
						fUsrBbsAcc = row["fUsrBbsAcc"].ToString().Trim(),
						fUsrMobileFlg = row["fUsrMobileFlg"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
