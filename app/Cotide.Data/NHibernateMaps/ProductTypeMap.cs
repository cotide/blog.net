//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductTypeMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 15:25:35 
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
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class ProductTypeMap : IAutoMappingOverride<ProductType>
    { 
        public void Override(AutoMapping<ProductType> mapping)
        {
            mapping.Map(x => x.TypeName).Length(25);
            mapping.References(x => x.BaseProductType);
            mapping.HasMany(x => x.Shops);
        } 
    }
}
