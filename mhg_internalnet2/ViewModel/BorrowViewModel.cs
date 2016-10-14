using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace mhg_internalnet2.ViewModel
{
    public class BorrowViewModel
    {
        public int BorrowId { get; set; }
        public string Describtion { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MMMM yyyy}")]
        public DateTime FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MMMM yyyy}")]
        public DateTime ToDate { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }
        public DateTime BorrowDate { get; set; }
        public int PaymentNumber => MonthDifference(FromDate, ToDate);
        public string EmployeeName { get; set; }
        public decimal MonthlyInstallment => Amount / PaymentNumber;

        [Required]
        public string UserId { get; set; }
        public List<BorrowDistributionViewModel> BorrowDistributions { get; set; }

        public int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year)) + 1;
        }
    }
}