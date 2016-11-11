using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("VacationsType")]
  public  class VacationsType
    {
        [Key]
        public int Id { get; set; }
        public string VacationName { get; set; }
    }
}
