using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;
using XSockets.Plugin.Framework.Attributes;
using System.Threading.Tasks;
using ASP.NET_MVC5_Pusher_Chat.Repos;

namespace ASP.NET_MVC5_Pusher_Chat.XSockets
{
    public class ChatController : XSocketController
    {
        public string Username { get; set; }
        private ChatRepository repo;

        /// <summary>
        /// Set the username for the client (passed in with the connection).
        /// Send all persisted messages to the connected client
        /// </summary>        
        public override async Task OnOpened()
        {

            if (this.HasParameterKey("username"))
            {
                this.Username = this.GetParameter("username");
            }
            this.repo = new ChatRepository();
            await this.Invoke(this.repo.GetAll(), "allmessages");
        }

        /// <summary>
        /// Persist the message and then send it to all connected clients.
        /// </summary>
        public async Task ChatMessage(string text)
        {
            var messageModel = this.repo.CreateMessage(this.Username, text);
            await this.InvokeToAll(messageModel, "newmessage");
        }
    }
}