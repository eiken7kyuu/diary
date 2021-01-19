using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProtectedDiary.Data;
using ProtectedDiary.Helpers;
using ProtectedDiary.Models;
using ProtectedDiary.PageModels;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Pages.Diaries
{
    public class CreateModel : PostPageModel
    {
        public CreateModel(DiaryContext context, IConfiguration configuration, Sanitizer sanitizer)
         : base(context, configuration, sanitizer)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Diary.Content = _sanitizer.Sanitize(Diary.Content);
            Diary.UserId = this.User.Claims.UserId();
            _context.Diaries.Add(Diary);
            await _context.SaveChangesAsync();
            return RedirectToPage("Posted", new { userId = Diary.UserId, id = Diary.Id });
        }
    }
}