using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        private DbSet<T> entities;

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            entities = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entities.Update(entity);
            //context.SaveChanges();
        }

        public List<T> FindAllByCondition(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(predicate).ToList();
        }

        public IQueryable<T> FindAllByConditionWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)//بدون شرط فقط join
        {
            var query = entities.AsNoTracking().Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public Tuple<List<T>, int> FindAllByConditionWithIncludesAndPagination(int skip, int take, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)// تعتبر ميثود تنظيمية بحيث اذا كان الجدول يحتوي على الآف الأسطر .... بهذه الميثود يقوم بإظهار البيانات كمثال يظهر عشر اسطر عشر اسطر او يتم تقسيم الصفحات ويتم عرض البيانات  مقسمة في تلك الصفحات
        {
            try
            {
                var records = entities.Where(predicate);
                int totalCount = records == null ? 0 : records.Count();
                var query = records.OrderByDescending(x => x.ID).Skip(skip).Take(take).AsNoTracking();
                return Tuple.Create(includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList(), totalCount);
            }
            catch (Exception)
            {
                return Tuple.Create(new List<T>(), 0);
            }

        }

        public IQueryable<T> FindAllWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var query = entities.AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public T FindByCondition(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
        }

        public T FindByConditionWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = entities.AsNoTracking().Where(predicate);
            var result = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (!result.Any())
                return null;

            return result.First();
        }

        public bool FindIsExistByCondition(Expression<Func<T, bool>> predicate)
        {
            return entities.AsNoTracking().Any(predicate);
        }

        public List<T> GetAll()
        {
            return entities.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
        }

        public T Insert(T entity)
        {
            entities.Add(entity);
            //context.SaveChanges();

            return entity;
        }

        public void Update(T entity)
        {

            entities.Update(entity);
            //context.SaveChanges();
        }
    }
}
