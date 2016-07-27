using CreativeWeb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativeWeb.Controllers
{
    public class HomeController : Controller
    {
        ItemContext db = new ItemContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SelectType(string type, int page = 1)
        {
            int pageSize = 9;
            return View(db.Items.Where(item => item.Type == type).ToList().ToPagedList(page, pageSize));
          
        }
    }
}