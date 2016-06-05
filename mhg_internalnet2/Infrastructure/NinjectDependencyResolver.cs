using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service;
using Domain.Service.Abstract;
using Ninject;

namespace mhg_internalnet2.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
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
            // put bindings here
            kernel.Bind<IRepository<Departments>>().To<EfRepository<Departments>>();
            kernel.Bind<IDepartmentsService>().To<DepartmentsService>();
        }
    }
}