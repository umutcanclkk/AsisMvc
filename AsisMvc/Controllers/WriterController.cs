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
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writervalidator = new WriterValidator();

        // GET: Writer
        // Tüm yazarları listeleyen bir eylem (action) metodu.
        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }

        // Yeni bir yazar eklemek için HTTP GET talebini karşılayan bir eylem (action) metodu.
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        // Yeni bir yazar eklemek için HTTP POST talebini karşılayan bir eylem (action) metodu.
        [HttpPost]
        public ActionResult AddWriter(Writer p)
        {
            // Yazar nesnesini doğrulamak için FluentValidation ile doğrulama yapılıyor.
            ValidationResult results = writervalidator.Validate(p);

            // Eğer doğrulama başarılı ise yazarı ekleyip yönlendirme yapılıyor.
            if (results.IsValid)
            {
                wm.WriterAdd(p);
                return RedirectToAction("Index");
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

        // Mevcut bir yazarı düzenlemek için HTTP GET talebini karşılayan bir eylem (action) metodu.
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writerValues = wm.GetByID(id);
            return View(writerValues);
        }

        // Mevcut bir yazarı düzenlemek için HTTP POST talebini karşılayan bir eylem (action) metodu.
        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            // Yazar nesnesini doğrulamak için FluentValidation ile doğrulama yapılıyor.
            ValidationResult results = writervalidator.Validate(p);

            // Eğer doğrulama başarılı ise yazarı güncelleyip yönlendirme yapılıyor.
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("Index");
            }
            // Eğer doğrulama başarısız ise hata mesajlarını model durumuna ekleyip aynı sayfayı tekrar gösteriyor.
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
