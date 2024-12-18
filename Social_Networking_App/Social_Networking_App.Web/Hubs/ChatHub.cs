using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace Social_Networking_App.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var currentTime = DateTime.Now.ToString();

            await Clients.All.SendAsync("ReceiveMessage", user, message, currentTime);
          
        }
    }
}
