namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class AddProjectModel
    {

        /// <summary>
        /// 项目名称
        /// </summary> 
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目图片
        /// </summary>
        public string ProjectImg { get; set; }

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
        /// 项目类型
        /// </summary>
        public int ProjectTypeId { get; set; }
         
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

    }
}
