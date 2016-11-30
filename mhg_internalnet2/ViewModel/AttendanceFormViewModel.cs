using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class AttendanceFormViewModel
    {
        [Required]
        public int FingerPrintNumber { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
    }

    public class AttendanceSheet
    {
        public string Day { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string WorkingHours { get; set; }
        public string Delay { get; set; }
    }
}