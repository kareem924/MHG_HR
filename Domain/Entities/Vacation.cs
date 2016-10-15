using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vacation
    {
        public int VacationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsApproved { get; set; }
        public string Reason { get; set; }
        public string DisapprovalReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public byte TypeId { get; set; }
        [ForeignKey("Emp")]
        public int EmployeeId { get; set; }
        public virtual Employee Emp { get; set; }
    }
}
