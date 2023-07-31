using BusinessLayer.Concrete; // Business Layer'daki Concrete klasöründen sınıfları kullanmak için
using BusinessLayer.ValidationRules; // Business Layer'daki ValidationRules klasöründen sınıfları kullanmak için
using DataAccessLayer.EntityFramework; // Entity Framework ile veritabanı işlemleri için
using EntityLayer.Concrete; // Entity Layer'daki sınıfları kullanmak için
using FluentValidation.Results; // FluentValidation'dan doğrulama sonuçlarını kullanmak için
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsisMvc.Controllers
{
    public class MessageController : Controller
    {
        // MessageManager, EfMessageDal ve MessageValidator sınıflarını kullanmak için nesneler oluşturuluyor.
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        // GET: Message
        // Gelen kutusundaki mesajları listeleyen bir eylem (action) metodu.
        [Authorize]
        public ActionResult Inbox(string p)
        {
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        // Gönderilen kutusundaki mesajları listeleyen bir eylem (action) metodu.
        public ActionResult Sendbox(string p)
        {
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
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

            // Eğer doğrulama başarılı ise mesajı gönderilen kutusuna ekleyip yönlendirme yapılıyor.
            if (results.IsValid)
            {
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
