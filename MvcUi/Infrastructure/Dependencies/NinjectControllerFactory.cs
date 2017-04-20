using BLL.Abstract;
using BLL.Managers;
using BLL.UrlFlow;
using BLL.VMBuilders;
using MvcUi.Controllers;
using MvcUi.Infrastructure.Auth;
using Ninject;
using System;
using System.Web.Mvc;
using TeamProject.DAL;
using Ninject.Web.Common;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL.Repositories.Interfaces;
using System.Collections.Generic;

namespace MvcUi.Infrastructure
{
    [CustomErrorHandler]
    internal class NinjectControllerFactory : DefaultControllerFactory,IDependencyResolver
    // TODO а можно этот клас использовать как dependency resolver и вообще зачем ресолвер нужен?
    {
        private IKernel kernel;
        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)kernel.Get(controllerType);
        }
        private void AddBindings()
        {
            //правильно ли в формате UnitOfWork делать inject как сингл обьект? 
            kernel.Bind<IAuthentication>().To<CustomAuthentication>().InSingletonScope();
            kernel.Bind<ICinemaWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<IHomeUrlFlow>().To<HomeUrlFlow>().InSingletonScope();
            kernel.Bind<IAccountManager>().To<AccountManager>();
            kernel.Bind<IEmailService>().To<MyEmailSender>();
            kernel.Bind<IMovieManager>().To<MovieManager>();
            kernel.Bind<IMovieVMBuilder>().To<MovieVMBuilder>();
        }
    }
}