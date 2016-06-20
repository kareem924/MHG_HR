using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Branches")]
    public  class Branch
    {
       
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime OpenningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Telephone { get; set; }
        public string Color { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand Brand { get; set; }
       
    }
}
