using System;
using System.Collections.Generic;

namespace OnlineShop.Models.DB
{
    /// <summary>
    /// 產品總表
    /// </summary>
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
        }

        public long Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public sbyte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
