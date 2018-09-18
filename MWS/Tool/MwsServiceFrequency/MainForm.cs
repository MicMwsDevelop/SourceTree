using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.MwsServiceFrequency;
using ClosedXML.Excel;
using MwsLib.DB.SQLite.MwsServiceFrequency;
using System.IO;
using MwsLib.Common;

namespace MwsServiceFrequency
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.FileName = "";
				dlg.InitialDirectory = Environment.GetEnvironmentVariable("SystemDrive");
				dlg.Filter = "paletteサービス利用頻度ファイル(*.xlsx)*.xlsx||すべてのファイル(*.*)|*.*";
				dlg.Title = "paletteサービス利用頻度ファイルを選択してください";
				dlg.RestoreDirectory = true;
				if (DialogResult.OK == dlg.ShowDialog())
				{
					using (var book = new XLWorkbook(dlg.FileName, XLEventTracking.Disabled))
					{
						var sheet = book.Worksheet("palette利用頻度");

						// テーブル作成
						var table = sheet.RangeUsed().AsTable();

						// データを行単位で取得
						string[] buf = new string[table.DataRange.ColumnCount()];
						MwsServiceFrequencyDataList addList = new MwsServiceFrequencyDataList();
						MwsServiceFrequencyDataList list = new MwsServiceFrequencyDataList();
						int columnCount = table.DataRange.ColumnCount();
						YearMonth? usedMonth = null;
						foreach (var dataRow in table.DataRange.Rows())
						{
							if (null != dataRow.Cell(16).Value)
							{
								if ("" != Convert.ToString(dataRow.Cell(16).Value))
								{
									// 使用回数が１回以上
									if (false == usedMonth.HasValue)
									{
										YearMonth work;
										if (YearMonth.TryParse(Convert.ToString(dataRow.Cell(22).Value), out work))
										{
											// 利用年月
											usedMonth = work;
											SQLiteMwsServiceFrequencyAccess.DeleteAllMwsServiceFrequencyData(Directory.GetCurrentDirectory(), usedMonth.Value);
										}
									}
									for (int i = 0; i < columnCount; i++)
									{
										if (null != dataRow.Cell(i + 1).Value)
										{
											buf[i] = Convert.ToString(dataRow.Cell(i + 1).Value);
										}
										else
										{
											buf[i] = string.Empty;
										}
									}
									list.Add(new MwsServiceFrequencyData(buf));
									if (10000 <= list.Count)
									{
										SQLiteMwsServiceFrequencyAccess.SetMwsServiceFrequencyDataList(Directory.GetCurrentDirectory(), list);
										addList.AddRange(list);
										list.Clear();
									}
								}
							}
						}
						if (0 < list.Count)
						{
							SQLiteMwsServiceFrequencyAccess.SetMwsServiceFrequencyDataList(Directory.GetCurrentDirectory(), list);
							addList.AddRange(list);
						}
						if (0 < addList.Count)
						{
							listBoxFrequency.Items.Add(addList);
						}
					}
				}
			}
		}
	}
}
