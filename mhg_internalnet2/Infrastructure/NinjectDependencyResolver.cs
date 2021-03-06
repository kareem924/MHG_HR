﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service;
using Domain.Service.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;
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
            kernel.Bind<IRepository<ApplicationUser>>().To<EfRepository<ApplicationUser>>();
            kernel.Bind<IUseresRolesServices>().To<UserRolesService>();
            kernel.Bind<IRepository<IdentityRole>>().To<EfRepository<IdentityRole>>();
            kernel.Bind<IRolesService>().To<RolesSerivce>();
            kernel.Bind<IRepository<Job>>().To<EfRepository<Job>>();
            kernel.Bind<IJobService>().To<JobsService>();
            kernel.Bind<IRepository<Branch>>().To<EfRepository<Branch>>();
            kernel.Bind<IBranchService>().To<BranchService>();
            kernel.Bind<IRepository<Brand>>().To<EfRepository<Brand>>();
            kernel.Bind<IBrandService>().To<BrandService>();
            kernel.Bind<IRepository<UserDocuments>>().To<EfRepository<UserDocuments>>();
            kernel.Bind<IUserDocumentsService>().To<UserDocumentsService>();
            kernel.Bind<IRepository<OfficalHolidays>>().To<EfRepository<OfficalHolidays>>();
            kernel.Bind<IOfficalHolidaysService>().To<OfficalHolidayService>();
            kernel.Bind<IRepository<Borrow>>().To<EfRepository<Borrow>>();
            kernel.Bind<IBorrowService>().To<BorrowService>();
            kernel.Bind<IRepository<Employee>>().To<EfRepository<Employee>>();
            kernel.Bind<IRepository<Agenda>>().To<EfRepository<Agenda>>();
            kernel.Bind<IRepository<Tasks>>().To<EfRepository<Tasks>>();
            kernel.Bind<ITasksService>().To<TasksService>();
            kernel.Bind<IAgendaService>().To<AgendaService>();
            kernel.Bind<IDependencyAgendaService>().To<DependencyAgendaService>();
            kernel.Bind<IRepository<DependencyAgenda>>().To<EfRepository<DependencyAgenda>>();
            kernel.Bind<IVacationService>().To<VacationService>();
            kernel.Bind<IRepository<Vacation>>().To<EfRepository<Vacation>>();
            kernel.Bind<IVacationTypes>().To<VacationTypeService>();
            kernel.Bind<IRepository<VacationsType>>().To<EfRepository<VacationsType>>();
            kernel.Bind<INotificationService>().To<NotificationService>();
            kernel.Bind<IRepository<Notifications>>().To<EfRepository<Notifications>>();
            kernel.Bind<IAttendancesService>().To<AttendancesService>();
            kernel.Bind<IRepository<Attendance>>().To<EfRepository<Attendance>>();
        }
    }
}