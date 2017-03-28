using System.Web.UI;
using Castle.Windsor; 
using CommonServiceLocator.WindsorAdapter;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Domain.PersistenceSupport;

namespace Tests
{
    public class ServiceLocatorInitializer
    {
        public static void Init()
        {
            IWindsorContainer container = new WindsorContainer(); 
            ComponentRegistrar.AddComponentsTo(container); 
            NHibernateProfiler.Initialize();
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}
