﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Models.DB
{
    /// <summary>
    /// 會員表
    /// </summary>
    [Table("user")]
    public partial class User
    {
        public User()
        {
            Cart = new HashSet<Cart>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [Column("mobile")]
        [StringLength(20)]
        public string Mobile { get; set; }
        [Column("status")]
        public sbyte Status { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

        [InverseProperty("UIdNavigation")]
        public virtual ICollection<Cart> Cart { get; set; }
    }
}