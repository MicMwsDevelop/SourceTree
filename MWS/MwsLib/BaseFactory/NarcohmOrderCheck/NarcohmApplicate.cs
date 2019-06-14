using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace MwsLib.BaseFactory.NarcohmOrderCheck
{
    /// <summary>
    /// ナルコーム製品申込情報
    /// </summary>
    public class NarcohmApplicate
    {
        /// <summary>
        /// 申込番号(オートナンバー)
        /// </summary>
        public int ApplicateID { get; set; }
        
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
        /// 電話番号
        /// </summary>
        public string Telephone { get; set; }

		/// <summary>
		/// 件名
		/// </summary>
		public string Subject { get; set; }

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
		/// サービス開始日
		/// </summary>
		public Date? ServiceStartDate { get; set; }

		/// <summary>
		/// 販売種別
		/// </summary>
		public MwsDefine.ApplyType SaleType { get; set; }

		/// <summary>
		/// メール送信日時
		/// </summary>
		public DateTime? MailSendDateTime { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// 作成者
        /// </summary>
        public string CreatePerson { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdatePerson { get; set; }

		/// <summary>
		/// ナルコーム製品申込詳細情報リスト
		/// </summary>
		public List<NarcohmApplicateDetail> DetailList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NarcohmApplicate()
        {
			ApplicateID = 0;
			CustomerNo = 0;
			TokuisakiNo = string.Empty;
			ClinicName = string.Empty;
			Telephone = string.Empty;
			Subject = string.Empty;
			SectionCode = string.Empty;
			SectionName = string.Empty;
			BranchCode = string.Empty;
			BranchName = string.Empty;
			SalesmanCode = string.Empty;
			SalesmanName = string.Empty;
			ServiceStartDate = null;
			SaleType = MwsDefine.ApplyType.Etc;
			MailSendDateTime = null;
			CreateDateTime = null;
			CreatePerson = string.Empty;
			UpdateDateTime = null;
			UpdatePerson = string.Empty;
			DetailList = new List<NarcohmApplicateDetail>();
		}

		/// <summary>
		/// 医院情報の設定
		/// </summary>
		/// <param name="customer">医院情報</param>
		public void SetCustomerData(CustomerInfo customer)
		{
			CustomerNo = customer.CustomerNo;
			TokuisakiNo = customer.TokuisakiNo;
			ClinicName = customer.ClinicName;
			Telephone = customer.Telephone;
			SectionCode = customer.SectionCode;
			SectionName = customer.SectionName;
			BranchCode = customer.BranchCode;
			BranchName = customer.BranchName;
			SalesmanCode = customer.SalesmanCode;
			SalesmanName = customer.SalesmanName;
		}

		/// <summary>
		/// リストビュー表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[14];
			array[0] = ApplicateID.ToString();
			array[3] = CustomerNo.ToString();
			array[4] = ClinicName;
			array[5] = Subject;
			array[6] = BranchName;
			array[7] = SalesmanName;
			array[8] = (ServiceStartDate.HasValue) ? ServiceStartDate.Value.ToString() : "";
			array[9] = MwsDefine.ApplyTypeString[SaleType];
			array[13] = (MailSendDateTime.HasValue) ? MailSendDateTime.Value.ToString() : "";
			if (0 < DetailList.Count)
			{
				NarcohmApplicateDetail detail = DetailList[0];
				array[1] = (detail.OrderNo.HasValue) ? detail.OrderNo.Value.ToString() : "";
				array[2] = (detail.OrderDate.HasValue) ? detail.OrderDate.Value.ToString() : "";
				array[10] = detail.GoodsName;
				array[11] = detail.Price.ToString();
				array[12] = detail.Count.ToString();
			}
			else
			{
				//array[1] = "";
				//array[2] = "";
			}
			return array;
		}
	}

	/// <summary>
	/// ナルコーム製品申込詳細情報
	/// </summary>
	public class NarcohmApplicateDetail
	{
		/// <summary>
		/// 申込詳細番号(オートナンバー)
		/// </summary>
		public int ApplicateDetailID { get; set; }

		/// <summary>
		/// 申込番号
		/// </summary>
		public int ApplicateID { get; set; }

		/// <summary>
		/// 受注番号
		/// </summary>
		public int? OrderNo { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public Date? OrderDate { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsCode { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// 合計
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public Date? UseStartDate { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public Date? UseEndDate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NarcohmApplicateDetail()
		{
			ApplicateDetailID = 0;
			ApplicateID = 0;
			OrderNo = null;
			OrderDate = null;
			GoodsCode = string.Empty;
			GoodsName = string.Empty;
			Price = 0;
			Count = 0;
			Total = 0;
			UseStartDate = null;
			UseEndDate = null;
		}

		/// <summary>
		/// 受注情報の格納
		/// </summary>
		/// <param name="order">受注情報</param>
		public void SetNarcohmOrderInfo(NarcohmOrderInfo order)
		{
			OrderNo = order.OrderNo;
			OrderDate = order.OrderDate;
			GoodsCode = order.GoodsCode;
			GoodsName = order.GoodsName;
			Price = order.Price;
			Count = order.Count;
			Total = order.Total;
		}

		/// <summary>
		/// リストビュー表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[9];
			array[0] = OrderNo.ToString();
			array[1] = (OrderDate.HasValue) ? OrderDate.ToString() : "";
			array[2] = GoodsCode;
			array[3] = GoodsName;
			array[4] = "\\" + StringUtil.CommaEdit(Price);
			array[5] = Count.ToString();
			array[6] = "\\" + StringUtil.CommaEdit(Total);
			array[7] = (UseStartDate.HasValue) ? UseStartDate.Value.ToYearMonth().ToString() : "";
			array[8] = (UseEndDate.HasValue) ? UseEndDate.Value.ToYearMonth().ToString() : "";
			return array;
		}
	}
}
