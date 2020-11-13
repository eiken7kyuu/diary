using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProtectedDiary.Data;
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

        public IList<Diary> Diaries { get; set; }

        public Author Author { get; set; }


        public async Task<IActionResult> OnGet(long? userId)
        {
            if (!(userId is long nonNullUserId))
            {
                return NotFound();
            }

            Diaries = _context.Diaries.Where(d => d.UserId == userId).ToList();
            if (Diaries == null)
            {
                return NotFound();
            }

            Author = await _authorRequester.GetAuthor(nonNullUserId, this.User.Claims);
            return Page();
        }
    }
}