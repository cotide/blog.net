//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleReplyMessageMap.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/16 23:42:31 
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
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace Cotide.Infrastructure.NHibernateMaps
{
    //public class ArticleReplyMessageMap : SubclassMap<ArticleReplyMessage>
    //{
    //    public ArticleReplyMessageMap()
    //    {
    //        Table(MapConst.Mapname + "ArticleReplyMessages"); 
    //        References(m => m.BaseArticleReplyMessage);
    //        References(m => m.BaseArticleMessage);
    //        HasMany(x => x.ArticleReplyMessages);
    //        DiscriminatorValue("1");
    //    }
    //}

    //public class ArticleReplyMessageMap : SubclassMap<ArticleReplyMessage>
    //{
         
    //    public ArticleReplyMessageMap()
    //    { 
    //        References(m => m.ArticleMessage);
    //        References(m => m.BaseArticleReplyMessage);
    //        DiscriminatorValue("1");
    //    }

    //}
}
