using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class ArticleTypeMap : IAutoMappingOverride<ArticleType>
    {
        public void Override(AutoMapping<ArticleType> mapping)
        {
            mapping.Map(x => x.TypeName).Length(30);
            mapping.HasMany(x => x.Articles).Cascade.All();
            mapping.References(x => x.User);
            mapping.Map(m => m.IsShow).CustomType(typeof(bool));
        }
    } 
}
