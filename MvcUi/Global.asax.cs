using MvcUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TeamProject.DAL;

namespace MvcUi
{
    [CustomErrorHandler]
    //нужно ли самому настраиваить injects для NinjectHttpModule,OnePerRequestHttpModule,Bootstrapper 
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        ///NLog comit a log into system directiry it's have no need to be injected
        /// </summary>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            //как лучше логировать и какие изменения должын логинится насколько подробно?
            logger.Info("Application Start");
            var Ninject = new NinjectControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(Ninject);
            DependencyResolver.SetResolver(Ninject);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }
        public override void Init()
        {
            logger.Info("Application Init");
        }
        public override void Dispose()
        {
            logger.Info("Application Dispose");
        }
        protected void Application_Error()
        {
            logger.Info("Application Error");
            
        }
        protected void Application_End()
        {
            logger.Info("Application End");
        }
    }
}
