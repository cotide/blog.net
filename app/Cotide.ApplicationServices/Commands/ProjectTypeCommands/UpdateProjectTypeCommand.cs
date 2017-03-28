using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ProjectTypeCommands
{
    /// <summary>
    /// 修改项目类型命令
    /// </summary>
    public class UpdateProjectTypeCommand : CommandBase
    {
        public readonly int ProjductTypeId;

        /// <summary>
        /// 项目类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projductTypeId"></param>
        public UpdateProjectTypeCommand(int projductTypeId)
        {
            Guard.IsNotZeroOrNegative(projductTypeId, "projductTypeId");
            ProjductTypeId = projductTypeId;
        }
    }
}
