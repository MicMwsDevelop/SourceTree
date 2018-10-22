using MwsSimulation.Print;
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// 用紙選択画面
	/// </summary>
	public partial class SelectPaperForm : Form
	{
		/// <summary>
		/// 用紙種別
		/// </summary>
		public PrintEstimateDef.PaperType Type { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SelectPaperForm()
		{
			InitializeComponent();

			Type = PrintEstimateDef.PaperType.Estimate;
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (radioButtonEstimate.Checked)
			{
				Type = PrintEstimateDef.PaperType.Estimate;
			}
			else if (radioButtonPurchaseOrder.Checked)
			{
				Type = PrintEstimateDef.PaperType.PurchaseOrder;
			}
			else
			{
				Type = PrintEstimateDef.PaperType.OrderConfirm;
			}
		}

	}
}
