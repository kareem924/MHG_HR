using System.Linq;
using Domain.Entities;

namespace Domain.Service.Abstract
{
    public interface IDepartmentsService
    {
        /// <summary>
        /// Gets all Departments
        /// </summary>
        IQueryable<Departments> GetAllDepartments();
    }
}