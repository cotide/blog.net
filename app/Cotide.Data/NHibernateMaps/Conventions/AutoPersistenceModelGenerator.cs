using System;
using Cotide.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using SharpArch.Domain.DomainModel;
using SharpArch.NHibernate.FluentNHibernate;

namespace Cotide.Infrastructure.NHibernateMaps.Conventions
{

    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {

        #region IAutoPersistenceModelGenerator Members

        public AutoPersistenceModel Generate()
        {
            return AutoMap.AssemblyOf<Article>(new AutomappingConfiguration())
                .Conventions.Setup(GetConventions())
                .IgnoreBase<Entity>()
                .IgnoreBase(typeof(EntityWithTypedId<>))
                .UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
        }

        #endregion

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<ForeignKeyConvention>();
                c.Add<HasManyConvention>();
                c.Add<HasManyToManyConvention>();
                c.Add<ManyToManyTableNameConvention>();
                c.Add<PrimaryKeyConvention>();
                c.Add<ReferenceConvention>();
                c.Add<TableNameConvention>();
            };
        }
    }
}
