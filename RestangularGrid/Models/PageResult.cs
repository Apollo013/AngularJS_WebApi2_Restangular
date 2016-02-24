using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestangularGrid.Models
{
    /// <summary>
    /// This class allows us to add paging to our ui.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; private set; }

        public long TotalRecordCount { get; set; }

        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Items = new List<T>(items);

            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecordCount = totalRecordCount;

            PageCount = totalRecordCount > 0
                        ? (int)Math.Ceiling(totalRecordCount / (double)PageSize)
                        : 0;
        }

        public List<T> Items { get; set; }
    }
}
