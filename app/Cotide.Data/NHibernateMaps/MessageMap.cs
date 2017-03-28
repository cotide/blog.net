//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：MessageMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/17 12:14:32 
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
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using FluentNHibernate.Mapping;

namespace Cotide.Infrastructure.NHibernateMaps
{
    //public class MessageMap : ClassMap<Message>
    //{
    //    public MessageMap()
    //    {
    //        Table(MapConst.Mapname + "ArticleMessages");
    //        Id(m => m.Id).Column("ArticleMessageId").GeneratedBy.HiLo(MapConst.Mapname + "hibernate_unique_key", "next_hi", "0",
    //                                                                p =>
    //                                                                p.AddParam("where", string.Format("table_name = '{0}'", MapConst.Mapname + "ArticleMessages")));
    //        Map(m => m.Content).Length(4001);
    //        Map(m => m.CreateDate);
    //        Map(m => m.IsShow);
    //        References(m => m.User);
    //        Map(m => m.NickName).Length(100);
    //        Map(m => m.Email).Length(50);
    //        Map(m => m.WebSiteUrl).Length(200);
    //        DiscriminateSubClassesOnColumn("OwnerType");
    //    }
    //}
}
