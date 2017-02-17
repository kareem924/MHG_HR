using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Roles")]
    public class Roles
    {
        public Roles()
        {
            RolesUsers = new HashSet<UserRoles>();
        }
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UserRoles> RolesUsers { get; set; }
    }
}
