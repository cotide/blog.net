using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ProjectCommands
{
    /// <summary>
    /// 创建项目命令
    /// </summary>
    public class CreateProjectCommand : CommandBase
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public readonly string ProjectName;

        /// <summary>
        /// 项目简介
        /// </summary>
        public readonly string Introduction;

        /// <summary>
        /// 项目内容
        /// </summary>
        public readonly string Content;

        /// <summary>
        /// 所属用户ID
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 项目类型ID
        /// </summary>
        public readonly int ProductTypeId;

        /// <summary>
        /// 是否显示
        /// </summary>
        public readonly bool IsShow;

        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 项目图片（原图）
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
        /// 构造函数
        /// </summary>
        /// <param name="projectName">项目名称</param>
        /// <param name="introduction">项目简介</param>
        /// <param name="content">项目内容</param>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">是否显示</param>
        /// <param name="productTypeId">项目类型ID</param>
        public CreateProjectCommand(
            string projectName,
            string introduction,
            string content, 
            int userId, 
            bool isShow, 
            int productTypeId)
        {
            Guard.IsNotNullOrEmpty(projectName, "projectName");
            Guard.IsNotNullOrEmpty(introduction, "introduction");
            Guard.IsNotNullOrEmpty(content, "content");
            Guard.IsNotZeroOrNegative(userId, "userId");
            Guard.IsNotZeroOrNegative(productTypeId, "productTypeId");
            ProjectName = projectName;
            Introduction = introduction;
            Content = content;
            UserId = userId;
            IsShow = isShow;
            ProductTypeId = productTypeId;
        }
    }
}
