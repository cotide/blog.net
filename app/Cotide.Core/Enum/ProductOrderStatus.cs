//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductOrderStatus.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/29 17:04:51 
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

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum ProductOrderStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 1, 

        /// <summary>
        /// 配送中
        /// </summary>
        Distribution =2,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 3,
        
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 4
    }
}
