using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleCommands.Base;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleCommands
{
    /// <summary>
    /// 创建文章留言回复命令
    /// </summary>
    public class CreateArticleMessageReplyCommand : BaseCreateArticleMessageCommand
    {
        /// <summary>
        /// 构造函数(用户留言)
        /// </summary>
        /// <param name="articleMessageId">文章留言ID</param>
        /// <param name="rootArticleMessageId"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="content">留言内容</param>
        public CreateArticleMessageReplyCommand(
            int articleMessageId, 
            int userId,
            string content)
            : base(userId, content)
        {
            Guard.IsNotZeroOrNegative(articleMessageId, "articleMessageId"); 
            Guard.IsNotZeroOrNegative(userId, "userId");
            Guard.IsNotNullOrEmpty(content, "content");
            ArticleMessageId = articleMessageId; 
            IsShow = true;
        }

        /// <summary>
        /// 构造函数(匿名用户留言)
        /// </summary>
        /// <param name="articleMessageId"></param> 
        /// <param name="nickName"></param>
        /// <param name="content"></param>
        public CreateArticleMessageReplyCommand(
            int articleMessageId, 
            string nickName,
            string content)
            : base(nickName, content)
        {

            Guard.IsNotZeroOrNegative(articleMessageId, "articleMessageId"); 
            Guard.IsNotNullOrEmpty(nickName, "nickName");
            Guard.IsNotNullOrEmpty(content, "content");
            ArticleMessageId = articleMessageId; 
            IsShow = true;
        }

        /// <summary>
        /// 文章留言ID
        /// </summary>
        public readonly int ArticleMessageId;
         
    }
}
