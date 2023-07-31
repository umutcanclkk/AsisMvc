using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System.Web.Mvc;

namespace AsisMvc.Controllers
{
    public class CategoryController : Controller
    {
        // CategoryManager sınıfından bir örnek oluşturuyoruz ve veri erişim katmanı (EfCategoryDal) ile ilişkilendiriyoruz.
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        // GET: Category
        // Ana sayfa için kullanılacak metot.
        public ActionResult Index()
        {
            return View();
        }

        // Tüm kategorileri listeleyen metot.
        public ActionResult GetCategoryList()
        {
            var categoryvalues = cm.GetList();
            return View(categoryvalues);
        }

        // Yeni kategori eklemek için GET isteği ile çağrılan metot.
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        // Yeni kategori eklemek için POST isteği ile çağrılan metot.
        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            // Kategori nesnesi için FluentValidation ile kural denetimi yapılıyor.
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult result = categoryValidator.Validate(p);

            if (result.IsValid)
            {
                // Eğer kategori geçerliyse, kategori ekleniyor ve kategori listesi sayfasına yönlendiriliyor.
                cm.CategoryAdd(p);
                return RedirectToAction("GetCategoryList");
            }
            else
            {
                // Eğer kategori geçerli değilse, hatalar ModelState üzerinden View'a ekleniyor.
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
