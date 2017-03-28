using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.LinkCommands
{
    /// <summary>
    /// 删除友情链接命令
    /// </summary>
    public class DeleteLinkCommand : CommandBase
    {

        /// <summary>
        /// 链接ID
        /// </summary>
        public readonly int LinkId;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 删除链接构造函数
        /// </summary>
        /// <param name="linkId">友情链接ID</param>
        /// <param name="userId">用户ID</param>
        public DeleteLinkCommand(int linkId, int userId)
        {
            Guard.IsNotZeroOrNegative(linkId, "linkId");
            Guard.IsNotZeroOrNegative(userId, "userId");
            LinkId = linkId;
            UserId = userId;
        }
    }
}
