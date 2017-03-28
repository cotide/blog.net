using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.LinkCommands
{
    /// <summary>
    /// 创建友情链接命令
    /// </summary>
    public class CreateLinkCommand : CommandBase
    {

        /// <summary>
        /// 链接文本
        /// </summary>
        public readonly string LinkTxt;

        /// <summary>
        /// 链接地址
        /// </summary>
        public readonly string LinkUrl;

        /// <summary>
        /// 文章所属用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 链接描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 链接图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 前端是否显示 默认为True
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 链接类型ID
        /// </summary>
        public int LinkTypeId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="linkTxt">链接文本</param>
        /// <param name="linkTypeId">链接类型ID</param>
        /// <param name="linkUrl">链接地址</param>
        /// <param name="userId">用户ID</param>
        public CreateLinkCommand(
            string linkTxt, 
            int linkTypeId,
            string linkUrl, 
            int userId)
        {
            Guard.IsNotNullOrEmpty(linkTxt, "linkTxt");
            Guard.IsNotZeroOrNegative(linkTypeId, "linkTypeId");
            Guard.IsNotNullOrEmpty(linkUrl, "linkUrl");
            Guard.IsNotZeroOrNegative(userId, "userId");
            LinkTxt = linkTxt;
            LinkTypeId = linkTypeId;
            LinkUrl = linkUrl;
            UserId = userId;
            IsShow = true;
        }
    }
}
