//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductImgMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 15:18:41 
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
    public class ProductImgMap : IAutoMappingOverride<ProductImg>
    { 
        public void Override(AutoMapping<ProductImg> mapping)
        {
            mapping.Map(x => x.Name).Length(25);
            mapping.Map(x => x.ImgTip).Length(25);
            mapping.Map(x => x.FileName).Length(30);
            mapping.Map(x => x.AccessLocation).Length(200); 
        }
         
    }
}
