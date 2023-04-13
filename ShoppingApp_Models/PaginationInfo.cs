using ShoppingApp_Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp_Models
{
    public class PaginationInfo<T> : List<T>
    {
        private List<Category> itemsList;
        private int count;
        private int pageSize;

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public PaginationInfo(List<T> item, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(item);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public static PaginationInfo<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var Count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationInfo<T>(items, Count, pageIndex, pageSize);
        }
    }
}
