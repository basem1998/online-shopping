using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Graduation_Project.Models;

namespace Graduation_Project.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int productId { get; private set; }

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumberOfOrder,Name,DateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumberOfOrder,Name,DateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);

        }


        //POST

        [HttpPost]
        //string Name,double Price,string Image, Guid id,
        public ActionResult AddProductToOrder(FormCollection form)
        {


            if (Session["pendingItems"] == null)
            {
                Session["pendingItems"] = new List<OrderItem>();
            }
            var list = Session["pendingItems"] as List<OrderItem>;

            var isExisting = list.Any(a => a.ProductId == Guid.Parse(form["Id"]) && a.SizeId == int.Parse(form["Productsize"]));
            if (isExisting == false)
            {
                var item = new OrderItem();
                item.ProductId = Guid.Parse(form["id"]);
                item.Qty = 1;
                item.Product = new Product();
                item.Product.Name = form["Name"];
                item.Product.Image = form["Image"];
                item.Price = decimal.Parse(form["Price"]);
                item.SizeId = int.Parse(form["Productsize"]);
                list.Add(item);
                Session["pendingItems"] = list;
            }
            if (Request.Form["Orderonly"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Request.Form["Orderonly"] != null)
            {
                return RedirectToAction("CheckOut", "Orders");
            }



            return View();
        }



        //Get
        public ActionResult CheckOut()
        {

            var list = Session["pendingItems"] as List<OrderItem>;
            foreach (var item in list)
            {
                item.Size = db.Sizes.Find(item.SizeId);
            }
            return View(list);

        }

        //Post
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(List<OrderItem> OrderItems)
        {

            Session["pendingItems"] = OrderItems;
            return RedirectToAction("Create", "Customers");
        }

    



    //        public ActionResult AddProductToOrderandCheckOut(string Name, string Image, Guid id)
    //        {
    //            TempData["Productsize"] = new SelectList(db.Sizes, "Id", "Name");


    //            if (Session["pendingItems"] == null)
    //            {
    //                Session["pendingItems"] = new List<OrderItem>();
    //            }
    //            var list = Session["pendingItems"] as List<OrderItem>;


    //            var item = new OrderItem();
    //            item.ProductId = id;
    //            item.Qty = 1;

    //            item.Product = new Product();
    //            item.Product.Name = Name;
    //            item.Product.Image = Image;

    //            list.Add(item);
    //            Session["pendingItems"] = list;

    //            ViewBag.message = "This product added to order";
    //            return RedirectToAction("CheckOut", "Orders");
    //        }
    //        //Post
    //        [HttpPost]
    //        public ActionResult AddProductToOrderandCheckOut()
    //        {
    //            return View();

    //        }
    public ActionResult DeleteOrderitem(Guid id)
    {

        var list = Session["pendingItems"] as List<OrderItem>;
        var ItemDelete = list.FirstOrDefault(a => a.ProductId == id);
        list.Remove(ItemDelete);
        return RedirectToAction("CheckOut", "Orders");

    }




}
}


