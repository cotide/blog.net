//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleTagMap.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/4 18:13:19 
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
    public class ArticleTagMap : IAutoMappingOverride<ArticleTag>
    {
        #region Implementation of IAutoMappingOverride<ArticleTag>

        public void Override(AutoMapping<ArticleTag> mapping)
        {
            mapping.Map(x => x.TagName).Length(50);
            mapping.References(x => x.User); 
            mapping.HasManyToMany(m => m.Articles).ParentKeyColumn("ArticleTagId").ChildKeyColumn("ArticleId").Table("ArticleTags_Article").Cascade.All();
        }

        #endregion
    }
}
