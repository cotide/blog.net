namespace Cotide.Framework.Extensions
{
    /// <summary>
    /// ModelState扩展类
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取所有错误信息
        /// </summary> 
        /// <param name="modelStateDictionary">所要扩展的对象</param>
        /// <param name="separator">分隔符</param>
        /// <returns>错误列表</returns>
        public static string ExpendErrors(this System.Web.Mvc.ModelStateDictionary modelStateDictionary, string separator)
        {
            var sbErrors = new System.Text.StringBuilder();
            foreach (var item in modelStateDictionary.Values)
            {
                if (item.Errors.Count > 0)
                {
                    for (var i = item.Errors.Count - 1; i >= 0; i--)
                    {
                        sbErrors.Append(item.Errors[i].ErrorMessage);
                        sbErrors.Append(separator);
                    }
                }
            }
            return sbErrors.ToString();
        }
    }
}