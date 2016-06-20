using System.Collections;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Service.Abstract
{
    public interface IBranchService
    {
        void DeleteBranch(Branch branch);
        Branch GetBranchById(int branchId);
        void InsertBranch(Branch branch);
        void UpdateBranch(Branch branch);
        IEnumerable<Branch> GetAllBranches();
        IEnumerable<Branch> GetAllBranchesByBrandId(int brandId);
    }
}
