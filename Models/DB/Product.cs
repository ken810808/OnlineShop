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
    /// 產品總表
    /// </summary>
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            Cart = new HashSet<Cart>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("product_name")]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Column("price")]
        [Precision(20, 6)]
        public decimal Price { get; set; }
        [Column("sale_price")]
        [Precision(20, 6)]
        public decimal SalePrice { get; set; }
        [Column("img_url")]
        [StringLength(200)]
        public string ImgUrl { get; set; }
        [Column("description")]
        [StringLength(200)]
        public string Description { get; set; }
        [Column("stock_quantity")]
        public int StockQuantity { get; set; }
        [Column("status")]
        public sbyte Status { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<Cart> Cart { get; set; }
    }
}