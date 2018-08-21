//
// VersionInfoForm.cs
//
// バージョン情報画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// 
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	public partial class VersionInfoForm : Form
	{
		public VersionInfoForm()
		{
			InitializeComponent();
		}

		private void VersionInfoForm_Load(object sender, EventArgs e)
		{
			//自分自身のバージョン情報を取得する
			System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
			textBoxProgramVer.Text = ver.FileVersion;
			textBoxDataVer.Text = string.Format("{0} ({1})", MainForm.gVersionInfo.Item1, MainForm.gVersionInfo.Item2.ToString());
		}
	}
}
