using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.MakePurchaseFile
{
	public class ナルコーム仕入商品情報 : ICloneable, IEquatable<ナルコーム仕入商品情報>
	{
		public string 商品コード { get; set; }
		public string 仕入商品コード { get; set; }
		public int 仕入価格 { get; set; }
		public string 仕入先 { get; set; }
		public string 商品名 { get; set; }
		public short 仕入フラグ { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return @"INSERT INTO TMP_ナルコーム商品 (商品コード, 仕入商品コード, 仕入価格, 仕入先, 商品名, 仕入フラグ) VALUES (@1, @2, @3, @4, @5, @6)";
			}
		}

		/// <summary>
		/// CREATE TABLE SQL文字列の取得
		/// </summary>
		public static string CreateTableString
		{
			get
			{
				return "CREATE TABLE [dbo].[TMP_ナルコーム商品] ([商品コード][nvarchar](50) NULL, [仕入商品コード] [nvarchar](50) NULL, [仕入価格] [int] NULL, [仕入先] [nvarchar](50) NULL, [商品名] [nvarchar](255) NULL, [仕入フラグ] [smallint] NULL) ON [PRIMARY]";
			}
		}

		/// <summary>
		/// DROP TABLE SQL文字列の取得
		/// </summary>
		public static string DropTableString
		{
			get
			{
				return "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TMP_ナルコーム商品]') AND type in (N'U')) DROP TABLE [dbo].[TMP_ナルコーム商品]";
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ナルコーム仕入商品情報()
		{
			商品コード = string.Empty;
			仕入商品コード = string.Empty;
			仕入価格 = 0;
			仕入先 = string.Empty;
			商品名 = string.Empty;
			仕入フラグ = 0;
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
		public bool Equals(ナルコーム仕入商品情報 other)
		{
			if (other != null)
			{
				if (商品コード == other.商品コード
					&& 仕入商品コード == other.仕入商品コード
					&& 仕入価格 == other.仕入価格
					&& 仕入先 == other.仕入先
					&& 商品名 == other.商品名
					&& 仕入フラグ == other.仕入フラグ)
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
			if (obj is ナルコーム仕入商品情報)
			{
				return this.Equals((ナルコーム仕入商品情報)obj);
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
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@1", 商品コード));
			param.Add(new SqlParameter("@2", 仕入商品コード));
			param.Add(new SqlParameter("@3", 仕入価格.ToString()));
			param.Add(new SqlParameter("@4", 仕入先));
			param.Add(new SqlParameter("@5", 商品名));
			param.Add(new SqlParameter("@6", 仕入フラグ.ToString()));
			return param.ToArray();
		}
	}
}
