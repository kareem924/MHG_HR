using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("BorrowDistribution")]
   public class BorrowDistribution
    {
        [Key]
        public int BorrowDistributionId { get; set; }
        public int BorrowId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsExempt { get; set; }
        public virtual Borrow Borrow { get; set; }

    }
}
