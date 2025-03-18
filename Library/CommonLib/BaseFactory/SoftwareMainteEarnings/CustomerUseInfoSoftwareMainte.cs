//
// CustomerUseInfoSoftwareMainte.cs
// 
// ソフトウェア保守料利用情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
//
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.SoftwareMainteEarnings
{
	/// <summary>
	/// ソフトウェア保守料利用情報
	/// </summary>
	public class CustomerUseInfoSoftwareMainte
	{
        /// <summary>
        /// 顧客No
        /// </summary>
        public int CUSTOMER_ID { get; set; }

        /// <summary>
        /// サービスID
        /// </summary>
        public int SERVICE_ID { get; set; }

        /// <summary>
        /// 利用開始日
        /// </summary>
        public Date? USE_START_DATE { get; set; }

        /// <summary>
        /// 課金終了日
        /// </summary>
        public Date? USE_END_DATE { get; set; }

        /// <summary>
        /// 解約受付日
        /// </summary>
        public Date? CANCELLATION_DAY { get; set; }

        /// <summary>
        /// 課金対象外フラグ
        /// </summary>
        public int? PAUSE_END_STATUS { get; set; }

        /// <summary>
        /// 利用期限日
        /// </summary>
        public Date? PERIOD_END_DATE { get; set; }

        /// <summary>
        /// palette ES課金終了日
        /// </summary>
        public Date? ES_USE_END_DATE { get; set; }

        /// <summary>
        /// UPDATE SET SQL文字列の取得
        /// </summary>
        public string UpdateSetSqlString
        {
            get
            {
                return string.Format(@"UPDATE {0} SET USE_END_DATE = @1, UPDATE_DATE = @2, UPDATE_PERSON = @3, RENEWAL_FLG = @4"
                                    + " WHERE CUSTOMER_ID = {1} AND SERVICE_ID = {2}"
                                    , CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
                                    , CUSTOMER_ID
                                    , SERVICE_ID);
            }
        }
		
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CustomerUseInfoSoftwareMainte()
        {
            CUSTOMER_ID = 0;
            SERVICE_ID = 0;
            USE_START_DATE = null;
            USE_END_DATE = null;
            CANCELLATION_DAY = null;
            PAUSE_END_STATUS = null;
            PERIOD_END_DATE = null;
            ES_USE_END_DATE = null;
        }

        /// <summary>
        /// UPDATE SETパラメタの取得
        /// </summary>
		/// <param name="updateUser">更新者</param>
		/// <param name="endDate">利用終了日</param>
        /// <returns></returns>
        public SqlParameter[] GetUpdateSetParameters(string updateUser, Date endDate)
        {
            SqlParameter[] param = {
                new SqlParameter("@1", endDate.ToString()),     // USE_END_DATE
                new SqlParameter("@2", DateTime.Now.ToString()),  // UPDATE_DATE
                new SqlParameter("@3", updateUser),             // UPDATE_USER
                new SqlParameter("@4","1")                      // RENEWAL_FLAG
            };
            return param;
        }
    }
}
