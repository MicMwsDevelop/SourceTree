//
// tMih受注詳細.cs
//
// 受注詳細情報クラス
// [JunpDB].[dbo].[tMih受注詳細]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMih受注詳細
	{
		public int f受注番号 { get; set;}
		public int f年度 { get;set;}
		public string f商品コード { get;set;}
		public int f表示順 { get;set;}
		public short? f区分 { get;set;}
		public string f区分名 { get; set; }
		public string f商品名 { get; set; }
		public int? f数量 { get; set; }
		public int? f標準価格 { get; set; }
		public int? f金額 { get; set; }
		public int? f提供価格 { get; set; }
		public string f税区分 { get; set; }
		public short? f税率 { get; set; }
		public string f税込区分 { get; set; }
		public int? f売上原価 { get; set; }
		public string f掛率 { get; set; }
		public short? f商品区分1 { get; set; }
		public short? f商品区分2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMih受注詳細()
		{
		}
	}
}
