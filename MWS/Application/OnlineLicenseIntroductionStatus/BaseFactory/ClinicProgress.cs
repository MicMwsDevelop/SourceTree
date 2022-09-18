//
// ClinicProgress.cs
// 
// オンライン資格顧客進捗管理クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
// Ver1.01 総計の東日本営業部と西日本営業部に対応(2022/09/05 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineLicenseIntroductionStatus.BaseFactory
{
	/// <summary>
	/// オンライン資格確認進捗管理
	/// </summary>
	public class ClinicProgress
	{
		/// <summary>
		/// オンライン資格確認進捗管理ファイル「全顧客」読込対象シート名
		/// </summary>
		public const string TargetSheetName = "全顧客";

		/// <summary>
		/// オンライン資格確認進捗管理ファイル「全顧客」データ開始行
		/// </summary>
		public const int StartRow = 2;

		/// <summary>
		/// 
		/// </summary>
		public string 拠点名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 都道府県 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 導入意思 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string オン資担当 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 工事種別 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ステータス { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? 現調完了月 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? 導入月 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 部署 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 価格帯 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ClinicProgress()
		{
			拠点名 = string.Empty;
			顧客No = 0;
			顧客名 = string.Empty;
			都道府県 = string.Empty;
			導入意思 = string.Empty;
			オン資担当 = string.Empty;
			工事種別 = string.Empty;
			ステータス = string.Empty;
			現調完了月 = null;
			導入月 = null;
			部署 = string.Empty;
			価格帯 = string.Empty;
		}

		/// <summary>
		/// 結果取得
		/// </summary>
		/// <returns></returns>
		public ProgressResult GetResult()
		{
			ProgressResult ret = new ProgressResult();
			return ret;
		}

		/// <summary>
		/// 日付文字列の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		private string GetDateString(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				DateTime tm = cell.GetDateTime();
				return tm.ToShortDateString();
			}
			if (XLDataType.Text == cell.DataType)
			{
				return cell.GetString();
			}
			return string.Empty;
		}

		/// <summary>
		/// 部署名の取得
		/// </summary>
		public string Get部署名()
		{
			if ("営業部" == 部署)
			{
				switch (拠点名)
				{
					case "東日本SC":
					case "首都圏SC":
						return "東日本営業部";
					case "関西SC":
					case "西日本SC":
						return "西日本営業部";
					case "中日本SC":
						if ("神奈川県" == 都道府県 || "山梨県" == 都道府県 || "静岡県" == 都道府県)
						{
							return "東日本営業部";
						}
						return "西日本営業部";
				}

			}
			return 拠点名;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="startCol">開始カラム</param>
		private void SetData(IXLWorksheet ws, int row)
		{
			拠点名 = ws.Cell(row, 1).GetString().Trim();
			顧客No = int.Parse(ws.Cell(row, 2).GetString().Trim());
			顧客名 = ws.Cell(row, 3).GetString().Trim();
			都道府県 = ws.Cell(row, 4).GetString().Trim();
			導入意思 = ws.Cell(row, 5).GetString().Trim();
			オン資担当 = ws.Cell(row, 6).GetString().Trim();
			工事種別 = ws.Cell(row, 7).GetString().Trim();
			ステータス = ws.Cell(row, 8).GetString().Trim();
			if ("済" == ws.Cell(row, 9).GetString())
			{
				現調完了月 = new DateTime(9999, 12, 31);
			}
			else
			{
				string dateStr = this.GetDateString(ws.Cell(row, 9));
				if (0 < dateStr.Length)
				{
					DateTime work;
					if (DateTime.TryParse(dateStr, out work))
					{
						現調完了月 = work;
					}
				}
			}
			if ("済" == ws.Cell(row, 10).GetString())
			{
				導入月 = new DateTime(9999, 12, 31);
			}
			else
			{
				string dateStr = this.GetDateString(ws.Cell(row, 10));
				if (0 < dateStr.Length)
				{
					DateTime work;
					if (DateTime.TryParse(dateStr, out work))
					{
						導入月 = work;
					}
				}
			}
			部署 = ws.Cell(row, 11).GetString().Trim();
			価格帯 = ws.Cell(row, 12).GetString().Trim();
		}

		/// <summary>
		/// 進捗管理ファイルの読込
		/// </summary>
		/// <param name="pathname">進捗管理ファイルパス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>NTT東日本進捗管理表リスト</returns>
		public static List<ClinicProgress> ReadProgressExcelFile(string pathname, out string msg)
		{
			msg = string.Empty;
			if (File.Exists(pathname))
			{
				List<ClinicProgress> list = new List<ClinicProgress>();
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						IXLWorksheet ws = wb.Worksheet(ClinicProgress.TargetSheetName);
						for (int i = ClinicProgress.StartRow; ; i++)
						{
							if ("" == ws.Cell(i, 1).GetString())
							{
								break;
							}
							ClinicProgress data = new ClinicProgress();
							data.SetData(ws, i);
							list.Add(data);
						}
					}
				}
				catch (Exception ex)
				{
					msg = ex.Message;
					return null;
				}
				return list;
			}
			else
			{
				msg = string.Format("{0}が見つかりません。", pathname);
			}
			return null;
		}

		/// <summary>
		/// 各オフィス毎の導入状況総計の取得
		/// </summary>
		/// <param name="list"></param>
		/// <param name="judge"></param>
		/// <returns></returns>
		/// Ver1.01 総計の東日本営業部と西日本営業部に対応(2022/09/05 勝呂)
		public static List<ProgressResult> GetProgressResult(List<ClinicProgress> list, StatusJudgement judge)
		{
			List<ClinicProgress> clinicList = list.FindAll(p => "東日本SC" == p.Get部署名());
			clinicList.AddRange(list.FindAll(p => "首都圏SC" == p.Get部署名()));
			clinicList.AddRange(list.FindAll(p => "中日本SC" == p.Get部署名()));
			clinicList.AddRange(list.FindAll(p => "関西SC" == p.Get部署名()));
			clinicList.AddRange(list.FindAll(p => "西日本SC" == p.Get部署名()));
			clinicList.AddRange(list.FindAll(p => "東日本営業部" == p.Get部署名()));
			clinicList.AddRange(list.FindAll(p => "西日本営業部" == p.Get部署名()));

			List<ProgressResult> ret = new List<ProgressResult>();
			ProgressResult result = null;
			foreach (ClinicProgress clinic in clinicList)
			{
				if (null == result)
				{
					result = new ProgressResult();
					ret.Add(result);
					result.部署名 = clinic.Get部署名();
				}
				else if (result.部署名 != clinic.Get部署名())
				{
					result = new ProgressResult();
					ret.Add(result);
					result.部署名 = clinic.Get部署名();
				}
				result.顧客数++;

				if (judge.Is導入意志あり(clinic.導入意思))
				{
					result.導入意志あり++;
				}
				if (judge.Is未確認_反応無し(clinic.導入意思))
				{
					result.未確認_反応無し++;
				}
				if (judge.IsNTT_外注_依頼数(clinic.工事種別))
				{
					result.NTT_外注_依頼数++;
				}
				if (judge.IsIPSEC依頼提出数(clinic.工事種別))
				{
					result.IPSEC依頼提出数++;
				}
				if (judge.Isヒアリングシート提出数(clinic.ステータス))
				{
					result.ヒアリングシート提出数++;
				}
				if (judge.IsNTT案件納品数(clinic.工事種別, clinic.ステータス))
				{
					result.NTT案件納品数++;
				}
				if (judge.IsIPSEC納品数(clinic.工事種別, clinic.ステータス))
				{
					result.IPSEC納品数++;
				}
				if (judge.IsMIC自力_その他納品数(clinic.工事種別, clinic.ステータス))
				{
					result.MIC自力_その他納品数++;
				}
			}
			return ret;
		}

		/// <summary>
		/// 設定ミスかどうか？
		/// </summary>
		/// <param name="clinic">顧客進捗管理</param>
		/// <param name="judge">ステータス判定定義クラス</param>
		/// <returns>判定</returns>
		private static bool IsMistake(ClinicProgress clinic, StatusJudgement judge)
		{
			if (judge.IsMistake導入意志(clinic.導入意思))
			{
				return true;
			}
			if (judge.IsMistake工事種別(clinic.工事種別))
			{
				return true;
			}
			if (judge.IsMistakeステータス(clinic.ステータス))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 設定ミスリストの取得
		/// </summary>
		/// <param name="clinicList">顧客進捗管理リスト</param>
		/// <param name="judge">ステータス判定定義クラス</param>
		/// <returns>未集計顧客リスト</returns>
		public static List<ClinicProgress> GetMistakeList(List<ClinicProgress> clinicList, StatusJudgement judge)
		{
			List<ClinicProgress> ret = new List<ClinicProgress>();
			foreach (ClinicProgress clinic in clinicList)
			{
				if (IsMistake(clinic, judge))
				{
					ret.Add(clinic);
				}
			}
			return ret;
		}

		/// <summary>
		/// オン資導入状況リストの取得
		/// </summary>
		/// <param name="clinicList">顧客進捗管理リスト</param>
		/// <returns>オン資導入状況リスト</returns>
		public static List<オンライン資格確認進捗管理> GetOnlineIntroductionStatusList(List<ClinicProgress> clinicList)
		{
			List<オンライン資格確認進捗管理> ret = new List<オンライン資格確認進捗管理>();
			foreach (ClinicProgress clinic in clinicList)
			{
				オンライン資格確認進捗管理 online = new オンライン資格確認進捗管理();
				online.顧客No = clinic.顧客No;
				online.拠点名 = clinic.拠点名;
				online.顧客名 = clinic.顧客名;
				online.オン資担当 = clinic.オン資担当;
				online.導入意思 = clinic.導入意思;
				online.工事種別 = clinic.工事種別;
				online.ステータス = clinic.ステータス;
				online.現調完了月 = clinic.現調完了月;
				online.導入月 = clinic.導入月;
				online.都道府県 = clinic.都道府県;
				online.部署 = clinic.部署;
				online.価格帯 = clinic.価格帯;
				ret.Add(online);
			}
			return ret;
		}
	}
}
