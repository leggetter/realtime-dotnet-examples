using ASP.NET_MVC5_Realtime_Chat.Repos;
using PubNubMessaging.Core;
using PusherServer;
using System.Net;
using System.Web.Mvc;

namespace ASP.NET_MVC5_Realtime_Chat.Controllers
{
    public class ChatController : Controller
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

        #region Pusher
        public ActionResult Pusher()
        {
            ViewBag.PusherKey = Config.PusherAppKey;
            return View("Pusher", "_Chat");
        }

        [HttpPost]
        public ActionResult PusherMessage()
        {
            var repo = new ChatRepository();
            var pusher = new Pusher(
                Config.PusherAppId,
                Config.PusherAppKey,
                Config.PusherAppSecret
                );

            var text = Request.Form["text"];
            var username = Request.Form["username"];

            var message = repo.CreateMessage(username, text);

            // Channel, event, data payload - evented PubSub
            // Library deals with serialising `message` object
            pusher.Trigger("chat", "chatmessage", message);

            return Json(message);
        }
        #endregion

        #region PubNub
        public ActionResult PubNub()
        {
            ViewBag.PubNubPublishKey = Config.PubNubPublishKey;
            ViewBag.PubNubSubscribeKey = Config.PubNubSubscribeKey;
            return View("PubNub", "_Chat");
        }

        [HttpPost]
        public ActionResult PubNubMessage()
        {
            var text = Request.Form["text"];
            var username = Request.Form["username"];

            var repo = new ChatRepository();
            var message = repo.CreateMessage(username, text);

            // Channel, data payload - PubSub
            // Library deals with serialising `message` object
            Pubnub pubnub = new Pubnub(
                Config.PubNubPublishKey,
                Config.PubNubSubscribeKey
            );
            pubnub.Publish(
                "chat",
                message,
                (string result) => { },
                (PubnubClientError e) => { }
            );

            return Json(message);
        }
        #endregion

        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Messages()
        {
            var repo = new ChatRepository();
            var messages = repo.GetAll();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NexmoInboundSMS()
        {
            // fromNumber/msisdn, to, text
            var fromNumber = Request["msisdn"];
            var text = Request["text"];

            var phoneRepo = new PhoneNumberRepository();
            if (!phoneRepo.NumberExists(fromNumber))
            {
                phoneRepo.Create(new Models.PhoneNumber() { number = fromNumber });
            }

            var chatRepo = new ChatRepository();
            var message = chatRepo.CreateMessage(fromNumber, text);

            Pubnub pubnub = new Pubnub(
                Config.PubNubPublishKey,
                Config.PubNubSubscribeKey
            );
            pubnub.Publish(
                "chat",
                message,
                (string result) => { },
                (PubnubClientError e) => { }
            );

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}