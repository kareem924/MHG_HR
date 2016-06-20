using System.Collections.Generic;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Service.Abstract
{
    public interface IJobService
    {
        IEnumerable<Job> GetAllJobs();
        IEnumerable<Job> GetAllJobsByDepartment(int departmentId);
    }
}
