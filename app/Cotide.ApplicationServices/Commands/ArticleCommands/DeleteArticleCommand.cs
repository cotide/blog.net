using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleCommands
{
    /// <summary>
    /// 删除文章命令
    /// </summary>
    public class DeleteArticleCommand : CommandBase
    {
        /// <summary>
        /// 操作用户ID
        /// </summary>
        public readonly int UserId;
         
        /// <summary>
        /// 文章ID
        /// </summary>
        public readonly int ArticleId;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="userId">操作用户ID</param>
        public DeleteArticleCommand(int articleId, int userId)
        {
            Guard.IsNotZeroOrNegative(articleId, "articleId");
            Guard.IsNotZeroOrNegative(userId,"userId");
            ArticleId = articleId;
            UserId = userId;
        }

    }
}
