using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Pages.Diaries
{
    public class CreateModel : PageModel
    {
        private readonly DiaryContext _context;

        public CreateModel(DiaryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Diary Diary { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Diary.UserId = this.User.Claims.UserId();
            _context.Diaries.Add(Diary);
            await _context.SaveChangesAsync();
            return RedirectToPage("../UserDiaries", new { userId = Diary.UserId });
        }
    }
}
