//
// MailLogOut.cs
// 
// メールログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
//
using System;
using System.Collections.Generic;

namespace AdjustServiceApply.Log
{
	public class MailLogOut : List<string>
	{
		public MailLogOut()
		{
		}

		/// <summary>
		/// ログ文字列追加
		/// </summary>
		/// <param name="msg"></param>
		new public void Add(string msg)
		{
			string log = string.Format("{0}::{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
			base.Add(log);
		}
	}
}
