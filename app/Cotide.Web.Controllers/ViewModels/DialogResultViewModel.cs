//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DialogResultViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/11/24 23:22:34 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System.ComponentModel;

namespace Cotide.Web.Controllers.ViewModels
{
    /// <summary>
    /// 弹窗结果视图ViewModel
    /// </summary>
    public class DialogResultViewModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success;
        /// <summary>
        /// 提示文本
        /// </summary>
        public string Message;
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClose { get; set; }
        /// <summary>
        /// 关闭时间
        /// </summary>
        public int Colsed { get; set; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }

 
    /// <summary>
    /// 结果
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// 正确
        /// </summary>
        [Description("正确")]
        Right,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error 

    }
}
