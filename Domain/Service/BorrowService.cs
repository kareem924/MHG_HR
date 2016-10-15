using Domain.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Service
{
   public class BorrowService : IBorrowService
    {
        private readonly IRepository<Borrow> _borrowRepository;

        public BorrowService(IRepository<Borrow> borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }
        public void DeleteBorrow(Borrow borrow)
        {
            _borrowRepository.Delete(borrow);
        }
        public IEnumerable<Borrow> GetAllBorrowByUserId(int userId)
        {
            return _borrowRepository.Table.Where(x => x.UserId == userId.ToString());
        }

        public IEnumerable<Borrow> GetAllBorrows()
        {
            return _borrowRepository.Table;
        }

        public Borrow GetBorrowById(int borrowId)
        {
            return _borrowRepository.GetById(borrowId);
        }

        public void InsertBorrow(Borrow borrow)
        {
            if (borrow == null)
                throw new ArgumentNullException("borrow");
            _borrowRepository.Insert(borrow);
        }

        public void UpdateBorrow(Borrow borrow)
        {
            if (borrow == null)
                throw new ArgumentNullException("borrow");
            _borrowRepository.Update(borrow);
        }
    }
}
