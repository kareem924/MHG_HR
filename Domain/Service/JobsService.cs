using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
    public class JobsService: IJobService
    {
        #region fields
        private readonly IRepository<Job> _jobRepository;

        public JobsService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        #endregion
        public IEnumerable<Job> GetAllJobs()
        {
            return _jobRepository.Table;
        }

        public IEnumerable<Job> GetAllJobsByDepartment(int departmentId)
        {
            return _jobRepository.Table.Where(x=>x.DepartmentId==departmentId);
        }
    }
}
