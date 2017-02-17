using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Abstract
{
   public interface IUserDocumentsService
    {
        void DeleteUserDocument(UserDocuments userDocument);
        UserDocuments GetUserDocumentById(int UserDocumentId);
        void InsertUserDocument(UserDocuments userDocument);
        void UpdateUserDocument(UserDocuments userDocument);
        IEnumerable<UserDocuments> GetAllUserDocuments();
        IEnumerable<UserDocuments> GetAllUserDocumentByUserId(int userId);
    }
}
