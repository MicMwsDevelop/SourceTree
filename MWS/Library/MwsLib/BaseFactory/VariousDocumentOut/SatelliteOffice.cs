//
// SatelliteOffice.cs
// 
// 拠点情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.VariousDocumentOut
{
	/// <summary>
	/// 拠点情報
	/// </summary>
	public class SatelliteOffice
	{
		/// <summary>
		/// 営業部
		/// </summary>
		public string SaleDepartment { get; set; }

		/// <summary>
		/// 拠点
		/// </summary>
		public string Branch { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string Zipcode { get; set; }

		/// <summary>
		/// 住所1
		/// </summary>
		public string Address1 { get; set; }

		/// <summary>
		/// 住所2
		/// </summary>
		public string Address2 { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string Tel { get; set; }

		/// <summary>
		/// FAX番号
		/// </summary>
		public string Fax { get; set; }

		/// <summary>
		/// 住所
		/// </summary>
		public string 住所
		{
			get
			{
				if (0 < Address2.Length)
				{
					return Address1 + Address2;
				}
				return Address1;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SatelliteOffice()
		{
			this.Empty();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Empty()
		{
			SaleDepartment = string.Empty;
			Branch = string.Empty;
			Zipcode = string.Empty;
			Address1 = string.Empty;
			Address2 = string.Empty;
			Tel = string.Empty;
			Fax = string.Empty;
		}

		/// <summary>
		/// メンバーのクローンを作成する
		/// （ICloneableの実装）
		/// </summary>
		/// <returns>クローンオブジェクト</returns>
		public Object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(SatelliteOffice other)
		{
			if (other != null)
			{
				if (SaleDepartment == other.SaleDepartment
					&& Branch == other.Branch
					&& Zipcode == other.Zipcode
					&& Address1 == other.Address1
					&& Address2 == other.Address2
					&& Tel == other.Tel
					&& Fax == other.Fax)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is SatelliteOffice)
			{
				return this.Equals((SatelliteOffice)obj);
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
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<SatelliteOffice> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<SatelliteOffice> result = new List<SatelliteOffice>();
                foreach (DataRow row in table.Rows)
                {
					SatelliteOffice data = new SatelliteOffice
					{
						SaleDepartment = row["部署名"].ToString().Trim(),
						Branch = row["拠点名"].ToString().Trim(),
						Zipcode = row["郵便番号"].ToString().Trim(),
						Address1 = row["住所1"].ToString().Trim(),
						Address2 = row["住所2"].ToString().Trim(),
						Tel = row["電話番号"].ToString().Trim(),
						Fax = row["FAX番号"].ToString().Trim(),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
	}
}
