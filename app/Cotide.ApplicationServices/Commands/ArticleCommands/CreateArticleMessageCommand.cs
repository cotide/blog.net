using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleCommands.Base;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleCommands
{
    /// <summary>
    /// 创建文章用户留言命令
    /// </summary>
    public class CreateArticleMessageCommand : BaseCreateArticleMessageCommand
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public readonly int ArticleId;

        /// <summary>
        /// 构造函数(用户留言)
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="userId">用户ID Null为匿名用户留言</param>
        /// <param name="content">留言内容</param> 
        public CreateArticleMessageCommand(
            int articleId,
            int userId,
            string content)
            :base(userId,content)
        {
            Guard.IsNotZeroOrNegative(articleId, "articleId"); 
            Guard.IsNotZeroOrNegative(userId,"userId");
            Guard.IsNotNullOrEmpty(content, "content");
            ArticleId = articleId; 
            IsShow = true;
        }


        /// <summary>
        /// 构造函数(匿名用户留言)
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="nickName">匿名用户名</param>
        /// <param name="content">留言内容</param> 
        public CreateArticleMessageCommand(
            int articleId, 
            string nickName,
            string content)
            :base(nickName,content)
        {
            Guard.IsNotZeroOrNegative(articleId, "articleId");
            Guard.IsNotNullOrEmpty(nickName, "nickName");
            Guard.IsNotNullOrEmpty(content, "content");
            ArticleId = articleId; 
            IsShow = true;
        }
         
    }
}
