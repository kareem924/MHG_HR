﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Departments")]
    public class Departments
    {
        [Key]
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}