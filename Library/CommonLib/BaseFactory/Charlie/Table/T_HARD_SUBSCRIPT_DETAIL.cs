//
// T_HARD_SUBSCRIPT_DETAIL.cs
//
// ハードサブスク情報管理 機器情報クラス
// [CharlieDB].[dbo].[T_HARD_SUBSCRIPT_DETAIL]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// ハードサブスク情報管理 機器情報
	/// </summary>
	public class T_HARD_SUBSCRIPT_DETAIL : ICloneable, IEquatable<T_HARD_SUBSCRIPT_DETAIL>
	{
		/// <summary>
		/// 貸出機器番号
		/// </summary>
		public int RentalDetailNo { get; set; }

		/// <summary>
		/// 内部契約番号
		/// </summary>
		public int InternalRentalNo { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsCode { get; set; }

		/// <summary>
		/// 機器区分
		/// </summary>
		public short CategoryNo { get; set; }

		/// <summary>
		/// 機器名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public short Quantity { get; set; }

		/// <summary>
		/// シリアルNo
		/// </summary>
		public string SerialNo { get; set; }

		/// <summary>
		/// 資産コード
		/// </summary>
		public string AssetsCode { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSCRIPT_DETAIL]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_HARD_SUBSCRIPT_DETAIL()
		{
			RentalDetailNo = 0;
			InternalRentalNo = 0;
			GoodsCode = string.Empty;
			CategoryNo = 0;
			GoodsName = string.Empty;
			Quantity = 0;
			SerialNo = string.Empty;
			AssetsCode = string.Empty;
			CreateDate = null;
			CreatePerson = string.Empty;
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
		/// Deep Copy
		/// </summary>
		/// <returns>オブジェクト</returns>
		public T_HARD_SUBSCRIPT_DETAIL DeepCopy()
		{
			T_HARD_SUBSCRIPT_DETAIL ret = new T_HARD_SUBSCRIPT_DETAIL();
			ret.RentalDetailNo = this.RentalDetailNo;
			ret.InternalRentalNo = this.InternalRentalNo;
			ret.GoodsCode = this.GoodsCode;
			ret.CategoryNo = this.CategoryNo;
			ret.GoodsName = this.GoodsName;
			ret.Quantity = this.Quantity;
			ret.SerialNo = this.SerialNo;
			ret.AssetsCode = this.AssetsCode;
			ret.CreateDate = this.CreateDate;
			ret.CreatePerson = this.CreatePerson;
			return ret;
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(T_HARD_SUBSCRIPT_DETAIL other)
		{
			if (other != null)
			{
				if (RentalDetailNo == other.RentalDetailNo
					&& InternalRentalNo == other.InternalRentalNo
					&& GoodsCode == other.GoodsCode
					&& CategoryNo == other.CategoryNo
					&& GoodsName == other.GoodsName
					&& Quantity == other.Quantity
					&& SerialNo == other.SerialNo
					&& AssetsCode == other.AssetsCode
					&& CreateDate.Equals(other.CreateDate)
					&& CreatePerson == other.CreatePerson)
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
			if (obj is T_HARD_SUBSCRIPT_DETAIL)
			{
				return this.Equals((T_HARD_SUBSCRIPT_DETAIL)obj);
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
		/// [charlieDB].[dbo].[T_HARDSUBSC_DETAIL]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_HARDSUBSC_DETAIL</returns>
		public static List<T_HARD_SUBSCRIPT_DETAIL> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_HARD_SUBSCRIPT_DETAIL> result = new List<T_HARD_SUBSCRIPT_DETAIL>();
				foreach (DataRow row in table.Rows)
				{
					T_HARD_SUBSCRIPT_DETAIL data = new T_HARD_SUBSCRIPT_DETAIL
					{
						RentalDetailNo = DataBaseValue.ConvObjectToInt(row["RentalDetailNo"]),
						InternalRentalNo = DataBaseValue.ConvObjectToInt(row["InternalRentalNo"]),
						GoodsCode = row["GoodsCode"].ToString().Trim(),
						CategoryNo = DataBaseValue.ConvObjectToShort(row["CategoryNo"]),
						GoodsName = row["GoodsName"].ToString().Trim(),
						Quantity = DataBaseValue.ConvObjectToShort(row["Quantity"]),
						SerialNo = row["SerialNo"].ToString().Trim(),
						AssetsCode = row["AssetsCode"].ToString().Trim(),
						CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]),
						CreatePerson = row["CreatePerson"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 機器名の取得
		/// </summary>
		/// <returns></returns>
		public string GetCategoryName()
		{
			switch (CategoryNo)
			{
				case 0: return "その他";
				case 1: return "本体";
				case 2: return "モニタ";
				case 3: return "HDD";
				case 4: return "ルーター";
				case 5: return "HUB";
				case 6: return "UPS";
			}
			return "その他";
		}

		/// <summary>
		/// 機器情報リスト
		/// </summary>
		/// <returns></returns>
		public static List<Tuple<short, string>> GetCategoryList()
		{
			List<Tuple<short, string>> ret = new List<Tuple<short, string>>();
			ret.Add(new Tuple<short, string>(0, "その他"));
			ret.Add(new Tuple<short, string>(1, "本体"));
			ret.Add(new Tuple<short, string>(2, "モニタ"));
			ret.Add(new Tuple<short, string>(3, "HDD"));
			ret.Add(new Tuple<short, string>(4, "ルーター"));
			ret.Add(new Tuple<short, string>(5, "HUB"));
			ret.Add(new Tuple<short, string>(6, "UPS"));
			return ret;
		}

		/// <summary>
		/// リストの比較
		/// </summary>
		/// <param name="srcList">比較元</param>
		/// <param name="dstList">比較先</param>
		/// <returns>判定</returns>
		public static bool EqualList(List<T_HARD_SUBSCRIPT_DETAIL> srcList, List<T_HARD_SUBSCRIPT_DETAIL> dstList)
		{
			if (srcList.Count == dstList.Count)
			{
				for (int i = 0; i < srcList.Count; i++)
				{
					if (false == srcList[i].Equals(dstList[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters(string person)
		{
			SqlParameter[] param = {
				new SqlParameter("@1", InternalRentalNo),
				new SqlParameter("@2", GoodsCode),
				new SqlParameter("@3", CategoryNo),
				new SqlParameter("@4", GoodsName),
				new SqlParameter("@5", Quantity),
				new SqlParameter("@6", SerialNo),
				new SqlParameter("@7", AssetsCode),
				new SqlParameter("@8", DateTime.Now),
				new SqlParameter("@9", person)
			};
			return param;
		}
	}
}
