using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.Services;
using ProtectedDiary.TwitterAuth;
using Tweetinvi;
using Tweetinvi.Models;

namespace ProtectedDiary.Pages
{
    public class DiaryModel : PageModel
    {
        private readonly DiaryContext _context;
        private readonly IAuthorRequester _authorRequester;


        public DiaryModel(DiaryContext context, IAuthorRequester authorRequester)
        {
            _context = context;
            _authorRequester = authorRequester;
        }

        public Diary Diary { get; set; }
        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(long? userId, long? id)
        {
            if (userId == null || id == null)
            {
                return NotFound();
            }

            Diary = await _context.Diaries.FirstOrDefaultAsync(x => x.Id == id);

            if (Diary == null)
            {
                return NotFound();
            }

            Author = await _authorRequester.GetAuthor(Diary.UserId, this.User.Claims);
            return Page();
        }
    }
}
