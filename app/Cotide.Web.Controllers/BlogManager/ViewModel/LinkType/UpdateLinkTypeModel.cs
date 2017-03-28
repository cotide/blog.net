using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Web.Controllers.BlogManager.ViewModel.LinkType
{
    public class UpdateLinkTypeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id;

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }


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
