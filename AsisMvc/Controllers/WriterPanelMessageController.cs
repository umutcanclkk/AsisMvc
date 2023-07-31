using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsisMvc.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
        // Gelen kutusundaki bir mesajın detaylarını gösteren bir eylem (action) metodu.
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        // Gönderilen kutusundaki bir mesajın detaylarını gösteren bir eylem (action) metodu.
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        // Yeni bir mesaj oluşturmak için HTTP GET talebini karşılayan bir eylem (action) metodu.
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        // Yeni bir mesaj oluşturmak için HTTP POST talebini karşılayan bir eylem (action) metodu.
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            // Mesaj nesnesini doğrulamak için FluentValidation ile doğrulama yapılıyor.
            ValidationResult results = messagevalidator.Validate(p);
            string sender = (string)Session["WriterMail"];

            // Eğer doğrulama başarılı ise mesajı gönderilen kutusuna ekleyip yönlendirme yapılıyor.
            if (results.IsValid)
            {
                p.SenderMail = sender;
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("Sendbox");
            }
            // Eğer doğrulama başarısız ise hata mesajlarını model durumuna ekleyip aynı sayfayı tekrar gösteriyor.
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }
    }
}