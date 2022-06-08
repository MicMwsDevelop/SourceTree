//
// 汎用データレイアウト売上明細データ.cs
//
// 汎用データレイアウト売上明細データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
// 
using System;
using System.Collections.Generic;

namespace CommonLib.BaseFactory.Pca
{
	/// <summary>
	/// 汎用データレイアウト売上明細データ
	/// </summary>
	[Serializable]
	public class 汎用データレイアウト売上明細データ
	{
		/// <summary>
		/// 0:掛売、1:現収、2:カード、3:そ の他、5:仮伝、6:契約
		/// </summary>
		public int 伝区 { get; set; }
		public int 売上日 { get; set; }
		public int 請求日 { get; set; }
		public int 伝票No { get; set; }
		public string 得意先コード { get; set; }
		public string 得意先名 { get; set; }
		public string 直送先コード { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string 分類コード { get; set; }
		public string 伝票区分 { get; set; }
		public string 商品コード { get; set; }
		/// <summary>
		/// 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
		/// </summary>
		public int マスター区分 { get; set; }
		public string 商品名 { get; set; }
		/// <summary>
		/// 0:売上、1:返品、2:単価訂正、9: 一般商品以外を示す
		/// </summary>
		public int 区 { get; set; }
		public string 倉庫コード { get; set; }
		public int 入数 { get; set; }
		public int 箱数 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 売上金額 { get; set; }
		public int 原単価 { get; set; }
		public int 原価金額 { get; set; }
		public int 粗利益 { get; set; }
		public int 外税額 { get; set; }
		public int 内税額 { get; set; }
		public int 税区分 { get; set; }
		/// <summary>
		/// 0:税抜き、1:税込み
		/// </summary>
		public int 税込区分 { get; set; }
		public string 備考 { get; set; }
		public int 標準価格 { get; set; }
		/// <summary>
		/// 0:自動入荷しない、1:自動入荷する
		/// </summary>
		public int 同時入荷区分 { get; set; }
		public int 売単価 { get; set; }
		public int 売価金額 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 計算式コード { get; set; }
		public int 商品項目１ { get; set; }
		public int 商品項目２ { get; set; }
		public int 商品項目３ { get; set; }
		public int 売上項目１ { get; set; }
		public int 売上項目２ { get; set; }
		public int 売上項目３ { get; set; }
		public int 税率 { get; set; }
		public int 伝票消費税額 { get; set; }
		public string ﾌﾟﾛｼﾞｪｸﾄコード { get; set; }
		public string 伝票No2 { get; set; }
		public int データ区分 { get; set; }
		public string 商品名２ { get; set; }
		public int 単位区分 { get; set; }
		public string ロットNo { get; set; }

		/// <summary>
		/// 56 DX-Rev1.00
		/// </summary>
		public string 直送先名 { get; set; }

		/// <summary>
		/// 57 DX-Rev2.00
		/// </summary>
		public string 決済会社コード { get; set; }
		/// <summary>
		/// 58 DX-Rev2.00
		/// </summary>
		public string 決済会社名 { get; set; }
		/// <summary>
		/// 59 DX-Rev2.00
		/// </summary>
		public int 決済日 { get; set; }
		/// <summary>
		/// 60 DX-Rev2.00
		/// </summary>
		public int 決済手数料 { get; set; }
		/// <summary>
		/// 61 DX-Rev2.00
		/// </summary>
		public int 手数料外税額 { get; set; }
		/// <summary>
		/// 62 DX-Rev2.00
		/// </summary>
		public int 手数料内税額 { get; set; }
		/// <summary>
		/// 63 DX-Rev2.00
		/// </summary>
		public int 手数料税区分 { get; set; }
		/// <summary>
		/// 64 DX-Rev2.00
		/// </summary>
		public int 手数料税率 { get; set; }
		/// <summary>
		/// 65 DX-Rev2.00
		/// </summary>
		public int 手数料税込区分 { get; set; }
		/// <summary>
		/// 66 DX-Rev2.00
		/// </summary>
		public string 決済摘要コード { get; set; }
		/// <summary>
		/// 67 DX-Rev2.00
		/// </summary>
		public string 決済摘要名 { get; set; }
		/// <summary>
		/// 68 DX-Rev3.00
		/// </summary>
		public int 売上税種別 { get; set; }

		/// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
		/// <summary>
		/// 69 DX-Rev3.00
		/// </summary>
		public int 原価税込区分 { get; set; }
		/// <summary>
		/// 70 DX-Rev3.00
		/// </summary>
		public int 原価税率 { get; set; }
		/// <summary>
		/// 71 DX-Rev3.00
		/// </summary>
		public int 原価税種別 { get; set; }

		/// <summary>
		/// 記事レコードかどうか？
		/// </summary>
		public bool IsArticleRecord
		{
			get
			{
				return (PcaGoodsIDDefine.ArticleCode == 商品コード) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 汎用データレイアウト売上明細データ()
		{
			伝区 = 0;
			売上日 = 0;
			請求日 = 0;
			伝票No = 0;
			得意先コード = string.Empty;
			得意先名 = string.Empty;
			直送先コード = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			分類コード = string.Empty;
			伝票区分 = string.Empty;
			商品コード = string.Empty;
			マスター区分 = 0;
			商品名 = string.Empty;
			区 = 0;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			売上金額 = 0;
			原単価 = 0;
			原価金額 = 0;
			粗利益 = 0;
			外税額 = 0;
			内税額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			標準価格 = 0;
			同時入荷区分 = 0;
			売単価 = 0;
			売価金額 = 0;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = 0;
			商品項目１ = 0;
			商品項目２ = 0;
			商品項目３ = 0;
			売上項目１ = 0;
			売上項目２ = 0;
			売上項目３ = 0;
			税率 = 0;
			伝票消費税額 = 0;
			ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
			伝票No2 = string.Empty;
			データ区分 = 0;
			商品名２ = string.Empty;
			単位区分 = 0;
			ロットNo = string.Empty;

			直送先名 = string.Empty;

			決済会社コード = string.Empty;
			決済会社名 = string.Empty;
			決済日 = 0;
			決済手数料 = 0;
			手数料外税額 = 0;
			手数料内税額 = 0;
			手数料税区分 = 0;
			手数料税率 = 0;
			手数料税込区分 = 0;
			決済摘要コード = string.Empty;
			決済摘要名 = string.Empty;

			// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
			売上税種別 = 0;
			原価税込区分 = 0;
			原価税率 = 0;
			原価税種別 = 0;
		}

		/// <summary>
		/// CSV文字列の取得
		/// </summary>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToCsvString(int pcaVer)
		{
			// 汎用データレイアウト指定 7: Rev4.20
			List<string> list = new List<string>();
			/*01*/list.Add(伝区.ToString());
			/*02*/list.Add("\"" + 売上日.ToString() + "\"");
			/*03*/list.Add("\"" + 請求日.ToString() + "\"");
			/*04*/list.Add(伝票No.ToString());
			/*05*/list.Add("\"" + 得意先コード + "\"");
			/*06*/list.Add("\"" + 得意先名 + "\"");
			/*07*/list.Add("\"" + 直送先コード + "\"");
			/*08*/list.Add("\"" + 先方担当者名 + "\"");
			/*09*/list.Add("\"" + 部門コード + "\"");
			/*10*/list.Add("\"" + 担当者コード + "\"");
			/*11*/list.Add(摘要コード);
			/*12*/list.Add("\"" + 摘要名 + "\"");
			/*13*/list.Add("\"" + 分類コード + "\"");
			/*14*/list.Add("\"" + 伝票区分 + "\"");
			/*15*/list.Add("\"" + 商品コード + "\"");
			/*16*/list.Add(マスター区分.ToString());
			/*17*/list.Add("\"" + 商品名 + "\"");
			/*18*/list.Add(区.ToString());
			/*19*/list.Add("\"" + 倉庫コード + "\"");
			/*20*/list.Add(入数.ToString());
			/*21*/list.Add(箱数.ToString());
			/*22*/list.Add(数量.ToString());
			/*23*/list.Add("\"" + 単位 + "\"");
			/*24*/list.Add(単価.ToString());
			/*25*/list.Add(売上金額.ToString());
			/*26*/list.Add(原単価.ToString());
			/*27*/list.Add(原価金額.ToString());
			/*28*/list.Add(粗利益.ToString());
			/*29*/list.Add(外税額.ToString());
			/*30*/list.Add(内税額.ToString());
			/*31*/list.Add(税区分.ToString());
			/*32*/list.Add(税込区分.ToString());
			/*33*/list.Add("\"" + 備考 + "\"");
			/*34*/list.Add(標準価格.ToString());
			/*35*/list.Add(同時入荷区分.ToString());
			/*36*/list.Add(売単価.ToString());
			/*37*/list.Add(売価金額.ToString());
			/*38*/list.Add("\"" + 規格型番 + "\"");
			/*39*/list.Add("\"" + 色 + "\"");
			/*40*/list.Add("\"" + サイズ + "\"");
			/*41*/list.Add(計算式コード.ToString());
			/*42*/list.Add(商品項目１.ToString());
			/*43*/list.Add(商品項目２.ToString());
			/*44*/list.Add(商品項目３.ToString());
			/*45*/list.Add(売上項目１.ToString());
			/*46*/list.Add(売上項目２.ToString());
			/*47*/list.Add(売上項目３.ToString());
			/*48*/list.Add(税率.ToString());
			/*49*/list.Add(伝票消費税額.ToString());
			/*50*/list.Add("\"" + ﾌﾟﾛｼﾞｪｸﾄコード + "\"");
			/*51*/list.Add("\"" + 伝票No2 + "\"");
			/*52*/list.Add(データ区分.ToString());
			/*53*/list.Add("\"" + 商品名２ + "\"");
			if (8 <= pcaVer)
			{
				// 汎用データレイアウト指定 8: Rev4.50
				/*54*/list.Add(単位区分.ToString());
				/*55*/list.Add("\"" + ロットNo + "\"");
			}
			if (9 <= pcaVer)
			{
				// 汎用データレイアウト指定 9: DX-Rev1.00
				/*56*/list.Add("\"" + 直送先名 + "\"");
			}
			if (10 <= pcaVer)
			{
				// 汎用データレイアウト指定 10: DX-Rev2.00
				/*57*/list.Add("\"" + 決済会社コード + "\"");
				/*58*/list.Add("\"" + 決済会社名 + "\"");
				/*59*/list.Add(決済日.ToString());
				/*60*/list.Add(決済手数料.ToString());
				/*61*/list.Add(手数料外税額.ToString());
				/*62*/list.Add(手数料内税額.ToString());
				/*63*/list.Add(手数料税区分.ToString());
				/*64*/list.Add(手数料税率.ToString());
				/*65*/list.Add(手数料税込区分.ToString());
				/*66*/list.Add("\"" + 決済摘要コード + "\"");
				/*67*/list.Add("\"" + 決済摘要名 + "\"");
			}
			// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
			if (11 <= pcaVer)
			{
				// 汎用データレイアウト指定 11: DX-Rev3.00
				/*68*/list.Add(売上税種別.ToString());
				/*69*/list.Add(原価税込区分.ToString());
				/*70*/list.Add(原価税率.ToString());
				/*71*/list.Add(原価税種別.ToString());
			}
			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// CSVレコード格納
		/// </summary>
		/// <param name="csv"></param>
		public bool SetCsvRecord(string[] csv)
		{
			if (53 <= csv.Length)
			{
				// 汎用データレイアウト指定 7: Rev4.20
				伝区 = int.Parse(csv[0].Trim('\"'));
				売上日 = int.Parse(csv[1].Trim('\"'));
				請求日 = int.Parse(csv[2].Trim('\"'));
				伝票No = int.Parse(csv[3].Trim('\"'));
				得意先コード = csv[4].Trim('\"');
				得意先名 = csv[5].Trim('\"');
				直送先コード = csv[6].Trim('\"');
				先方担当者名 = csv[7].Trim('\"');
				部門コード = csv[8].Trim('\"');
				担当者コード = csv[9].Trim('\"');
				摘要コード = csv[10].Trim('\"');
				摘要名 = csv[11].Trim('\"');
				分類コード = csv[12].Trim('\"');
				伝票区分 = csv[13].Trim('\"');
				商品コード = csv[14].Trim('\"');
				マスター区分 = int.Parse(csv[15].Trim('\"'));
				商品名 = csv[16].Trim('\"');
				区 = int.Parse(csv[17].Trim('\"'));
				倉庫コード = csv[18].Trim('\"');
				入数 = int.Parse(csv[19].Trim('\"'));
				箱数 = int.Parse(csv[20].Trim('\"'));
				数量 = int.Parse(csv[21].Trim('\"'));
				単位 = csv[22].Trim('\"');
				単価 = int.Parse(csv[23].Trim('\"'));
				売上金額 = int.Parse(csv[24].Trim('\"'));
				原単価 = int.Parse(csv[25].Trim('\"'));
				原価金額 = int.Parse(csv[26].Trim('\"'));
				粗利益 = int.Parse(csv[27].Trim('\"'));
				外税額 = int.Parse(csv[28].Trim('\"'));
				内税額 = int.Parse(csv[29].Trim('\"'));
				税区分 = int.Parse(csv[30].Trim('\"'));
				税込区分 = int.Parse(csv[31].Trim('\"'));
				備考 = csv[32].Trim('\"');
				標準価格 = int.Parse(csv[33].Trim('\"'));
				同時入荷区分 = int.Parse(csv[34].Trim('\"'));
				売単価 = int.Parse(csv[35].Trim('\"'));
				売価金額 = int.Parse(csv[36].Trim('\"'));
				規格型番 = csv[37].Trim('\"');
				色 = csv[38].Trim('\"');
				サイズ = csv[39].Trim('\"');
				計算式コード = int.Parse(csv[40].Trim('\"'));
				商品項目１ = int.Parse(csv[41].Trim('\"'));
				商品項目２ = int.Parse(csv[42].Trim('\"'));
				商品項目３ = int.Parse(csv[43].Trim('\"'));
				売上項目１ = int.Parse(csv[44].Trim('\"'));
				売上項目２ = int.Parse(csv[45].Trim('\"'));
				売上項目３ = int.Parse(csv[46].Trim('\"'));
				税率 = int.Parse(csv[47].Trim('\"'));
				伝票消費税額 = int.Parse(csv[48].Trim('\"'));
				ﾌﾟﾛｼﾞｪｸﾄコード = csv[49].Trim('\"');
				伝票No2 = csv[50].Trim('\"');
				データ区分 = int.Parse(csv[51].Trim('\"'));
				商品名２ = csv[52].Trim('\"');
				if (54 <= csv.Length)
				{
					// 汎用データレイアウト指定 8: Rev4.50
					単位区分 = int.Parse(csv[53].Trim('\"'));
					ロットNo = csv[54].Trim('\"');
				}
				if (56 <= csv.Length)
				{
					// 汎用データレイアウト指定 9: DX-Rev1.00
					直送先名 = csv[55].Trim('\"');
				}
				if (57 <= csv.Length)
				{
					// 汎用データレイアウト指定 10: DX-Rev2.00
					決済会社コード = csv[56].Trim('\"');
					決済会社名 = csv[57].Trim('\"');
					決済日 = int.Parse(csv[58].Trim('\"'));
					決済手数料 = int.Parse(csv[59].Trim('\"'));
					手数料外税額 = int.Parse(csv[60].Trim('\"'));
					手数料内税額 = int.Parse(csv[61].Trim('\"'));
					手数料税区分 = int.Parse(csv[62].Trim('\"'));
					手数料税率 = int.Parse(csv[63].Trim('\"'));
					手数料税込区分 = int.Parse(csv[64].Trim('\"'));
					決済摘要コード = csv[65].Trim('\"');
					決済摘要名 = csv[66].Trim('\"');
				}
				// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
				if (68 <= csv.Length)
				{
					// 汎用データレイアウト指定 11: DX-Rev3.00
					売上税種別 = int.Parse(csv[67].Trim('\"'));
					原価税込区分 = int.Parse(csv[68].Trim('\"'));
					原価税率 = int.Parse(csv[69].Trim('\"'));
					原価税種別 = int.Parse(csv[70].Trim('\"'));
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// 請求先が違うかどうか？
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <returns>判定</returns>
		public bool IsDifferentSeikyusaki(string tokuisakiNo)
		{
			if (this.IsArticleRecord)
			{
				if (tokuisakiNo == 商品名.Replace("得意先No. ", ""))
				{
					return true;
				}
			}
			return false;
		}
	}
}
