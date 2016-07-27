using CreativeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativeWeb.Controllers
{
    public class CartController : Controller
    {
        ItemContext db = new ItemContext();

        // GET: Cart
        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int itemId, string returnUrl, string quantity="1")
        {           
            Item item = db.Items
                .Where(it => it.Id == itemId).FirstOrDefault();
      int q=      Convert.ToInt32(quantity);
            if (item != null)
            {
                GetCart().AddItem(item, q);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult RemoveFromCart(int itemId, string returnUrl)
        {
            Item item = db.Items
                .Where(it => it.Id == itemId).FirstOrDefault();
            if (item != null)
            {
                GetCart().RemoveLine(item);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult Checkout()
        {
            return View();
        }

    }
}