using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    // BusinessLayer'da bulunan CategoryManager sınıfı ICategoryService arayüzünü uygular.
    public class CategoryManager : ICategoryService
    {
        // ICategoryDal arayüzünden bir referans alanı.
        private ICategoryDal _categoryDal;

        // CategoryManager sınıfının constructor metodu.
        public CategoryManager(ICategoryDal categorydal)
        {
            _categoryDal = categorydal;
        }

        // Yeni bir kategori eklemek için kullanılır.
        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        // Mevcut bir kategoriyi silmek için kullanılır.
        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);
        }

        // Mevcut bir kategorinin bilgilerini güncellemek için kullanılır.
        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }

        // Belirtilen ID'ye sahip bir kategoriyi getirir.
        public Category GetByID(int id)
        {
            return _categoryDal.Get(x => x.CategoryID == id);
        }

        // Tüm kategorileri listeleme işlemi için kullanılır.
        public List<Category> GetList()
        {
            return _categoryDal.List();
        }
    }
}
