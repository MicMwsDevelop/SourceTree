//
// CheckUseCustomerInfo.cs
// 
// 顧客利用情報 異常データ検出クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/31 勝呂):新規作成
// Ver1.01(2024/02/26 勝呂):異常データ検出パターンOの追加
//
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.CheckMwsServiceIllegalData
{
	public class CheckUseCustomerInfo
	{
		public static string OrgFilename = "MWSサービス異常データ.xlsx.org";

		public static string ExcelFilename = "MWSサービス異常データ_{0}.xlsx";

		public static string ExcelSheetName = "異常データ検出";

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int ServiceID { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? UseStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public DateTime? UseEndtDate { get; set; }

		/// <summary>
		/// 課金対象外フラグ
		/// </summary>
		public bool PauseEndStatus { get; set; }

		/// <summary>
		/// 作成日
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// 更新日
		/// </summary>
		public DateTime? UpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// 利用期限日
		/// </summary>
		public DateTime? PeriodEndDate { get; set; }

		/// <summary>
		/// エラーコード
		/// </summary>
		public string ErrorCode { get; set; }

		/// <summary>
		/// 状態
		/// </summary>
		public string Condition { get; set; }

		/// <summary>
		/// 対処方法
		/// </summary>
		public string Handle { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CheckUseCustomerInfo()
		{
			CustomerID = 0;
			CustomerName = string.Empty;
			ServiceID = 0;
			ServiceName = string.Empty;
			UseStartDate = null;
			UseEndtDate = null;
			PauseEndStatus = false;
			CreateDate = null;
			CreatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
			PeriodEndDate = null;
			ErrorCode = string.Empty;
			Condition = string.Empty;
			Handle = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<CheckUseCustomerInfo> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CheckUseCustomerInfo> result = new List<CheckUseCustomerInfo>();
				foreach (DataRow row in table.Rows)
				{
					CheckUseCustomerInfo data = new CheckUseCustomerInfo();
					data.CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]);
					data.CustomerName = row["CustomerName"].ToString().Trim();
					data.ServiceID = DataBaseValue.ConvObjectToInt(row["ServiceID"]);
					data.ServiceName = row["ServiceName"].ToString().Trim();
					data.UseStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["UseStartDate"]);
					data.UseEndtDate = DataBaseValue.ConvObjectToDateTimeNull(row["UseEndtDate"]);
					data.PauseEndStatus = ("0" == row["PauseEndStatus"].ToString()) ? false : true;
					data.CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]);
					data.CreatePerson = row["CreatePerson"].ToString().Trim();
					data.UpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["UpdateDate"]);
					data.UpdatePerson = row["UpdatePerson"].ToString().Trim();
					data.PeriodEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["PeriodEndDate"]);

					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 顧客利用情報と申込情報の異常データの検出
		/// </summary>
		/// <param name="apply">申込情報</param>
		/// <returns>判定</returns>
		public bool IsIllegalData(V_COUPLER_APPLY apply)
		{
			if (null == apply || "1" == apply.system_flg)
			{
				// 申込情報なし or (システム反映済フラグ == 1)
				if (false == UseStartDate.HasValue)
				{
					if (false == UseEndtDate.HasValue)
					{
						// 利用開始日==null && 課金終了日==null
						ErrorCode = "A";
						Condition = "利用開始日と課金終了日が未設定。サービスは有効でない";
						Handle = "処理なし。サービスがPC安心サポートの場合は顧客利用情報から削除";
					}
					else
					{
						// 利用開始日==null && 課金終了日<>null
						ErrorCode = "B";
						Condition = "利用開始日が未設定。サービスが有効でない";
						Handle = "顧客利用情報の利用開始日を設定";
					}
					return true;
				}
				if (PauseEndStatus)
				{
					if (false == PeriodEndDate.HasValue)
					{
						// 課金対象外フラグ==1 && 利用期限日==null
						ErrorCode = "D";
						Condition = "解約状態が正しくない";
						Handle = "顧客利用情報の利用期限日に終了日を設定";
						return true;
					}
				}
				else
				{
					if (PeriodEndDate.HasValue)
					{
						// 課金対象外フラグ==1 && 利用期限日<>null
						ErrorCode = "E";
						Condition = "解約状態が正しくない";
						Handle = "顧客利用情報の課金対象外フラグを設定";
						return true;
					}
					else
					{
						if (UseEndtDate.HasValue && UseEndtDate.Value < DateTime.Today.EndOfNextMonth())
						{
							// 課金終了日 < 翌月末日
							ErrorCode = "C";
							Condition = "課金終了日が翌月末日前。解約情報がない";
							Handle = "伝票起票が必要";
							return true;
						}
					}
				}
			}
			else
			{
				// 申込情報あり
				if (false == UseStartDate.HasValue)
				{
					if (false == UseEndtDate.HasValue)
					{
						// 利用開始日==null && 課金終了日==null
						ErrorCode = "F";
						Condition = "利用開始日と課金終了日が未設定。サービスは有効でない";
						Handle = "処理なし";
					}
					else
					{
						// 利用開始日==null && 課金終了日<>null
						ErrorCode = "G";
						Condition = "利用開始日が未設定。利用申込受付中のまま、解約申込ができない";
						Handle = "顧客利用情報の利用開始日を設定。申込情報の利用申込は取消";
					}
					return true;
				}
				if (UseEndtDate.HasValue)
				{
					// 課金終了日<>null
					if (PauseEndStatus)
					{
						if (false == PeriodEndDate.HasValue)
						{
							// 課金対象外フラグ==1 && 利用期限日==null
							if ("0" == apply.apply_type)
							{
								// 利用申込
								if (UseEndtDate.Value < apply.apply_date.Value)
								{
									// 利用申込受付日が課金終了日の翌月以降
									ErrorCode = "I";
									Condition = "解約状態が正しくない";
									Handle = "顧客利用情報の利用期限日に課金終了日を設定。申込情報の申込受付日は課金終了日の翌月以降なので問題なし";
									return true;
								}
								else if (apply.apply_date.Value <= UseEndtDate.Value.EndOfLastMonth())
								{
									// 利用申込受付日が課金終了月の前月以前
									ErrorCode = "J";
									Condition = "解約状態が正しくない";
									Handle = "顧客利用情報の利用期限日に課金終了日を設定。申込情報の申込受付日が課金終了日の前月以前なので利用申込は取消";
									return true;
								}
							}
						}
					}
					else
					{
						if (PeriodEndDate.HasValue)
						{
							// 課金対象外フラグ==0 && 利用期限日<>null
							if ("0" == apply.apply_type)
							{
								// 利用申込
								if (UseEndtDate.Value < apply.apply_date.Value)
								{
									// 利用申込受付日が課金終了月の翌月以降
									ErrorCode = "K";
									Condition = "解約状態が正しくない";
									Handle = "顧客利用情報の課金対象外フラグを設定。申込情報の申込受付日が課金終了日の翌月以降なので問題なし";
									return true;
								}
								else if (apply.apply_date.Value <= UseEndtDate.Value.EndOfLastMonth())
								{
									// 利用申込受付日が課金終了月の前月以前
									ErrorCode = "L";
									Condition = "解約状態が正しくない";
									Handle = "顧客利用情報の課金対象外フラグを設定。申込情報の申込受付日が課金終了日の前月以前なので利用申込は取消";
									return true;
								}
							}
						}
						else
						{
							// 課金対象外フラグ==0 && 利用期限日==null
							if ("0" == apply.apply_type)
							{
								// 利用申込
								if (DateTime.Today.EndOfNextMonth() <= UseEndtDate.Value)
								{
									// 課金終了日が翌月末日以降
									if (apply.apply_date.Value <= UseEndtDate.Value.EndOfLastMonth())
									{
										// 利用申込受付日が課金終了月の前月以前
										ErrorCode = "H";
										Condition = "利用申込中のままの状態。課金は問題なし";
										Handle = "顧客利用情報の課金終了日が翌月末日以降なので問題なし。申込情報の申込受付日が課金終了日の前月以前なので利用申込は取消";
										return true;
									}
								}
							}
							else
							{
								// 解約申込
								if (apply.apply_date.Value < UseEndtDate.Value.BeginOfLastMonth())
								{
									// 解約申込受付日が課金終了月の２か月前以前
									// Ver1.01(2024/02/26 勝呂):異常データ検出パターンOの追加
									if (DateTime.Today.EndOfNextMonth() <= UseEndtDate.Value)
									{
										// 課金終了日が翌月末日以降
										ErrorCode = "O";
										Condition = "申込受付日が課金終了月の２か月前以前。課金終了日が翌月末日以降。解約情報がない";
										Handle = "対応方法は不明？";
									}
									else
									{
										// 課金終了日が翌月末日前日以前
										ErrorCode = "M";
										Condition = "申込受付日が課金終了月の２か月前以前。課金終了日が翌月末日前日以前。解約情報がない";
										Handle = "解約処理を行うか判断が必要";
									}
									return true;
								}
								else if (apply.apply_date.Value.ToDate().ToYearMonth() == UseEndtDate.Value.ToDate().ToYearMonth())
								{
									// 解約申込受付月と課金終了月が同月
									ErrorCode = "N";
									Condition = "解約申込とWWでの解約処理が同月に行われた";
									Handle = "申込情報の解約申込を取消";
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 出力データタイトル行
		/// </summary>
		/// <returns></returns>
		public static string[] GetTilte()
		{
			string[] ret = new string[17];
			ret[0] = "顧客No";
			ret[1] = "顧客名";
			ret[2] = "サービスID";
			ret[3] = "サービス名";
			ret[4] = "申込種別";
			ret[5] = "受付日";
			ret[6] = "利用開始日";
			ret[7] = "課金終了日";
			ret[8] = "課金対象外フラグ";
			ret[9] = "作成日";
			ret[10] = "作成者";
			ret[11] = "更新日";
			ret[12] = "更新者";
			ret[13] = "利用期限日";
			ret[14] = "エラーコード";
			ret[15] = "状態";
			ret[16] = "対処方法";
			return ret;
		}

		/// <summary>
		/// 出力データ文字列の取得
		/// </summary>
		/// <returns>出力データ文字列</returns>
		public string[] GetOutputData(V_COUPLER_APPLY apply)
		{
			string[] ret = new string[17];
			ret[0] = CustomerID.ToString();
			ret[1] = CustomerName;
			ret[2] = ServiceID.ToString();
			ret[3] = ServiceName;
			if (null != apply)
			{
				ret[4] = ("0" == apply.apply_type) ? "利用申込": "解約申込";
				ret[5] = (apply.apply_date.HasValue) ? apply.apply_date.Value.ToShortDateString() : apply.apply_date.ToString();
			}
			ret[6] = (UseStartDate.HasValue) ? UseStartDate.Value.ToShortDateString() : UseStartDate.ToString();
			ret[7] = (UseEndtDate.HasValue) ? UseEndtDate.Value.ToShortDateString() : UseEndtDate.ToString();
			ret[8] = (PauseEndStatus) ? "1": "0";
			ret[9] = (CreateDate.HasValue) ? CreateDate.Value.ToShortDateString() : CreateDate.ToString();
			ret[10] = CreatePerson;
			ret[11] = (UpdateDate.HasValue) ? UpdateDate.Value.ToShortDateString() : UpdateDate.ToString();
			ret[12] = UpdatePerson;
			ret[13] = (PeriodEndDate.HasValue) ? PeriodEndDate.Value.ToShortDateString() : PeriodEndDate.ToString();
			ret[14] = ErrorCode;
			ret[15] = Condition;
			ret[16] = Handle;
			return ret;
		}
	}
}
