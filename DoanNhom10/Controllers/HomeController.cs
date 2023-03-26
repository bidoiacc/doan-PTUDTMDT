using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanNhom10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "PetQ - Yêu thương thú cưng qua từng sản phẩm!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hãy liên hệ với chúng tôi nếu bạn cần hỗ trợ";

            return View();
        }
    }
}