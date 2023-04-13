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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categorydb = _context.Category.FirstOrDefault(x => x.Id == category.Id);
            if(categorydb != null)
            {
                categorydb.CategoryName = category.CategoryName;
                categorydb.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
