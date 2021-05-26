using MwsLib.Common;
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.AlertCloudBackupPcSupportPlus
{
	public class CloudBackupPcSupportPlus
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// PC安心サポートPlus利用開始日
		/// </summary>
		public DateTime? PcStartDate { get; set; }

		/// <summary>
		/// PC安心サポートPlus利用終了日
		/// </summary>
		public DateTime? PcEndDate { get; set; }

		/// <summary>
		/// クラウドバックアップ利用開始日
		/// </summary>
		public DateTime? ClStartDate { get; set; }

		/// <summary>
		///クラウドバックアップ利用終了日
		/// </summary>
		public DateTime? ClEndDate { get; set; }

		/// <summary>
		/// 対応済
		/// </summary>
		public int Supported { get; set; }

		/// <summary>
		/// PC安心サポートPlus利用期間文字列の取得
		/// </summary>
		public string PcSupportPlusSpanString
		{
			get
			{
				if (PcStartDate.HasValue && PcEndDate.HasValue)
				{
					return string.Format("{0}～{1}", new Date(PcStartDate.Value).GetNormalString(), new Date(PcEndDate.Value).GetNormalString());
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// クラウドバックアップ利用期間文字列の取得
		/// </summary>
		public string CloudBackupSpanString
		{
			get
			{
				if (ClStartDate.HasValue && ClEndDate.HasValue)
				{
					return string.Format("{0}～{1}", new Date(ClStartDate.Value).GetNormalString(), new Date(ClEndDate.Value).GetNormalString());
				}
				return string.Empty;
			}
		}

		/// <summary>
		/// エクセルタイトル行の出力
		/// </summary>
		public string[] Title
		{
			get
			{
				return new string[] { "顧客No", "顧客名", "PC安心サポートPlus利用開始日", "PC安心サポートPlus利用終了日", "クラウドバックアップ利用開始日", "クラウドバックアップ利用終了日", "対応済" };
			}
		}


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CloudBackupPcSupportPlus()
		{
			CustomerNo = 0;
			ClinicName = string.Empty;
			PcStartDate = null;
			PcEndDate = null;
			ClStartDate = null;
			ClEndDate = null;
			Supported = 0;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return CustomerNo + Supported;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CloudBackupPcSupportPlus other)
        {
			if (CustomerNo != other.CustomerNo) return false;
			if (ClinicName != other.ClinicName) return false;
			if (PcStartDate != other.PcStartDate) return false;
			if (PcEndDate != other.PcEndDate) return false;
			if (ClStartDate != other.ClStartDate) return false;
			if (ClEndDate != other.ClEndDate) return false;
			if (Supported != other.Supported) return false;
			return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o is CloudBackupPcSupportPlus)
            {
                return Equals((CloudBackupPcSupportPlus)o);
            }
            else
            {
                return false;
            }
        }
		
		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<CloudBackupPcSupportPlus> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CloudBackupPcSupportPlus> result = new List<CloudBackupPcSupportPlus>();
				foreach (DataRow row in table.Rows)
				{
					CloudBackupPcSupportPlus data = new CloudBackupPcSupportPlus();
					data.CustomerNo = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					data.ClinicName = row["CLINIC_NAME"].ToString().Trim();
					data.PcStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["PC_START_DATE"]);
					data.PcEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["PC_END_DATE"]);
					data.ClStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["CL_START_DATE"]);
					data.ClEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["CL_END_DATE"]);
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// エクセルデータ行の出力
		/// </summary>
		/// <returns></returns>
		public string[] GetAlartArray()
		{
			string[] array = new string[6];
			array[0] = CustomerNo.ToString();
			array[1] = ClinicName;
			array[2] = PcStartDate.ToString();
			array[3] = PcEndDate.ToString();
			array[4] = ClStartDate.ToString();
			array[5] = ClEndDate.ToString();
			return array;
		}

		/// <summary>
		/// エクセル行の設定
		/// </summary>
		/// <param name="data"></param>
		public bool SetRecord(string csv)
		{
			string[] split = csv.Split(',');
			if (7 == split.Length)
			{
				int work;
				if (int.TryParse(split[0], out work))
				{
					CustomerNo = work;
				}
				ClinicName = split[1];
				DateTime tm;
				if (DateTime.TryParse(split[2], out tm))
				{
					PcStartDate = tm;
				}
				if (DateTime.TryParse(split[3], out tm))
				{
					PcEndDate = tm;
				}
				if (DateTime.TryParse(split[4], out tm))
				{
					ClStartDate = tm;
				}
				if (DateTime.TryParse(split[5], out tm))
				{
					ClEndDate = tm;
				}
				if (int.TryParse(split[6], out work))
				{
					Supported = work;
				}
				return true;
			}
			return false;
		}

        /// <summary>
        /// 同じ内容かどうか？
        /// </summary>
        /// <param name="other"></param>
        /// <returns>判定</returns>
        public bool IsMatch(CloudBackupPcSupportPlus other)
        {
			if (CustomerNo != other.CustomerNo) return false;
			if (ClinicName != other.ClinicName) return false;
			if (PcStartDate != other.PcStartDate) return false;
			if (PcEndDate != other.PcEndDate) return false;
			if (ClStartDate != other.ClStartDate) return false;
			if (ClEndDate != other.ClEndDate) return false;
			return true;
        }

	}
}
