//
// UpdateCouplerProductUser.cs
//
// カプラー顧客情報更新クラス
// [COUPLER].[dbo].[PRODUCTUSER]
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.Coupler.Table;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.AdjustServiceApply
{
	public class UpdateCouplerProductUser
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
		/// 初期パスワード
		/// </summary>
		public string default_paswd { get; set; }

		/// <summary>
		/// 同時接続クライアント数
		/// </summary>
		public int license_count { get; set; }

		/// <summary>
		/// システムコード
		/// </summary>
		public int system_code { get; set; }

		/// <summary>
		/// 削除フラグ（T_CUSTOMER_FOUNDATIONS.DELETE_FLG）
		/// </summary>
		public bool delete_flg { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public UpdateCouplerProductUser()
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
			default_paswd = string.Empty;
			license_count = 0;
			system_code = 0;
			delete_flg = false;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<UpdateCouplerProductUser> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<UpdateCouplerProductUser> result = new List<UpdateCouplerProductUser>();
				foreach (DataRow row in table.Rows)
				{
					UpdateCouplerProductUser data = new UpdateCouplerProductUser
					{
						cp_id = row["cp_id"].ToString().Trim(),
						user_type = DataBaseValue.ConvObjectToInt(row["user_type"]),
						trial_flg = ("0" == row["trial_flg"].ToString()) ? false : true,
						end_flg = ("0" == row["end_flg"].ToString()) ? false : true,
						customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"]),
						customer_nm = row["customer_nm"].ToString().Trim(),
						email1 = row["email1"].ToString().Trim(),
						email2 = row["email2"].ToString().Trim(),
						login_start_date = DataBaseValue.ConvObjectToDateTimeNull(row["login_start_date"]),
						login_end_date = DataBaseValue.ConvObjectToDateTimeNull(row["login_end_date"]),
						default_paswd = row["default_paswd"].ToString().Trim(),
						license_count = DataBaseValue.ConvObjectToInt(row["license_count"]),
						system_code = DataBaseValue.ConvObjectToInt(row["system_code"]),
						delete_flg = ("0" == row["delete_flg"].ToString()) ? false : true,
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 同時接続クライアント数を取得
		/// </summary>
		/// <returns></returns>
		public int GetClientLicenseCount()
		{
			if (0 < customer_id)
			{
				if (100 < system_code && system_code < 200)
				{
					// tMikユーザ.fus同時接続ｸﾗｲｱﾝﾄ数
					return license_count;
				}
				// システムコードがpalette以外は同時接続クライアント数は10
				return 10;
			}
			// 製品と顧客が紐づいていない場合は同時接続クライアント数は1
			return 1;
		}

		/// <summary>
		/// T_COUPLER_PRODUCTUSERの設定
		/// </summary>
		/// <param name="createUser">作成者</param>
		/// <returns>T_COUPLER_PRODUCTUSER</returns>
		public T_COUPLER_PRODUCTUSER SetInsertIntoCouplerProductuser(string createUser)
		{
			T_COUPLER_PRODUCTUSER ret = new T_COUPLER_PRODUCTUSER();
			ret.cp_id = cp_id;
			ret.user_type = user_type;
			ret.trial_flg = trial_flg;
			ret.end_flg = end_flg;
			ret.customer_id = customer_id;
			ret.customer_nm = customer_nm;
			ret.email1 = email1;
			ret.email2 = email2;
			ret.login_start_date = login_start_date;
			ret.login_end_date = login_end_date;
			ret.login_paswd = default_paswd;
			ret.default_paswd = default_paswd;
			//ret.paswd_update = null;
			ret.license_count = GetClientLicenseCount();
			//ret.ver_id = 0;
			//ret.testuser_flg = false;
			ret.create_date = DateTime.Now;
			ret.create_user = createUser;
			//ret.update_date = null;
			//ret.update_user = string.Empty;
			return ret;
		}
	}
}
