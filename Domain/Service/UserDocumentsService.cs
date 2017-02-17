using Domain.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Service
{
   public class UserDocumentsService : IUserDocumentsService
    {
        private readonly IRepository<UserDocuments> _userDocuementsRepository;
        public UserDocumentsService(IRepository<UserDocuments> userDocuementsRepository)
        {
            _userDocuementsRepository = userDocuementsRepository;
        }
        public void DeleteUserDocument(UserDocuments userDocument)
        {
            _userDocuementsRepository.Delete(userDocument);
        }
        public IEnumerable<UserDocuments> GetAllUserDocumentByUserId(int userId)
        {
            return _userDocuementsRepository.Table.Where(x => x.UserId == userId);
        }
        public IEnumerable<UserDocuments> GetAllUserDocuments()
        {
            return _userDocuementsRepository.Table;
        }
        public UserDocuments GetUserDocumentById(int UserDocumentId)
        {
            return _userDocuementsRepository.GetById(UserDocumentId);
        }
        public void InsertUserDocument(UserDocuments userDocument)
        {
            if (userDocument == null)
                throw new ArgumentNullException("userDocument");
            _userDocuementsRepository.Insert(userDocument);
        }
        public void UpdateUserDocument(UserDocuments userDocument)
        {
            if (userDocument == null)
                throw new ArgumentNullException("branch");
            _userDocuementsRepository.Update(userDocument);
        }
    }
}
