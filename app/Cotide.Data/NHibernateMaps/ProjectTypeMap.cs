using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class ProjectTypeMap : IAutoMappingOverride<ProjectType>
    {
        #region Implementation of IAutoMappingOverride<ProjectType>

        public void Override(AutoMapping<ProjectType> mapping)
        {
            mapping.Map(x => x.TypeName).Length(30);
            mapping.References(x => x.User);
        }

        #endregion
    }
}
