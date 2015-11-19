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
    }
}