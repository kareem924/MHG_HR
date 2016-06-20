
using System.Collections.Generic;


namespace Domain.Service.Abstract
{
    public interface IUseresRolesServices
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        void InsertUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(ApplicationUser user);
        ApplicationUser GetUser(string userId);


    }
}
