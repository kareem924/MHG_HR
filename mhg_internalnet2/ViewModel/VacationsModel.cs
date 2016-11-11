using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class VacationsModel
    {
        public int VacationId { get; set; }
        [Required]
        [DisplayName("From")]
        public DateTime FromDate { get; set; }
        [Required]
        [DisplayName("To")]
        public DateTime ToDate { get; set; }
        public bool IsApproved { get; set; }
        [Required]
        [DisplayName("Vacation Reason")]
        public string Reason { get; set; }
        [Required]
        [DisplayName("Vacation Type")]
        public int VacationType { get; set; }
        public string DisapprovalReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public byte TypeId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class VacationTypeModel
    {
        public int Id { get; set; }
        public string VacationName { get; set; }
    }
}