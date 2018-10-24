//
// MwsSimulationSettings.cs
// 
// MIC WEB SERVICE 課金シミュレーション環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;

namespace MwsSimulation.Settings
{
	/// <summary>
	/// MIC WEB SERVICE 課金シミュレーション環境設定
	/// </summary>
	public class MwsSimulationSettings : ICloneable, IEquatable<MwsSimulationSettings>
	{
		/// <summary>
		/// 画面サイズ（おまとめプラン）
		/// </summary>
		public Size SimulationMatomeFormSize;

		/// <summary>
		/// 画面サイズ（月額課金）
		/// </summary>
		public Size SimulationMonthlyFormSize;

		/// <summary>
		/// 印刷オフセット
		/// </summary>
		public Point PaperOffset;

		/// <summary>
		/// 拠点情報リスト
		/// </summary>
		public BranchSettingsList BranchList;

		/// <summary>
		/// 担当者リスト
		/// </summary>
		public StringCollection StaffList;

		/// <summary>
		/// カレント拠点インデックス
		/// </summary>
		public int CurrentBranchIndex;

		/// <summary>
		/// カレント担当者インデックス
		/// </summary>
		public int CurrentStaffIndex;

		/// <summary>
		/// 備考リスト
		/// </summary>
		public StringCollection RemarkList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MwsSimulationSettings()
        {
			SimulationMatomeFormSize = new Size(1145, 803);
			SimulationMonthlyFormSize = new Size(1145, 803);
			PaperOffset = new Point(0, 0);
			BranchList = new BranchSettingsList();
			StaffList = new StringCollection();
			CurrentBranchIndex = 0;
			CurrentStaffIndex = 0;
			RemarkList = new StringCollection();
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
		/// <param name="other">比較するMwsSimulationSettings</param>
		/// <returns>判定</returns>
		public bool Equals(MwsSimulationSettings other)
		{
			if (other != null)
			{
				if (SimulationMatomeFormSize == other.SimulationMatomeFormSize
					&& SimulationMonthlyFormSize == other.SimulationMonthlyFormSize
					&& PaperOffset == other.PaperOffset
					&& BranchList.Equals(other.BranchList)
					&& StaffList.Equals(other.StaffList)
					&& CurrentBranchIndex == other.CurrentBranchIndex
					&& CurrentStaffIndex == other.CurrentStaffIndex
					&& RemarkList.Equals(other.RemarkList))
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
		/// <param name="obj">比較するMwsSimulationSettingsオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is MwsSimulationSettings)
			{
				return this.Equals((MwsSimulationSettings)obj);
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
	/// 拠点情報
	/// </summary>
	public class BranchSettings : ICloneable, IEquatable<BranchSettings>
	{
		/// <summary>
		/// 拠点名
		/// </summary>
		public string Name;

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string Zipcode;

		/// <summary>
		/// 住所１
		/// </summary>
		public string Address1;

		/// <summary>
		/// 住所２
		/// </summary>
		public string Address2;

		/// <summary>
		/// 電話番号
		/// </summary>
		public string Tel;

		/// <summary>
		/// FAX番号
		/// </summary>
		public string Fax;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BranchSettings()
		{
			Name = string.Empty;
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
		/// Deep Copy
		/// </summary>
		/// <returns>チェック項目グループ</returns>
		public BranchSettings DeepCopy()
		{
			BranchSettings branch = new BranchSettings();
			branch.Name = this.Name;
			branch.Zipcode = this.Zipcode;
			branch.Address1 = this.Address1;
			branch.Address2 = this.Address2;
			branch.Tel = this.Tel;
			branch.Fax = this.Fax;
			return branch;
		}

		/// <summary>
		/// このインスタンスと、指定したチェック項目グループクラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するBranchSettings</param>
		/// <returns>チェック項目グループが同じ場合はtrue、それ以外の場合はfalseを返す</returns>
		public bool Equals(BranchSettings other)
		{
			if (other != null)
			{
				if (Name != other.Name)
					return false;
				if (Zipcode != other.Zipcode)
					return false;
				if (Address1 != other.Address1)
					return false;
				if (Address2 != other.Address2)
					return false;
				if (Tel != other.Tel)
					return false;
				if (Fax != other.Fax)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するBranchSettingsオブジェクト</param>
		/// <returns>チェック項目グループが同じ場合はtrue、それ以外の場合はfalseを返す</returns>
		public override bool Equals(object obj)
		{
			if (obj is BranchSettings)
			{
				return this.Equals((BranchSettings)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// チェック項目グループのハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 文字列の取得
		/// </summary>
		/// <returns>文字列</returns>
		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// 表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[6];
			array[0] = Name;
			array[1] = Zipcode;
			array[2] = Address1;
			array[3] = Address2;
			array[4] = Tel;
			array[5] = Fax;
			return array;
		}
	}

	/// <summary>
	/// 拠点情報リスト
	/// </summary>
	public class BranchSettingsList : List<BranchSettings>
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BranchSettingsList()
		{
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
		/// <returns>チェック項目グループリスト</returns>
		public BranchSettingsList DeepCopy()
		{
			BranchSettingsList list = new BranchSettingsList();
			foreach (BranchSettings group in this)
			{
				list.Add(group.DeepCopy());
			}
			return list;
		}

		/// <summary>
		/// このインスタンスと、指定したクラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するBranchSettingsList</param>
		/// <returns>判定</returns>
		public bool Equals(BranchSettingsList other)
		{
			if (null != other)
			{
				if (this.SequenceEqual(other))
					return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するBranchSettingsListオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is BranchSettingsList)
			{
				return this.Equals((BranchSettingsList)obj);
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
