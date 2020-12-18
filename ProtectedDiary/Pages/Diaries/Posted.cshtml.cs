using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Pages.Diaries
{
    public class PostedModel : PageModel
    {
        private readonly DiaryContext _context;

        public Diary Diary { get; set; }

        public PostedModel(DiaryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(long? userId, long? id)
        {
            if (userId != this.User.Claims.UserId() || id == null)
            {
                return NotFound();
            }

            Diary = await _context.Diaries.FirstOrDefaultAsync(m => m.Id == id);

            if (Diary == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}