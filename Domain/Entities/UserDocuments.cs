using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("UserDocuments")]
   public class UserDocuments
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentHashName { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}
