using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.Commands.LinkTypeCommands
{
    /// <summary>
    /// 创建链接类型命令
    /// </summary>
    public class CreateLinkTypeCommand : CommandBase
    {
        /// <summary>
        /// 链接类型
        /// </summary>
        public readonly string TypeName;

        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int UserId;


        /// <summary>
        /// 创建链接类型命令
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="userId">用户ID</param>
        public CreateLinkTypeCommand(
            string typeName,
            int userId)
        {
            Guard.IsNotNull(typeName, "typeName");
            Guard.IsNotZeroOrNegative(userId, "userId");
            TypeName = typeName;
            UserId = userId;
            IsShow = false;
            Sort = 0;
        }

        /// <summary>
        /// 前端是否显示 默认为否
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
