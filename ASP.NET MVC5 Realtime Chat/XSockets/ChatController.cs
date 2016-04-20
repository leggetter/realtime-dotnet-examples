using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;
using XSockets.Core.Common.Socket.Event.Interface;
using XSockets.Plugin.Framework.Attributes;
using System.Threading.Tasks;
using ASP.NET_MVC5_Realtime_Chat.Repos;
using ASP.NET_MVC5_Realtime_Chat.Models;
using System.Collections.Generic;

namespace ASP.NET_MVC5_Realtime_Chat.XSockets
{
    [XSocketMetadata("chat")]
    public class ChatController : XSocketController
    {
        private ChatRepository repo;

        public ChatController(): base()
        {
            this.repo = new ChatRepository();
        }

        /// <summary>
        /// Get all existing messages
        /// </summary>
        /// <returns></returns>
        public List<Message> GetAllMessages()
        {
            return this.repo.GetAll();
        }

        /// <summary>
        /// Persist the message and then send it to all connected clients.
        /// </summary>
        /// <param name="message">The message being sent from one client</param>
        /// <returns></returns>
        public override async Task OnMessage(IMessage message)
        {
            Message messageModel = (Message)message.Data.Deserialize<Message>();

            messageModel = this.repo.CreateMessage(messageModel);
            await this.PublishToAll(messageModel, "chatmessage");
        }
    }
}