using Microsoft.EntityFrameworkCore;
using ShoppingApp_DataAccess.Data;
using ShoppingApp_DataAccess.Infrastracture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_DataAccess.Infrastracture.Impliments
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            //_context.Products.Include(x = x.Category);
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll(string? IncludeProperty = null)
        {
            IQueryable<T> queryable = _dbSet;
            if (IncludeProperty != null)
            {
                foreach(var entity in IncludeProperty.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(entity);
                }
            }
            return queryable.ToList();
        }

        public T GetT(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, string? IncludeProperty = null)
        {
            IQueryable<T> queryable = _dbSet;
            queryable = queryable.Where(Predicate);
            if (IncludeProperty != null)
            {
                foreach (var entity in IncludeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(entity);
                }
            }
            return queryable.FirstOrDefault();
        }

        public IEnumerable<T> Search(System.Linq.Expressions.Expression<Func<T, bool>> Predicate)
        {
            return _dbSet.Where(Predicate).ToList();
        }
    }
}
