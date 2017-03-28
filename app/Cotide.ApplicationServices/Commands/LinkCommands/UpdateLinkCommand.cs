using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.LinkCommands
{
    public class UpdateLinkCommand : CommandBase
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public readonly int LinkId;

        /// <summary>
        /// 链接文本
        /// </summary>
        public string LinkTxt { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }


        /// <summary>
        /// 链接描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 链接图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 前端是否显示(默认显示)
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 链接类型ID
        /// </summary>
        public int? LinkTypeId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="linkId">链接ID</param>
        public UpdateLinkCommand(int linkId)
        {
            Guard.IsNotZeroOrNegative(linkId, "linkId");
            LinkId = linkId;
        }
    }
}
