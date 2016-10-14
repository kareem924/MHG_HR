
using System.Collections;
using System.Collections.Generic;
using Domain.Entities;


namespace Domain.Service.Abstract
{
    public interface IUseresRolesServices
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        IEnumerable<Employee> GetAllEmployees();
        void InsertUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(ApplicationUser user);
        ApplicationUser GetUser(string userId);


    }
}
