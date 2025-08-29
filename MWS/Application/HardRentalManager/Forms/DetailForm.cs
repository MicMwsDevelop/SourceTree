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
using System.Windows.Forms;

namespace HardRentalManager.Forms
{
	/// <summary>
	/// 機器情報入力画面クラス
	/// </summary>
	public partial class DetailForm : Form
	{
		/// <summary>
		/// 内部契約番号
		/// </summary>
		public int InternalRentalNo { get; set; }

		/// <summary>
		/// 機器情報（保存用）
		/// </summary>
		public T_HARD_RENTAL_DETAIL SaveDetail { get; set; }

		/// <summary>
		/// 機器情報
		/// </summary>
		public T_HARD_RENTAL_DETAIL Detail { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DetailForm()
		{
			InitializeComponent();

			InternalRentalNo = 0;
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
				labelCategory.Text = SaveDetail.CategoryName;
				numericTextBoxQuantity.Text = SaveDetail.Quantity.ToString();
				textBoxSerialNo.Text = SaveDetail.SerialNo;
				textBoxAssetsCode.Text = SaveDetail.AssetsCode;
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
					labelCategory.Text = Program.GetCategoryName(pca.sms_skbn3.Value);
					labelCategory.Tag = pca.sms_skbn3.Value;
					textBoxAssetsCode.Text = string.Empty;
					textBoxSerialNo.Text = string.Empty;
				}
				else
				{
					MessageBox.Show("指定された商品コードに該当する商品がありません。商品コードをお確かめください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					labelGoodsName.Text = string.Empty;
					labelCategory.Text = string.Empty;
					labelCategory.Tag = null;
					textBoxAssetsCode.Text = string.Empty;
					textBoxSerialNo.Text = string.Empty;
				}
			}
			else
			{
				MessageBox.Show("商品コードを正しく入力してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				labelGoodsName.Text = string.Empty;
				labelCategory.Text = string.Empty;
				labelCategory.Tag = null;
				textBoxAssetsCode.Text = string.Empty;
				textBoxSerialNo.Text = string.Empty;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == labelGoodsName.Text.Length)
			{
				MessageBox.Show("機器名が設定されていません。商品コードによる検索を行ってください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			short quantity = (short)numericTextBoxQuantity.ToInt();
			if (quantity < 1)
			{
				MessageBox.Show("数量を入力してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string serialNo = textBoxSerialNo.Text.Trim();
			string assetsCode = textBoxAssetsCode.Text.Trim();
			if (1 < quantity && (0 < serialNo.Length || 0 < assetsCode.Length))
			{
				MessageBox.Show("資産管理する機器の数量は１固定です。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (null == SaveDetail)
			{
				// 新規入力
				Detail = new T_HARD_RENTAL_DETAIL();
				Detail.InternalRentalNo = InternalRentalNo;
				Detail.GoodsCode = numericTextBoxGoodsCode.Text;
				Detail.GoodsName = labelGoodsName.Text;
				Detail.CategoryName = labelCategory.Text;
				Detail.Quantity = quantity;
				Detail.SerialNo = serialNo;
				Detail.AssetsCode = assetsCode;
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				// 変更
				Detail.GoodsCode = numericTextBoxGoodsCode.Text;
				Detail.GoodsName = labelGoodsName.Text;
				Detail.GoodsName = labelCategory.Text;
				Detail.Quantity = quantity;
				Detail.SerialNo = serialNo;
				Detail.AssetsCode = assetsCode;
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
