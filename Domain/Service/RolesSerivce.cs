using System.Collections.Generic;
using Domain.Repositories;
using Domain.Service.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Service
{
   public class RolesSerivce : IRolesService
    {
        #region Fields

        private readonly IRepository<IdentityRole> _repositoryRole;
        #endregion
        #region ctor
        public RolesSerivce(EfRepository<IdentityRole> repositoryRole )
        {
            _repositoryRole = repositoryRole;
        }
        #endregion
        public IEnumerable<IdentityRole> GetAllRoles()
        {
          return  _repositoryRole.Table;
        }
    }
}
