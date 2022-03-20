using Microsoft.AspNetCore.SignalR;
using OnlineShop.Interfaces;

namespace OnlineShop.Hubs
{
    public class ProductHub:Hub<IProductHub>
    {
        /// <summary>
        /// 建立連線
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
