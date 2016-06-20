using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public string UserName { get; set; }

        public string RoleID { get; set; }
    }
}