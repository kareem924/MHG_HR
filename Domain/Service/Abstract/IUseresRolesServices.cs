
using System.Collections;
using System.Collections.Generic;
using Domain.Entities;


namespace Domain.Service.Abstract
{
    public interface IUseresRolesServices
    {
        IEnumerable<Users> GetAllUsers();
        IEnumerable<Employee> GetAllEmployees();
        void InsertUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);
        Users GetUser(string userId);


    }
}
