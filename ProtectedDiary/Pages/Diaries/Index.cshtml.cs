using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProtectedDiary.Data;
using ProtectedDiary.Helpers;
using ProtectedDiary.Models;
using ProtectedDiary.PageModels;
using ProtectedDiary.Services;

namespace ProtectedDiary.Pages.Diaries
{
    public class IndexModel : AuthorPageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly int _pageSize = 20;

        public PaginatedList<Diary> Diaries { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            DiaryContext context,
            IAuthorRequester authorRequester) : base(context, authorRequester)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(long? userId, int pageIndex = 1)
        {
            if (!(userId is long nonNullUserId))
            {
                return NotFound();
            }

            var diaries = _context.Diaries.Where(d => d.UserId == userId).OrderByDescending(x => x.CreatedAt);
            Diaries = await PaginatedList<Diary>.CreateAsync(diaries.AsNoTracking(), pageIndex, _pageSize);

            Author = await _authorRequester.GetAuthor(nonNullUserId);
            return Page();
        }
    }
}