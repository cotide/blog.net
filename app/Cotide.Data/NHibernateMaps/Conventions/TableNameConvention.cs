using FluentNHibernate.Conventions;

namespace Cotide.Infrastructure.NHibernateMaps.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            instance.Table(Inflector.Net.Inflector.Pluralize(MapConst.Mapname + instance.EntityType.Name));
        }
    }
}
