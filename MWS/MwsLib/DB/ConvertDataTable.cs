using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MwsLib.DB
{
	public static class ConvertDataTable
	{
		/// <summary>
		/// DataTable → List<T>
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List<T></returns>
		public static List<T> DataTableToList<T>(DataTable dt)
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
