namespace DEPI_PROJECT.BLL.DTOs.Query
{
    public class PagedQueryDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedQueryDto()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PagedQueryDto(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}