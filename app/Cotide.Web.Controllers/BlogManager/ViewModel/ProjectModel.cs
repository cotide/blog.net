using System;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    /// <summary>
    /// 项目视图
    /// </summary>
    public class ProjectModel
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
        /// 项目类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public   bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public   DateTime? LastDateTime { get; set; }
    }
}
