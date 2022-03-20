
namespace OnlineShop.Interfaces
{
    public interface IProductHub
    {
        /// <summary>
        /// 通知產品價格異動
        /// </summary>
        /// <param name="pId">商品ID</param>
        /// <param name="price">價格</param>
        /// <returns></returns>
        Task NotifyPriceChange(int pId, string price);

        /// <summary>
        /// 通知產品下架
        /// </summary>
        /// <param name="pId">商品ID</param>
        /// <returns></returns>
        Task NotifyProductRecall(int pId);
    }
}
