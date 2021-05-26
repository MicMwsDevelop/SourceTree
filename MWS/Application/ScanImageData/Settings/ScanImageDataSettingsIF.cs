//
// ScanImageDataSettingsIF.cs
// 
// 文書インデックス管理 環境設定インターフェイス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ScanImageData.Settings
{
	/// <summary>
	/// MIC WEB SERVICE 課金シミュレーション環境設定インターフェイス
	/// </summary>
	public static class ScanImageDataSettingsIF
	{
		/// <summary>
		/// 環境設定ファイル名称
		/// </summary>
		public const string SETTINGS_FILE_NAME = "ScanImageDataSettings.xml";

		/// <summary>
		/// 環境設定
		/// </summary>
		private static ScanImageDataSettings Settings = null;

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
						XmlSerializer serializer = new XmlSerializer(typeof(ScanImageDataSettings));
						Settings = serializer.Deserialize(fileStream) as ScanImageDataSettings;
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
					Settings = new ScanImageDataSettings();
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
				XmlSerializer serializer = new XmlSerializer(typeof(ScanImageDataSettings));
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
		public static ScanImageDataSettings GetScanImageDataSettings(bool reload = false)
		{
			SetSettingsFileName();

			LoadPcSupportManagerSettings(reload);

			if (null == Settings)
			{
				throw new ApplicationException("環境設定の取得に失敗");
			}
			return Settings.Clone() as ScanImageDataSettings;
		}

		/// <summary>
		/// 環境設定の設定
		/// </summary>
		/// <param name="settings">環境設定</param>
		public static void SetScanImageDataSettings(ScanImageDataSettings settings)
		{
			SetSettingsFileName();

			Settings = (ScanImageDataSettings)settings.Clone();

			SaveScanImageDataSettings();
		}
	}
}
