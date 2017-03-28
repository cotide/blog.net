using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class ArticleMap : IAutoMappingOverride<Article>
    {
        public void Override(AutoMapping<Article> mapping)
        { 
            mapping.Map(x => x.Title).Length(200);
            mapping.References(x => x.User);
            mapping.References(x => x.ArticleType);
            mapping.HasManyToMany(m => m.ArticleTags).ParentKeyColumn("ArticleId").ChildKeyColumn("ArticleTagId").Table("ArticleTags_Article");
            mapping.HasMany(x => x.ArticleMessages);
            mapping.Map(x => x.Content).Length(4001);
            mapping.Map(x => x.ContentDesc).Length(800);
            mapping.Map(x => x.UrlQuoteUrl).Length(200);
            mapping.Map(m => m.IsShow).CustomType(typeof(bool)); 
        }
    }
}
