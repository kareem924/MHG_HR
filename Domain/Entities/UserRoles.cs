using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("UserRoles")]
    public class UserRoles
    {
        
        [Key]
        public int RoleUserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }
        public virtual Roles Role { get; set; }
    }
}
