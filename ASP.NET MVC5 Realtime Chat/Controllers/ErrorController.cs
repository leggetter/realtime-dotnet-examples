using System.Web.Mvc;

namespace ASP.NET_MVC5_Realtime_Chat.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Http404()
        {
            Response.StatusCode = 404;

            return View();
        }
    }
}