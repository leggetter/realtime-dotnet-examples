using XSockets.Core.Common.Socket;
using XSockets.Plugin.Framework;

[assembly: System.Web.PreApplicationStartMethod(typeof(ASP.NET_MVC5_Realtime_Chat.App_Start.XSocketsStartup), "Start")]
namespace ASP.NET_MVC5_Realtime_Chat.App_Start
{
    public static class XSocketsStartup
    {
        private static IXSocketServerContainer container;
        public static void Start()
        {
            container = Composable.GetExport<IXSocketServerContainer>();
            container.Start();
        }
    }
}