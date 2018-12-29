using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatDemo.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} left");
        }

        public Task Send(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
        }

        public Task SendAllExceptMe(string message)
        {
            return Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
        }

        public Task SendToGroup(string groupName, string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId}@{groupName}: {message}");
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} left {groupName}");
        }

        public Task Echo(string message)
        {
            return Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
        }
    }
}
