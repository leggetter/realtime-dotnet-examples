using ASP.NET_MVC5_Pusher_Chat.Repos;
using PusherServer;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ASP.NET_MVC5_Pusher_Chat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignalR()
        {
            return View("SignalR", "_Chat");
        }

        public ActionResult XSockets()
        {
            return View("XSockets", "_Chat");
        }

        public ActionResult Faye()
        {
            return View("Faye", "_Chat");
        }

        public ActionResult Pusher()
        {
            return View("Pusher", "_Chat");
        }

        [HttpPost]
        public ActionResult Message()
        {
            var repo = new ChatRepository();

            var text = Request.Form["text"];
            var username = Request.Form["username"];

            var message = repo.CreateMessage(username, text);
            return Json(message);
        }

        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Messages()
        {
            var repo = new ChatRepository();
            var messages = repo.GetAll();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PusherMessage()
        {
            var repo = new ChatRepository();
            var pusher = new Pusher(
                WebConfigurationManager.AppSettings["PusherAppId"],
                WebConfigurationManager.AppSettings["PusherAppKey"],
                WebConfigurationManager.AppSettings["PusherAppSecret"]
                );

            var text = Request.Form["text"];
            var username = Request.Form["username"];

            var message = repo.CreateMessage(username, text);

            pusher.Trigger("chat", "chatmessage", message);

            return Json(message);
        }
    }
}