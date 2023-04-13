using ShoppingApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_DataAccess.Infrastracture.Interface
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Update(Cart cart);
    }
}
