
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Users
    {
        public Users()
        {
            UserRoles = new HashSet<UserRoles>();
        }
        [Key]
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string ProfilePicPath { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
