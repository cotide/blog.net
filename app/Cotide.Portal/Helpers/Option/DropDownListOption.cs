using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    /// <summary>
    /// 无限极下拉框配置项
    /// </summary>
    public class DropDownListOption
    { 
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DropDownListOption()
        {
            TopParentID = "";
            IsRequired = true;
        }

        /// <summary>
        /// 父级分栏样式
        /// </summary>
        private string _topParentStyle = "|-";

        /// <summary>
        /// 父级分栏样式
        /// </summary>
        public string TopParentStyle
        {
            get { return _topParentStyle; }
            set { _topParentStyle = value; }
        }

        /// <summary>
        /// 子项分栏样式
        /// </summary>
        private string _subkey = "|--";

        /// <summary>
        /// 子项分栏样式
        /// </summary>
        public string Subkey
        {
            get { return _subkey; }
            set { _subkey = value; }
        }

        /// <summary>
        /// 最顶级标识
        /// </summary>
        private string _topTitle = "-请选择分类-";

        public string TopTitle
        {
            get { return _topTitle; }
            set { _topTitle = value; }
        }

        public string TopParentID { get; set; }

        /// <summary>
        /// ID列名 
        /// </summary>
        public string IdColumnsName { get; set; }

        /// <summary>
        /// 父级ID列名
        /// </summary>
        public string BaseIdColumnsName { get; set; }

        /// <summary>
        /// 显示列名
        /// </summary>
        public string DisplayColumnsName { get; set; }

        /// <summary>
        /// 是否支持排序
        /// </summary>
        public bool IsSort { get; set; }

        /// <summary>
        /// 排序列名
        /// </summary>
        public string SortColumnsName { get; set; }


        /// <summary>
        /// 是否必选  默认为是
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 为空描述 
        /// </summary>
        public string EmptyMsg { get; set; }

        /// <summary>
        /// 默认描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 失去焦点描述
        /// </summary>
        public string FocusMsg { get; set; }
    }

}