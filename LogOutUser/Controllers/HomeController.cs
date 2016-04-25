using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LogOutUser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Transclude()
        {
            return View();
        }
        public ActionResult Encrypt()
        {
            return View();
        }

        public ActionResult SignalR()
        {
            return View();
        }
        public async Task<ActionResult> TreadTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(100);
                   
                });
            }
            return View();
        }
    }
}