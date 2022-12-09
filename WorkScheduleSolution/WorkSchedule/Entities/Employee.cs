﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkSchedule.Entities
{
    internal partial class Employee
    {
        public Employee()
        {
            EmployeeSkill = new HashSet<EmployeeSkill>();
            Schedules = new HashSet<Schedule>();
        }

        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(12)]
        [Unicode(false)]
        public string HomePhone { get; set; }
        [Required]
        public bool? Active { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeSkill> EmployeeSkill { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}