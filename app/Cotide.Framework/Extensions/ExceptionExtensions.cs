using System;

namespace Cotide.Framework.Extensions
{
    /// <summary>
    ///  字符串辅助类
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 获取内部异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception GetInnerException<T>(this Exception ex) where T : Exception
        {
            if(ex is T)
            {
                return ex;
            }
            var rsult = ex;
            var find = false;
            while (rsult.InnerException != null)
            {
                rsult = rsult.InnerException;
                if (!(rsult is T)) continue;
                find = true;
                break;
            }
            return find ? rsult : null;
        }
    }
}