using System;
using System.Collections.Generic;

namespace OnlineShop.Models.DB
{
    /// <summary>
    /// 購物車紀錄
    /// </summary>
    public partial class Cart
    {
        public long Id { get; set; }
        public long UId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User UIdNavigation { get; set; } = null!;
    }
}
