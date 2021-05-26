using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.NarcohmOrderCheck
{
    /// <summary>
    /// 医院情報
    /// </summary>
    public class CustomerInfo
    {
        /// <summary>
        /// 顧客No
        /// </summary>
        public int CustomerNo { get; set; }

        /// <summary>
        /// 得意先No
        /// </summary>
        public string TokuisakiNo { get; set; }

        /// <summary>
        /// 顧客名
        /// </summary>
        public string ClinicName { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string MailAddress { get; set; }

        /// <summary>
        /// 営業部コード
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// 営業部名
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// 拠点コード
        /// </summary>
        public string BranchCode { get; set; }

        /// <summary>
        /// 拠点名
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// 営業担当者コード
        /// </summary>
        public string SalesmanCode { get; set; }

        /// <summary>
        /// 営業担当者名
        /// </summary>
        public string SalesmanName { get; set; }

        /// <summary>
        /// 終了フラグ
        /// </summary>
        public bool EndFlag { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public CustomerInfo()
        {

        }
    }
}
