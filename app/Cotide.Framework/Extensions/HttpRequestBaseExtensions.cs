using System.Web;

namespace Cotide.Framework.Extensions
{
    /// <summary>
    /// 参数处理类
    /// </summary>
    public static class HttpRequestBaseExtensions
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="request">Request对象</param>
        /// <param name="key">参数名字</param>
        /// <returns>参数的值(默认为空字符串)</returns>
        public static string Get(this HttpRequestBase request, string key)
        {
            return request.HttpMethod == "POST"
                       ? request.Form[key]
                       : (request.HttpMethod == "GET" ? request.QueryString[key] : string.Empty);
        }
    }
}