using ShoppingApp_DataAccess.Data;
using ShoppingApp_DataAccess.Infrastracture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_DataAccess.Infrastracture.Impliments
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public ICartRepository Cart { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            ApplicationUser = new ApplicationUserRepository(context);
            Cart = new CartRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
