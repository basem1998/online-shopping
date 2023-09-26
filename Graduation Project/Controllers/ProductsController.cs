using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Graduation_Project.Models;
using System.IO;
using System.Net.Mail;

namespace Graduation_Project.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        //Get Method
        public ActionResult Suggestion()
        {
            //if (!String.IsNullOrEmpty(x))
            //{

            TempData["ProducttypeId"] = new SelectList(db.producttypes, "Id", "Name");

            TempData["GenderId"] = new SelectList(db.Genders, "Id", "Name");

            TempData["ColorId"] = new SelectList(db.Colors, "Id", "Name");
            return View();
            //}
            //var test = db.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Color).Include(p => p.Gender).Include(p => p.Producttype);
            //return View(test.ToList());
        }



        //var result = from s in stringList
        //             where s.Contains("Tutorials")
        //             select s;
        [HttpPost]
        public ActionResult Suggestion(FormCollection form)
        {


            TempData["ProducttypeId"] = new SelectList(db.producttypes, "Id", "Name");
            TempData["GenderId"] = new SelectList(db.Genders, "Id", "Name");
            TempData["ColorId"] = new SelectList(db.Colors, "Id", "Name");

            int producttypeid = int.Parse(form["ProducttypeId"].ToString());
            int genderid = int.Parse(form["GenderId"].ToString());
            int colorid = int.Parse(form["ColorId"].ToString());


            return View(db.Products.Where(c => c.ProducttypeId == producttypeid && c.GenderId == genderid && c.ColorId == colorid).ToList());
            }
        
        // GET: Products
        public ActionResult Index(int? Id, string SearchName)
        {


            if (!String.IsNullOrEmpty(SearchName))
            {

                char[] spearator = { ' ' };


                // Using the Method 
                String[] Search_list = SearchName.Split(spearator);


                var products = db.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Color).Include(p => p.Gender).Include(p => p.Producttype);
                return View(products.Where(a => Search_list.Any(s => s.ToUpper() == a.Name.ToUpper() || s.ToUpper() == a.Brand.Name.ToUpper()
                || s.ToUpper() == a.Producttype.Name.ToUpper()
                || s.ToUpper() == a.Color.Name.ToUpper())).ToList());
                
       
            }


            if (Id != null)
            {
                return View(db.Products.Where(a => a.CategoryId == Id).ToList());
            }

            var Market = db.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Color).Include(p => p.Gender).Include(p => p.Producttype);
            return View(Market.ToList());

        }
        ////GET
        //[HttpGet]
        //public ActionResult SelectCategory (int? CategoryId)
        //{
        //    if (CategoryId != null)
        //    {
        //        return View(db.Products.Where(a => a.CategoryId == CategoryId).ToList());
        //    }
        //    return View();
        //}

       
       


        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            Session["productId"] = id;
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
           

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categoerys, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name");
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            ViewBag.ProducttypeId = new SelectList(db.producttypes, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Product product, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                //string path = Server.MapPath("Images"+ "jdhurghdfj"+".PNG");
                //ImageFile.SaveAs(path);
                //product.Image = product.Id.ToString()+".PNG";

                string path = Path.Combine(Server.MapPath("~/Images"), ImageFile.FileName);
                ImageFile .SaveAs(path);
                product.Image = ImageFile.FileName;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
         

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categoerys, "Id", "Name", product.CategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name", product.ColorId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", product.GenderId);
            ViewBag.ProducttypeId = new SelectList(db.producttypes, "Id", "Name", product.ProducttypeId);
            return View(product); 
            var CustomerName = new Customer();
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("ahmedalgazar065@gmail.com", "1041998#");
            mail.From = new MailAddress("ahmedalgazar065@gmail.com");
            mail.To.Add(new MailAddress(CustomerName.UserName));
            mail.Subject = " Visit Our Page";
            mail.IsBodyHtml = true;
            string body = " Visit our site and inquire about new products in your favorite section ";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
        }


        //public ActionResult Create([Bind(Include = "Id,Name,size,Discription,CountryofOrigin,Image,ColorId,ProducttypeId,CategoryId,GenderId,BrandId")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        product.Id = Guid.NewGuid();
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
        //    ViewBag.CategoryId = new SelectList(db.Categoerys, "Id", "Name", product.CategoryId);
        //    ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name", product.ColorId);
        //    ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", product.GenderId);
        //    ViewBag.ProducttypeId = new SelectList(db.producttypes, "Id", "Name", product.ProducttypeId);
        //    return View(product);
        //}

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categoerys, "Id", "Name", product.CategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name", product.ColorId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", product.GenderId);
            ViewBag.ProducttypeId = new SelectList(db.producttypes, "Id", "Name", product.ProducttypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase ImageFile)
        //([Bind(Include = "Id,Name,size,Price,Discount,Discription,CountryofOrigin,Image,ColorId,ProducttypeId,CategoryId,GenderId,BrandId")] Product product)
        {
            //product.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                string oldPath = product.Image;
                if (ImageFile != null)
                {
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                   string path = Path.Combine(Server.MapPath("~/Images"), ImageFile.FileName);
                    ImageFile.SaveAs(path);
                    product.Image = ImageFile.FileName;

                }
               

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categoerys, "Id", "Name", product.CategoryId);
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Name", product.ColorId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", product.GenderId);
            ViewBag.ProducttypeId = new SelectList(db.producttypes, "Id", "Name", product.ProducttypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      

        //public ActionResult AddProductToOrderItems(Guid id)
        //{
        //    //  checked session pendingItem
        //    if (Session["pendingItems"] == null)
        //    {
        //        Session["pendingItems"] = new List<OrderItem>(); 
        //    }
        //    var list = Session["pendingItems"] as List<OrderItem>;

        //    var item = new OrderItem();
        //    item.ProductId = id;
        //    item.Qty = 1;

        //    list.Add(item);
        //    Session["pendingItems"] = list;

        //    ViewBag.message = "This product added to order";
        //    return RedirectToAction("Index", "Home");
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
