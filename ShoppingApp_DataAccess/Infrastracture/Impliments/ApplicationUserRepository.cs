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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private AppDbContext _context;
        public ApplicationUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ApplicationUser application)
        {
            throw new NotImplementedException();
        }
    }
}
