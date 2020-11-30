using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProtectedDiary.Data;
using ProtectedDiary.Helpers;
using ProtectedDiary.Models;
using ProtectedDiary.Services;

namespace ProtectedDiary.Pages
{
    public class UserDiariesModel : PageModel
    {
        private readonly ILogger<UserDiariesModel> _logger;

        private readonly DiaryContext _context;
        private readonly IAuthorRequester _authorRequester;

        public UserDiariesModel(ILogger<UserDiariesModel> logger,
            DiaryContext context,
            IAuthorRequester authorRequester)
        {
            _logger = logger;
            _context = context;
            _authorRequester = authorRequester;
        }

        public PaginatedList<Diary> Diaries { get; set; }

        public Author Author { get; set; }

        private readonly int _pageSize = 20;

        public async Task<IActionResult> OnGet(long? userId, int pageIndex = 1)
        {
            if (!(userId is long nonNullUserId))
            {
                return NotFound();
            }

            var diaries = _context.Diaries.Where(d => d.UserId == userId).OrderByDescending(x => x.CreatedAt);
            Diaries = await PaginatedList<Diary>.CreateAsync(diaries.AsNoTracking(), pageIndex, _pageSize);

            if (Diaries == null)
            {
                return NotFound();
            }

            Author = await _authorRequester.GetAuthor(nonNullUserId, this.User.Claims);
            return Page();
        }
    }
}