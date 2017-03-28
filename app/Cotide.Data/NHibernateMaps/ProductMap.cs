//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 14:54:11 
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
    public class ProductMap : IAutoMappingOverride<Product>
    {
        public void Override(AutoMapping<Product> mapping)
        {
            mapping.Map(x => x.ProductName).Length(200);
            mapping.Map(x => x.ProductNo).Length(50);
            mapping.References(x => x.ProductType); 
            mapping.HasMany(x => x.ProductImgs);
            mapping.HasManyToMany(m => m.ProductTags)
                .ParentKeyColumn("ProductId")
                .ChildKeyColumn("ProductTagsId")
                .Table("ProductTags_Product");
            mapping.Map(x => x.Description).Length(4001);
            mapping.Map(x => x.SimpleDescription).Length(4001); 
          //  mapping.Map(x => x.ProductAttrXml).Length(4001);
            mapping.Map(x => x.Remark).Length(255);
            mapping.Map(x => x.Title).Length(100);
            mapping.Map(x => x.MetaKeywords).Length(150);
        } 
    }
}
