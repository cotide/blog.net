//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/2/19 20:48:34 
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
using Cotide.Domain.Enum;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class AdMap : IAutoMappingOverride<Ad>
    {  
        public void Override(AutoMapping<Ad> mapping)
        {
            mapping.Map(x => x.AdName).Length(50);
            mapping.Map(x => x.AdDesc).Length(100);
            mapping.Map(x => x.AdImg).Length(200); 
            mapping.Map(x => x.StandardAdImg).Length(200);
            mapping.Map(x => x.SmallAdImg).Length(200);
            mapping.Map(x => x.AdType).CustomType<AdType>();
        }
         
    }
}
