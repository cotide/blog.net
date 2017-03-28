using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.LinkCommands
{
    /// <summary>
    /// 删除友情链接命令
    /// </summary>
    public class DeleteLinkTypeCommand : CommandBase
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
        /// 删除链接构造函数
        /// </summary>
        /// <param name="linkTypeId">链接类型ID</param>
        /// <param name="userId">用户ID</param>
        public DeleteLinkTypeCommand(int linkTypeId, int userId)
        {
            Guard.IsNotZeroOrNegative(linkTypeId, "linkTypeId");
            Guard.IsNotZeroOrNegative(userId, "userId");
            LinkTypeId = linkTypeId;
            UserId = userId;
        }
    }
}
