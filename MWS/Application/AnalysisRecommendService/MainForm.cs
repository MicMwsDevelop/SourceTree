using MwsLib.BaseFactory.AnalysisRecommendService;
using MwsLib.DB.SqlServer.AnalysisRecommendService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnalysisRecommendService
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// ターゲットユーザー
		/// </summary>
		private RecommendService Target;

		private int CurrentIndex = 0;

		private List<RecommendService> RecommendList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			Target = null;
			RecommendList = null;
			CurrentIndex = 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		private void SetRecomenndServiceListView(RecommendService recommend)
		{
			listViewRecommendService.Items.Clear();

			foreach (Tuple<int, string> service in recommend.ServiceList)
			{
				string[] line = new string[2];
				line[0] = service.Item1.ToString();
				line[1] = service.Item2;
				ListViewItem item = new ListViewItem(line);
				item.Tag = service;
				if (-1 == Target.ServiceList.FindIndex(p => p.Item1 == service.Item1))
				{
					item.UseItemStyleForSubItems = false;
					item.SubItems[0].ForeColor = Color.Red;
					item.SubItems[1].ForeColor = Color.Red;
				}
				listViewRecommendService.Items.Add(item);
			}
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			RecommendList = AnalysisRecommendServiceAccess.GetRecommendServiceList(Program.DATABASE_ACCESS_CT);
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (0 != numericTextBoxTargetCustomerNo.ToInt())
			{
				Target = AnalysisRecommendServiceAccess.GetRecommendService(numericTextBoxTargetCustomerNo.ToInt(), Program.DATABASE_ACCESS_CT);
				if (null != Target)
				{
					listViewTargetService.Items.Clear();
					foreach (Tuple<int, string> service in Target.ServiceList)
					{
						string[] line = new string[2];
						line[0] = service.Item1.ToString();
						line[1] = service.Item2;
						ListViewItem item = new ListViewItem(line);
						item.Tag = service;
						listViewTargetService.Items.Add(item);
					}
					comboBoxRecommend.Items.Clear();
					CurrentIndex = 0;

					RecommendService.SetRecommndCount(Target, RecommendList);
					RecommendList.Sort((a, b) => b.RecommendCount - a.RecommendCount);
					foreach (RecommendService sv in RecommendList)
					{
						if (0 < sv.RecommendCount)
						{
							if (Target.CustomerID != sv.CustomerID)
							{
								comboBoxRecommend.Items.Add(sv.CustomerID.ToString());
							}
						}
					}
					comboBoxRecommend.SelectedIndex = 0;

					SetRecomenndServiceListView(RecommendList[0]);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxRecommend_SelectedIndexChanged(object sender, EventArgs e)
		{
			string id = comboBoxRecommend.SelectedItem as string;
			int index = RecommendList.FindIndex(p => p.CustomerID == int.Parse(id));
			if (-1 != index)
			{
				SetRecomenndServiceListView(RecommendList[index]);
			}
		}
	}
}
