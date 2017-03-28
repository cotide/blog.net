using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{ 
    /// <summary>
    /// Ztree配置项
    /// </summary>
    public class ZtreeOption
    {
        public ZtreeOption()
        { 
        }

        /// <summary>
        /// 异步配置
        /// </summary>
        public AsyncOption Async;

        /// <summary>
        /// 显示配置
        /// </summary>
        public ViewOption View;

        /// <summary>
        /// 回调事件配置
        /// </summary>
        public CallbackOption CallbackOption;

    } 

    /// <summary>
    /// 异步配置
    /// </summary>
    public class AsyncOption
    {
        public AsyncOption()
        {
            Type = "get";
            DataType = "text";
            Enable = false;
        }

        /// <summary>
        /// 表示异步加载采用 post/get 方法请求 
        /// 默认值为:"get"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Ajax 获取的数据类型。[Enable = true 时生效]
        /// 默认为："text"
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Ajax 获取数据的 URL 地址。[Enable = true 时生效]
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Ajax 请求提交的静态参数键值对。[Enable = true 时生效] 
        /// key : 参数名
        /// value : 参数值
        /// </summary>
        public IDictionary<string, string> OtherParam;

        /// <summary>
        /// 设置 zTree 是否开启异步加载模式
        /// </summary>
        public bool Enable { get; set; }

    } 
     
    /// <summary>
    /// 显示配置
    /// </summary>
    public class ViewOption
    {
        public ViewOption()
        {
            AutoCancelSelected = true;
            DblClickExpand = true;
            SelectedMulti = true;
        }

        /// <summary>
        /// 点击节点时，按下 Ctrl 键是否允许取消选择操作。 
        /// 默认值: true
        /// </summary>
        public bool AutoCancelSelected { get; set; }

        /// <summary>
        /// 双击节点时，是否自动展开父节点的标识
        /// 默认值: true
        /// </summary>
        public bool DblClickExpand { get; set; }

        /// <summary>
        /// 设置是否允许同时选中多位节点。
        /// 默认值: true
        /// </summary>
        public bool SelectedMulti { get; set; }

        /// <summary>
        /// 设置 zTree 是否显示节点的图标。
        /// 默认值：true
        /// </summary>
        public bool ShowIcon { get; set; }

        /// <summary>
        /// 设置 zTree 是否显示节点之间的连线。
        /// 默认值：true
        /// </summary>
        public bool ShowLine { get; set; }
    }

    /// <summary>
    /// 回调事件配置
    /// </summary>
    public class CallbackOption
    {
        public CallbackOption()
        {
            BeforeClick = null;
        }

        /// <summary>
        /// 用于捕获单击节点之前的事件回调函数，并且根据返回值确定是否允许单击操作
        /// 默认值：NULL
        /// </summary>
        public string BeforeClick { get; set; }

        /// <summary>
        /// 用于捕获节点被点击的事件回调函数
        /// 如果设置了 BeforeClick 方法，且返回 false，将无法触发 onClick 事件回调函数。
        /// 默认值：NULL
        /// </summary>
        public string OnClick { get; set; }
    }

    /// <summary>
    /// 树形控件源配置
    /// </summary>
    public class ZtreeSoureOption
    {
        /// <summary>
        /// ID列名
        /// </summary>
        public string IdColumnName { get; set; }

        /// <summary>
        /// 菜单名列名
        /// </summary>
        public string MenuNameColumnName { get; set; }

        /// <summary>
        /// 父节点列名
        /// </summary>
        public string PidColumnName { get; set; }
         
    }
}