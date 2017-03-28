using System.ComponentModel;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 用户性别
    /// </summary>
    public enum UserSex
    {
        
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male=0,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female=1, 
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other=2
    }
}
