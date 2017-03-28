using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class LinkMap : IAutoMappingOverride<Link>
    {
        #region Implementation of IAutoMappingOverride<Link>

        public void Override(AutoMapping<Link> mapping)
        {
            mapping.Map(x => x.LinkTxt).Length(200);
            mapping.References(x => x.LinkType);
            mapping.Map(x => x.Description).Length(500);
            mapping.Map(x => x.LinkUrl).Length(200);
            mapping.Map(x => x.Img).Length(200);
            mapping.References(x => x.User); 
            mapping.Map(x => x.Description).Length(500);
        }

        #endregion
    }
}
