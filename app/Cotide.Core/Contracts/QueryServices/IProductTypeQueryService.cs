//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IProductTypeQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 14:19:47 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 商品类型接口
    /// </summary>
    public interface IProductTypeQueryService
    { 
        /// <summary>
        /// 获取商品类型
        /// </summary> 
        /// <returns></returns>
        DataTable GetAllToDataTable();
    }
}
