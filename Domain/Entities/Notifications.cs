using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Notifications")]
  public  class Notifications
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Emp")]
        public int? UserId { get; set; }
        [ForeignKey("EmpNotfifcationTo")]
        public int? NotfifcationTo { get; set; }
        public string Details { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameter { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public virtual Employee Emp { get; set; }
        public virtual Employee EmpNotfifcationTo { get; set; }
    }
}
