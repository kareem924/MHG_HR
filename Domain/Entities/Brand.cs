using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Brands")]
    public class Brand
    {
        public Brand()
        {
            this.branshes = new HashSet<Branch>();
        }
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<Branch> branshes { get; set; }
  
    }
}
