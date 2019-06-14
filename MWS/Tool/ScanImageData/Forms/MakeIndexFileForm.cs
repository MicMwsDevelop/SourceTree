using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.BaseFactory.ScanImageData;
using MwsLib.DB.SQLite.ScanImageData;
using System.IO;
using System.Reflection;

namespace ScanImageData.Forms
{
	public partial class MakeIndexFileForm : Form
	{
		private string CurrentFolder;

		public MakeIndexFileForm()
		{
			InitializeComponent();

			CurrentFolder = Directory.GetCurrentDirectory();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MakeIndexFileForm_Load(object sender, EventArgs e)
		{
			DataTable dt = SQLiteScanImageDataGetIO.GetIndexFileInfo(CurrentFolder);
			if (null != dt)
			{
				try
				{
					this.dataGridViewIndex.SuspendLayout();

					// バインド
					this.dataGridViewIndex.DataSource = dt;
				}
				finally
				{
					this.dataGridViewIndex.ResumeLayout();
				}

			}
		}

		/// <summary>
		/// 出力対象リスト初期化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClear_Click(object sender, EventArgs e)
		{
			//SQLiteScanImageDataAccess.DeleteIndexFileInfo(CurrentFolder);
			this.dataGridViewIndex.DataSource = null;
		}

		/// <summary>
		/// インデックスファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutput_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt = Ctype(this.dataGridViewIndex.DataSource, DataTable).copy();

			//DataTable dt = this.dataGridViewIndex.DataSource. as DataTable;
			//List<IndexFileInfo> list = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<IndexFileInfo>("Column1")).ToList();

			List<IndexFileInfo> list = new List<IndexFileInfo>();
			list = MakeIndexFileForm.ConvertDataTable<IndexFileInfo>(dt);
			int aaa =1;
		}

		private static List<T> ConvertDataTable<T>(DataTable dt)
		{
			List<T> data = new List<T>();
			foreach (DataRow row in dt.Rows)
			{
				T item = GetItem<T>(row);
				data.Add(item);
			}
			return data;
		}
		private static T GetItem<T>(DataRow dr)
		{
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach (DataColumn column in dr.Table.Columns)
			{
				foreach (PropertyInfo pro in temp.GetProperties())
				{
					if (pro.Name == column.ColumnName)
						pro.SetValue(obj, dr[column.ColumnName], null);
					else
						continue;
				}
			}
			return obj;
		}
	}
}
