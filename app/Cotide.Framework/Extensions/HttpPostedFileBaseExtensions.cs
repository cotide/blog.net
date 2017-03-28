#region using

using System.Web;

#endregion

namespace Cotide.Framework.Extensions
{
    /// <summary>
    /// 提交文件处理类
    /// </summary>
    public static class HttpPostedFileBaseExtensions
    {
        /// <summary>
        ///   是否存在文件
        /// </summary>
        /// <param name = "file">文件参数</param>
        /// <returns>是否存在</returns>
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }

        /// <summary>
        ///   是否存在文件
        /// </summary>
        /// <param name = "file">文件参数</param>
        /// <returns>是否存在</returns>
        public static bool HasFile(this HttpPostedFile file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
}