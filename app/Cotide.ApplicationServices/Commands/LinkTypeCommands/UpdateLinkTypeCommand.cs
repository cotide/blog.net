using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.LinkCommands
{
    public class UpdateLinkTypeCommand : CommandBase
    {
        /// <summary>
        /// 链接类型ID
        /// </summary>
        public readonly int LinkTypeId;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="linkTypeId">链接ID</param>
        /// <param name="userId">用户ID</param>
        public UpdateLinkTypeCommand(int linkTypeId, int userId)
        {
            Guard.IsNotZeroOrNegative(linkTypeId, "linkTypeId");
            Guard.IsNotZeroOrNegative(userId, "userId");
            LinkTypeId = linkTypeId;
            UserId = userId;
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
         
        /// <summary>
        /// 前端是否显示 默认为否
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
    }
}
