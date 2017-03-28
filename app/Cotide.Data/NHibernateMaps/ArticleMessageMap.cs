using Cotide.Domain;
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace Cotide.Infrastructure.NHibernateMaps
{
    //public sealed class ArticleMessageMap :  ClassMap<ArticleMessage>
    //{

    //    #region Implementation of IAutoMappingOverride<ArticleMessage> 
    //    public ArticleMessageMap()
    //    {
    //        Table(MapConst.Mapname + "ArticleMessages");
    //        Id(m => m.Id).Column("ArticleMessageId").GeneratedBy.HiLo(MapConst.Mapname + "hibernate_unique_key", "next_hi", "0",
    //                                                                p =>
    //                                                                p.AddParam("where", string.Format("table_name = '{0}'", MapConst.Mapname + "ArticleMessages")));

    //        Map(m => m.Content).Length(4001);
    //        Map(m => m.CreateDate);
    //        Map(m => m.IsShow);
    //        References(m => m.Article);
    //        HasMany(m => m.ArticleReplyMessages);
    //        References(m => m.User);
    //        Map(m => m.NickName).Length(100);
    //        Map(m => m.Email).Length(50);
    //        Map(m => m.WebSiteUrl).Length(200); 
    //    } 
    //    #endregion
    //}

    public class ArticleMessageMap : IAutoMappingOverride<ArticleMessage>
    {
    
        public void Override(AutoMapping<ArticleMessage> mapping)
        { 
            mapping.Map(m => m.Content).Length(4001);
            mapping.Map(m => m.CreateDate);
            mapping.Map(m => m.IsShow);
            mapping.References(m => m.User);
            mapping.Map(m => m.NickName).Length(100);
            mapping.Map(m => m.Email).Length(50);
            mapping.Map(m => m.WebSiteUrl).Length(200);
            mapping.References(x => x.BaseArticleMessage);
            mapping.References(m => m.Article);
            mapping.HasMany(m => m.ArticleMessages); 
        }
         
    }
}
