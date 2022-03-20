namespace OnlineShop.Models.ApiViewModels
{
    public class ProductVM
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品說明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 庫存數量
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 商品狀態, 0:異常,
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 商品定價
        /// </summary>
        public decimal Price{ get; set; }

        /// <summary>
        /// 促銷價格
        /// </summary>
        public decimal SalesPrice { get; set; }
        
        /// <summary>
        /// 商品圖片
        /// </summary>
        public string ImgUrl { get; set; }
    }

}
