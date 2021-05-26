using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.Common;
using MwsLib.BaseFactory.Pca;

namespace MwsLib.BaseFactory.ShipmentActing
{
	/// <summary>
	/// 出荷代行
	/// </summary>
	public class ShipmentActingData : vMicPCA受注明細
	{
		/// <summary>
		/// 0:商品, 1:送料, 2:記事行, 3:消費税行
		/// </summary>
		public short MeiType;

		/// <summary>
		/// 引当数（出荷数）
		/// </summary>
		public int Hikiatesu;

		/// <summary>
		/// 出荷日付
		/// </summary>
		public Date? HassouDate;

		/// <summary>
		/// 明細１行目フラグ
		/// </summary>
		public short First;

		/// <summary>
		/// 合計金額
		/// </summary>
		public int SumKingaku;

		/// <summary>
		/// 担当者名(営業部名)
		/// </summary>
		public string tanto;

		/// <summary>
		/// 粗利益
		/// </summary>
		public int jucd_arari;

		/// <summary>
		/// 外税額
		/// </summary>
		public int jucd_zei;

		/// <summary>
		/// 内税額
		/// </summary>
		public int jucd_uchi;

		/// <summary>
		/// 規格・型番
		/// </summary>
		string jucd_kikaku;

		/// <summary>
		/// 色
		/// </summary>
		string jucd_color;

		/// <summary>
		/// サイズ
		/// </summary>
		string jucd_size;
/*
		Dim Commit As Short '出荷明細出力品目フラグ
*/

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ShipmentActingData() : base()
		{
			this.Clear();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ShipmentActingData(vMicPCA受注明細 src) : base(src)
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public override void Clear()
		{
			MeiType = 0;
			Hikiatesu = 0;
			HassouDate = null;
			First = 0;
			SumKingaku = 0;
			tanto = string.Empty;
			jucd_arari = 0;
			jucd_zei = 0;
			jucd_uchi = 0;
			jucd_kikaku = string.Empty;
			jucd_color = string.Empty;
			jucd_size = string.Empty;
		}

		/// <summary>
		/// 二重引用符で括った文字列の取得
		/// </summary>
		/// <param name="str">文字列</param>
		/// <returns>二重引用符文字列</returns>
		private string DoubleQuote(string str)
		{
			return string.Format("\"{0}\"", str);
		}

		/// <summary>
		/// 納品書用データファイル(NouhinFile)のタイトル行の取得
		/// </summary>
		/// <returns></returns>
		public static string NouhinTitle()
		{
			return "先頭,受注番号,お客様コードNo,郵便番号,住所,顧客名,電話番号,受注顧客No,年,月,日,担当,伝票番号,摘要,品名,数量,単位,単価,金額,備考,合計";
		}

		/// <summary>
		/// 業務委託先で出力するための納品書用データ
		/// 納品書用データファイル(NouhinFile)
		/// </summary>
		/// <param name="user">全ユーザー2</param>
		/// <param name="denNo">伝票番号</param>
		/// <returns>CSV文字列</returns>
		public string OutNouhin(vMic全ユーザー2 user, int denNo)
		{
			// 先頭,受注番号,"お客様コードNo","郵便番号","住所","顧客名","電話番号","受注顧客No","年","月","日","担当",伝票番号,"摘要","品名",数量,"単位",単価,金額,"備考",合計
			List<string> list = new List<string>();

			// 先頭
			list.Add(First.ToString());

			// 受注番号
			list.Add(base.jucd_jno.ToString());

			if (user.Is別途請求先)
			{
				// "お客様コードNo"
				list.Add(DoubleQuote(user.得意先No));

				// "郵便番号"
				list.Add(DoubleQuote(user.郵便番号));

				// "住所"
				list.Add(DoubleQuote(user.住所));

				// "顧客名"
				list.Add(DoubleQuote(user.顧客名 + "様"));

				// "電話番号"
				list.Add(DoubleQuote(user.電話番号));
			}
			else
			{
				// "お客様コードNo"
				list.Add(DoubleQuote(user.請求先コード));

				// "郵便番号"
				list.Add(DoubleQuote(user.請求先郵便番号));

				// "住所"
				list.Add(DoubleQuote(user.請求先住所));

				// "顧客名"
				list.Add(DoubleQuote(user.請求先名 + "様"));

				// "電話番号"
				list.Add(DoubleQuote(user.請求先電話番号));
			}
			// "受注顧客No"
			list.Add(DoubleQuote(user.顧客No.ToString()));

			//"年","月","日" es.2020,09,04
			list.Add(DoubleQuote(string.Format("{0:D4}", HassouDate.Value.Year)));
			list.Add(DoubleQuote(string.Format("{0:D2}", HassouDate.Value.Month)));
			list.Add(DoubleQuote(string.Format("{0:D2}", HassouDate.Value.Day)));

			// "担当"-営業部名
			list.Add(DoubleQuote(tanto));

			// 伝票番号
			list.Add(denNo.ToString());

			// "摘要"-出力なし
			list.Add(DoubleQuote(""));

			// "品名"
			list.Add(DoubleQuote(base.jucd_mei));

			switch (MeiType)
			{
				// 0:商品
				case 0:
					// 数量
					list.Add(Hikiatesu.ToString());
					// "単位"
					list.Add(DoubleQuote(base.jucd_tani));
					// 単価
					list.Add(base.jucd_tanka.ToString());
					// 金額
					list.Add((base.jucd_tanka * Hikiatesu).ToString());
					break;
				// 1:送料
				case 1:
					// 数量
					list.Add(base.jucd_suryo.ToString());
					// "単位"
					list.Add(DoubleQuote(base.jucd_tani));
					// 単価
					list.Add(base.jucd_tanka.ToString());
					// 金額
					list.Add(base.jucd_kingaku.ToString());
					break;
				// 2:記事行
				case 2:
					// 数量
					list.Add("");
					// "単位"
					list.Add(DoubleQuote(""));
					// 単価
					list.Add("");
					// 金額
					list.Add("");
					break;
				// 3:消費税行
				case 3:
					// 数量
					list.Add("");
					// "単位"
					list.Add(DoubleQuote(""));
					// 単価
					list.Add("");
					// 金額
					list.Add(base.jucd_kingaku.ToString());
					break;
			}
			// "備考"
			list.Add(DoubleQuote(""));

			// 合計
			list.Add(SumKingaku.ToString());

			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// 業務委託先で出力するための発送用データ
		/// 発送用データファイル(HassouFile)
		/// </summary>
		/// <param name="user">全ユーザー2</param>
		/// <param name="daibikiKaishugaku">代引き回収金額</param>
		/// <param name="yamatoUnyu">ヤマト運輸</param>
		/// <returns>CSV文字列</returns>
		public string OutHassou(vMic全ユーザー2 user, int daibikiKaishugaku, bool yamatoUnyu)
		{
			List<string> list = new List<string>();

			// 宛先1,宛先2,宛先3,住所1,住所2,住所3,郵便番号,電話番号,記事1,記事2,発送日,代引き回収金額
			string name = string.Empty;
			string add = string.Empty;
			string zip = string.Empty;
			string tel = string.Empty;
			if (0 < user.発送先名.Length)
			{
				name = user.発送先名;
				add = user.発送先住所;
				zip = user.発送先郵便番号;
				tel = user.発送先電話番号;
			}
			else
			{
				name = user.顧客名;
				add = user.住所;
				zip = user.郵便番号;
				tel = user.電話番号;
			}
			// 宛先(32byteで3分割)
			List<string> work = StringUtil.GetListSubstringByByte(name, 32);
			if (3 == work.Count)
			{
				list.AddRange(work);
			}
			else if (2 == work.Count)
			{
				list.AddRange(work);
				list.Add("");
			}
			else if (1 == work.Count)
			{
				list.AddRange(work);
				list.Add("");
				list.Add("");
			}
			else
			{
				list.Add("");
				list.Add("");
				list.Add("");
			}
			// 住所(32byteで3分割)
			work = StringUtil.GetListSubstringByByte(add, 32);
			if (3 == work.Count)
			{
				list.AddRange(work);
			}
			else if (2 == work.Count)
			{
				list.AddRange(work);
				list.Add("");
			}
			else if (1 == work.Count)
			{
				list.AddRange(work);
				list.Add("");
				list.Add("");
			}
			else
			{
				list.Add("");
				list.Add("");
				list.Add("");
			}
			list.Add(zip);  // 郵便番号
			list.Add(tel);  // 電話番号

			// 記事
			list.Add("消耗品在中");
			list.Add("顧客Ｎｏ．" + StringUtil.ConvertWideForUnicode(user.顧客No.ToString()));     // 顧客Ｎｏ．１００５５９５５

			// 発送日 2020年04月08日
			list.Add(HassouDate.Value.GetJapaneseString(true, '0', true, true));

			// 代引き回収金額
			if (0 == daibikiKaishugaku)
			{
				list.Add("0");
				list.Add("");
			}
			else if (yamatoUnyu)
			{
				// ヤマト運輸
				list.Add("2");
				list.Add(daibikiKaishugaku.ToString());
			}
			else
			{
				list.Add("1");
				list.Add(daibikiKaishugaku.ToString());
			}
			return String.Join(",", list.ToArray());
		}

		/// <summary>
		/// ＰＣＡ汎用データ  売上明細データ
		/// ＰＣＡ商魂・商管用汎用売上明細データファイル(HsykdFile)
		/// </summary>
		/// <param name="user">全ユーザー2</param>
		/// <param name="daibikiKaishugaku">代引き回収金額</param>
		/// <param name="denNo">伝票番号</param>
		/// <param name="taxRate">税率</param>
		/// <returns>CSV文字列</returns>
		public string OutHanUriage(vMic全ユーザー2 user, int daibikiKaishugaku, int denNo, int taxRate)
		{
			if (3 == MeiType)
			{
				// 消費税行は出力しない
				return string.Empty;
			}
			PCA売上明細汎用データ pca = new PCA売上明細汎用データ();
			if (0 < daibikiKaishugaku)
			{
				// 1:現収
				pca.伝区 = 1;
			}
			pca.売上日 = HassouDate.Value.ToIntYMD();
			pca.請求日 = HassouDate.Value.ToIntYMD();
			pca.伝票No = denNo;
			pca.得意先コード = (user.Is別途請求先) ? user.請求先コード : user.得意先No;
			//pca.得意先名 = string.Empty;
			pca.直送先コード = base.jucd_ncd;
			//pca.先方担当者名 = string.Empty;
			pca.部門コード = "011";				// 部門マスタ 011 = 営業管理部
			pca.担当者コード = "0099";			// 担当者マスタ 0099 = 営業管理部
			pca.摘要コード = base.jucd_tekcd;
			pca.摘要名 = base.jucd_tekmei;
			//pca.分類コード = string.Empty;
			//pca.伝票区分 = string.Empty;
			pca.商品コード = base.jucd_scd;
			pca.マスター区分 = base.jucd_mkbn;
			pca.商品名 = base.jucd_mei;
			//pca.区 = 0;
			pca.倉庫コード = "50";	// 倉庫コード Ver7.4 倉庫50に変更
			pca.入数 = base.jucd_iri;
			pca.箱数 = base.jucd_hako;
			pca.数量 = (0 == MeiType) ? Hikiatesu : base.jucd_suryo;
			pca.単位 = base.jucd_tani;
			pca.単価 = base.jucd_tanka;
			pca.売上金額 = (0 == MeiType) ? base.jucd_tanka * Hikiatesu : base.jucd_kingaku;
			pca.原単価 = base.jucd_gentan;
			pca.原価金額 = (0 == MeiType) ? base.jucd_gentan * Hikiatesu : base.jucd_genka;
			pca.粗利益 = jucd_arari;
			pca.外税額 = jucd_zei;
			pca.内税額 = jucd_uchi;
			pca.税区分 = base.jucd_tax;
			pca.税込区分 = base.jucd_komi;
			pca.備考 = base.jucd_biko;
			pca.標準価格 = base.jucd_hyo;
			//pca.同時入荷区分 = 0;
			pca.売単価 = base.jucd_baitan;
			pca.売価金額 = base.jucd_baika;
			pca.規格型番 = jucd_kikaku;
			pca.色 = jucd_color;
			pca.サイズ = jucd_size;
			//pca.計算式コード = 0;
			//pca.商品項目１ = 0;
			//pca.商品項目２ = 0;
			//pca.商品項目３ = 0;
			//pca.売上項目１ = 0;
			//pca.売上項目２ = 0;
			//pca.売上項目３ = 0;

			// PCA Ver9対応
			pca.税率 = taxRate;

			//pca.伝票消費税額 = 0;
			//pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
			//pca.伝票No2 = string.Empty;
			//pca.データ区分 = 0;
			//pca.商品名２ = string.Empty;

			//pca.単位区分 = 0;
			//pca.ロットNo = string.Empty;
			//pca.売上税種別 = 0;
			//pca.原価税込区分 = 0;
			//pca.原価税率 = 0;
			//pca.原価税種別 = 0;

			return pca.ToCsvString(8);
		}

		/// <summary>
		/// 合計金額を取得
		/// </summary>
		/// <param name="list"></param>
		/// <returns>合計金額</returns>
		public static int GetTotalKingaku(List<ShipmentActingData> list)
		{
			int total = 0;
			foreach (ShipmentActingData ship in list)
			{
				if (0 == ship.MeiType)
				{
					// 0:商品
					total += ship.jucd_tanka * ship.Hikiatesu;
				}
				else if (1 == ship.MeiType)
				{
					// 1:送料
					total += ship.jucd_kingaku;
				}
			}
			return total;
		}
	}
}
