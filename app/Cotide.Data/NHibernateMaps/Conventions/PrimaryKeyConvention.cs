using FluentNHibernate.Conventions;

namespace Cotide.Infrastructure.NHibernateMaps.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id"); 
            if (instance.Type == typeof(string))
            {
                instance.UnsavedValue("null");
                instance.GeneratedBy.Assigned(); 
            } 
            else if (instance.Type == typeof(int))
            {
                var tableName = Inflector.Net.Inflector.Pluralize(
                    MapConst.Mapname
                    +
                    instance.EntityType.Name);

                instance.UnsavedValue("0");
                instance.GeneratedBy.HiLo(MapConst.Mapname
                    +
                    "hibernate_unique_key", "next_hi", "0",
                    p => p.AddParam(
                        "where",
                        string.Format(
                        "table_name = '{0}'",
                        tableName)));
            }

        }
    }
}
