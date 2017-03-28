using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleCommands
{
    /// <summary>
    /// 修改文章命令
    /// </summary>
    public class UpdateArticleCommand : CommandBase
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
        /// 文章标题
        /// </summary>   
        public string Title { get; set; }
         
        /// <summary>
        /// 阅读总数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary> 
        public string Content { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public int[] ArticleTagIds { get; set; }
         
        /// <summary>
        /// 文章类别
        /// </summary> 
        public int? ArticleTypeId { get; set; }

        /// <summary>
        /// 引用通告
        /// </summary>
        public string UrlQuoteUrl { get; set; }

        /// <summary>
        /// 是否前端显示 默认为True
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 是否进行用户权限判断 
        /// </summary>
        public bool IsCheckUserState { get;private set; }

        /// <summary>
        /// 文章简述
        /// </summary>
        public string ContentDesc { get; set; }

        /// <summary>
        /// 构造函数 (权限验证)
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="userId">操作用户ID</param>
        public UpdateArticleCommand(int articleId, int userId)
        {
            Guard.IsNotZeroOrNegative(userId, "userId");
            Guard.IsNotZeroOrNegative(articleId, "articleId");
            ArticleId = articleId;
            UserId = userId;
            IsCheckUserState = true; 
        }

        /// <summary>
        /// 构造函数 (非权限验证)
        /// </summary>
        /// <param name="articleId"></param>
        public UpdateArticleCommand(int articleId)
        {
            Guard.IsNotZeroOrNegative(articleId, "articleId");
            ArticleId = articleId;
            IsCheckUserState = false;
        }

    }
}
