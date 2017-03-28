using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Cotide.Infrastructure.NHibernateMaps.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention, IConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }
}
