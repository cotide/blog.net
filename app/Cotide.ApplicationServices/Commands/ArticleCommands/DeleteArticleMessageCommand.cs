using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleCommands
{
    /// <summary>
    /// 删除文章留言命令
    /// </summary>
    public class DeleteArticleMessageCommand : CommandBase
    {
         /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 文章留言ID
        /// </summary>
        public readonly int ArticleMessageId;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="articleMessageId">文章留言ID</param>
        public DeleteArticleMessageCommand(int userId, int articleMessageId)
        {
            Guard.IsNotZeroOrNegative(userId, "userId");
            Guard.IsNotZeroOrNegative(articleMessageId, "articleMessageId");
            UserId = userId;
            ArticleMessageId = articleMessageId;
        }
    }
}
