using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Cotide.Domain.Contracts.Services;
using Cotide.Framework.Extensions;
using Cotide.Infrastructure.NHibernateMaps;
using Cotide.Infrastructure.NHibernateMaps.Conventions;
using Cotide.Portal.CastleWindsor;
using Cotide.Web.Controllers;
using Cotide.Web.Controllers.Blog;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Microsoft.Practices.ServiceLocation;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.Castle;
using log4net.Config;

namespace Cotide.Portal
{
    public class MvcApplication : HttpApplication
    {
        private WebSessionStorage webSessionStorage;
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            if (ConfigurationManager.AppSettings["OpenNHibernateProfiler"] != null
              &&
              ConfigurationManager.AppSettings["OpenNHibernateProfiler"].Equals("true"))
            {
                NHibernateProfiler.Initialize();
            }
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
            XmlConfigurator.Configure();
            InitializeServiceLocator(); 
            // 替换身份验证
            // this.PostAuthenticateRequest += new EventHandler(Application_OnPostAuthenticateRequest); 

        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {

            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            container.RegisterControllers(typeof(BlogController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        public override void Init()
        {
            base.Init();
            // The WebSessionStorage must be created during the Init() to tie in HttpApplication events
            webSessionStorage = new WebSessionStorage(this);
        }

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization cannot reside in Init() but
        /// must only be called once.  Consequently, we invoke a thread-safe singleton class to
        /// ensure it's only initialized once.
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(
                InitializeNHibernateSession);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated && !Request.IsStaticFile())
            {
                var identityService = ServiceLocator.Current.GetInstance<IIdentityService>();
                Context.User = identityService.GetCurrentUser();
            }
        }

        /// <summary>
        /// If you need to communicate to multiple databases, you'd add a line to this method to
        /// initialize the other database as well.
        /// </summary>
        private void InitializeNHibernateSession()
        {
            NHibernateSession.Init(
                webSessionStorage,
                new[] { Server.MapPath("~/bin/Cotide.Infrastructure.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                Server.MapPath("~/NHibernate.config"));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            var ex = Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }
    }
}