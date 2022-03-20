namespace OnlineShop.Models
{
    /// <summary>
    /// 接口自定義返回model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// 請求成功狀態
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// 狀態代碼
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 文字內容
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 數據(列表..等)
        /// </summary>
        public T? Data { get; set; }
    }
}
