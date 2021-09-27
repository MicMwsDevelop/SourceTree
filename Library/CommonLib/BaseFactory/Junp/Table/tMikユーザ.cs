using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMikユーザ
	{
		public int fusCliMicID { get; set; }

		public string fus売伝No { get; set; }

		public string fus医保医療コード { get; set; }

		public string fus国保医療コード { get; set; }

		public string fus院長名 { get; set; }

		public string fus院長名フリガナ { get; set; }

		public string fus発送先名 { get; set; }

		public string fus発送先郵便番号 { get; set; }

		public string fus発送先住所 { get; set; }

		public string fus発送先電話番号 { get; set; }

		public string fus発送先備考 { get; set; }

		public string fus請求先コード { get; set; }

		public string fus請求先名 { get; set; }

		public string fus請求先郵便番号 { get; set; }

		public string fus請求先住所 { get; set; }

		public string fus請求先電話番号 { get; set; }

		public string fus請求先備考 { get; set; }

		public string fusシステム名 { get; set; }

		public string fusオプション1 { get; set; }

		public string fusオプション2 { get; set; }

		public string fusオプション3 { get; set; }

		public string fusオプション4 { get; set; }

		public string fusオプション5 { get; set; }

		public string fusオプション6 { get; set; }

		public string fusレセプト用紙 { get; set; }

		public string fus連単 { get; set; }

		public string fusカルテ用紙 { get; set; }

		public string fus処方箋用紙 { get; set; }

		public string fus領収書用紙 { get; set; }

		public string fusメディア { get; set; }

		public string fusFD種 { get; set; }

		public string fus分院管理 { get; set; }

		public string fus納品月 { get; set; }

		public string fus売上月 { get; set; }

		public int fus単体 { get; set; }

		public int fusサーバー { get; set; }

		public int fusクライアント { get; set; }

		public string fus販売店名 { get; set; }

		public string fusLicensedKey { get; set; }

		public string fus備考 { get; set; }

		public string fus販売形態 { get; set; }

		public string fus代行回収 { get; set; }

		public string fusS保守契約 { get; set; }

		public string fusH保守契約 { get; set; }

		public string fusハード構成 { get; set; }

		public string fusリース情報 { get; set; }

		public string fus登録カード回収 { get; set; }

		public string fus保守契約書回収 { get; set; }

		public string fus代行回収回収 { get; set; }

		public string fus改正時情報 { get; set; }

		public DateTime? fus更新日 { get; set; }

		public string fus更新者 { get; set; }

		public string fus休診日 { get; set; }

		public string fus診療時間 { get; set; }

		public string fusﾒｰﾙｱﾄﾞﾚｽ { get; set; }

		public string fusClientLicense1 { get; set; }

		public string fusClientLicense2 { get; set; }

		public string fusClientLicense3 { get; set; }

		public string fusClientLicense4 { get; set; }

		public string fusClientLicense5 { get; set; }

		public string fusClientLicense6 { get; set; }

		public string fusClientLicense7 { get; set; }

		public string fusClientLicense8 { get; set; }

		public string fusClientLicense9 { get; set; }

		public string fusClientLicense10 { get; set; }

		public string fusClientLicense11 { get; set; }

		public string fusClientLicense12 { get; set; }

		public string fusOS { get; set; }

		public string fus登録ｶｰﾄﾞ回収日 { get; set; }

		public string fus領収書用紙2 { get; set; }

		public string fusODeS加入 { get; set; }

		public string fus前ｼｽﾃﾑ名称 { get; set; }

		public string fus前ｼｽﾃﾑ終了 { get; set; }

		public string fus備考2 { get; set; }

		public string fus販売店担当者名 { get; set; }

		public int fus販売店No { get; set; }

		public string fusユーザー { get; set; }

		public string fusレセコン使用者 { get; set; }

		public string fus休止 { get; set; }

		public string fus開設者 { get; set; }

		public string fus運用サポート情報 { get; set; }

		public string fusスタッフ情報 { get; set; }

		public string fus施設情報 { get; set; }

		public string fus患者層 { get; set; }

		public string fus特記事項 { get; set; }

		public string fus診療科目 { get; set; }

		public string fusメーカー名 { get; set; }

		public string fusリースアップ年月 { get; set; }

		public string fusClientLicense { get; set; }

		public string fus請求種別 { get; set; }

		public string fusレセ電開始月 { get; set; }

		public int fus同時接続ｸﾗｲｱﾝﾄ数 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMikユーザ()
		{
			fusCliMicID = 0;
			fus売伝No = string.Empty;
			fus医保医療コード = string.Empty;
			fus国保医療コード = string.Empty;
			fus院長名 = string.Empty;
			fus院長名フリガナ = string.Empty;
			fus発送先名 = string.Empty;
			fus発送先郵便番号 = string.Empty;
			fus発送先住所 = string.Empty;
			fus発送先電話番号 = string.Empty;
			fus発送先備考 = string.Empty;
			fus請求先コード = string.Empty;
			fus請求先名 = string.Empty;
			fus請求先郵便番号 = string.Empty;
			fus請求先住所 = string.Empty;
			fus請求先電話番号 = string.Empty;
			fus請求先備考 = string.Empty;
			fusシステム名 = string.Empty;
			fusオプション1 = string.Empty;
			fusオプション2 = string.Empty;
			fusオプション3 = string.Empty;
			fusオプション4 = string.Empty;
			fusオプション5 = string.Empty;
			fusオプション6 = string.Empty;
			fusレセプト用紙 = string.Empty;
			fus連単 = string.Empty;
			fusカルテ用紙 = string.Empty;
			fus処方箋用紙 = string.Empty;
			fus領収書用紙 = string.Empty;
			fusメディア = string.Empty;
			fusFD種 = string.Empty;
			fus分院管理 = string.Empty;
			fus納品月 = string.Empty;
			fus売上月 = string.Empty;
			fus単体 = 0;
			fusサーバー = 0;
			fusクライアント = 0;
			fus販売店名 = string.Empty;
			fusLicensedKey = string.Empty;
			fus備考 = string.Empty;
			fus販売形態 = string.Empty;
			fus代行回収 = string.Empty;
			fusS保守契約 = string.Empty;
			fusH保守契約 = string.Empty;
			fusハード構成 = string.Empty;
			fusリース情報 = string.Empty;
			fus登録カード回収 = string.Empty;
			fus保守契約書回収 = string.Empty;
			fus代行回収回収 = string.Empty;
			fus改正時情報 = string.Empty;
			fus更新日 = null;
			fus更新者 = string.Empty;
			fus休診日 = string.Empty;
			fus診療時間 = string.Empty;
			fusﾒｰﾙｱﾄﾞﾚｽ = string.Empty;
			fusClientLicense1 = string.Empty;
			fusClientLicense2 = string.Empty;
			fusClientLicense3 = string.Empty;
			fusClientLicense4 = string.Empty;
			fusClientLicense5 = string.Empty;
			fusClientLicense6 = string.Empty;
			fusClientLicense7 = string.Empty;
			fusClientLicense8 = string.Empty;
			fusClientLicense9 = string.Empty;
			fusClientLicense10 = string.Empty;
			fusClientLicense11 = string.Empty;
			fusClientLicense12 = string.Empty;
			fusOS = string.Empty;
			fus登録ｶｰﾄﾞ回収日 = string.Empty;
			fus領収書用紙2 = string.Empty;
			fusODeS加入 = string.Empty;
			fus前ｼｽﾃﾑ名称 = string.Empty;
			fus前ｼｽﾃﾑ終了 = string.Empty;
			fus備考2 = string.Empty;
			fus販売店担当者名 = string.Empty;
			fus販売店No = 0;
			fusユーザー = string.Empty;
			fusレセコン使用者 = string.Empty;
			fus休止 = string.Empty;
			fus開設者 = string.Empty;
			fus運用サポート情報 = string.Empty;
			fusスタッフ情報 = string.Empty;
			fus施設情報 = string.Empty;
			fus患者層 = string.Empty;
			fus特記事項 = string.Empty;
			fus診療科目 = string.Empty;
			fusメーカー名 = string.Empty;
			fusリースアップ年月 = string.Empty;
			fusClientLicense = string.Empty;
			fus請求種別 = string.Empty;
			fusレセ電開始月 = string.Empty;
			fus同時接続ｸﾗｲｱﾝﾄ数 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMikユーザ> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMikユーザ> result = new List<tMikユーザ>();
				foreach (DataRow row in table.Rows)
				{
					//tMikユーザ data = new tMikユーザ
					//{
					//	fusCliMicID = DataBaseValue.ConvObjectToInt(row["fusCliMicID"]),
					//	fus売伝No = row["fus売伝No"].ToString().Trim(),
					//	fus医保医療コード = row["fus医保医療コード"].ToString().Trim(),
					//	fus国保医療コード = row["fus国保医療コード"].ToString().Trim(),
					//	fus院長名 = row["fus院長名"].ToString().Trim(),
					//	fus院長名フリガナ = row["fus院長名フリガナ"].ToString().Trim(),
					//	fus発送先名 = row["fus発送先名"].ToString().Trim(),
					//	fus発送先郵便番号 = row["fus発送先郵便番号"].ToString().Trim(),
					//	fus発送先住所 = row["fus発送先住所"].ToString().Trim(),
					//	fus発送先電話番号 = row["fus発送先電話番号"].ToString().Trim(),
					//	fus発送先備考 = row["fus発送先備考"].ToString().Trim(),
					//	fus請求先コード = row["fus請求先コード"].ToString().Trim(),
					//	fus請求先名 = row["fus請求先名"].ToString().Trim(),
					//	fus請求先郵便番号 = row["fus請求先郵便番号"].ToString().Trim(),
					//	fus請求先住所 = row["fus請求先住所"].ToString().Trim(),
					//	fus請求先電話番号 = row["fus請求先電話番号"].ToString().Trim(),
					//	fus請求先備考 = row["fus請求先備考"].ToString().Trim(),
					//	fusシステム名 = row["fusシステム名"].ToString().Trim(),
					//	fusオプション1 = row["fusオプション1"].ToString().Trim(),
					//	fusオプション2 = row["fusオプション2"].ToString().Trim(),
					//	fusオプション3 = row["fusオプション3"].ToString().Trim(),
					//	fusオプション4 = row["fusオプション4"].ToString().Trim(),
					//	fusオプション5 = row["fusオプション5"].ToString().Trim(),
					//	fusオプション6 = row["fusオプション6"].ToString().Trim(),
					//	fusレセプト用紙 = DataBaseValue.ConvObjectToBool(row["fusレセプト用紙"]),
					//	fus連単 = DataBaseValue.ConvObjectToBool(row["fus連単"]),
					//	fusカルテ用紙 = row["fusカルテ用紙"].ToString().Trim(),
					//	fus処方箋用紙 = row["fus処方箋用紙"].ToString().Trim(),
					//	fus領収書用紙 = row["fus領収書用紙"].ToString().Trim(),
					//	fusメディア = row["fusメディア"].ToString().Trim(),
					//	fusFD種 = row["fusFD種"].ToString().Trim(),
					//	fus分院管理 = DataBaseValue.ConvObjectToBool(row["fus分院管理"]),
					//	fus納品月 = row["fus納品月"].ToString().Trim(),
					//	fus売上月 = row["fus売上月"].ToString().Trim(),
					//	fus単体 = DataBaseValue.ConvObjectToInt(row["fus単体"]),
					//	fusサーバー = DataBaseValue.ConvObjectToInt(row["fusサーバー"]),
					//	fusクライアント = DataBaseValue.ConvObjectToInt(row["fusクライアント"]),
					//	fus販売店名 = row["fus販売店名"].ToString().Trim(),
					//	fusLicensedKey = row["fusLicensedKey"].ToString().Trim(),
					//	fus備考 = row["fus備考"].ToString().Trim(),
					//	fus販売形態 = DataBaseValue.ConvObjectToBool(row["fus販売形態"]),
					//	fus代行回収 = DataBaseValue.ConvObjectToBool(row["fus代行回収"]),
					//	fusS保守契約 = DataBaseValue.ConvObjectToBool(row["fusS保守契約"]),
					//	fusH保守契約 = DataBaseValue.ConvObjectToBool(row["fusH保守契約"]),
					//	fusハード構成 = DataBaseValue.ConvObjectToBool(row["fusハード構成"]),
					//	fusリース情報 = DataBaseValue.ConvObjectToBool(row["fusリース情報"]),
					//	fus登録カード回収 = DataBaseValue.ConvObjectToBool(row["fus登録カード回収"]),
					//	fus保守契約書回収 = DataBaseValue.ConvObjectToBool(row["fus保守契約書回収"]),
					//	fus代行回収回収 = DataBaseValue.ConvObjectToBool(row["fus代行回収回収"]),
					//	fus改正時情報 = row["fus改正時情報"].ToString().Trim(),
					//	fus更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fus更新日"]),
					//	fus更新者 = row["fus更新者"].ToString().Trim(),
					//	fus休診日 = row["fus休診日"].ToString().Trim(),
					//	fus診療時間 = row["fus診療時間"].ToString().Trim(),
					//	fusﾒｰﾙｱﾄﾞﾚｽ = row["fusﾒｰﾙｱﾄﾞﾚｽ"].ToString().Trim(),
					//	fusClientLicense1 = row["fusClientLicense1"].ToString().Trim(),
					//	fusClientLicense2 = row["fusClientLicense2"].ToString().Trim(),
					//	fusClientLicense3 = row["fusClientLicense3"].ToString().Trim(),
					//	fusClientLicense4 = row["fusClientLicense4"].ToString().Trim(),
					//	fusClientLicense5 = row["fusClientLicense5"].ToString().Trim(),
					//	fusClientLicense6 = row["fusClientLicense6"].ToString().Trim(),
					//	fusClientLicense7 = row["fusClientLicense7"].ToString().Trim(),
					//	fusClientLicense8 = row["fusClientLicense8"].ToString().Trim(),
					//	fusClientLicense9 = row["fusClientLicense9"].ToString().Trim(),
					//	fusClientLicense10 = row["fusClientLicense10"].ToString().Trim(),
					//	fusClientLicense11 = row["fusClientLicense11"].ToString().Trim(),
					//	fusClientLicense12 = row["fusClientLicense12"].ToString().Trim(),
					//	fusOS = row["fusOS"].ToString().Trim(),
					//	fus登録ｶｰﾄﾞ回収日 = row["fus登録ｶｰﾄﾞ回収日"].ToString().Trim(),
					//	fus領収書用紙2 = row["fus領収書用紙2"].ToString().Trim(),
					//	fusODeS加入 = DataBaseValue.ConvObjectToBool(row["fusODeS加入"]),
					//	fus前ｼｽﾃﾑ名称 = row["fus前ｼｽﾃﾑ名称"].ToString().Trim(),
					//	fus前ｼｽﾃﾑ終了 = DataBaseValue.ConvObjectToBool(row["fus前ｼｽﾃﾑ終了"]),
					//	fus備考2 = row["fus備考2"].ToString().Trim(),
					//	fus販売店担当者名 = row["fus販売店担当者名"].ToString().Trim(),
					//	fus販売店No = DataBaseValue.ConvObjectToInt(row["fus販売店No"]),
					//	fusユーザー = DataBaseValue.ConvObjectToBool(row["fusユーザー"]),
					//	fusレセコン使用者 = row["fusレセコン使用者"].ToString().Trim(),
					//	fus休止 = row["fus休止"].ToString().Trim(),
					//	fus開設者 = row["fus開設者"].ToString().Trim(),
					//	fus運用サポート情報 = row["fus運用サポート情報"].ToString().Trim(),
					//	fusスタッフ情報 = row["fusスタッフ情報"].ToString().Trim(),
					//	fus施設情報 = row["fus施設情報"].ToString().Trim(),
					//	fus患者層 = row["fus患者層"].ToString().Trim(),
					//	fus特記事項 = row["fus特記事項"].ToString().Trim(),
					//	fus診療科目 = row["fus診療科目"].ToString().Trim(),
					//	fusメーカー名 = row["fusメーカー名"].ToString().Trim(),
					//	fusリースアップ年月 = row["fusリースアップ年月"].ToString().Trim(),
					//	fusClientLicense = row["fusClientLicense"].ToString().Trim(),
					//	fus請求種別 = row["fus請求種別"].ToString().Trim(),
					//	fusレセ電開始月 = row["fusレセ電開始月"].ToString().Trim(),
					//	fus同時接続ｸﾗｲｱﾝﾄ数 = DataBaseValue.ConvObjectToInt(row["fus同時接続ｸﾗｲｱﾝﾄ数"]),
					//};

					tMikユーザ data = new tMikユーザ();
					data.fusCliMicID = DataBaseValue.ConvObjectToInt(row["fusCliMicID"]);
					data.fus売伝No = row["fus売伝No"].ToString().Trim();
					data.fus医保医療コード = row["fus医保医療コード"].ToString().Trim();
					data.fus国保医療コード = row["fus国保医療コード"].ToString().Trim();
					data.fus院長名 = row["fus院長名"].ToString().Trim();
					data.fus院長名フリガナ = row["fus院長名フリガナ"].ToString().Trim();
					data.fus発送先名 = row["fus発送先名"].ToString().Trim();
					data.fus発送先郵便番号 = row["fus発送先郵便番号"].ToString().Trim();
					data.fus発送先住所 = row["fus発送先住所"].ToString().Trim();
					data.fus発送先電話番号 = row["fus発送先電話番号"].ToString().Trim();
					data.fus発送先備考 = row["fus発送先備考"].ToString().Trim();
					data.fus請求先コード = row["fus請求先コード"].ToString().Trim();
					data.fus請求先名 = row["fus請求先名"].ToString().Trim();
					data.fus請求先郵便番号 = row["fus請求先郵便番号"].ToString().Trim();
					data.fus請求先住所 = row["fus請求先住所"].ToString().Trim();
					data.fus請求先電話番号 = row["fus請求先電話番号"].ToString().Trim();
					data.fus請求先備考 = row["fus請求先備考"].ToString().Trim();
					data.fusシステム名 = row["fusシステム名"].ToString().Trim();
					data.fusオプション1 = row["fusオプション1"].ToString().Trim();
					data.fusオプション2 = row["fusオプション2"].ToString().Trim();
					data.fusオプション3 = row["fusオプション3"].ToString().Trim();
					data.fusオプション4 = row["fusオプション4"].ToString().Trim();
					data.fusオプション5 = row["fusオプション5"].ToString().Trim();
					data.fusオプション6 = row["fusオプション6"].ToString().Trim();
					data.fusレセプト用紙 = row["fusレセプト用紙"].ToString().Trim();
					data.fus連単 = row["fus連単"].ToString().Trim();
					data.fusカルテ用紙 = row["fusカルテ用紙"].ToString().Trim();
					data.fus処方箋用紙 = row["fus処方箋用紙"].ToString().Trim();
					data.fus領収書用紙 = row["fus領収書用紙"].ToString().Trim();
					data.fusメディア = row["fusメディア"].ToString().Trim();
					data.fusFD種 = row["fusFD種"].ToString().Trim();
					data.fus分院管理 = row["fus分院管理"].ToString().Trim();
					data.fus納品月 = row["fus納品月"].ToString().Trim();
					data.fus売上月 = row["fus売上月"].ToString().Trim();
					data.fus単体 = DataBaseValue.ConvObjectToInt(row["fus単体"]);
					data.fusサーバー = DataBaseValue.ConvObjectToInt(row["fusサーバー"]);
					data.fusクライアント = DataBaseValue.ConvObjectToInt(row["fusクライアント"]);
					data.fus販売店名 = row["fus販売店名"].ToString().Trim();
					data.fusLicensedKey = row["fusLicensedKey"].ToString().Trim();
					data.fus備考 = row["fus備考"].ToString().Trim();
					data.fus販売形態 = row["fus販売形態"].ToString().Trim();
					data.fus代行回収 = row["fus代行回収"].ToString().Trim();
					data.fusS保守契約 = row["fusS保守契約"].ToString().Trim();
					data.fusH保守契約 = row["fusH保守契約"].ToString().Trim();
					data.fusハード構成 = row["fusハード構成"].ToString().Trim();
					data.fusリース情報 = row["fusリース情報"].ToString().Trim();
					data.fus登録カード回収 = row["fus登録カード回収"].ToString().Trim();
					data.fus保守契約書回収 = row["fus保守契約書回収"].ToString().Trim();
					data.fus代行回収回収 = row["fus代行回収回収"].ToString().Trim();
					data.fus改正時情報 = row["fus改正時情報"].ToString().Trim();
					data.fus更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fus更新日"]);
					data.fus更新者 = row["fus更新者"].ToString().Trim();
					data.fus休診日 = row["fus休診日"].ToString().Trim();
					data.fus診療時間 = row["fus診療時間"].ToString().Trim();
					data.fusﾒｰﾙｱﾄﾞﾚｽ = row["fusﾒｰﾙｱﾄﾞﾚｽ"].ToString().Trim();
					data.fusClientLicense1 = row["fusClientLicense1"].ToString().Trim();
					data.fusClientLicense2 = row["fusClientLicense2"].ToString().Trim();
					data.fusClientLicense3 = row["fusClientLicense3"].ToString().Trim();
					data.fusClientLicense4 = row["fusClientLicense4"].ToString().Trim();
					data.fusClientLicense5 = row["fusClientLicense5"].ToString().Trim();
					data.fusClientLicense6 = row["fusClientLicense6"].ToString().Trim();
					data.fusClientLicense7 = row["fusClientLicense7"].ToString().Trim();
					data.fusClientLicense8 = row["fusClientLicense8"].ToString().Trim();
					data.fusClientLicense9 = row["fusClientLicense9"].ToString().Trim();
					data.fusClientLicense10 = row["fusClientLicense10"].ToString().Trim();
					data.fusClientLicense11 = row["fusClientLicense11"].ToString().Trim();
					data.fusClientLicense12 = row["fusClientLicense12"].ToString().Trim();
					data.fusOS = row["fusOS"].ToString().Trim();
					data.fus登録ｶｰﾄﾞ回収日 = row["fus登録ｶｰﾄﾞ回収日"].ToString().Trim();
					data.fus領収書用紙2 = row["fus領収書用紙2"].ToString().Trim();
					data.fusODeS加入 = row["fusODeS加入"].ToString().Trim();
					data.fus前ｼｽﾃﾑ名称 = row["fus前ｼｽﾃﾑ名称"].ToString().Trim();
					data.fus前ｼｽﾃﾑ終了 = row["fus前ｼｽﾃﾑ終了"].ToString().Trim();
					data.fus備考2 = row["fus備考2"].ToString().Trim();
					data.fus販売店担当者名 = row["fus販売店担当者名"].ToString().Trim();
					data.fus販売店No = DataBaseValue.ConvObjectToInt(row["fus販売店No"]);
					data.fusユーザー = row["fusユーザー"].ToString().Trim();
					data.fusレセコン使用者 = row["fusレセコン使用者"].ToString().Trim();
					data.fus休止 = row["fus休止"].ToString().Trim();
					data.fus開設者 = row["fus開設者"].ToString().Trim();
					data.fus運用サポート情報 = row["fus運用サポート情報"].ToString().Trim();
					data.fusスタッフ情報 = row["fusスタッフ情報"].ToString().Trim();
					data.fus施設情報 = row["fus施設情報"].ToString().Trim();
					data.fus患者層 = row["fus患者層"].ToString().Trim();
					data.fus特記事項 = row["fus特記事項"].ToString().Trim();
					data.fus診療科目 = row["fus診療科目"].ToString().Trim();
					data.fusメーカー名 = row["fusメーカー名"].ToString().Trim();
					data.fusリースアップ年月 = row["fusリースアップ年月"].ToString().Trim();
					data.fusClientLicense = row["fusClientLicense"].ToString().Trim();
					data.fus請求種別 = row["fus請求種別"].ToString().Trim();
					data.fusレセ電開始月 = row["fusレセ電開始月"].ToString().Trim();
					data.fus同時接続ｸﾗｲｱﾝﾄ数 = DataBaseValue.ConvObjectToInt(row["fus同時接続ｸﾗｲｱﾝﾄ数"]);

					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
