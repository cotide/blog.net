//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserAddressMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 16:09:46 
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
    public class UserAddressMap : IAutoMappingOverride<UserAddress>
    { 
        public void Override(AutoMapping<UserAddress> mapping)
        {
            mapping.References(x => x.User);
            mapping.Map(x => x.Country).Length(15);
            mapping.Map(x => x.Provice).Length(15);
            mapping.Map(x => x.City).Length(15);
            mapping.Map(x => x.ZipCode).Length(10);
            mapping.Map(x => x.Address).Length(500);
        }
         
    }
}
