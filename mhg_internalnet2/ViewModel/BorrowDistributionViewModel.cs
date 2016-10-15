using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class BorrowDistributionViewModel
    {
        public int BorrowDistributionId { get; set; }
        public int BorrowId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsExempt { get; set; }
        public string  EmpName { get; set; }
        public BorrowViewModel Borrow { get; set; }
    }
}