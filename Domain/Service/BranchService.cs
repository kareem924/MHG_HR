using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Service.Abstract;

namespace Domain.Service
{
  public  class BranchService:IBranchService
    {
        private readonly IRepository<Branch> _branchRepository;

        public BranchService(IRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public void DeleteBranch(Branch branch)
        {
             _branchRepository.Delete(branch);
        }

        public Branch GetBranchById(int branchId)
        {
            return _branchRepository.GetById(branchId);
        }

        public void InsertBranch(Branch branch)
        {
            if (branch == null)
                throw new ArgumentNullException("branch");
            _branchRepository.Insert(branch);
        }

        public void UpdateBranch(Branch branch)
        {
            if (branch == null)
                throw new ArgumentNullException("branch");
            _branchRepository.Update(branch);
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return _branchRepository.Table;

        }

        public IEnumerable<Branch> GetAllBranchesByBrandId(int brandId)
        {
            return _branchRepository.Table.Where(x=>x.BrandId==brandId);
        }

       
    }
}
