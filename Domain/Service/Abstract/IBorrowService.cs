using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Abstract
{
    public interface IBorrowService
    {
        void DeleteBorrow(Borrow borrow);
        Borrow GetBorrowById(int borrowId);
        void InsertBorrow(Borrow borrow);
        void UpdateBorrow(Borrow borrow);
        IEnumerable<Borrow> GetAllBorrows();
        IEnumerable<Borrow> GetAllBorrowByUserId(string userId);
    }
}
