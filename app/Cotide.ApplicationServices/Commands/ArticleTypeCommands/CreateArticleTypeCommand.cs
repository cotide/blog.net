using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleTypeCommands
{
    /// <summary>
    /// 创建文章类别命令
    /// </summary>
    public class CreateArticleTypeCommand : CommandBase
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public readonly string TypeName;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 前端是否显示 默认为True
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="userId">用户ID</param>
        public CreateArticleTypeCommand(string typeName,int userId)
        {
            Guard.IsNotNullOrEmpty(typeName, "typeName");
            Guard.IsNotZeroOrNegative(userId, "userId");
            TypeName = typeName.Trim();
            UserId = userId;
            IsShow = true;
        }
    }
}
