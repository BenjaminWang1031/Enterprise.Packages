using System;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Text;

namespace Enterprise.Component.Common.Cache
{

    public static class CacheHelper
    {
        /// <summary>
        /// Get value from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(string key)
        {
            return HttpRuntime.Cache.Get(key); 
        }

        ///// <summary>
        ///// Add cache , only used for user (menu\buttonlist\roles)
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        //public static void AddDependentCache(string key, object value)
        //{
        //    //Cache can not depends on an null object
        //    if (value == null)
        //        return;
        //    if (GetValue(ConstantsHelper.ComponentCacheKey) == null)
        //        AddCache(ConstantsHelper.ComponentCacheKey, DateTime.Now);
        //    string[] dependencyKey = new string[] { ConstantsHelper.ComponentCacheKey };
        //    CacheDependency cacheDependency = new CacheDependency(null, dependencyKey);
        //    HttpRuntime.Cache.Insert(key, value, cacheDependency, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(GetCachedHours()));
        //}

        /// <summary>
        /// add cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(GetCachedHours()));
        }

        /// <summary>
        /// Clear cache
        /// </summary>
        /// <param name="key"></param>
        public static void ClearCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// update cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void UpdateCache(string key, object value)
        {
            if (HttpContext.Current != null && value != null)
            {
                HttpContext.Current.Cache[key] = value;
            }
        }

        private static double GetCachedHours()
        {
            var sKey = "SYS_CACHEHOURS";
            var sVal = System.Configuration.ConfigurationManager.AppSettings[sKey];
            return string.IsNullOrEmpty(sVal) ? 6 : Convert.ToDouble(sVal);
        }
    }
}
