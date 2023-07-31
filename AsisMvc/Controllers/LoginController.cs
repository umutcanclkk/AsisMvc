using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AsisMvc.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());
        // GET: Login

        // GET isteğine cevap veren aksiyon yöntemi
        [HttpGet]
        public ActionResult Index()
        {
            return View(); // Giriş formunu gösteren View'i döndürür.
        }

        // POST isteğine cevap veren aksiyon yöntemi
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            // Veritabanı bağlantısını oluşturur
            Context c = new Context();

            // Veritabanından, girilen kullanıcı adı ve şifreye sahip admin bilgisini alır.
            var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);

            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;

                // Kullanıcı bilgisi doğrulanırsa yapılacak işlemler burada gerçekleştirilir.
                // Örneğin, giriş başarılı olduğunda ana sayfaya yönlendirebilirsiniz.
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                // Kullanıcı bilgisi doğrulanamazsa, tekrar giriş sayfasına yönlendirilir.
                return RedirectToAction("Index");
            }


        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //Context c = new Context();

            //// Veritabanından, girilen kullanıcı adı ve şifreye sahip yazar bilgisini alır.
            //var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            var writeruserinfo = wm.GetWriter(p.WriterMail, p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;

                // Kullanıcı bilgisi doğrulanırsa yapılacak işlemler burada gerçekleştirilir.
                // Örneğin, giriş başarılı olduğunda ana sayfaya yönlendirebilirsiniz.
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                // Kullanıcı bilgisi doğrulanamazsa, tekrar giriş sayfasına yönlendirilir.
                return RedirectToAction("WriterLogin");
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}
