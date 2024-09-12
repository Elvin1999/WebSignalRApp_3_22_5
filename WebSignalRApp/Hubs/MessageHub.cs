using Microsoft.AspNetCore.SignalR;
using WebSignalRApp.Helpers;

namespace WebSignalRApp.Hubs
{
    public class MessageHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("ReceiveConnectInfo", "User connected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("ReceiveDisconnectInfo", "User disconnected");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message + "'s Offer : ", FileHelper.Read());
        }

        public async Task JoinRoom(string room,string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,room);
            await Clients.OthersInGroup(room).SendAsync("ReceiveJoinInfo", user);
        }

        public async Task SendMessageRoom(string room,string user)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveInfoRoom", user,FileHelper.Read(room));
        }
        
        public async Task SendWinnerMessageRoom(string room,string message)
        {
            await Clients.Group(room).SendAsync("ReceiveWinInfoRoom", message,FileHelper.Read(room));
        }
    }
}
