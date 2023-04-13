using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_DataAccess.Infrastracture.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? incldeProperty = null);
        IEnumerable<T> Search(Expression<Func<T, bool>> Predicate);
        T GetT(Expression<Func<T, bool>> Predicate,string? incldeProperty = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
