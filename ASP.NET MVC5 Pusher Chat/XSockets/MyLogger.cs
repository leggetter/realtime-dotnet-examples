using System.IO;
using Serilog;
using XSockets.Logger;

namespace ASP.NET_MVC5_Pusher_Chat.XSockets
{
    // For details about configuration, visit http://serilog.net
    public class MyLogger : XLogger
    {
        public MyLogger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
            //.WriteTo.RollingFile(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log\\Log-{Date}.txt"))
            .WriteTo.Trace()
            .CreateLogger();
        }
    }
}
