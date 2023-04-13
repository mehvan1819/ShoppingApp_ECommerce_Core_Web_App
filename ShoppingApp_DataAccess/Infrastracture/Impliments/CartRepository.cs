using ShoppingApp_DataAccess.Data;
using ShoppingApp_DataAccess.Infrastracture.Interface;
using ShoppingApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_DataAccess.Infrastracture.Impliments
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private AppDbContext _context;
        public CartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}

