using ShoppingApp_DataAccess.Data;
using ShoppingApp_DataAccess.Infrastracture.Interface;
using ShoppingApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_DataAccess.Infrastracture.Impliments
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
           var prodctdb = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if(prodctdb != null)
            {
                prodctdb.Name = product.Name;
                prodctdb.Description = product.Description;
                prodctdb.Price = product.Price;
                if(prodctdb.ImageUrl != null)
                {
                    prodctdb.ImageUrl = product.ImageUrl;
                }
                
            }
        }
    }
}
