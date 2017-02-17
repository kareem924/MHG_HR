using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
    public class UserRolesService : IUseresRolesServices
    {
        #region fields

        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Employee> _employeeRepository;

        public UserRolesService(IRepository<Users> usersRepository, IRepository<Employee> employeeRepository)
        {
            _usersRepository = usersRepository;
            _employeeRepository = employeeRepository;
        }

        public void DeleteUser(Users user)
        {
            
        }

        #endregion
        public IEnumerable<Users> GetAllUsers()
        {
            return _usersRepository.Table;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.Table;
        }
        public Users GetUser(string userId)
        {
            return _usersRepository.GetById(userId);
        }

        /// <summary>
        /// Inserts an Users
        /// </summary>
        /// <param name="user">user</param>
        public virtual void InsertUser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _usersRepository.Insert(user);
        }

        /// <summary>
        /// Updates the blog post
        /// </summary>
        /// <param name="user">user</param>
        public virtual void UpdateUser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _usersRepository.Update(user);
        }

    }
}
