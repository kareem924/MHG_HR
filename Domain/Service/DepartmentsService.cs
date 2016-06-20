using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
    public class DepartmentsService : IDepartmentsService
    {
        #region fields
        private readonly IRepository<Departments> _departmentRepository;
        #endregion
        #region ctor
        public DepartmentsService(IRepository<Departments> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion
        #region Methods
        public IQueryable<Departments> GetAllDepartments()
        {
            return _departmentRepository.Table;
        }
        #endregion

    }
}
