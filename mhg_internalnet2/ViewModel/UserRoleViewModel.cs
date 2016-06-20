using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}