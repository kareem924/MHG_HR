using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class BorrowViewModel
    {
        public int BorrowId { get; set; }
        public string Describtion { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public DateTime BorrowDate { get; set; }
        public int PaymentNumber { get; set; }
        public decimal MonthlyInstallment { get; set; }
        public string UserId { get; set; }
        public List<BorrowDistributionViewModel> BorrowDistributions { get; set; }
    }
}