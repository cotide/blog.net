//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductOrderItemMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 15:58:43 
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
using Cotide.Domain;
using Cotide.Domain.Enum;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class ProductOrderItemMap : IAutoMappingOverride<ProductOrderItem>
    {
        public void Override(AutoMapping<ProductOrderItem> mapping)
        {
            
            mapping.Map(x => x.ProductName).Length(200);
            mapping.Map(x => x.OrderItemNo).Length(50);
            mapping.Map(x => x.OrderStatus).CustomType<ProductOrderStatus>();
            mapping.Map(x => x.Remark).Length(500);
        }

        
    }
}
