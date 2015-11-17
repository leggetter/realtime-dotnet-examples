using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(ASP.NET_MVC5_Pusher_Chat.App_Start.SignalRStartup))]
namespace ASP.NET_MVC5_Pusher_Chat.App_Start
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}