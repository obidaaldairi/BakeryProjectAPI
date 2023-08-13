using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T FindByCondition(Expression<Func<T, bool>> predicate);
        T FindByConditionWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        public List<T> FindAllByCondition(Expression<Func<T, bool>> predicate);
        public IQueryable<T> FindAllWithIncludes(params Expression<Func<T, object>>[] includes);
        public IQueryable<T> FindAllByConditionWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        public Tuple<List<T>, int> FindAllByConditionWithIncludesAndPagination(int skip, int take, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        List<T> GetAll();
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
