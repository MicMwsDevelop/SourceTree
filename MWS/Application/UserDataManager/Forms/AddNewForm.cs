using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDataManager.Forms
{
	/// <summary>
	/// 新規登録画面
	/// </summary>
	public partial class AddNewForm : Form
	{
		List<UserDataFile> FileList;


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AddNewForm()
		{
			InitializeComponent();

			FileList = new List<UserDataFile>();
		}

		/// <summary>
		/// Drag & Drop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewFileList_DragDrop(object sender, DragEventArgs e)
		{
			List<UserDataFile> list = new List<UserDataFile>();
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string pathname in files)
			{
				//long size = Program.GetFileSize(pathname);
				//if (0 < size)
				//{
				//	string sizeStr = "1KB";
				//	if (1024 <= size)
				//	{
				//		sizeStr = string.Format("{0}KB", size / 1024);
				//	}
				//	dataGridViewFileList.Rows.Add(pathname, sizeStr);
				//}
				Program.SetUserDataList(pathname, list);
			}
			FileList.AddRange(list);
			foreach (UserDataFile data in FileList)
			{
				dataGridViewFileList.Rows.Add(data.ToStringArry());
				//dataGridViewFileList.Rows.Add(data);
			}
		}

		/// <summary>
		/// Drag & Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewFileList_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		/// <summary>
		/// クリア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonクリア_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ｷｬﾝｾﾙ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{

		}
	}
}
