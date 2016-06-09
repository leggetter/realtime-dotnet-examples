using System.Web.Configuration;

namespace ASP.NET_MVC5_Realtime_Chat
{
    public static class Config
    {
        public static string PubNubPublishKey {
            get
            {
                return WebConfigurationManager.AppSettings["PubNubPublishKey"];
            }
        }

        public static string PubNubSubscribeKey
        {
            get
            {
                return WebConfigurationManager.AppSettings["PubNubSubscribeKey"];
            }
        }

        public static string PusherAppId
        {
            get
            {
                return WebConfigurationManager.AppSettings["PusherAppId"];
            }
        }

        public static string PusherAppKey
        {
            get
            {
                return WebConfigurationManager.AppSettings["PusherAppKey"];
            }
        }

        public static string PusherAppSecret
        {
            get
            {
               return  WebConfigurationManager.AppSettings["PusherAppSecret"];
            }
        }

    }
}