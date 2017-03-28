using System.ComponentModel;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal,
        /// <summary>
        /// 冻结
        /// </summary>
        [Description("冻结")]
        Freeze
    }
}
