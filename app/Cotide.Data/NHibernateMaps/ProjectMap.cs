using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Cotide.Infrastructure.NHibernateMaps
{
    public class ProjectMap : IAutoMappingOverride<Project>
    {
        #region Implementation of IAutoMappingOverride<Project>

        public void Override(AutoMapping<Project> mapping)
        {
            mapping.Map(x => x.ProjectName).Length(200);
            mapping.Map(x => x.ProjectImg).Length(200);
            mapping.Map(x => x.SmallProjectImg).Length(200);
            mapping.Map(x => x.StandardProjectImg).Length(200);
            mapping.Map(x => x.WebSite).Length(200);
            mapping.Map(x => x.Content).Length(4001);
            mapping.Map(x => x.Introduction).Length(4001);
            mapping.References(x => x.User);
        }

        #endregion
    }
}
