using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ProjectTypeCommands
{
    /// <summary>
    /// 创建项目类型命令
    /// </summary>
    public class CreateProjectTypeCommand : CommandBase
    {
        /// <summary>
        /// 项目类型名称
        /// </summary>
        public readonly string TypeName;

        /// <summary>
        /// 是否显示
        /// </summary>
        public readonly bool IsShow;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="typeName">项目类型名称</param>
        /// <param name="isShow">是否显示</param>
        /// <param name="userId">用户ID</param>
        public CreateProjectTypeCommand(
            string typeName, 
            bool isShow, 
            int userId)
        {
            Guard.IsNotNullOrEmpty(typeName, "typeName");
            Guard.IsNotZeroOrNegative(userId, "userId");
            TypeName = typeName;
            IsShow = isShow;
            UserId = userId;
        }
    }
}
