using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleTypeCommands
{
    /// <summary>
    /// 修改文章类别命令
    /// </summary>
    public class UpdateArticleTypeCommand : CommandBase
    {
        /// <summary>
        /// 文章类别ID
        /// </summary>
        public readonly int ArticleTypeId;


        /// <summary>
        /// 当前用户ID
        /// </summary>
        public readonly int CurrentUserId;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="articleTypeId">文章类别ID</param>
        public UpdateArticleTypeCommand(int articleTypeId, int currentUserId)
        {
            Guard.IsNotZeroOrNegative(articleTypeId, "articleTypeId");
            Guard.IsNotZeroOrNegative(currentUserId, "currentUserId");
            ArticleTypeId = articleTypeId;
            CurrentUserId = currentUserId;
        }
        
        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 是否前端显示
        /// </summary>
        public bool? IsShow { get; set; }
    }
}
