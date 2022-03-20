using System;
using System.Collections.Generic;

namespace OnlineShop.Models.DB
{
    /// <summary>
    /// 會員表
    /// </summary>
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public sbyte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
