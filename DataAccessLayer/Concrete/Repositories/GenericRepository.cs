using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        // aşağıda yeni bir c ürettik bu c ise DbSet tarafından gelen abaout writer vb. bilgileri IRepository interface tarafından çekip veriyi yansıtır
        Context c = new Context();

        DbSet<T> _object;



        // Burada bir yapıcı method kullanıldı yukarıdaki class ile aynı isim 
        public GenericRepository()
        {
            _object=c.Set<T>();
        }



        public void Delete(T p)
        {
            var deletedEntity=c.Entry(p);
            deletedEntity.State=EntityState.Deleted;
           /// _object.Remove(p);
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            //SingleOrDefault methodu liste de sadece bir değer döndürmek için kullanılır
            return _object.SingleOrDefault(filter);
        }

        public void Insert(T p)
        {
            var addedEntity = c.Entry(p);
            addedEntity.State = EntityState.Added;
           // _object.Add(p); 
            c.SaveChanges();
                }

        public List<T> List()
        {
           return _object.ToList();

        }
        // aşağıda neyi istersek veri tabanından onu listeler kısaca filter e ne göderirsek  onu şartlı listeler!
        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            var updatedEntity = c.Entry(p);
            updatedEntity.State= EntityState.Modified;
            c.SaveChanges();        }
    }
}
