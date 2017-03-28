using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleTagCommands
{
    /// <summary>
    /// 删除文章标签命令
    /// </summary>
    public class DeleteArticleTagCommand : CommandBase
    {
        /// <summary>
        /// 文章标签ID
        /// </summary>
        public readonly int ArticleTagId;

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="articleTagId">文章标签ID</param>
        public DeleteArticleTagCommand(int articleTagId)
        {
            Guard.IsNotZeroOrNegative(articleTagId, "articleTagId");
            ArticleTagId = articleTagId;
        }
    }
}
