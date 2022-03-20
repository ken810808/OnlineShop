using System.ComponentModel;

namespace OnlineShop.Enums
{
    /// <summary>
    /// 商品狀態
    /// </summary>
    public enum ProductStatusEnum
    {
        /// <summary>
        /// 未預期狀態
        /// </summary>
        [Description("未預期狀態")]
        UnExpected=0,

        /// <summary>
        /// 上架
        /// </summary>
        [Description("上架")]
        Launch = 1,

        /// <summary>
        /// 暫停販售
        /// </summary>
        [Description("暫停販售")]
        Stopped = 2,

        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        ReCall = 3,
    }
}
