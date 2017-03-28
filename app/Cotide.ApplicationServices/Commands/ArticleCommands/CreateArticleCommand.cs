using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleCommands
{
    /// <summary>
    /// 创建文章命令
    /// </summary>
    public class CreateArticleCommand : CommandBase
    { 
        /// <summary>
        /// 文章标题
        /// </summary>   
        public readonly string Title;

        /// <summary>
        /// 文章所属用户
        /// </summary> 
        public readonly int UserId;

        /// <summary>
        /// 文章内容
        /// </summary> 
        public readonly string Content;



        /// <summary>
        /// 文章类别
        /// </summary> 
        public int? ArticleTypeId { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public int[] ArticleTagIds { get; set; }

        /// <summary>
        /// 引用通告
        /// </summary>
        public string UrlQuoteUrl { get; set; }

        /// <summary> 
        /// 是否前端显示 默认为True
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 文章内容简述
        /// </summary>
        public string ContentDesc { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="userId">用户ID</param>
        /// <param name="content">内容</param> 
        /// <param name="articleTagIds">文章标签ID列表</param>
        public CreateArticleCommand(
            string title, 
            int userId,
            string content,
            params int[] articleTagIds)
        {
            Guard.IsNotNull(title, "title");
            Guard.IsNotZeroOrNegative(userId, "userId");  
            Title = title.Trim();
            UserId = userId;
            Content = content; 
            if (articleTagIds != null)
            {
                ArticleTagIds = articleTagIds;
            } 
            IsShow = true;
        }

    }
}
