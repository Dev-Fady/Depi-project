
using System.Linq.Expressions;
using DEPI_PROJECT.BLL.DTOs.Query;

namespace DEPI_PROJECT.BLL.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedQueryDto pagedQueryDto)
        {
            int pageNumber = pagedQueryDto.PageNumber;
            int pageSize = pagedQueryDto.PageSize;

            return query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();

        }

        public static IQueryable<T> IF<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)

        {
            if (condition)
            {
                return query.Where(predicate);
            }
            return query;
        }
        
        public static IQueryable<T> OrderByExtended<T>(this IQueryable<T> query, List<Tuple<bool, Expression<Func<T, object>>>> selections, bool IsDesc)
        {
            foreach (var select in selections)
            {
                if (select.Item1)
                {
                    if (IsDesc)
                    {
                        return query.OrderByDescending(select.Item2).AsQueryable();
                    }
                    return query.OrderBy(select.Item2).AsQueryable();
                }
            }
            return query;
        }
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> query, PagedQueryDto pagedQueryDto)
        {
            int pageNumber = pagedQueryDto.PageNumber;
            int pageSize = pagedQueryDto.PageSize;

            return query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();

        }

        public static IEnumerable<T> IF<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)

        {
            if (condition)
            {
                return query.Where(predicate);
            }
            return query;
        }
        
        public static IEnumerable<T> OrderByExtended<T>(this IEnumerable<T> query, List<Tuple<bool, Func<T, object>>> selections, bool IsDesc)
        {
            foreach (var select in selections)
            {
                if (select.Item1)
                {
                    if (IsDesc)
                    {
                        return query.OrderByDescending(select.Item2);
                    }
                    return query.OrderBy(select.Item2).AsQueryable();
                }
            }
            return query;
        }
    }
}