using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.NarcohmOrderCheck;
using System.Data;
using MwsLib.BaseFactory;

namespace MwsLib.DB.SqlServer.NarcohmOrderCheck
{
    public static class NarcohmOderCheckController
    {
        //////////////////////////////////////////////////////////////////
        /// JunpDB
        //////////////////////////////////////////////////////////////////

        /// <summary>
        /// 医院情報の詰め替え
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <returns>医院情報</returns>
        public static List<CustomerInfo> ConvertCustomerInfo(DataTable table)
        {
            List<CustomerInfo> result = null;
            if (null != table)
            {
                result = new List<CustomerInfo>();
                foreach (DataRow row in table.Rows)
                {
                    CustomerInfo data = new CustomerInfo();
                    data.CustomerNo = DataBaseValue.ConvObjectToInt(row["顧客No"]);
                    data.TokuisakiNo = row["得意先No"].ToString().Trim();
                    data.ClinicName = row["顧客名"].ToString().Trim();
                    data.Zipcode = row["郵便番号"].ToString().Trim();
                    data.Address = row["住所"].ToString().Trim();
                    data.Telephone = row["電話番号"].ToString().Trim();
                    data.MailAddress = row["メールアドレス"].ToString().Trim();
                    data.SectionCode = row["営業部コード"].ToString().Trim();
                    data.SectionName = row["営業部名"].ToString().Trim();
                    data.BranchCode = row["拠点コード"].ToString().Trim();
                    data.BranchName = row["拠点名"].ToString().Trim();
                    data.SalesmanCode = row["営業担当者コード"].ToString().Trim();
                    data.SalesmanName = row["営業担当者名"].ToString().Trim();
                    data.EndFlag = ("0" == row["終了フラグ"].ToString()) ? false : true;
                    result.Add(data);
                }
            }
            return result;
        }

		/// <summary>
		/// ナルコーム製品受注情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ナルコーム製品受注情報</returns>
		public static List<NarcohmOrderInfo> ConvertNarcohmOrderInfo(DataTable table)
		{
			List<NarcohmOrderInfo> result = null;
			if (null != table)
			{
				result = new List<NarcohmOrderInfo>();
				foreach (DataRow row in table.Rows)
				{
					NarcohmOrderInfo data = new NarcohmOrderInfo();
					data.OrderNo = DataBaseValue.ConvObjectToInt(row["受注番号"]);
					data.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["受注日"]);
					data.Subject = row["件名"].ToString().Trim();
					data.GoodsCode = row["商品コード"].ToString().Trim();
					data.GoodsName = row["商品名"].ToString().Trim();
					data.Price = DataBaseValue.ConvObjectToInt(row["標準価格"]);
					data.Count = DataBaseValue.ConvObjectToInt(row["数量"]);
					data.Total = DataBaseValue.ConvObjectToInt(row["金額"]);
					result.Add(data);
				}
			}
			return result;
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ナルコーム製品申込ヘッダ情報の詰め替え
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_HEADER]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ナルコーム製品申込情報リスト</returns>
		public static List<NarcohmApplicate> ConvertNarcohmApplicateHeader(DataTable table)
        {
            List<NarcohmApplicate> result = null;
            if (null != table)
            {
                result = new List<NarcohmApplicate>();
                foreach (DataRow row in table.Rows)
                {
                    NarcohmApplicate data = new NarcohmApplicate();
                    data.ApplicateID = DataBaseValue.ConvObjectToInt(row["ApplicateID"]);
                    data.CustomerNo = DataBaseValue.ConvObjectToInt(row["CustomerNo"]);
                    data.TokuisakiNo = row["TokuisakiNo"].ToString().Trim();
                    data.ClinicName = row["ClinicName"].ToString().Trim();
                    data.Telephone = row["Telephone"].ToString().Trim();
                    data.Subject = row["Subject"].ToString().Trim();
					data.SectionCode = row["SectionCode"].ToString().Trim();
					data.SectionName = row["SectionName"].ToString().Trim();
                    data.BranchCode = row["BranchCode"].ToString().Trim();
                    data.BranchName = row["BranchName"].ToString().Trim();
                    data.SalesmanCode = row["SalesmanCode"].ToString().Trim();
                    data.SalesmanName = row["SalesmanName"].ToString().Trim();
					data.ServiceStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["ServiceStartDate"]);
					data.SaleType = (MwsDefine.ApplyType)DataBaseValue.ConvObjectToInt(row["SaleType"]);
                    data.MailSendDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["MailSendDate"]);
                    data.CreateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]);
                    data.CreatePerson = row["CreatePerson"].ToString().Trim();
                    data.UpdateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UpdateDate"]);
                    data.UpdatePerson = row["UpdatePerson"].ToString().Trim();
                    result.Add(data);
				}
			}
            return result;
        }

		/// <summary>
		/// ナルコーム製品申込詳細情報の詰め替え
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ナルコーム製品申込詳細情報リスト</returns>
		public static List<NarcohmApplicateDetail> ConvertNarcohmApplicateDetail(DataTable table)
		{
			List<NarcohmApplicateDetail> result = null;
			if (null != table)
			{
				result = new List<NarcohmApplicateDetail>();
				foreach (DataRow row in table.Rows)
				{
					NarcohmApplicateDetail data = new NarcohmApplicateDetail();
					data.ApplicateDetailID = DataBaseValue.ConvObjectToInt(row["ApplicateDetailID"]);
					data.ApplicateID = DataBaseValue.ConvObjectToInt(row["ApplicateID"]);
					data.OrderNo = DataBaseValue.ConvObjectToInt(row["OrderNo"]);
					data.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["OrderDate"]);
					data.GoodsCode = row["GoodsCode"].ToString().Trim();
					data.GoodsName = row["GoodsName"].ToString().Trim();
					data.Count = DataBaseValue.ConvObjectToInt(row["Count"]);
					data.Price = DataBaseValue.ConvObjectToInt(row["Price"]);
					data.Total = DataBaseValue.ConvObjectToInt(row["Total"]);
					data.UseStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["UseStartDate"]);
					data.UseEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["UseEndDate"]);
					result.Add(data);
				}
			}
			return result;
		}
	}
}
