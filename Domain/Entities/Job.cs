using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Jobs")]
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public byte DepartmentId { get; set; }
        public string Name { get; set; }
        public string JobCode { get; set; }
        public string JobLevel { get; set; }
        public string Grade { get; set; }
    }
}
