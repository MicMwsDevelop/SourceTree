//
// tMic出荷代行トップ印刷休業日.cs
//
// 出荷代行トップ印刷休業日クラス
// [JunpDB].[dbo].[tMic出荷代行トップ印刷休業日]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using System;

namespace MwsLib.BaseFactory.Junp.Table
{
	public class tMic出荷代行トップ印刷休業日
	{
		public int fID { get; set; }
		public DateTime? f日付 { get; set; }
		public string f種別 { get; set; }
		public string f名称 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMic出荷代行トップ印刷休業日()
		{
			fID = 0;
			f日付 = null;
			f種別 = string.Empty;
			f名称 = string.Empty;
		}
	}
}
