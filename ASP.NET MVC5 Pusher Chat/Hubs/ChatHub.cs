using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ASP.NET_MVC5_Pusher_Chat.Models;
using ASP.NET_MVC5_Pusher_Chat.Repos;

namespace ASP.NET_MVC5_Pusher_Chat.Hubs
{
    public class ChatHub : Hub
    {
        private ChatRepository repo;

        public ChatHub()
        {
            this.repo = new ChatRepository();
        }

        public void Send(string name, string text)
        {
            var message = this.repo.CreateMessage(name, text);
            Clients.All.newMessage(message);
        }

        public List<Message> GetAll()
        {
            return repo.GetAll();
        }
    }
}