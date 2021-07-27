using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptechConvert
{
	[System.Xml.Serialization.XmlRoot("Patients")]
	public class Patients
	{
		[System.Xml.Serialization.XmlElement("Patient")]
		public List<Patient> PatientList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public Patients()
		{
			PatientList = new List<Patient>();
		}
	}
}
