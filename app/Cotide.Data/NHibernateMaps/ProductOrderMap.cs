//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductOrderMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 15:37:38 
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
    public class ProductOrderMap : IAutoMappingOverride<ProductOrder>
    {
        
        public void Override(AutoMapping<ProductOrder> mapping)
        { 
            mapping.Map(x => x.OrderNo).Length(50);
            mapping.References(x => x.User);
            mapping.HasMany(x => x.ProductOrderItems);
            mapping.Map(x => x.OrderPayStatus).CustomType<ProductOrderPayStatus>();
            mapping.Map(x => x.Phone).Length(15);
            mapping.Map(x => x.Address).Length(500);
            mapping.Map(x => x.Ip).Length(15);
        }
         
    }
}
