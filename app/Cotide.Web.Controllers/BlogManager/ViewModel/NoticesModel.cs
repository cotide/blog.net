using System;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class NoticesModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary> 
        public string Content { get; set; }

        /// <summary>
        /// 是否前端显示
        /// </summary> 
        public bool IsShow { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdate { get; set; }
    }
}
