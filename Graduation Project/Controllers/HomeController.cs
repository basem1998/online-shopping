using Graduation_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Graduation_Project.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.producttypes.ToList());
        }
        public ActionResult Details(Guid productId)
        {
            var product = db.Products.Find(productId);

            if (product == null)
            {
                return HttpNotFound();
            }
            TempData["Productsize"] = new SelectList(db.Sizes.Where(s => s.Sizegroup.producttypeId == product.ProducttypeId), "Id", "name");
          
            return View(product);

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
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("ahmedalgazar065@gmail.com", "1041998#");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("ahmedalgazar065@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل:" + contact.Name + "<br>" +
                          "بريد المرسل:" + contact.Email + "<br>" +
                          "عنوان الرسالة:" + contact.Subject + "<br>" +
                          "محتوى الرسالة: <b>" + contact.Message + "<b>";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }


    }
}