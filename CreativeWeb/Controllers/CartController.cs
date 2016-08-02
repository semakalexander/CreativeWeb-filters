using CreativeWeb.Models;
using System.Linq;
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

        public RedirectToRouteResult AddToCart(int itemId, string returnUrl, string quantity)
        {
            var item = db.Items.FirstOrDefault(it => it.Id == itemId);
            var q = 1;
            int.TryParse(quantity, out q);
            if (item != null)
            {
                GetCart().AddItem(item, q);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public void ChangeQuantity(int id, int quantity)
        {
            CartLine line = GetCart().Lines.FirstOrDefault(it => it.Item.Id == id);
            if (line != null)
            {
                line.Quantity = quantity;
            }
        }

        public RedirectToRouteResult RemoveFromCart(int itemId, string returnUrl)
        {
            Item item = db.Items.FirstOrDefault(it => it.Id == itemId);
            if (item != null)
            {
                GetCart().RemoveLine(item);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart != null)
                return cart;
            cart = new Cart();
            Session["Cart"] = cart;
            return cart;
        }

        public ActionResult Checkout()
        {
            return View();
        }

    }
}