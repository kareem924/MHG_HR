using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Service.Abstract
{
   public interface IRolesService
    {
        IEnumerable<IdentityRole> GetAllRoles();
    }
}
