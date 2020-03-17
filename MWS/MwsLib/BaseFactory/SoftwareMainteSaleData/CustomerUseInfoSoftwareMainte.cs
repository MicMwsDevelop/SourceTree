using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

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
        /// 作成日
        /// </summary>
        public Date? CREATE_DATE { get; set; }

        /// <summary>
        /// 利用終了日
        /// </summary>
        public Date? PERIOD_END_DATE { get; set; }

        /// <summary>
        /// palette ES課金終了日
        /// </summary>
        public Date? ES_USE_END_DATE { get; set; }

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
            CREATE_DATE = null;
            PERIOD_END_DATE = null;
            ES_USE_END_DATE = null;
        }
    }
}
