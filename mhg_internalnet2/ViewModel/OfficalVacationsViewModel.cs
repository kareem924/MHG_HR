using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class OfficalVacationsViewModel
    {
        [ScaffoldColumn(false)]
        public int OfficialVactionId { get; set; }
        [Required]
        [DisplayName("Vacation Name")]
        public string Description { get; set; }
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Day { get; set; }
    }
}