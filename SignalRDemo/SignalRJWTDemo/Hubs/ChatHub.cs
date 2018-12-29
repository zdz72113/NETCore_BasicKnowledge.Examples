using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRJWTDemo.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", $"{Context.UserIdentifier}: {message}");
        }
    }
}
