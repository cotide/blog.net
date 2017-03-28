using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Framework.Caching.Providers;
using Cotide.Framework.Commands;
using Cotide.Framework.File;
using Cotide.Framework.Mapper;
using Cotide.Framework.UnitOfWork;
using Cotide.Infrastructure.NHibernateMaps;
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using Cotide.Infrastructure.Repositories.Extension;
using Cotide.Web.Controllers.Blog;
using NHibernate; 
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.NHibernate.FluentNHibernate;
using SharpArch.Web.Mvc.Castle; 

namespace Tests
{
    /// <summary>
    /// 依赖注入辅助类
    /// </summary>
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddTasksTo(container);
            AddQueryServicesTo(container);
            AddMappersTo(container);

            container.Register(Component.For<CommandProcessor, ICommandProcessor>());
            container.Register(Component.For<FileAttachmentUtility, IFileAttachmentUtility>());
            container.Register(Component.For<HttpRuntimeCache, ICache>());
            container.Register(Component.For<Log4NetLogger, ILogger>());
            container.Register(Component.For<EnsNhUnitOfWork, IUnitOfWork>().Named("uow"));
            container.Register(Component.For<DefaultSessionFactoryKeyProvider, ISessionFactoryKeyProvider>().Named("ISessionFactoryKeyProvider"));

            container.Register(Component.For<AutoPersistenceModelGenerator, IAutoPersistenceModelGenerator>().Named("IAutoPersistenceModelGenerator"));

        }

        static void AddMappersTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssembly(Assembly.GetAssembly(typeof(BlogController))).Where(m => m.Namespace != null && m.Namespace.Contains(".Mappers"))
                    .WithService.FirstInterface());
            container.Register(Component.For(typeof(IMapper<,>)).ImplementedBy(typeof(BaseMapper<,>)).Named("mapper1"));
            container.Register(Component.For(typeof(IMapper<,,>)).ImplementedBy(typeof(BaseMapper<,,>)).Named("mapper2"));
        }

        static void AddTasksTo(IWindsorContainer container)
        {

            container.Register(
           AllTypes
           .FromAssemblyNamed("Cotide.Tasks").Pick()
           .WithService.FirstInterface());
        }

        static void AddQueryServicesTo(IWindsorContainer container)
        {

            container.Register(
             AllTypes
                 .FromAssemblyNamed("Cotide.QueryServices").Pick()
                 .WithService.FirstInterface());

        }

        static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                .FromAssemblyNamed("Cotide.Infrastructure").Pick()
                .WithService.FirstNonGenericCoreInterface("Cotide.Domain"));


        }

        static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(Component.For<EntityDuplicateChecker, IEntityDuplicateChecker>().Named("entityDuplicateChecker"));
            container.Register(Component.For(typeof(INHibernateRepository<>)).ImplementedBy(
                typeof(NHibernateRepository<>)).Named("nhibernateRepositoryType"));
            container.Register(Component.For(typeof(INHibernateRepositoryWithTypedId<,>)).ImplementedBy(
                typeof(NHibernateRepositoryWithTypedId<,>)).Named("nhibernateRepositoryWithTypedId"));

            container.AddComponent("dbProxyRepositoryType",
                                 typeof(IDbProxyRepository<>), typeof(DbProxyRepository<>));

            container.AddComponent("dbProxyRepositoryWithTypedId",
                                   typeof(IDbProxyRepositoryWithTypedId<,>),
                                   typeof(DbProxyRepositoryWithTypedId<,>));
        }
    }
}
