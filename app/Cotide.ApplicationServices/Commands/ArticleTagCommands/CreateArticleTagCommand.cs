using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleTagCommands
{
    /// <summary>
    /// 创建文章标签命令
    /// </summary>
    public class CreateArticleTagCommand : CommandBase
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public readonly string TagName;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 是否前端显示 默认为True
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tagName">标签名称</param>
        /// <param name="userId">用户ID</param>
        public CreateArticleTagCommand(string tagName, int userId)
        {
            Guard.IsNotNullOrEmpty(tagName, "tagName");
            Guard.IsNotZeroOrNegative(userId, "userId");
            TagName = tagName.Trim();
            UserId = userId;
            IsShow = true;
        }
    }
}
