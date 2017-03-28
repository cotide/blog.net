using System; 
using System.Web;

namespace Cotide.Framework.Utility
{
    /// <summary>
    /// 请求处理辅助类
    /// </summary>
    public class RequestHelper
    {
        //public static float GetFloat(string strName, float defValue)
        //{
        //    if (GetQueryFloat(strName, defValue) == defValue)
        //    {
        //        return GetFormFloat(strName, defValue);
        //    }
        //    return GetQueryFloat(strName, defValue);
        //}

        //public static float GetFormFloat(string strName, float defValue)
        //{
        //    return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        //}

        //public static int GetFormInt(string strName, int defValue)
        //{
        //    return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        //}

        /// <summary>
        /// 获取请求参数值
        /// </summary>
        /// <param name="strName">参数名称</param>
        /// <returns>返回请求参数值</returns>
        public static string GetFormString(string strName)
        {
            return HttpContext.Current.Request.Form[strName] ?? "";
        }

        //public static int GetInt(string strName, int defValue)
        //{
        //    if (GetQueryInt(strName, defValue) == defValue)
        //    {
        //        return GetFormInt(strName, defValue);
        //    }
        //    return GetQueryInt(strName, defValue);
        //}

        /// <summary>
        /// 获取请求IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string str2;
            // 如果客户端使用了代理服务器,使用Request.ServerVariables("HTTP_X_FORWARDED_FOR") 得到IP地址,如果没用使用代理服务器,得到的是"",
            // 则用Request.ServerVariables("REMOTE_ADDR") 得到IP地址. 
            string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (((str2 = userHostAddress) == null) || (str2 == ""))
            {
                // 用Request.ServerVariables("REMOTE_ADDR") 得到IP地址. 
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            return !string.IsNullOrEmpty(userHostAddress) ? userHostAddress : "0.0.0.0";
        }

        public static string GetPageName()
        {
            var strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }

        //public static float GetQueryFloat(string strName, float defValue)
        //{
        //    return Utils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        //}

        //public static int GetQueryInt(string strName, int defValue)
        //{
        //    return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        //}

        //public static long GetQueryInt64(string strName)
        //{
        //    return Utils.StrToInt64(HttpContext.Current.Request.QueryString[strName], 0);
        //}

        public static string GetQueryString(string strName)
        {
            return HttpContext.Current.Request.QueryString[strName] == null ? "" : HttpContext.Current.Request.QueryString[strName].ToString().Trim();
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        
        public static string GetServerString(string strName)
        {
            return HttpContext.Current.Request.ServerVariables[strName] == null ? "" : HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 获取请求参数值
        /// </summary>
        /// <param name="strName">参数KEY</param>
        /// <returns>返回KEY值</returns>
        public static string GetString(string strName)
        {
            return "".Equals(GetQueryString(strName)) ? GetFormString(strName) : GetQueryString(strName);
        }

        /// <summary>
        /// 获取当前URL
        /// </summary>
        /// <returns>返回当前URL</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获取有关客户端上次请求信息，没否返回""
        /// </summary>
        /// <returns>烦求上次请求信息</returns>
        public static string GetUrlReferrer()
        {
            return HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.ToString().Trim();
        }

        /// <summary>
        /// 检查请求游览器类型
        /// </summary>
        /// <returns>true为符合列表条件游览器，false为否</returns>
        public static bool IsBrowserGet()
        {
            var strArray = new string[] { "ie", "opera", "netscape", "mozilla" };
            var str = HttpContext.Current.Request.Browser.Type.ToLower();
            for (var i = 0; i < strArray.Length; i++)
            {
                if (str.IndexOf(strArray[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  检查是否GET提交
        /// </summary>
        /// <returns>true为是 false为否</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 检查是否POST提交
        /// </summary>
        /// <returns>true为是 false为否</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断是否搜索引擎请求
        /// </summary>
        /// <returns>true为是 false为否</returns>
        public static bool IsSearchEnginesGet()
        {
            var strArray = new string[] { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom" };
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                var str = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                for (var i = 0; i < strArray.Length; i++)
                {
                    if (str.IndexOf(strArray[i]) >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpContext.Current.Request.Files[0].SaveAs(path);
            }
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 返回当前页面是否是跨站提交
        /// </summary>
        /// <returns>当前页面是否是跨站提交</returns>
        public static bool IsCrossSitePost()
        {

            // 如果不是提交则为true
            if (!IsPost())
            {
                return true;
            }
            return IsCrossSitePost(GetUrlReferrer(), GetHost());
        }

        /// <summary>
        /// 判断是否是跨站提交
        /// </summary>
        /// <param name="urlReferrer">上个页面地址</param>
        /// <param name="host">论坛url</param>
        /// <returns>bool</returns>
        public static bool IsCrossSitePost(string urlReferrer, string host)
        {
            if (urlReferrer.Length < 7)
            {
                return true;
            }
            // 移除http://
            //			string tmpReferrer = urlReferrer.Remove(0, 7);
            //			if (tmpReferrer.IndexOf(":") > -1)
            //				tmpReferrer = tmpReferrer.Substring(0, tmpReferrer.IndexOf(":"));
            //			else
            //				tmpReferrer = tmpReferrer.Substring(0, tmpReferrer.IndexOf('/')); 
            var u = new Uri(urlReferrer);
            return u.Host != host;
        }
    }
}
