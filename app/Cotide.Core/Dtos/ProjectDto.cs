using System;

namespace Cotide.Domain.Dtos
{
    public class ProjectDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary> 
        public string ProjectName { get; set; }

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
        /// 网址
        /// </summary> 
        public string WebSite { get; set; }

        /// <summary>
        /// 项目简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 项目所属用户
        /// </summary> 
        public int UserId { get; set; }

        /// <summary>
        /// 项目类型ID
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// 项目类型名称
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastDateTime { get; set; }
    }
}
