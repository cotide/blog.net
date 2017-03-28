using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class LinkTypeMap : IAutoMappingOverride<LinkType>
    {
        #region Implementation of IAutoMappingOverride<Link>

        public void Override(AutoMapping<LinkType> mapping)
        {
            mapping.Map(x => x.TypeName).Length(30);
            mapping.Map(x => x.IsShow);
            mapping.Map(x => x.Sort);
            mapping.Map(x => x.CreateDate);
            mapping.Map(x => x.LastDateTime);
            mapping.References(x => x.User); 
        }

        #endregion
    }
}
