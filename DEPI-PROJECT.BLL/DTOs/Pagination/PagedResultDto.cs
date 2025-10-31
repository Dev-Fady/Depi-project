using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace DEPI_PROJECT.BLL.DTOs.Pagination
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PagedResultDto(IEnumerable<T> Items, int PageNumber, int TotalCount, int pageSize)
        {
            this.Items = Items;
            this.PageNumber = PageNumber;
            this.TotalCount = TotalCount;
            this.TotalPages = (TotalCount + pageSize - 1) / pageSize;
        }

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

    }
}
