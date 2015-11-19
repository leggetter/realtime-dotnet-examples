using ASP.NET_MVC5_Pusher_Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5_Pusher_Chat.Repos
{
    public class ChatRepository
    {
        ChatEntities _entities;

        public ChatRepository()
        {
            this._entities = new ChatEntities();
        }

        public List<Message> GetAll()
        {
            return this._entities.Messages.ToList();
        }

        public Message CreateMessage(string username, string text)
        {
            var message = new Message();
            message.text = text;
            message.username = username;
            return this.CreateMessage(message);
        }

        public Message CreateMessage(Message message)
        {
            message.created = DateTime.Now;
            message = this._entities.Messages.Add(message);
            this._entities.SaveChanges();

            return message;
        }
    }
}