using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Pages.Diaries
{
    public class PostedModel : PageModel
    {
        private readonly DiaryContext _context;

        public long UserId { get; set; }
        public long Id { get; set; }

        public PostedModel(DiaryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(long? userId, long? id)
        {
            if (userId != this.User.Claims.UserId() || id == null)
            {
                return NotFound();
            }

            UserId = (long)userId;
            Id = (long)id;
            return Page();
        }
    }
}