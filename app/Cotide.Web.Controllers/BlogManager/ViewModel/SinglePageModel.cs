using System;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class SinglePageModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 单页名称
        /// </summary> 
        public string SinglePageName { get; set; }

        /// <summary>
        /// 单页标题
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 单页内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章所属用户
        /// </summary> 
        public int UserId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 用户菜单名称
        /// </summary>
        public string UserMenuName { get; set; }

        /// <summary>
        /// 用户菜单ID
        /// </summary>
        public int? UserMenuId { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastDateTime { get; set; }
    }
}
