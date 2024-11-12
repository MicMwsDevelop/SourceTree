//
// 商品マスタ.cs
//
// 汎用データレイアウト PCA商品マスタ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/10/16 勝呂):新規作成
// 
using System;

namespace CommonLib.BaseFactory.Pca
{
	/// <summary>
	/// PCA商品マスタ
	/// </summary>
	public class 商品マスタ
	{
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public int システム区分 { get; set; }
		public int マスター区分 { get; set; }
		public int 在庫管理 { get; set; }
		public int 実績管理 { get; set; }
		public string 単位 { get; set; }
		public int 入数 { get; set; }
		public string 規格_型番 { get; set; }
		public string 色 { get; set; }
		public string ｻｲｽﾞ { get; set; }
		public string 商品区分1コード { get; set; }
		public string 商品区分2コード { get; set; }
		public string 商品区分3コード { get; set; }
		public string 商品区分4コード { get; set; }
		public string 商品区分5コード { get; set; }
		public int 税区分 { get; set; }
		public int 税込区分 { get; set; }
		public int 単価小数桁 { get; set; }
		public int 数量小数桁 { get; set; }
		public int 標準価格 { get; set; }
		public int 原価 { get; set; }
		public int 売価1 { get; set; }
		public int 売価2 { get; set; }
		public int 売価3 { get; set; }
		public int 売価4 { get; set; }
		public int 売価5 { get; set; }
		public string 倉庫コード { get; set; }
		public string 主仕入先コード { get; set; }
		public int 在庫単価 { get; set; }
		public int 仕入単価 { get; set; }
		public string 売上計算式 { get; set; }
		public string 仕入計算式 { get; set; }
		public int 商品項目1 { get; set; }
		public int 商品項目2 { get; set; }
		public int 商品項目3 { get; set; }
		public int 使用区分 { get; set; }
		public string 商品ｺｰﾄﾞ2 { get; set; }
		public string 商品ｺｰﾄﾞ3 { get; set; }
		public int 入数小数桁 { get; set; }
		public int 箱数小数桁 { get; set; }
		public int 数量端数 { get; set; }
		public int 有効期間開始日 { get; set; }
		public int 有効期間終了日 { get; set; }
		public string コメント { get; set; }
		public string 商品名2 { get; set; }
		public int 単位区分2入数 { get; set; }
		public string 単位区分2単位 { get; set; }
		public string 単位区分2単位コメント { get; set; }
		public int 単位区分3入数 { get; set; }
		public string 単位区分3単位 { get; set; }
		public string 単位区分3単位コメント { get; set; }
		public int 単位区分4入数 { get; set; }
		public string 単位区分4単位 { get; set; }
		public string 単位区分4単位コメント { get; set; }
		public int 単位区分5入数 { get; set; }
		public string 単位区分5単位 { get; set; }
		public string 単位区分5単位コメント { get; set; }
		public int 売上単位区分 { get; set; }
		public int 仕入単位区分 { get; set; }
		public int ロット管理 { get; set; }
		public int 仕入税込区分 { get; set; }
		public int 売上税種別 { get; set; }
		public int 仕入税種別 { get; set; }
		public int 税種別切替 { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public 商品マスタ()
		{
			商品コード = string.Empty;
			商品名 = string.Empty;
			システム区分 = 0;
			マスター区分 = 0;
			在庫管理 = 0;
			実績管理 = 0;
			単位 = string.Empty;
			入数 = 0;
			規格_型番 = string.Empty;
			色 = string.Empty;
			ｻｲｽﾞ = string.Empty;
			商品区分1コード = string.Empty;
			商品区分2コード = string.Empty;
			商品区分3コード = string.Empty;
			商品区分4コード = string.Empty;
			商品区分5コード = string.Empty;
			税区分 = 0;
			税込区分 = 0;
			単価小数桁 = 0;
			数量小数桁 = 0;
			標準価格 = 0;
			原価 = 0;
			売価1 = 0;
			売価2 = 0;
			売価3 = 0;
			売価4 = 0;
			売価5 = 0;
			倉庫コード = string.Empty;
			主仕入先コード = string.Empty;
			在庫単価 = 0;
			仕入単価 = 0;
			売上計算式 = string.Empty;
			仕入計算式 = string.Empty;
			商品項目1 = 0;
			商品項目2 = 0;
			商品項目3 = 0;
			使用区分 = 0;
			商品ｺｰﾄﾞ2 = string.Empty;
			商品ｺｰﾄﾞ3 = string.Empty;
			入数小数桁 = 0;
			箱数小数桁 = 0;
			数量端数 = 0;
			有効期間開始日 = 0;
			有効期間終了日 = 0;
			コメント = string.Empty;
			商品名2 = string.Empty;
			単位区分2入数 = 0;
			単位区分2単位 = string.Empty;
			単位区分2単位コメント = string.Empty;
			単位区分3入数 = 0;
			単位区分3単位 = string.Empty;
			単位区分3単位コメント = string.Empty;
			単位区分4入数 = 0;
			単位区分4単位 = string.Empty;
			単位区分4単位コメント = string.Empty;
			単位区分5入数 = 0;
			単位区分5単位 = string.Empty;
			単位区分5単位コメント = string.Empty;
			売上単位区分 = 0;
			仕入単位区分 = 0;
			ロット管理 = 0;
			仕入税込区分 = 0;
			売上税種別 = 0;
			仕入税種別 = 0;
			税種別切替 = 0;
		}

		/// <summary>
		/// CSVレコード格納
		/// </summary>
		/// <param name="csv"></param>
		public bool SetCsvRecord(string[] csv)
		{
			if (65 <= csv.Length)
			{
				商品コード = csv[0].Trim('\"').Trim();
				商品名 = csv[1].Trim('\"').Trim();
				システム区分 = int.Parse(csv[2].Trim('\"'));
				マスター区分 = int.Parse(csv[3].Trim('\"'));
				在庫管理 = int.Parse(csv[4].Trim('\"'));
				実績管理 = int.Parse(csv[5].Trim('\"'));
				単位 = csv[6].Trim('\"').Trim();
				入数 = DoubleToString(csv[7]);
				規格_型番 = csv[8].Trim('\"').Trim();
				色 = csv[9].Trim('\"').Trim();
				ｻｲｽﾞ = csv[10].Trim('\"').Trim();
				商品区分1コード = csv[11].Trim('\"').Trim();
				商品区分2コード = csv[12].Trim('\"').Trim();
				商品区分3コード = csv[13].Trim('\"').Trim();
				商品区分4コード = csv[14].Trim('\"').Trim();
				商品区分5コード = csv[15].Trim('\"').Trim();
				税区分 = int.Parse(csv[16].Trim('\"'));
				税込区分 = int.Parse(csv[17].Trim('\"'));
				単価小数桁 = int.Parse(csv[18].Trim('\"'));
				数量小数桁 = int.Parse(csv[19].Trim('\"'));
				標準価格 = DoubleToString(csv[20]);
				原価 = DoubleToString(csv[21]);
				売価1 = DoubleToString(csv[22]);
				売価2 = DoubleToString(csv[23]);
				売価3 = DoubleToString(csv[24]);
				売価4 = DoubleToString(csv[25]);
				売価5 = DoubleToString(csv[26]);
				倉庫コード = csv[27].Trim('\"').Trim();
				主仕入先コード = csv[28].Trim('\"').Trim();
				在庫単価 = DoubleToString(csv[29]);
				仕入単価 = DoubleToString(csv[30]);
				売上計算式 = csv[31].Trim('\"');
				仕入計算式 = csv[32].Trim('\"');
				商品項目1 = int.Parse(csv[33].Trim('\"'));
				商品項目2 = int.Parse(csv[34].Trim('\"'));
				商品項目3 = int.Parse(csv[35].Trim('\"'));
				使用区分 = int.Parse(csv[36].Trim('\"'));
				商品ｺｰﾄﾞ2 = csv[37].Trim('\"').Trim();
				商品ｺｰﾄﾞ3 = csv[38].Trim('\"').Trim();
				入数小数桁 = int.Parse(csv[39].Trim('\"'));
				箱数小数桁 = int.Parse(csv[40].Trim('\"'));
				数量端数 = int.Parse(csv[41].Trim('\"'));
				有効期間開始日 = int.Parse(csv[42].Trim('\"'));
				有効期間終了日 = int.Parse(csv[43].Trim('\"'));
				コメント = csv[44].Trim('\"').Trim();
				商品名2 = csv[45].Trim('\"').Trim();
				単位区分2入数 = int.Parse(csv[46].Trim('\"'));
				単位区分2単位 = csv[47].Trim('\"').Trim();
				単位区分2単位コメント = csv[48].Trim('\"').Trim();
				単位区分3入数 = int.Parse(csv[49].Trim('\"'));
				単位区分3単位 = csv[50].Trim('\"').Trim();
				単位区分3単位コメント = csv[51].Trim('\"').Trim();
				単位区分4入数 = int.Parse(csv[52].Trim('\"'));
				単位区分4単位 = csv[53].Trim('\"').Trim();
				単位区分4単位コメント = csv[54].Trim('\"').Trim();
				単位区分5入数 = int.Parse(csv[55].Trim('\"'));
				単位区分5単位 = csv[56].Trim('\"').Trim();
				単位区分5単位コメント = csv[57].Trim('\"').Trim();
				売上単位区分 = int.Parse(csv[58].Trim('\"'));
				仕入単位区分 = int.Parse(csv[59].Trim('\"'));
				ロット管理 = int.Parse(csv[60].Trim('\"'));
				仕入税込区分 = int.Parse(csv[61].Trim('\"'));
				売上税種別 = int.Parse(csv[62].Trim('\"'));
				仕入税種別 = int.Parse(csv[63].Trim('\"'));
				税種別切替 = int.Parse(csv[64].Trim('\"'));
				return true;
			}
			return false;
		}

		/// <summary>
		/// 小数点文字列を数値に変換
		/// </summary>
		/// <param name="data"></param>
		/// <returns>int</returns>
		private int DoubleToString(string data)
		{
			// 10.50 → 10
			if (0 < data.Length)
			{
				return (int)Math.Round(double.Parse(data.Trim('\"')));
			}
			return 0;
		}
	}
}
