//
// ScanImageManagerSettingsIF.cs
// 
// 文書インデックス管理 環境設定インターフェイス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
//
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ScanImageManager.Settings
{
	/// <summary>
	/// MIC WEB SERVICE 課金シミュレーション環境設定インターフェイス
	/// </summary>
	public static class ScanImageManagerSettingsIF
	{
		/// <summary>
		/// 環境設定ファイル名称
		/// </summary>
		public const string SETTINGS_FILE_NAME = "ScanImageManagerSettings.xml";

		/// <summary>
		/// 環境設定
		/// </summary>
		private static ScanImageManagerSettings Settings = null;

		/// <summary>
		/// 環境設定ファイル名
		/// </summary>
		private static string SettingsFileName = string.Empty;

		/// <summary>
		/// 環境設定ファイル名の設定
		/// </summary>
		private static void SetSettingsFileName()
		{
			if (string.IsNullOrEmpty(SettingsFileName))
			{
				SettingsFileName = Path.Combine(Directory.GetCurrentDirectory(), SETTINGS_FILE_NAME);
			}
		}

		/// <summary>
		/// 環境設定の読み込み(XMLファイルから取得)
		/// </summary>
		/// <returns>true:成功 false:失敗</returns>
		private static bool LoadPcSupportManagerSettings(bool reload = false)
		{
			bool result = true;

			if (reload || null == Settings)
			{
				// メモリ上の設定が保存されていない時、リロード要求時に取得
				if (true == File.Exists(SettingsFileName))
				{
					FileStream fileStream = null;
					try
					{
						fileStream = new FileStream(SettingsFileName, FileMode.Open);
						XmlSerializer serializer = new XmlSerializer(typeof(ScanImageManagerSettings));
						Settings = serializer.Deserialize(fileStream) as ScanImageManagerSettings;
					}
					catch (Exception)
					{
						result = false;
					}
					finally
					{
						if (fileStream != null)
						{
							fileStream.Close();
						}
					}
				}
				else
				{
					// 存在しない場合は初期値を設定
					Settings = new ScanImageManagerSettings();
				}
			}
			return result;
		}

		/// <summary>
		/// 環境設定の保存(XMLファイルに出力)
		/// </summary>
		/// <returns>true:成功 false:失敗</returns>
		private static bool SaveScanImageDataSettings()
		{
			bool result = true;

			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(SettingsFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				StreamWriter stream = new StreamWriter(fileStream, Encoding.UTF8);   // Unicodeで書き込む
				XmlSerializer serializer = new XmlSerializer(typeof(ScanImageManagerSettings));
				serializer.Serialize(stream, Settings);
			}
			catch (Exception)
			{
				result = false;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		/// <summary>
		/// 環境設定の取得
		/// </summary>
		/// <param name="reload">環境設定を再読みするかどうか（デフォルト：false）</param>
		/// <returns>環境設定</returns>
		/// <exception cref="ApplicationException">環境設定の読み込みが出来なかった場合に発生</exception>
		public static ScanImageManagerSettings GetScanImageDataSettings(bool reload = false)
		{
			SetSettingsFileName();

			LoadPcSupportManagerSettings(reload);

			if (null == Settings)
			{
				throw new ApplicationException("環境設定の取得に失敗");
			}
			return Settings.Clone() as ScanImageManagerSettings;
		}

		/// <summary>
		/// 環境設定の設定
		/// </summary>
		/// <param name="settings">環境設定</param>
		public static void SetScanImageDataSettings(ScanImageManagerSettings settings)
		{
			SetSettingsFileName();

			Settings = (ScanImageManagerSettings)settings.Clone();

			SaveScanImageDataSettings();
		}
	}
}
