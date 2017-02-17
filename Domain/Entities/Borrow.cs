using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("Borrow")]
    public class Borrow
    {
        public Borrow()
        {
            this.BorrowDistributions = new HashSet<BorrowDistribution>();
        }
        [Key]
        public int BorrowId { get; set; }
        public string Describtion { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime BorrowDate { get; set; }
        public int PaymentNumber { get; set; }
        public decimal MonthlyInstallment { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

     
        public virtual ICollection<BorrowDistribution> BorrowDistributions { get; set; }
    }
}
