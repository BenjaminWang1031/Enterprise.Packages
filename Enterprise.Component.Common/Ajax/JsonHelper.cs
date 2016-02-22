using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.Script.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Enterprise.Component.Common.Utility;

namespace Enterprise.Component.Common.Ajax
{
    public static class JsonHelper
    { 

        private static string mDefaultNullValue = "_default";

        /// <summary>
        /// ObjectToJson
        /// </summary>
        /// <param name="t">Object</param>
        /// <returns>Json string</returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t);
            //return new JavaScriptSerializer().Serialize(t);
        }

        /// <summary>
        /// EntityToJson
        /// </summary>
        /// <param name="t">Entity</param>
        /// <returns>Json string</returns>
        public static string EntityToJson(object entity)
        {
            return new JavaScriptSerializer().Serialize(entity);
        }

        /// <summary>
        /// JsonToObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            return new JavaScriptSerializer().Deserialize<T>(strJson);
        }

        /// <summary>
        /// Get tree json data
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string GetTreeJSON(IDataReader reader, string id, string pid)
        {
            var list = DbReaderToHash(reader);
            var o = ArrayToTreeData(list, id, pid);
            return ToJson(o);
        }

        /// <summary>
        /// Get tree json data
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static object GetTreeData(IDataReader reader, string id, string pid)
        {
            var list = DbReaderToHash(reader);
            return JsonHelper.ArrayToTreeData(list, id, pid);
        }

        /// <summary>
        /// Array To TreeData
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static object ArrayToTreeData(IList<Hashtable> list, string id, string pid)
        {
            id = id.ToLower();//Oracle
            pid = pid.ToLower();//Oracle

            var h = new Hashtable(); //index
            var r = new List<Hashtable>(); //list
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                h[item[id].ToString()] = item;
            }
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                if (!item.ContainsKey(pid) || item[pid] == null || !h.ContainsKey(item[pid].ToString()))
                {
                    r.Add(item);
                }
                else
                {
                    var pitem = h[item[pid].ToString()] as Hashtable;
                    if (!pitem.ContainsKey("children"))
                        pitem["children"] = new List<Hashtable>();
                    var children = pitem["children"] as List<Hashtable>;
                    children.Add(item);
                }
            }
            return r;
        }

        /// <summary>
        /// Db Reader To Hash
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Hashtable> DbReaderToHash(IDataReader reader)
        {
            var list = new List<Hashtable>();
            while (reader.Read())
            {
                var item = new Hashtable();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i).ToLower();
                    var value = reader[i];
                    item[name] = value;
                }
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Get DropDown Json Data
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">data source</param>
        /// <param name="textField">the text field</param>
        /// <param name="valueField">the id field</param>
        /// <returns>json data</returns>
        public static string GetDropDownJsonData<T>(List<T> list, string textField, string valueField, string defaultText)
        {
            StringBuilder _Json = new StringBuilder();
            _Json.Append("[");
            if (!string.IsNullOrEmpty(defaultText))
            {
                _Json.Append("{");
                _Json.AppendFormat("\"text\":\"{0}\",", FilterText(defaultText));
                _Json.AppendFormat("\"id\":\"{0}\"", mDefaultNullValue);
                _Json.Append("},");
            }
            if (list != null && list.Count > 0)
            {
                foreach (T entity in list)
                {
                    _Json.Append("{");
                    _Json.AppendFormat("\"text\":\"{0}\",", FilterText(entity.GetType().GetProperty(textField).GetValue(entity, null)));
                    _Json.AppendFormat("\"id\":\"{0}\"", entity.GetType().GetProperty(valueField).GetValue(entity, null));
                    _Json.Append("},");
                }
            }
            if (_Json.ToString().EndsWith(","))
            {
                _Json.Remove(_Json.Length - 1, 1);
            }
            _Json.Append("]");
            return _Json.ToString();
        }

        /// <summary>
        /// Get DropDown Json Data
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">data source</param>
        /// <param name="textFieldsList">the text field</param>
        /// <param name="splitChar">split char</param>
        /// <param name="valueField">the id field</param>
        /// <param name="otherFieldsList">other fields</param>
        /// <param name="defaultText">default text</param>
        /// <returns>json data</returns>
        public static string GetDropDownJsonData<T>(List<T> list, List<string> textFieldsList, string splitChar, string valueField, List<string> otherFieldsList, string defaultText)
        {
            StringBuilder _Json = new StringBuilder();
            _Json.Append("[");
            if (!string.IsNullOrEmpty(defaultText))
            {
                _Json.Append("{");
                if (otherFieldsList != null && otherFieldsList.Count > 0)
                {
                    foreach (var otherField in otherFieldsList)
                    {
                        _Json.AppendFormat("\"{0}\":\"\",", otherField);
                    }
                }
                _Json.AppendFormat("\"text\":\"{0}\",", FilterText(defaultText));
                _Json.AppendFormat("\"id\":\"{0}\"", mDefaultNullValue);
                _Json.Append("},");
            }
            if (list != null && list.Count > 0)
            {
                foreach (T entity in list)
                {
                    _Json.Append("{");
                    if (otherFieldsList != null && otherFieldsList.Count > 0)
                    {
                        foreach (var otherField in otherFieldsList)
                        {
                            _Json.AppendFormat("\"{0}\":\"{1}\",", otherField, FilterText(entity.GetType().GetProperty(otherField).GetValue(entity, null)));
                        }
                    }
                    _Json.AppendFormat("\"text\":\"{0}\",", FilterText(GetTextFieldsString(entity, textFieldsList, splitChar)));
                    _Json.AppendFormat("\"id\":\"{0}\"", entity.GetType().GetProperty(valueField).GetValue(entity, null));
                    _Json.Append("},");
                }
            }
            if (_Json.ToString().EndsWith(","))
            {
                _Json.Remove(_Json.Length - 1, 1);
            }
            _Json.Append("]");
            return _Json.ToString();
        }

        /// <summary>
        /// Get DropDown Json Data
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">data source</param>
        /// <param name="textFieldsList">the text field</param>
        /// <param name="splitChar">split char</param>
        /// <param name="valueField">the id field</param>
        /// <param name="defaultText">default text</param>
        /// <returns>json data</returns>
        public static string GetDropDownJsonData<T>(List<T> list, List<string> textFieldsList, string splitChar, string valueField, string defaultText)
        {
            StringBuilder _Json = new StringBuilder();
            _Json.Append("[");
            if (!string.IsNullOrEmpty(defaultText))
            {
                _Json.Append("{");
                _Json.AppendFormat("\"text\":\"{0}\",", FilterText(defaultText));
                _Json.AppendFormat("\"id\":\"{0}\"", mDefaultNullValue);
                _Json.Append("},");
            }
            foreach (T entity in list)
            {
                _Json.Append("{");
                _Json.AppendFormat("\"text\":\"{0}\",", FilterText(GetTextFieldsString(entity, textFieldsList, splitChar)));
                _Json.AppendFormat("\"id\":\"{0}\"", FilterText(entity.GetType().GetProperty(valueField).GetValue(entity, null)));
                _Json.Append("},");
            }
            if (_Json.ToString().EndsWith(","))
            {
                _Json.Remove(_Json.Length - 1, 1);
            }
            _Json.Append("]");
            return _Json.ToString();
        }

        /// <summary>
        /// Get text field string
        /// </summary>
        /// <param name="entity">source entity</param>
        /// <param name="textFieldsList">text field list</param>
        /// <param name="splitChar">split char</param>
        /// <returns>text field string with split char</returns>
        private static string GetTextFieldsString(object entity, List<string> textFieldsList, string splitChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in textFieldsList)
            {
                sb.Append(TypeHelper.GetObjectPropertyValue(entity, str) + splitChar);
            }
            var sbString = sb.ToString();
            if (sbString.EndsWith(splitChar))
            {
                sbString = sbString.Remove(sbString.Length - 1, 1);
            }
            return FilterText(sbString);
        }

        private static string FilterText(object strObj)
        {
            if (strObj != null && !string.IsNullOrEmpty(strObj.ToString()))
                return strObj.ToString().Replace(@"/", "").Replace(@"\", "").Replace("'", "").Replace("\"", "");
            return "";
        }
    }
}
