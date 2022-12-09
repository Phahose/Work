﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkSchedule.Entities
{
    internal partial class Location
    {
        public Location()
        {
            PlacementContracts = new HashSet<PlacementContract>();
        }

        [Key]
        public int LocationID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(2)]
        public string Province { get; set; }
        [StringLength(50)]
        public string Contact { get; set; }
        [Required]
        [StringLength(12)]
        [Unicode(false)]
        public string Phone { get; set; }
        [Required]
        public bool? Active { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<PlacementContract> PlacementContracts { get; set; }
    }
}