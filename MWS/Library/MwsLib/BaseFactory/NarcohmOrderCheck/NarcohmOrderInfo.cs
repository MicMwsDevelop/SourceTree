using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace MwsLib.BaseFactory.NarcohmOrderCheck
{
	/// <summary>
	/// ナルコーム製品受注情報
	/// </summary>
	[Serializable]
	public class NarcohmOrderInfo : IEquatable<NarcohmOrderInfo>
	{
        /// <summary>
        /// 受注番号
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 受注日
        /// </summary>
        public Date? OrderDate { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		//public int CustomerNo { get; set; }

		/// <summary>
		/// 医院名
		/// </summary>
		//public string ClinicName { get; set; }

		/// <summary>
		/// 受注金額
		/// </summary>
		//public int OrderPrice { get; set; }

		/// <summary>
		/// 件名
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// 担当者コード
		/// </summary>
		//public string SalesmanCode { get; set; }

		/// <summary>
		/// 担当者名
		/// </summary>
		//public string SalesmanName { get; set; }

		/// <summary>
		/// 担当支店名
		/// </summary>
		//public string BranchName { get; set; }

		/// <summary>
		/// 表示順
		/// </summary>
		//public string SeqNo { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsCode { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }

		/// <summary>
		/// 標準価格
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int Count { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public NarcohmOrderInfo()
        {
			OrderNo = 0;
			OrderDate = null;
			Subject = string.Empty;
			GoodsCode = string.Empty;
			GoodsName = string.Empty;
			Price = 0;
			Count = 0;
			Total = 0;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するNarcohmOrderInfo</param>
		/// <returns>判定</returns>
		public bool Equals(NarcohmOrderInfo other)
		{
			if (null != other)
			{
				if (OrderNo != other.OrderNo)
					return false;
				if (OrderDate != other.OrderDate)
					return false;
				if (Subject != other.Subject)
					return false;
				if (GoodsCode != other.GoodsCode)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (Price != other.Price)
					return false;
				if (Count != other.Count)
					return false;
				if (Total != other.Total)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するNarcohmOrderInfoオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is NarcohmOrderInfo)
			{
				return this.Equals((NarcohmOrderInfo)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return OrderNo.ToString() + Subject + GoodsCode + GoodsName + Price.ToString() + Count.ToString() + Total.ToString();
		}

		/// <summary>
		/// リストビュー表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[10];
			array[0] = OrderNo.ToString();
			array[1] = (OrderDate.HasValue) ? OrderDate.ToString() : "";
			array[2] = Subject;
			array[3] = GoodsCode;
			array[4] = GoodsName;
			array[5] = "\\" + StringUtil.CommaEdit(Price);
			array[6] = Count.ToString();
			array[7] = "\\" + StringUtil.CommaEdit(Total);
			array[8] = string.Empty;
			array[9] = string.Empty;
			return array;
		}
	}
}
