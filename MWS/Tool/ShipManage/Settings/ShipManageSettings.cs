﻿using System;

namespace ShipManage.Settings
{
	public class ShipManageSettings : ICloneable, IEquatable<ShipManageSettings>
	{
		/// <summary>
		/// 発送用データファイル 納品書用データファイル格納フォルダ
		/// </summary>
		public string HassouDir { get; set; }
   
		/// <summary>
		/// PCA汎用データファイル格納フォルダ
		/// </summary>
		public string HanDir { get; set; }

		/// <summary>
		/// 発送用データファイル名
		/// </summary>
		public string HassouFile { get; set; }

		/// <summary>
		/// 納品書用データファイル名
		/// </summary>
		public string NouhinFile { get; set; }

		/// <summary>
		/// PCA会計用汎用売上明細データファイル名
		/// </summary>
		public string HsykdFile { get; set; }
	  
		/// <summary>
		/// PCA会計用汎用出荷明細データファイル名
		/// </summary>
		public string SuridFile { get; set; }

		/// <summary>
		/// PCA会計用汎用仕入明細データファイル名
		/// </summary>
		public string SnykdFile { get; set; }

		/// <summary>
		/// 消費税率(8%)
		/// </summary>
		public int RateTax { get; set; }
	  
		/// <summary>
		/// PCAリンクサーバフラグ（PCAが リンクサーバ:1 / 同じサーバ:0）
		/// </summary>
		public bool LinkServer { get; set; }

		/// <summary>
		/// PCA更新時、PCA接続フラグ(PCAに直接接続する: 1 / PCAに直接接続しない:0)
		/// </summary>
		public bool PcaConnect { get; set; }
		
		/// <summary>
		/// 離島代引き発送用データファイル名
		/// </summary>
		public string RitoHassouFile { get; set; }
		
		/// <summary>
		/// 代引き手数料の商品コード
		/// </summary>
		public string DaibikiTesuryoCode { get; set; }

		/// <summary>
		/// 出荷依頼用アーカイブファイル
		/// </summary>
		public string ArchiveFile { get; set; }
		
		/// <summary>
		/// 着荷日指定記事の商品コード
		/// </summary>
		public string ChakubiShiteiCode { get; set; }
		
		/// <summary>
		/// 航空便指定送料の商品コード
		/// </summary>
		public string AirCargoShippingCode { get; set; }
		
		/// <summary>
		/// 在庫引当表エクセルファイル
		/// </summary>
		public string ZaikoListFile { get; set; }
		
		/// <summary>
		/// PCA読込用データファイル格納フォルダ
		/// </summary>
		public string PCAReadDataDir { get; set; }
		   
		/// <summary>
		/// 出荷代行依頼データ保存フォルダ
		/// </summary>
		public string ShukkaDataHozonDir { get; set; }
			  
		/// <summary>
		/// 出荷依頼メール 差出人
		/// </summary>
		public string MailFromAddress { get; set; }
				 
		/// <summary>
		/// 出荷依頼メール 宛先
		/// </summary>
		public string MailToAddress { get; set; }
					
		/// <summary>
		/// 出荷依頼メール ＣＣ
		/// </summary>
		public string MailCcAddress { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ShipManageSettings()
		{
			HassouDir = string.Empty;
			HanDir = string.Empty;
			HassouFile = string.Empty;
			NouhinFile = string.Empty;
			HsykdFile = string.Empty;
			SuridFile = string.Empty;
			SnykdFile = string.Empty;
			RateTax = 0;
			LinkServer = false;
			PcaConnect = false;
			RitoHassouFile = string.Empty;
			DaibikiTesuryoCode = string.Empty;
			ArchiveFile = string.Empty;
			ChakubiShiteiCode = string.Empty;
			AirCargoShippingCode = string.Empty;
			ZaikoListFile = string.Empty;
			PCAReadDataDir = string.Empty;
			ShukkaDataHozonDir = string.Empty;
			MailFromAddress = string.Empty;
			MailToAddress = string.Empty;
			MailCcAddress = string.Empty;
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
		/// <param name="other">比較するShipManageSettings</param>
		/// <returns>判定</returns>
		public bool Equals(ShipManageSettings other)
		{
			if (other != null)
			{
				if (HassouDir == other.HassouDir
					&& HanDir == other.HanDir
					&& HassouFile == other.HassouFile
					&& NouhinFile == other.NouhinFile
					&& HsykdFile == other.HsykdFile
					&& SuridFile == other.SuridFile
					&& SnykdFile == other.SnykdFile
					&& RateTax == other.RateTax
					&& LinkServer == other.LinkServer
					&& PcaConnect == other.PcaConnect
					&& RitoHassouFile == other.RitoHassouFile
					&& DaibikiTesuryoCode == other.DaibikiTesuryoCode
					&& ArchiveFile == other.ArchiveFile
					&& ChakubiShiteiCode == other.ChakubiShiteiCode
					&& AirCargoShippingCode == other.AirCargoShippingCode
					&& ZaikoListFile == other.ZaikoListFile
					&& PCAReadDataDir == other.PCAReadDataDir
					&& ShukkaDataHozonDir == other.ShukkaDataHozonDir
					&& MailFromAddress == other.MailFromAddress
					&& MailToAddress == other.MailToAddress
					&& MailCcAddress == other.MailCcAddress)
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
		/// <param name="obj">比較するShipManageSettingsブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is ShipManageSettings)
			{
				return this.Equals((ShipManageSettings)obj);
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
}