//
// ProgressResult.cs
// 
// 拠点別導入状況クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
//
using ClosedXML.Excel;
using System.Collections.Generic;

namespace OnlineLicenseIntroductionStatus.BaseFactory
{
	/// <summary>
	/// 拠点別導入状況
	/// </summary>
	public class ProgressResult
	{
		/// <summary>
		/// オンライン資格確認導入状況ファイル「総計」シート名
		/// </summary>
		private const string SheetnameTotal = "総計";

		/// <summary>
		/// オンライン資格確認導入状況ファイル「顧客リスト」シート名
		/// </summary>
		private const string SheetnameClinic = "顧客リスト";

		/// <summary>
		/// オンライン資格確認導入状況ファイル「設定ミス」シート名
		/// </summary>
		private const string SheetnameMistake = "設定ミス";

		/// <summary>
		/// 「総計」データ開始行
		/// </summary>
		private const int StartRowTotal = 5;

		/// <summary>
		/// 「顧客リスト」データ開始行
		/// </summary>
		private const int StartRowClinic = 2;

		/// <summary>
		/// 「設定ミス」データ開始行
		/// </summary>
		private const int StartRowMistake = 2;

		/// <summary>
		/// 
		/// </summary>
		public string 部署名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 顧客数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 導入意志あり { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 未確認_反応無し { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int NTT_外注_依頼数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int IPSEC依頼提出数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int ヒアリングシート提出数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int NTT案件納品数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int IPSEC納品数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int MIC自力_その他納品数 { get; set; }

		/// <summary>
		/// 全納品数
		/// </summary>
		public int 全納品数
		{
			get
			{
				return NTT案件納品数 + IPSEC納品数 + MIC自力_その他納品数;
			}
		}

		/// <summary>
		/// 導入率
		/// </summary>
		public int 導入率
		{
			get
			{
				if (0 < 顧客数)
				{
					return 全納品数 / 顧客数;
				}
				return 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ProgressResult()
		{
			部署名 = string.Empty;
			顧客数 = 0;
			導入意志あり = 0;
			未確認_反応無し = 0;
			NTT_外注_依頼数 = 0;
			IPSEC依頼提出数 = 0;
			ヒアリングシート提出数 = 0;
			NTT案件納品数 = 0;
			IPSEC納品数 = 0;
			MIC自力_その他納品数 = 0;
		}

		/// <summary>
		/// 導入状況ファイルへの書き込み
		/// </summary>
		/// <param name="excelFile">導入状況ファイルパス名</param>
		/// <param name="progressList">拠点別導入状況リスト</param>
		/// <param name="clinicList">顧客進捗管理リスト</param>
		/// <param name="mistakeList">設定ミスリスト</param>
		public static void WriteProgressResult(string excelFile, List<ProgressResult> progressList, List<ClinicProgress> clinicList, List<ClinicProgress> mistakeList)
		{
			using (XLWorkbook wb = new XLWorkbook(excelFile, XLEventTracking.Disabled))
			{
				// 「総計」に出力
				IXLWorksheet wsTotal = wb.Worksheet(SheetnameTotal);
				for (int i = 0, j = StartRowTotal; i < progressList.Count; i++, j++)
				{
					wsTotal.Cell(j, 2).SetValue(progressList[i].顧客数);
					wsTotal.Cell(j, 3).SetValue(progressList[i].導入意志あり);
					wsTotal.Cell(j, 4).SetValue(progressList[i].未確認_反応無し);
					wsTotal.Cell(j, 5).SetValue(progressList[i].NTT_外注_依頼数);
					wsTotal.Cell(j, 6).SetValue(progressList[i].IPSEC依頼提出数);
					wsTotal.Cell(j, 7).SetValue(progressList[i].ヒアリングシート提出数);
					wsTotal.Cell(j, 8).SetValue(progressList[i].NTT案件納品数);
					wsTotal.Cell(j, 9).SetValue(progressList[i].IPSEC納品数);
					wsTotal.Cell(j, 10).SetValue(progressList[i].MIC自力_その他納品数);
				}
				// 「顧客リスト」に出力
				IXLWorksheet wsClinic = wb.Worksheet(SheetnameClinic);
				for (int i = 0, j = StartRowClinic; i < clinicList.Count; i++, j++)
				{
					wsClinic.Cell(j, 1).SetValue(clinicList[i].拠点名);
					wsClinic.Cell(j, 2).SetValue(clinicList[i].顧客No);
					wsClinic.Cell(j, 3).SetValue(clinicList[i].顧客名);
					wsClinic.Cell(j, 4).SetValue(clinicList[i].都道府県);
					wsClinic.Cell(j, 5).SetValue(clinicList[i].導入意思);
					wsClinic.Cell(j, 6).SetValue(clinicList[i].オン資担当);
					wsClinic.Cell(j, 7).SetValue(clinicList[i].工事種別);
					wsClinic.Cell(j, 8).SetValue(clinicList[i].ステータス);
					wsClinic.Cell(j, 9).SetValue(clinicList[i].現調完了月);
					wsClinic.Cell(j, 10).SetValue(clinicList[i].導入月);
					wsClinic.Cell(j, 11).SetValue(clinicList[i].部署);
					wsClinic.Cell(j, 12).SetValue(clinicList[i].価格帯);
				}
				if (0 < mistakeList.Count)
				{
					// 「未集計リスト」に出力
					IXLWorksheet wsMistake = wb.Worksheet(SheetnameMistake);
					for (int i = 0, j = StartRowMistake; i < mistakeList.Count; i++, j++)
					{
						wsMistake.Cell(j, 1).SetValue(mistakeList[i].拠点名);
						wsMistake.Cell(j, 2).SetValue(mistakeList[i].顧客No);
						wsMistake.Cell(j, 3).SetValue(mistakeList[i].顧客名);
						wsMistake.Cell(j, 4).SetValue(mistakeList[i].都道府県);
						wsMistake.Cell(j, 5).SetValue(mistakeList[i].導入意思);
						wsMistake.Cell(j, 6).SetValue(mistakeList[i].オン資担当);
						wsMistake.Cell(j, 7).SetValue(mistakeList[i].工事種別);
						wsMistake.Cell(j, 8).SetValue(mistakeList[i].ステータス);
						wsMistake.Cell(j, 9).SetValue(mistakeList[i].現調完了月);
						wsMistake.Cell(j, 10).SetValue(mistakeList[i].導入月);
						wsMistake.Cell(j, 11).SetValue(mistakeList[i].部署);
						wsMistake.Cell(j, 12).SetValue(mistakeList[i].価格帯);
					}
				}
				// シートのアクティブ化
				wsTotal.SetTabActive();

				// Excelファイルの保存
				wb.Save();
			}
		}
	}
}
