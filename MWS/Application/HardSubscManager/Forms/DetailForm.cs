//
// DetailForm.cs
//
// 機器情報入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscManager.Forms
{
	/// <summary>
	/// 機器情報入力画面クラス
	/// </summary>
	public partial class DetailForm : Form
	{
		/// <summary>
		/// 機器区分リスト
		/// </summary>
		private List<Tuple<short, string>> CategoryList { get; set; }

		/// <summary>
		/// 貸出番号
		/// </summary>
		public int RentalNo { get; set; }

		/// <summary>
		/// 機器情報（保存用）
		/// </summary>
		public T_HARDSUBSC_DETAIL SaveDetail { get; set; }

		/// <summary>
		/// 機器情報
		/// </summary>
		public T_HARDSUBSC_DETAIL Detail { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DetailForm()
		{
			InitializeComponent();

			CategoryList = null;
			RentalNo = 0;
			SaveDetail = null;
			Detail = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DetailForm_Load(object sender, EventArgs e)
		{
			// 機器区分コンボボックスの設定
			CategoryList = T_HARDSUBSC_DETAIL.GetCategoryList();
			comboBoxCategory.DataSource = CategoryList;
			comboBoxCategory.DisplayMember = "Item2";
			comboBoxCategory.ValueMember = "Item1";
			comboBoxCategory.SelectedIndex = 0;

			if (null == SaveDetail)
			{
				// 新規入力
				numericTextBoxQuantity.Text = "1";
			}
			else
			{
				// 変更
				numericTextBoxGoodsCode.Text = SaveDetail.GoodsCode;
				labelGoodsName.Text = SaveDetail.GoodsName;
				comboBoxCategory.SelectedIndex = CategoryList[SaveDetail.CategoryNo].Item1;
				numericTextBoxQuantity.Text = SaveDetail.Quantity.ToString();
				textBoxAssetsCode.Text = SaveDetail.AssetsCode;
				textBoxSerialNo.Text = SaveDetail.SerialNo;
				Detail = SaveDetail.DeepCopy();
			}
		}

		/// <summary>
		/// 商品の検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (PcaGoodsIDDefine.GoodsCodeLength == numericTextBoxGoodsCode.Text.Length)
			{
				vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(numericTextBoxGoodsCode.Text, Program.gSettings.ConnectJunp.ConnectionString);
				if (null != pca)
				{
					labelGoodsName.Text = pca.sms_mei;
					textBoxAssetsCode.Text = string.Empty;
					textBoxSerialNo.Text = string.Empty;
				}
				else
				{
					MessageBox.Show("指定された商品コードに該当する商品がありません。商品コードをお確かめください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MessageBox.Show("商品コードを正しく入力してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == numericTextBoxQuantity.ToInt())
			{
				MessageBox.Show("数量が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (null == SaveDetail)
			{
				// 新規入力
				Detail = new T_HARDSUBSC_DETAIL();
				Detail.RentalNo = RentalNo;
				Detail.GoodsCode = numericTextBoxGoodsCode.Text;
				Detail.GoodsName = labelGoodsName.Text;
				Detail.CategoryNo = CategoryList[comboBoxCategory.SelectedIndex].Item1;
				Detail.Quantity = (short)numericTextBoxQuantity.ToInt();
				Detail.AssetsCode = textBoxAssetsCode.Text.Trim();
				Detail.SerialNo = textBoxSerialNo.Text.Trim();
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				// 変更
				Detail.GoodsCode = numericTextBoxGoodsCode.Text;
				Detail.GoodsName = labelGoodsName.Text;
				Detail.CategoryNo = CategoryList[comboBoxCategory.SelectedIndex].Item1;
				Detail.Quantity = (short)numericTextBoxQuantity.ToInt();
				Detail.AssetsCode = textBoxAssetsCode.Text.Trim();
				Detail.SerialNo = textBoxSerialNo.Text.Trim();
				if (false == SaveDetail.Equals(Detail))
				{
					this.DialogResult = DialogResult.OK;
				}
				else
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
			this.Close();
		}

		/// <summary>
		/// キャンセル
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
