
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Web;

namespace Enterprise.Component.Common.Utility
{
    public static class TypeHelper
    {
        /// <summary>
        /// GetObjetValue
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetObjectPropertyValue(object entity, string propertyName)
        {
            return entity.GetType().GetProperty(propertyName).GetValue(entity, null);
        }

        /// <summary>
        /// GetDecimalValue
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string GetDateTimeString(string strValue, string strFormat)
        {
            if (string.IsNullOrEmpty(GetObjectValue(strValue)))
                return "";
            return Convert.ToDateTime(strValue).ToString(strFormat);
        }

        /// <summary>
        /// GetDecimalValue
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static decimal? GetDecimalValue(string strValue)
        {
            if (string.IsNullOrEmpty(GetObjectValue(strValue)))
                return null;
            return Convert.ToDecimal(strValue);
        }

        /// <summary>
        /// GetDecimalValue
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static decimal GetDecimalNotNullValue(string strValue)
        {
            return Convert.ToDecimal(strValue);
        }

        /// <summary>
        /// SetObjetValue
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static void SetObjetValue(object entity, string propertyName, object value)
        {
            if (value == null)
                return;
            var property = entity.GetType().GetProperty(propertyName);
            if (property != null)
            {
                var fullName = property.PropertyType.FullName;
                if (fullName.Contains("System.Nullable`1"))
                {
                    var name = property.PropertyType.FullName.Replace("System.Nullable`1", "").Replace("[", "").Replace("]", "").Split(',')[0].Split('.')[1];
                    switch (name)
                    {
                        case "Decimal":
                            value = Convert.ToDecimal(value);
                            break;
                        case "String":
                            value = value.ToString();
                            break;
                        case "DateTime":
                            value = Convert.ToDateTime(value);
                            break;
                        case "Int32":
                            value = Convert.ToInt32(value);
                            break;
                    }
                    property.SetValue(entity, value, null);
                }
                else
                {
                    property.SetValue(entity, Convert.ChangeType(value, property.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// TableIsNullOrEmpty
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public static bool TableIsNullOrEmpty(DataTable tab)
        {
            if (tab != null && tab.Rows != null && tab.Rows.Count > 0)
                return false;
            return true;
        }

        /// <summary>
        /// Get date string from num 
        /// For instance:20131231
        /// </summary>
        /// <param name="mNum">For instance:20131231</param>
        /// <returns>For instance:2013/12/31</returns>
        public static string GetDateStrFromNum(decimal mNum)
        {
            var strNum = mNum.ToString();

            var strYear = strNum.Substring(0, 4);
            var strMonth = strNum.Substring(4, 2);
            var strDay = strNum.Substring(6, 2);

            return strYear + "/" + strMonth + "/" + strDay;
        }

        public static decimal GetNumFromDateString(string strDate)
        {
            if (string.IsNullOrEmpty(strDate))
                throw new Exception("Effective Date can not be empty !");
            return Convert.ToDecimal(Convert.ToDateTime(strDate).ToString("yyyyMMdd"));
        }

        /// <summary>
        /// GetObjectValue
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetObjectValue(object obj)
        {
            return obj == null ? "" : obj.ToString().Trim();
        }

        /// <summary>
        /// Get Dictionary value by key
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDicValue(Dictionary<string, object> dic, string key)
        {
            if (dic.ContainsKey(key))
                return GetObjectValue(dic[key]);
            return "";
        }

        /// <summary>
        /// Get param value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueFromQuestParams(string key)
        {
            var strValue = HttpContext.Current.Request.Params[key];
            return string.IsNullOrEmpty(strValue) ? "" : strValue.Trim();
        }

        /// <summary>
        /// Get double value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDoubleValue(object obj,out double dValue)
        {
            return Double.TryParse(obj == null ? "" : obj.ToString(), out dValue);
        }
    }
}
