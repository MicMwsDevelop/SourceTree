//
// HeadOfficeSettingsIF.cs
// 
// 本社情報インターフェイス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MwsLib.Settings.HeadOffice
{
	/// <summary>
	/// 本社情報インターフェイス
	/// </summary>
	public static class HeadOfficeSettingsIF
	{
		/// <summary>
		/// 本社情報ファイル名称
		/// </summary>
		public const string SETTINGS_FILENAME = "HeadOfficeSettings.xml";

		/// <summary>
		/// 本社情報
		/// </summary>
		private static HeadOfficeSettings Settings = null;

		/// <summary>
		/// 本社情報ファイル名
		/// </summary>
		private static string SettingsFileName = string.Empty;

		/// <summary>
		/// 本社情報ファイル名の設定
		/// </summary>
		private static void SetSettingsFileName()
		{
			if (string.IsNullOrEmpty(SettingsFileName))
			{
				SettingsFileName = Path.Combine(Directory.GetCurrentDirectory(), SETTINGS_FILENAME);
			}
		}

		/// <summary>
		/// 本社情報の読込
		/// </summary>
		/// <returns>判定</returns>
		private static bool LoadSettings(bool reload = false)
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
						XmlSerializer serializer = new XmlSerializer(typeof(HeadOfficeSettings));
						Settings = serializer.Deserialize(fileStream) as HeadOfficeSettings;
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
					Settings = new HeadOfficeSettings();
				}
			}
			return result;
		}

		/// <summary>
		/// 本社情報の保存(XMLファイルに出力)
		/// </summary>
		/// <returns>判定</returns>
		private static bool SaveSettings()
		{
			bool result = true;

			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(SettingsFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				StreamWriter stream = new StreamWriter(fileStream, Encoding.UTF8);   // Unicodeで書き込む
				XmlSerializer serializer = new XmlSerializer(typeof(HeadOfficeSettings));
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
		/// 本社情報の取得
		/// </summary>
		/// <param name="reload">環境設定を再読みするかどうか（デフォルト：false）</param>
		/// <returns>本社情報</returns>
		/// <exception cref="ApplicationException">環境設定の読み込みが出来なかった場合に発生</exception>
		public static HeadOfficeSettings GetSettings(bool reload = false)
		{
			SetSettingsFileName();

			LoadSettings(reload);

			if (null == Settings)
			{
				throw new ApplicationException("本社情報の取得に失敗");
			}
			return Settings.Clone() as HeadOfficeSettings;
		}

		/// <summary>
		/// 本社情報の設定
		/// </summary>
		/// <param name="settings">本社情報</param>
		public static void SetSettings(HeadOfficeSettings settings)
		{
			SetSettingsFileName();

			Settings = settings;

			SaveSettings();
		}
	}
}
