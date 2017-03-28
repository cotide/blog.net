//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：MakeImgSize.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/26 22:13:56 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Framework.Enumerable
{
    /// <summary>
    /// 图片缩量方式
    /// </summary>
    public enum ImgModel
    {
        /// <summary>
        /// 指定高宽缩放（可能变形） 
        /// </summary>
        Hw,
        /// <summary>
        /// 指定宽，高按比例   
        /// </summary>
        W,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,
        /// <summary>
        /// 指定高宽裁减（不变形）
        /// </summary>
        Cut

    }

}
