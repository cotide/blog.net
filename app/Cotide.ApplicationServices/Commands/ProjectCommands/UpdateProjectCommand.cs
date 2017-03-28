using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ProjectCommands
{
    /// <summary>
    /// 修改项目命令
    /// </summary>
    public class UpdateProjectCommand : CommandBase
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public readonly int ProjectId;

        /// <summary>
        /// 项目类型ID
        /// </summary>
        public int? ProjectTypeId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 所属用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 项目图片
        /// </summary>
        public string ProjectImg { get; set; }
         
        /// <summary>
        /// 项目图片(小图)
        /// </summary>
        public string SmallProjectImg { get; set; }

        /// <summary>
        /// 项目图片(标准图)
        /// </summary>
        public string StandardProjectImg { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectId">项目ID</param>
        public UpdateProjectCommand(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
