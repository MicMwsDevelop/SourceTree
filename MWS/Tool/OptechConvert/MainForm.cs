//
// MainForm.cs
//
// オプテックカルテコンバータ メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using OptechConvert.XML;
using OptechConvert.XML.Link;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OptechConvert
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// オプテックXMLファイルリスト
		/// </summary>
		public List<OptechXML> OptechList { get; set; }

		/// <summary>
		/// オプテック識別子→MIC項目コード定義
		/// </summary>
		public LinkMicItem LinkInfo { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			OptechList = new List<OptechXML>();
			LinkInfo = new LinkMicItem();

#if DEBUG
			textBoxClinicCode.Text = "1234567";
			textBoxOptechFolder.Text = @"D:\_WonderWeb\●MBO\46期\下期\オプテックコンバータ\サンプル出力";
#endif
		}

		/// <summary>
		/// LinkMicItem.xmlの読込
		/// </summary>
		/// <param name="path">読込先フォルダ名</param>
		public void ReadXmlFile(string path)
		{
			try
			{
				using (FileStream fs = new FileStream(Path.Combine(path, OptechDef.XML_LINKINFO_FILENAME), FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(LinkMicItem));
					LinkInfo = (LinkMicItem)s.Deserialize(fs);
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// XMLコンバートファイルの読込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadOptech_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog form = new FolderBrowserDialog())
			{
				form.Description = "フォルダを指定してください。";
				form.RootFolder = Environment.SpecialFolder.Desktop;
				form.SelectedPath = textBoxOptechFolder.Text;
				form.ShowNewFolderButton = true;
				if (DialogResult.OK == form.ShowDialog(this))
				{
					textBoxOptechFolder.Text = form.SelectedPath;
					listBoxPatient.Items.Clear();

					try
					{
						// オプテック識別子→MIC項目コード定義（LinkMicItem.xml）を読み込む
						this.ReadXmlFile(Directory.GetCurrentDirectory());

						// 患者毎のオプテックXMLファイルを読み込む
						string[] folders = Directory.GetDirectories(textBoxOptechFolder.Text, "*", SearchOption.TopDirectoryOnly);
						foreach (string folder in folders)
						{
							OptechXML xml = new OptechXML();
							xml.ReadXmlFile(folder);
							OptechList.Add(xml);

							listBoxPatient.Items.Add(Path.GetFileName(folder));
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "XMLファイル読込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						return;
					}
					MessageBox.Show("読込終了", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		/// <summary>
		/// インポートファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonImportFile_Click(object sender, EventArgs e)
		{
			if (0 == textBoxClinicCode.Text.Length)
			{
				MessageBox.Show("医療機関コードが設定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == listBoxPatient.Items.Count)
			{
				MessageBox.Show("オプテックコンバートファイルを先に読み込んでください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// インポートファイルの作成
			List<string> patientList = new List<string>();
			List<string> hokenInfList = new List<string>();
			List<string> shoshinInfList = new List<string>();
			List<string> rezeptInfoList = new List<string>();
			List<string> rezeptByomeiranList = new List<string>();
			List<string> dayList = new List<string>();
			List<string> buiList = new List<string>();
			List<string> scList = new List<string>();
			for (int i = 0; i < listBoxPatient.Items.Count; i++)
			{
				listBoxPatient.SelectedIndex = i;

				string patient;
				List<string> workHokenInf, workShoshinInf, workRezeptInf, workRezeptByomeiran, workDay, workBui, workSc;
				OptechList[i].MakeConvertList(checkBoxExceptRezeptCheck.Checked, LinkInfo, textBoxClinicCode.Text, i + 1, out patient, out workHokenInf, out workShoshinInf, out workRezeptInf, out workRezeptByomeiran, out workDay, out workBui, out workSc);
				patientList.Add(patient);
				hokenInfList.AddRange(workHokenInf);
				shoshinInfList.AddRange(workShoshinInf);
				rezeptInfoList.AddRange(workRezeptInf);
				rezeptByomeiranList.AddRange(workRezeptByomeiran);
				dayList.AddRange(workDay);
				buiList.AddRange(workBui);
				scList.AddRange(workSc);
			}
			try
			{
				// インポートファイル出力
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.USER_PATIENT_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in patientList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.USER_HOKNEINF_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in hokenInfList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.CONVERT_SHOSHIN_INF_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in shoshinInfList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.CONVERT_REZEPT_INF_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in rezeptInfoList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.CONVERT_REZEPT_BYOMEIRAN_INF_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in rezeptByomeiranList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.CONVERT_DAYLIST_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in dayList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.CONVERT_BUILIST_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in buiList)
					{
						sw.WriteLine(str);
					}
				}
				using (var sw = new StreamWriter(Path.Combine(textBoxOptechFolder.Text, OptechDef.CONVERT_SCLIST_FILENAME), false, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string str in scList)
					{
						sw.WriteLine(str);
					}
				}
				MessageBox.Show(string.Format("{0} に出力しました。", textBoxOptechFolder.Text), "終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}
	}
}
