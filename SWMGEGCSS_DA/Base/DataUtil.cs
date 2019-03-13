using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
namespace SWMGEGCSS_DA.Base
{
    public class DataUtil
    {
        public static Nullable<T> DbValueToNullable<T>(object dbValue) where T : struct
        {
            Nullable<T> returnValue = null;
            if ((dbValue != null) && (dbValue != DBNull.Value) && (dbValue != string.Empty))
            {
                returnValue = (T)Convert.ChangeType(dbValue, typeof(T));
            }
            return returnValue;
        }
        public static T DbValueToDefault<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj == string.Empty) return default(T);
            else
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
        }
        public static List<string> SplitData(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new List<string>();
            }
            return text.Split('\\').ToList();
        }
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(values);
                }
            }
            return table;
        }
    }
}
