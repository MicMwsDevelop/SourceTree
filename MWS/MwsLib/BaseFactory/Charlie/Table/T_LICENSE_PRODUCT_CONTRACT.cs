//
// T_LICENSE_PRODUCT_CONTRACT.cs
//
// ESETライセンス管理情報テーブルクラス
// [CharlieDB].[dbo].[T_LICENSE_PRODUCT_CONTRACT]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Charlie.Table
{
	public class T_LICENSE_PRODUCT_CONTRACT
	{
		/// <summary>
		/// 利用申請番号
		/// REGIST00000001から連番
		/// </summary>
		public string REQUEST_NO { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int SERVICE_ID { get; set; }

		/// <summary>
		/// 解約申請番号
		/// CANCEL00000001から連番
		/// </summary>
		public string CANCEL_REQUEST_NO { get; set; }

		/// <summary>
		/// シリアル番号
		/// </summary>
		public string SERIAL { get; set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string LICENSE_USER_NAME { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string LICENSE_PASSWORD { get; set; }

		/// <summary>
		/// 製品認証キー
		/// </summary>
		public string LICENSE_KEY { get; set; }

		/// <summary>
		/// ライセンスID
		/// </summary>
		public string PUBLIC_LICENSE_ID { get; set; }

		/// <summary>
		/// ライセンス数
		/// </summary>
		public int? LICENSE_CNT { get; set; }

		/// <summary>
		/// 利用開始日時
		/// </summary>
		public DateTime? START_DATE { get; set; }

		/// <summary>
		/// 利用終了日時
		/// </summary>
		public DateTime? END_DATE { get; set; }

		/// <summary>
		/// 有効期限
		/// </summary>
		public DateTime? EXPIRATIONDATE { get; set; }

		/// <summary>
		/// 申込状態
		/// 0:利用申し込み 1:解約申し込み
		/// </summary>
		public bool APPLY_STATUS { get; set; }

		/// <summary>
		/// 利用取消フラグ
		/// 0:未取消 1:取消済み
		/// </summary>
		public bool RIYO_CANCEL_FLG { get; set; }

		/// <summary>
		/// 解約取消フラグ
		/// 0:未取消 1:取消済み
		/// </summary>
		public bool KAIYAKU_CANCEL_FLG { get; set; }

		/// <summary>
		/// 新規登録APIステータス
		/// </summary>
		public string REG_API_STATUS { get; set; }

		/// <summary>
		/// 解約APIステータス
		/// </summary>
		public string CAN_API_STATUS { get; set; }

		/// <summary>
		/// 解約受付日付
		/// </summary>
		public DateTime? CANCEL_APPLY_DATE { get; set; }

		/// <summary>
		/// 作成日
		/// </summary>
		public DateTime? CREATE_DATE { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CREATE_USER { get; set; }

		/// <summary>
		/// 更新日
		/// </summary>
		public DateTime? UPDATE_DATE { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UPDATE_USER { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? billing_exp_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool del_flg { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_LICENSE_PRODUCT_CONTRACT()
		{
			REQUEST_NO = string.Empty;
			CUSTOMER_ID = 0;
			SERVICE_ID = 0;
			CANCEL_REQUEST_NO = string.Empty;
			SERIAL = string.Empty;
			LICENSE_USER_NAME = string.Empty;
			LICENSE_PASSWORD = string.Empty;
			LICENSE_KEY = string.Empty;
			PUBLIC_LICENSE_ID = string.Empty;
			LICENSE_CNT = null;
			START_DATE = null;
			END_DATE = null;
			EXPIRATIONDATE = null;
			APPLY_STATUS = false;
			RIYO_CANCEL_FLG = false;
			KAIYAKU_CANCEL_FLG = false;
			REG_API_STATUS = string.Empty;
			CAN_API_STATUS = string.Empty;
			CANCEL_APPLY_DATE = null;
			CREATE_DATE = null;
			CREATE_USER = string.Empty;
			UPDATE_DATE = null;
			UPDATE_USER = string.Empty;
			billing_exp_date = null;
			del_flg = false;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_LICENSE_PRODUCT_CONTRACT]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ESETライセンス管理情報</returns>
		public static List<T_LICENSE_PRODUCT_CONTRACT> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_LICENSE_PRODUCT_CONTRACT> result = new List<T_LICENSE_PRODUCT_CONTRACT>();
				foreach (DataRow row in table.Rows)
				{
					T_LICENSE_PRODUCT_CONTRACT data = new T_LICENSE_PRODUCT_CONTRACT
					{
						REQUEST_NO = row["REQUEST_NO"].ToString().Trim(),
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						CANCEL_REQUEST_NO = row["CANCEL_REQUEST_NO"].ToString().Trim(),
						SERIAL = row["SERIAL"].ToString().Trim(),
						LICENSE_USER_NAME = row["LICENSE_USER_NAME"].ToString().Trim(),
						LICENSE_PASSWORD = row["LICENSE_USER_NAME"].ToString().Trim(),
						LICENSE_KEY = row["LICENSE_KEY"].ToString().Trim(),
						PUBLIC_LICENSE_ID = row["PUBLIC_LICENSE_ID"].ToString().Trim(),
						LICENSE_CNT = DataBaseValue.ConvObjectToIntNull(row["LICENSE_CNT"]),
						START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["START_DATE"]),
						END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["END_DATE"]),
						EXPIRATIONDATE = DataBaseValue.ConvObjectToDateTimeNull(row["EXPIRATIONDATE"]),
						APPLY_STATUS = ("0" == row["APPLY_STATUS"].ToString()) ? false : true,
						RIYO_CANCEL_FLG = ("0" == row["RIYO_CANCEL_FLG"].ToString()) ? false : true,
						KAIYAKU_CANCEL_FLG = ("0" == row["KAIYAKU_CANCEL_FLG"].ToString()) ? false : true,
						REG_API_STATUS = row["REG_API_STATUS"].ToString().Trim(),
						CAN_API_STATUS = row["CAN_API_STATUS"].ToString().Trim(),
						CANCEL_APPLY_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CANCEL_APPLY_DATE"]),
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_USER = row["CREATE_USER"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_USER = row["UPDATE_USER"].ToString().Trim(),
						billing_exp_date = DataBaseValue.ConvObjectToDateTimeNull(row["billing_exp_date"]),
						del_flg = ("0" == row["del_flg"].ToString()) ? false : true,
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
