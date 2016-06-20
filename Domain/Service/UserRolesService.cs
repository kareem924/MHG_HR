using System;
using System.Collections.Generic;

using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
    public class UserRolesService : IUseresRolesServices
    {
        #region fields

        private readonly IRepository<ApplicationUser> _usersRepository;

        public UserRolesService(IRepository<ApplicationUser> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void DeleteUser(ApplicationUser user)
        {
            
        }

        #endregion
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _usersRepository.Table;
        }

        public ApplicationUser GetUser(string userId)
        {
            return _usersRepository.GetById(userId);
        }

        /// <summary>
        /// Inserts an User
        /// </summary>
        /// <param name="user">user</param>
        public virtual void InsertUser(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _usersRepository.Insert(user);
        }

        /// <summary>
        /// Updates the blog post
        /// </summary>
        /// <param name="user">user</param>
        public virtual void UpdateUser(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _usersRepository.Update(user);
        }

    }
}
