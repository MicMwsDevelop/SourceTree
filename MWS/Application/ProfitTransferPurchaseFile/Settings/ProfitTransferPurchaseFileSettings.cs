//
// ProfitTransferPurchaseFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/09/22 越田)
//
using CommonLib.BaseFactory.MakePurchaseFile;
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProfitTransferPurchaseFile.Settings
{
	/// <summary>
	/// 環境設定定義クラス
	/// </summary>
	public class ProfitTransferPurchaseFileSettings : ICloneable, IEquatable<ProfitTransferPurchaseFileSettings>
	{

		/// <summary>
		/// 環境設定 - 摘要情報クラス(PCA部門に対応したPCA摘要情報保持)
		/// </summary>
		public class 摘要情報 : ICloneable, IEquatable<摘要情報>
		{
			/// <summary>
			/// 委託元か？(委託元(true)／委託先(false))
			/// </summary>
			public bool 委託元 { get; set; }

			/// <summary>
			/// 部門コード
			/// </summary>
			public string 部門コード { get; set; }

			/// <summary>
			/// 摘要コード
			/// </summary>
			public string 摘要コード { get; set; }

			/// <summary>
			/// 摘要名
			/// </summary>
			public string 摘要名 { get; set; }


			/// <summary>
			/// デフォルトコンストラクタ
			/// </summary>
			public 摘要情報()
			{
				委託元 = true;
				部門コード = string.Empty;
				摘要コード = string.Empty;
				摘要名 = string.Empty;
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
			public bool Equals(摘要情報 other)
			{
				if (other != null)
				{
					if (委託元 == other.委託元
						&& 部門コード == other.部門コード
						&& 摘要コード == other.摘要コード
						&& 摘要名 == other.摘要名
						)
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
				if (obj is 摘要情報)
				{
					return this.Equals((摘要情報)obj);
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
		}


		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string 出力先フォルダ { get; set; }

		/// <summary>
		/// PCAバージョン
		/// </summary>
		public short PcaVersion { get; set; }

		/// <summary>
		/// 部署間利益付け替え仕入データファイル名
		/// </summary>
		public string 部署間利益付け替え仕入データファイル名 { get; set; }


		/// <summary>
		/// 部署間利益付け替え開始伝票番号：440001
		/// </summary>
		public int 部署間利益付け替え開始伝票番号 { get; set; }


		/// <summary>
		/// 仕入先コード
		/// </summary>
		public string 仕入先コード { get; set; }

		/// <summary>
		/// 仕入先名
		/// </summary>
		public string 仕入先名 { get; set; }

		/// <summary>
		/// 委託元_PRO営業部_部門コード
		/// </summary>
		public string 委託元_PRO営業部_部門コード { get; set; }

		/// <summary>
		/// 委託元_PRO営業部_担当者コード
		/// </summary>
		public string 委託元_PRO営業部_担当者コード { get; set; }

		/// <summary>
		/// 委託元_SOL営業部_部門コード
		/// </summary>
		public string 委託元_SOL営業部_部門コード { get; set; }

		/// <summary>
		/// 委託元_SOL営業部_担当者コード
		/// </summary>
		public string 委託元_SOL営業部_担当者コード { get; set; }


		/// TODO: 文字列リストの形式をXMLファイルからDeserializeする際に属性指定しなくてもよいシンプルな記載方法又はxmlファイルの構成を要検討 by KOSHITA
		/// <summary>
		/// PRO営業部_CS事業部利益付け替え商品コード
		/// </summary>
		[XmlArray("PRO営業部_CS事業部利益付け替え商品コード")]
		[XmlArrayItem("商品コード")]
		public List<string> PRO営業部_CS事業部利益付け替え商品コード { get; set; }

		/// TODO: 文字列リストの形式をXMLファイルからDeserializeする際に属性指定しなくてもよいシンプルな記載方法又はxmlファイルの構成を要検討 by KOSHITA
		/// <summary>
		/// SOL営業部_CS事業部利益付け替え商品コード
		/// </summary>
		[XmlArray("SOL営業部_CS事業部利益付け替え商品コード")]
		[XmlArrayItem("商品コード")]
		public List<string> SOL営業部_CS事業部利益付け替え商品コード { get; set; }


		/// <summary>
		/// 摘要情報群
		/// </summary>
		public List<摘要情報> 摘要情報群 { get; set; }


		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// 部署間利益付け替え仕入データパス名
		/// </summary>
		public string 部署間利益付け替え仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, 部署間利益付け替え仕入データファイル名);
			}
		}


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ProfitTransferPurchaseFileSettings()
		{
			出力先フォルダ = string.Empty;
			PcaVersion = 0;
			部署間利益付け替え仕入データファイル名 = string.Empty;
			部署間利益付け替え開始伝票番号 = 0;
			仕入先コード = string.Empty;
			仕入先名 = string.Empty;
			委託元_PRO営業部_部門コード = string.Empty;
			委託元_PRO営業部_担当者コード = string.Empty;
			委託元_SOL営業部_部門コード = string.Empty;
			委託元_SOL営業部_担当者コード = string.Empty;
			PRO営業部_CS事業部利益付け替え商品コード = new List<string>();
			SOL営業部_CS事業部利益付け替え商品コード = new List<string>();
			摘要情報群 = new List<摘要情報>();
			Connect = new SqlServerConnectSettings();
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
		public bool Equals(ProfitTransferPurchaseFileSettings other)
		{
			if (other != null)
			{
				if (出力先フォルダ == other.出力先フォルダ
					&& PcaVersion == other.PcaVersion
					&& 部署間利益付け替え仕入データファイル名 == other.部署間利益付け替え仕入データファイル名
					&& 部署間利益付け替え開始伝票番号 == other.部署間利益付け替え開始伝票番号
					&& 仕入先コード == other.仕入先コード
					&& 仕入先名 == other.仕入先名
					&& 委託元_PRO営業部_部門コード == other.委託元_PRO営業部_部門コード
					&& 委託元_PRO営業部_担当者コード == other.委託元_PRO営業部_担当者コード
					&& 委託元_SOL営業部_部門コード == other.委託元_SOL営業部_部門コード
					&& 委託元_SOL営業部_担当者コード == other.委託元_SOL営業部_担当者コード
					&& PRO営業部_CS事業部利益付け替え商品コード.Equals(other.PRO営業部_CS事業部利益付け替え商品コード)
					&& SOL営業部_CS事業部利益付け替え商品コード.Equals(other.SOL営業部_CS事業部利益付け替え商品コード)
					&& 摘要情報群.Equals(other.摘要情報群)
					&& Connect.Equals(other.Connect))
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
			if (obj is ProfitTransferPurchaseFileSettings)
			{
				return this.Equals((ProfitTransferPurchaseFileSettings)obj);
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
		/// PRO営業部利益杖替え商品コード群のクエリ指定用文字列の取得
		/// </summary>
		/// <returns></returns>
		public string GetPRO営業部ListonGoods()
		{
			string str = string.Empty;
			foreach (string 商品コード in PRO営業部_CS事業部利益付け替え商品コード)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + 商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// SOL営業部利益杖替え商品コード群のクエリ指定用文字列の取得
		/// </summary>
		/// <returns></returns>
		public string GetSOL営業部ListonGoods()
		{
			string str = string.Empty;
			foreach (string 商品コード in SOL営業部_CS事業部利益付け替え商品コード)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + 商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// 指定した委託元の部門コードに対応する摘要コードの取得
		/// </summary>
		/// <param name="部門コード"></param>
		/// <returns></returns>
		public string Get委託元摘要コード(string 部門コード)
		{
			foreach (var 摘要情報 in 摘要情報群)
			{
				if (摘要情報.委託元 && 摘要情報.部門コード == 部門コード)
				{
					return 摘要情報.摘要コード;
				}
			}

			// 本来あり得ないルート
			Debug.Assert(false);
			return "0000";
		}

		/// <summary>
		/// 指定した委託元の部門コードに対応する摘要名の取得
		/// </summary>
		/// <param name="部門コード"></param>
		/// <returns></returns>
		public string Get委託元摘要名(string 部門コード)
		{
			foreach (var 摘要情報 in 摘要情報群)
			{
				if (摘要情報.委託元 && 摘要情報.部門コード == 部門コード)
				{
					return 摘要情報.摘要名;
				}
			}

			// 本来あり得ないルート
			Debug.Assert(false);
			return string.Empty;
		}

		/// <summary>
		/// 指定した委託先の部門コードに対応する摘要コードの取得
		/// </summary>
		/// <param name="部門コード"></param>
		/// <returns></returns>
		public string Get委託先摘要コード(string 部門コード)
		{
			foreach (var 摘要情報 in 摘要情報群)
			{
				if (!摘要情報.委託元 && 摘要情報.部門コード == 部門コード)
				{
					return 摘要情報.摘要コード;
				}
			}

			// 本来あり得ないルート
			Debug.Assert(false);
			return "0000";
		}

		/// <summary>
		/// 指定した委託先の部門コードに対応する摘要名の取得
		/// </summary>
		/// <param name="部門コード"></param>
		/// <returns></returns>
		public string Get委託先摘要名(string 部門コード)
		{
			foreach (var 摘要情報 in 摘要情報群)
			{
				if (!摘要情報.委託元 && 摘要情報.部門コード == 部門コード)
				{
					return 摘要情報.摘要名;
				}
			}

			// 本来あり得ないルート
			Debug.Assert(false);
			return string.Empty;
		}
	}
}