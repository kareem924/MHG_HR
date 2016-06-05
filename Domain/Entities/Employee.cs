﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string HomePhone { get; set; }
        public string PassportNumber { get; set; }
        public string MobilePhone { get; set; }
        public string BankAccount { get; set; }
        public string ProfilePicPath { get; set; }
        public int? SocialSituationId { get; set; }
        public string Address { get; set; }
        public int? JobId { get; set; }
        public int FingerPrintNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public bool? IsMale { get; set; }
        public byte? BranchId { get; set; }
        public byte? WorkPlaceId { get; set; }
        public string ShortDescription { get; set; }
        public string Color { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}