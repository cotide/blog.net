using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Web.Controllers.BlogManager.ViewModel.Link
{
    /// <summary>
    /// 链接类型
    /// </summary>
    public class LinkTypeModel
    {
        /// <summary>
        /// 链接类型ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary> 
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary> 
        public DateTime? LastUpdate { get; set; }


        /// <summary>
        /// 前端是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
