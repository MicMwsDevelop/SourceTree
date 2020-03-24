using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.SoftwareMainteSaleData
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
        /// 課金開始日
        /// </summary>
        public Date? USE_START_DATE { get; set; }

        /// <summary>
        /// 課金終了日
        /// </summary>
        public Date? USE_END_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Date? CANCELLATION_DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PAUSE_END_STATUS { get; set; }

        /// <summary>
        /// 利用終了日
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
                                    + " WHERE CUSTOMER_ID = {1} AND AND SERVICE_ID = {2}"
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
        /// <returns></returns>
        public SqlParameter[] GetUpdateSetParameters(string user, Date endDate)
        {
            SqlParameter[] param = {
                new SqlParameter("@1", endDate.ToString()),
                new SqlParameter("@2", Date.Today.ToString()),
                new SqlParameter("@3", user),
                new SqlParameter("@4","1")
            };
            return param;
        }
    }
}
