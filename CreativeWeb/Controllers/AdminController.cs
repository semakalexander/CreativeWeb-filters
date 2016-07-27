using CreativeWeb.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreativeWeb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ItemContext db = new ItemContext();

        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        public ActionResult Edit(int id)
        {
            Item item = db.Items.FirstOrDefault(it => it.Id == id);
            var types = db.Items.Select(it => it.Type).Distinct().ToList();
            return View(new ItemAndTypes() { Item = item, Types = types });
        }

        [HttpPost]
        public ActionResult Edit(Item item, HttpPostedFileBase fileUpload)
        {
            var types = db.Items.Select(it => it.Type).Distinct().ToList();
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    string name = item.Barcode + Path.GetExtension(fileUpload.FileName);
                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\Images\\Items\\" + name;
                    fileUpload.SaveAs(path);
                    item.ImageSrc = "\\Content\\Images\\Items\\" + name;
                }
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("Товар {0} успішно змінено!", item.Name);
                return RedirectToAction("Index");
            }
            else
                return View(new ItemAndTypes() { Item = item, Types = types });

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item, HttpPostedFileBase fileUpload)
        {
            item.ImageSrc = "\\Content\\Images\\Items\\ItemDefault.jpg";
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    string name = item.Barcode + Path.GetExtension(fileUpload.FileName);
                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\Images\\Items\\" + name;
                    fileUpload.SaveAs(path);
                    item.ImageSrc = "\\Content\\Images\\Items\\" + name;
                }
                db.Items.Add(item);      
                db.SaveChanges();
                TempData["message"] = string.Format("Товар {0} успішно додано!", item.Name);
                return RedirectToAction("Index");
            }
            else
                return View(item);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var item = db.Items.FirstOrDefault(it => it.Id == id);
            if (item == null)
                return HttpNotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var item = db.Items.FirstOrDefault(it => it.Id == id);
            if (item == null)
                return HttpNotFound();
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public JsonResult GetAllTypes(string selected)
        {
            return Json(db.Items
                                .Where(it => it.Type != selected)
                                .Select(it => it.Type)
                                .Distinct()
                                .ToList(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CheckBarcode(string barcode)
        {
            var result = db.Items.FirstOrDefault(item => item.Barcode == barcode) == null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CheckType(string type)
        {
            var result = type != "Вибрати";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}