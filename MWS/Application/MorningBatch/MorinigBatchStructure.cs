//
// MorningBatchStructure.cs
//
// 朝バッチ構造体定義群
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2022/10/21 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using CommonLib.Common;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// 顧客情報
	/// </summary>
	public class stU005_out1a
	{
		/// <summary>
		/// 製品ID
		/// </summary>
		public string PRODUCT_ID { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string PASSWORD { get; set; }

		/// <summary>
		/// ユーザー種別
		/// </summary>
		public MwsDefine.UserClassification USER_CLASSIFICATION { get; set; }

		/// <summary>
		/// 体験版
		/// </summary>
		public bool TRIAL_FLG { get; set; }

		/// <summary>
		/// 顧客ID
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CUSTOMER_NAME { get; set; }

		/// <summary>
		/// 拠点メールアドレス
		/// </summary>
		public string BRANCH_MAIL { get; set; }

		/// <summary>
		/// メールアドレス１
		/// </summary>
		public string MAIL1 { get; set; }

		/// <summary>
		/// 保有ライセンス数
		/// </summary>
		public int CLIENT_LICENSES { get; set; }

		/// <summary>
		/// 休止・終了ステータス
		/// </summary>
		public bool END_FLG { get; set; }

		/// <summary>
		/// 体験利用開始日
		/// </summary>
		public DateTime? TRIAL_START_DATE { get; set; }

		/// <summary>
		/// 体験利用終了日
		/// </summary>
		public DateTime? TRIAL_END_DATE { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DELETE_FLG { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public stU005_out1a()
		{
			PRODUCT_ID = string.Empty;
			PASSWORD = string.Empty;
			USER_CLASSIFICATION = MwsDefine.UserClassification.PaletteUser;
			TRIAL_FLG = false;
			CUSTOMER_ID = 0;
			CUSTOMER_NAME = string.Empty;
			BRANCH_MAIL = string.Empty;
			MAIL1 = string.Empty;
			CLIENT_LICENSES = 0;
			END_FLG = false;
			TRIAL_START_DATE = null;
			TRIAL_END_DATE = null;
			DELETE_FLG = false;
		}

		///// <summary>
		///// 詰め替え
		///// </summary>
		///// <param name="table">データテーブル</param>
		///// <returns>製品管理情報</returns>
		//public static List<stU005_out1a> DataTableToList(DataTable table)
		//{
		//	if (null != table && 0 < table.Rows.Count)
		//	{
		//		List<stU005_out1a> result = new List<stU005_out1a>();
		//		foreach (DataRow row in table.Rows)
		//		{
		//			stU005_out1a data = new stU005_out1a
		//			{
		//				PRODUCT_ID = row["PRODUCT_ID"].ToString().Trim(),
		//				PASSWORD = row["PASSWORD"].ToString().Trim(),
		//				USER_CLASSIFICATION = (MwsDefine.UserClassification)DataBaseValue.ConvObjectToInt(row["USER_CLASSIFICATION"]),
		//				TRIAL_FLG = ("0" == row["TRIAL_FLG"].ToString()) ? false : true,
		//				CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
		//				CUSTOMER_NAME = row["CUSTOMER_NAME"].ToString().Trim(),
		//				BRANCH_MAIL = row["BRANCH_MAIL"].ToString().Trim(),
		//				MAIL1 = row["MAIL1"].ToString().Trim(),
		//				CLIENT_LICENSES = DataBaseValue.ConvObjectToInt(row["CLIENT_LICENSES"]),
		//				END_FLG = ("0" == row["END_FLG"].ToString()) ? false : true,
		//				TRIAL_START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["TRIAL_START_DATE"]),
		//				TRIAL_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["TRIAL_END_DATE"]),
		//				DELETE_FLG = ("0" == row["DELETE_FLG"].ToString()) ? false : true,
		//			};
		//			result.Add(data);
		//		}
		//		return result;
		//	}
		//	return null;
		//}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <param name="out1aSwitch">顧客データ処理判定（1:全件 2:差分）</param>
		/// <returns>製品管理情報</returns>
		public static List<stU005_out1a> DataTableToList(DataTable table, string out1aSwitch)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<stU005_out1a> result = new List<stU005_out1a>();
				foreach (DataRow row in table.Rows)
				{
					stU005_out1a data = new stU005_out1a();
					data.PRODUCT_ID = row["PRODUCT_ID"].ToString().Trim();
					data.PASSWORD = row["PASSWORD"].ToString().Trim();
					data.USER_CLASSIFICATION = (MwsDefine.UserClassification)DataBaseValue.ConvObjectToInt(row["USER_CLASSIFICATION"]);
					data.TRIAL_FLG = ("0" == row["TRIAL_FLG"].ToString()) ? false : true;
					data.CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					data.CUSTOMER_NAME = row["CUSTOMER_NAME"].ToString().Trim();
					data.BRANCH_MAIL = row["BRANCH_MAIL"].ToString().Trim();
					data.MAIL1 = row["MAIL1"].ToString().Trim();
					data.END_FLG = ("0" == row["END_FLG"].ToString()) ? false : true;
					data.TRIAL_START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["TRIAL_START_DATE"]);
					DateTime? dt = DataBaseValue.ConvObjectToDateTimeNull(row["TRIAL_END_DATE"]);
					if (dt.HasValue)
					{
						data.TRIAL_END_DATE = dt;
					}
					else
					{
						// 2018/5/24 Ver2.19.0対応 Charlieのログイン終了日は基本NULLになる（無期限扱い）COUPLERでは、ログイン終了日に2999/12/31でセットする
						data.TRIAL_END_DATE = new DateTime(2999, 12, 31);
					}
					if ("2" == out1aSwitch)
					{
						data.DELETE_FLG = ("0" == row["DELETE_FLG"].ToString()) ? false : true;
					}
					data.CLIENT_LICENSES = DataBaseValue.ConvObjectToInt(row["CLIENT_LICENSES"]);
					if (0 == data.CUSTOMER_ID)
					{
						// 製品と顧客が紐づいていない場合はクライアントライセンス数に1を設定する
						data.CLIENT_LICENSES = 1;
					}
					else
					{
						int useSystemCode;
						if (int.TryParse(row["USE_SYSTEM_CODE"].ToString().Trim(), out useSystemCode))
						{
							if (useSystemCode < 100 || 199 < useSystemCode)
							{
								// 2012/06/01 製品版(ユーザのシステムコードが、0100～0199以外の場合は、クライアント数を10にセットする)
								data.CLIENT_LICENSES = 10;
							}
						}
					}
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// エラーチェック
		/// </summary>
		/// <param name="sErr">エラーメッセージ</param>
		/// <returns>判定</returns>
		public bool ErrorCheck(out string sErr)
		{
			sErr = string.Empty;
			if ("" == PRODUCT_ID)
			{
				sErr = "製品ID 必須チェック";
				return true;
			}
			if (11 != PRODUCT_ID.Length)
			{
				sErr = "製品ID 桁数チェック";
				return true;
			}
			if (false == StringUtil.IsAlphanumericSingleByte(PRODUCT_ID))
			{
				sErr = "製品ID 半角英数チェック";
				return true;
			}
			if ("" == PASSWORD)
			{
				sErr = "パスワード 必須チェック";
				return true;
			}
			if (16 != PASSWORD.Length)
			{
				sErr = "パスワード 桁数チェック";
				return true;
			}
			if (false == StringUtil.IsAlphanumericSingleByte(PASSWORD))
			{
				sErr = "パスワード 半角英数チェック";
				return true;
			}
			if (8 != CUSTOMER_ID.ToString().Length)
			{
				sErr = "顧客ID 桁数チェック";
				return true;
			}
			if (40 >= CUSTOMER_NAME.Length)
			{
				sErr = "顧客名 桁数チェック";
				return true;
			}
			if (false == TRIAL_FLG && 0 == BRANCH_MAIL.Length)
			{
				sErr = "メールアドレス１ 必須チェック";
				return true;
			}
			if (200 < BRANCH_MAIL.Length)
			{
				sErr = "メールアドレス１ 桁数チェック";
				return true;
			}
			if (200 < MAIL1.Length)
			{
				sErr = "メールアドレス２ 桁数チェック";
				return true;
			}
			if (false == TRIAL_START_DATE.HasValue)
			{
				sErr = "利用開始日付チェック";
				return true;
			}
			if (false == TRIAL_END_DATE.HasValue)
			{
				sErr = "利用終了日付チェック";
				return true;
			}
			if (TRIAL_START_DATE.HasValue && TRIAL_END_DATE.HasValue)
			{
				if (TRIAL_END_DATE.Value < TRIAL_START_DATE.Value)
				{
					sErr = "利用開始日付前後チェック";
					return true;
				}
			}
			return false;
		}
	}


	/// <summary>
	/// サービス利用情報
	/// </summary>
	public class stU005_out1b
	{
		public int CUSTOMER_ID { get; set; }
		public string PRODUCT_ID { get; set; }
		public int SERVICE_ID { get; set; }
		public int PAUSE_END_STATUS { get; set; }
		public int SET_SALE { get; set; }
		public DateTime? USE_START_DATE { get; set; }
		public DateTime? USE_END_DATE { get; set; }
		public bool DELETE_FLG { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public stU005_out1b()
		{
			CUSTOMER_ID = 0;
			PRODUCT_ID = string.Empty;
			SERVICE_ID = 0;
			PAUSE_END_STATUS = 0;
			SET_SALE = 0;
			USE_START_DATE = null;
			USE_END_DATE = null;
			DELETE_FLG = false;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <param name="out1bSwitch">利用機能データ処理判定（1:全件 2:差分）</param>
		/// <param name="iServiceNoOut">有効期間外サービス数</param>
		/// <returns>サービス利用情報</returns>
		public static List<stU005_out1b> DataTableToList(DataTable table, string out1bSwitch, out int iServiceNoOut)
		{
			iServiceNoOut = 0;
			if (null != table && 0 < table.Rows.Count)
			{
				List<stU005_out1b> result = new List<stU005_out1b>();
				foreach (DataRow row in table.Rows)
				{
					int customerID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					string productID = row["PRODUCT_ID"].ToString().Trim();
					int serviceID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]);
					DateTime? useStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["USE_START_DATE"]);

					// Ver2.19.0 利用終了日のチェックは行わない
					if (false == useStartDate.HasValue)
					{
						Console.WriteLine(string.Format("顧客ID：{0} CouplerID：{1} サービスID：{2} 利用開始日が指定されていないため出力は行いませんでした。", customerID, productID, serviceID));
						iServiceNoOut++;
					}
					else
					{
						stU005_out1b data = new stU005_out1b();
						data.CUSTOMER_ID = customerID;
						data.PRODUCT_ID = productID;
						data.SERVICE_ID = serviceID;
						data.PAUSE_END_STATUS = DataBaseValue.ConvObjectToInt(row["PAUSE_END_STATUS"]);
						data.SET_SALE = DataBaseValue.ConvObjectToInt(row["SET_SALE"]);
						data.USE_START_DATE = useStartDate;
						DateTime? useEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["USE_END_DATE"]);
						if (false == useEndDate.HasValue)
						{
							// 2018/5/24 Ver2.19.0対応 Charlieの利用終了日は基本NULLになる（無期限扱い） COUPLERでは、サービス利用終了日に2999 / 12 / 31でセットする。
							data.USE_END_DATE = new DateTime(2999, 12, 31);
						}
						else
						{
							data.USE_END_DATE = useEndDate;
						}
						if ("2" == out1bSwitch)
						{
							data.DELETE_FLG = ("0" == row["DELETE_FLG"].ToString()) ? false : true;
						}
						result.Add(data);
					}
				}
				return result;
			}
			return null;
		}
	}

	/// <summary>
	/// カプラー受付情報
	/// </summary>
	public class CouplerApply
	{
		public int apply_id { get; set; }
		public string cp_id { get; set; }
		public int service_id { get; set; }
		public int apply_type { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CouplerApply()
		{
			apply_id = 0;
			cp_id = string.Empty;
			service_id = 0;
			apply_type = 0;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>製品管理情報</returns>
		public static List<CouplerApply> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CouplerApply> result = new List<CouplerApply>();
				foreach (DataRow row in table.Rows)
				{
					CouplerApply data = new CouplerApply
					{
						apply_id = DataBaseValue.ConvObjectToInt(row["apply_id"]),
						cp_id = row["cp_id"].ToString().Trim(),
						service_id = DataBaseValue.ConvObjectToInt(row["service_id"]),
						apply_type = DataBaseValue.ConvObjectToInt(row["apply_type"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}

	
	/// <summary>
	/// サービス情報
	/// </summary>
	public class CouplerService
	{
		public string cp_id { get; set; }
		public int service_id { get; set; }
		public int contrac_type { get; set; }
		public int payment_type { get; set; }
		public DateTime? start_date { get; set; }
		public DateTime? end_date { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CouplerService()
		{
			cp_id = string.Empty;
			service_id = 0;
			contrac_type = 0;
			payment_type = 0;
			start_date = null;
			end_date = null;
		}
	}

	public class CouplerProductuser
	{
		/// <summary>
		/// MWS-ID
		/// </summary>
		public string cp_id { get; set; }

		/// <summary>
		/// ユーザー種別
		/// 0:UNICORNユーザ
		/// 1:既存製品ユーザ
		/// 2:社員用ユーザー
		/// 3:デモ用ユーザー
		/// </summary>
		public int user_type { get; set; }

		/// <summary>
		/// 体験版 
		/// </summary>
		public bool trial_flg { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool end_flg { get; set; }

		/// <summary>
		/// 顧客ID
		/// </summary>
		public int customer_id { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string customer_nm { get; set; }

		/// <summary>
		/// メールアドレス1
		/// </summary>
		public string email1 { get; set; }

		/// <summary>
		/// メールアドレス2
		/// </summary>
		public string email2 { get; set; }

		/// <summary>
		/// 利用開始日時
		/// </summary>
		public DateTime? login_start_date { get; set; }

		/// <summary>
		/// 利用終了日時
		/// </summary>
		public DateTime? login_end_date { get; set; }

		/// <summary>
		/// ログインパスワード
		/// </summary>
		public string login_paswd { get; set; }

		/// <summary>
		/// 初期パスワード
		/// </summary>
		public string default_paswd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string paswd_update { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int license_count { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CouplerProductuser()
		{
			cp_id = string.Empty;
			user_type = 0;
			trial_flg = false;
			end_flg = false;
			customer_id = 0;
			customer_nm = string.Empty;
			email1 = string.Empty;
			email2 = string.Empty;
			login_start_date = null;
			login_end_date = null;
			login_paswd = string.Empty;
			default_paswd = string.Empty;
			paswd_update = string.Empty;
			license_count = 0;
		}
	}
}

