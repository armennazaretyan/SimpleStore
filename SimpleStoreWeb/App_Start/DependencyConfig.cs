using BusinessLayer.InfoServices;
using BusinessLayer.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleStoreWeb.App_Start
{
    public class DependencyConfig
    {
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
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

        private void AddBindings()
        {
            //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>();//.WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<ICategoryService>().To<CategoryService>();//.WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<IProductService>().To<ProductService>();//.WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            kernel.Bind<IOrderService>().To<OrderService>();//.WithConstructorArgument("unit", m => m.Kernel.Get<IUnitOfWork>());
            //kernel.Inject(Membership.Provider);
        }
    }
}