using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProtectedDiary.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        private int _totalPages;

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < _totalPages;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            _totalPages = (int)(Math.Ceiling((double)count / pageSize));

            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}