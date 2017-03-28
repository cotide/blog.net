//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductAttrType.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/11/14 16:44:40 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 商品扩展属性类型
    /// </summary>
    public enum ProductAttrType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Image = 0,

        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text = 1
    }
 
}
