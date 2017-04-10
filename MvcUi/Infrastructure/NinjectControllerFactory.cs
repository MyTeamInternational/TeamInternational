using BLL.Abstract;
using BLL.Managers;
using BLL.VMBuilders;
using MvcUi.Controllers;
using Ninject;
using System;
using System.Web.Mvc;
using TeamProject.DAL;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL.Repositories.Interfaces;

namespace MvcUi.Infrastructure
{
    [CustomErrorHandler]
    internal class NinjectControllerFactory : DefaultControllerFactory
    // TODO а можно этот клас использовать как dependency resolver и вообще зачем ресолвер нужен?
    {
        private IKernel kernel;
        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)kernel.Get(controllerType);
        }
        private void AddBindings()
        {
            kernel.Bind<ICinemaWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<IAccountManager>().To<AccountManager>();
            kernel.Bind<IEmailService>().To<MyEmailSender>();
            kernel.Bind<IMovieManager>().To<MovieManager>();
            kernel.Bind<IMovieVMBuilder>().To<MovieVMBuilder>();
        }
    }
}