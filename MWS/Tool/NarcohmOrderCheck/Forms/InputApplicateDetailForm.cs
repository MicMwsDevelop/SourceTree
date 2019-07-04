using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.BaseFactory.NarcohmOrderCheck;
using MwsLib.Common;
using MwsLib.DB.SqlServer.NarcohmOrderCheck;
using MwsLib.BaseFactory.Charlie.Table;

namespace NarcohmOrderCheck.Forms
{
	public partial class InputApplicateDetailForm : Form
	{
		public T_NARCOHM_APPLICATE_DETAIL ApplicateDetail;

		public DataTable DetailGoodsDataTable;

		public bool ModifyFlag;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public InputApplicateDetailForm()
		{
			InitializeComponent();

			ModifyFlag = false;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InputApplicateDetailForm_Load(object sender, EventArgs e)
		{
			// 受注番号
			if (ApplicateDetail.OrderNo.HasValue)
			{
				textBoxOrderNo.Text = ApplicateDetail.OrderNo.ToString();
			}
			// 受注日
			if (ApplicateDetail.OrderDate.HasValue)
			{
				dateTimePickerOrderDate.Checked = true;
				dateTimePickerOrderDate.Value = ApplicateDetail.OrderDate.Value.ToDateTime();
			}
			// 商品名
			comboBoxGoodsName.DisplayMember = "Display";
			comboBoxGoodsName.ValueMember = "Value";
			comboBoxGoodsName.DataSource = DetailGoodsDataTable;

			// 数量
			textBoxCount.Text = ApplicateDetail.Count.ToString();

			// 利用開始月
			if (ApplicateDetail.UseStartDate.HasValue)
			{
				dateTimePickerUseStartDate.Checked = true;
				dateTimePickerUseStartDate.Value = ApplicateDetail.UseStartDate.Value.ToDateTime();
			}
			// 利用終了月
			if (ApplicateDetail.UseEndDate.HasValue)
			{
				dateTimePickerUseEndDate.Checked = true;
				dateTimePickerUseEndDate.Value = ApplicateDetail.UseEndDate.Value.ToDateTime();
			}
			if (ModifyFlag)
			{
				// 商品名
				comboBoxGoodsName.SelectedIndexChanged -= new EventHandler(comboBoxGoodsName_SelectedIndexChanged);
				comboBoxGoodsName.SelectedValue = ApplicateDetail.GoodsCode;
				comboBoxGoodsName.SelectedIndexChanged += new EventHandler(comboBoxGoodsName_SelectedIndexChanged);

				// 金額
				textBoxPrice.Text = ApplicateDetail.Price.ToString();
			}
			else
			{
				// 商品名
				comboBoxGoodsName.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// 商品名の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxGoodsName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != comboBoxGoodsName.SelectedIndex)
			{
				textBoxPrice.Text = NarcohmOrderCheckAccess.GetNarcohmProductPrice(comboBoxGoodsName.SelectedValue as string, false).ToString();
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (-1 == comboBoxGoodsName.SelectedIndex)
			{
				MessageBox.Show("商品名が設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == dateTimePickerUseStartDate.Checked)
			{
				MessageBox.Show("利用開始月が設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == dateTimePickerUseEndDate.Checked)
			{
				MessageBox.Show("利用終了月が設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 受注番号
			if (0 < textBoxOrderNo.ToInt())
			{
				ApplicateDetail.OrderNo = textBoxOrderNo.ToInt();
			}
			else
			{
				ApplicateDetail.OrderNo = null;
			}
			// 受注日
			if (dateTimePickerOrderDate.Checked)
			{
				ApplicateDetail.OrderDate = new Date(dateTimePickerOrderDate.Value);
			}
			else
			{
				ApplicateDetail.OrderDate = null;
			}
			// 商品名
			ApplicateDetail.GoodsCode = comboBoxGoodsName.SelectedValue as string;
			ApplicateDetail.GoodsName = comboBoxGoodsName.Text;

			// 金額
			ApplicateDetail.Price = textBoxPrice.ToInt();

			// 数量
			ApplicateDetail.Count = textBoxCount.ToInt();

			// 合計
			ApplicateDetail.Total = ApplicateDetail.Price * ApplicateDetail.Count;

			// 利用開始月
			ApplicateDetail.UseStartDate = new Date(dateTimePickerUseStartDate.Value);

			// 利用終了月
			ApplicateDetail.UseEndDate = new Date(dateTimePickerUseEndDate.Value);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
