using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Graduation_Project.Models;
using Graduation_Project.Models.ViewModels;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Graduation_Project.Controllers
{
    public class AdminsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.orders.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var orderItems = db.OrderItems.Where(a => a.OrderId == id).ToList();
            if (orderItems == null)
            {
                return HttpNotFound();
            }
            return View(orderItems);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,E_mail")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,E_mail")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async System.Threading.Tasks.Task<ActionResult> CountCustomerOrders()
        {

            //var query = db.Customers 
            //       .GroupBy(p => new { p.UserName, p.Orders.Select(o=>o.OrderItems.Select(a=>a.Product.CategoryId))})
            //       .Select(g => new CustomerOrderCount { UserName = g.Key, Count = g.Count() });
            var query = db.OrderItems
                .GroupBy(p => new { p.Order.Customer.UserName, p.Product.Category.Name, })
                .Select(g => new CustomerOrderCount { UserName = g.Key.UserName, CategotyName = g.Key.Name, Count = g.Count() })
                .Where(a => a.Count >= 2);

            foreach (var item in query.ToList())
            {
                var apiKey = "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";
                // Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("ahmedalgazar065@gmail.com", "Sutra.com");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress(item.UserName, "user name");
                var plainTextContent = "thanks for visit our websit and wait for you agine ";
                var htmlContent = "<strong>thanks for visit our websit and wait for you agine</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                //var mail = new MailMessage();
                //var loginInfo = new NetworkCredential("ahmedalgazar065@gmail.com", "1041998#");
                //mail.From = new MailAddress("ahmedalgazar065@gmail.com");
                //mail.To.Add(new MailAddress(item.UserName));
                //mail.Subject = " Visit Our Page";
                //mail.IsBodyHtml = true;
                //string body = " Visit our site and inquire about new products in your favorite section ";
                //mail.Body = body;

                //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.EnableSsl = true;
                //smtpClient.Credentials = loginInfo;
                //smtpClient.Send(mail);

            }
            return View(query);
        }


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
