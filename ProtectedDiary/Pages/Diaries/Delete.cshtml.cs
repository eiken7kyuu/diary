using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Pages.Diaries
{
    public class DeleteModel : PageModel
    {
        private readonly DiaryContext _context;

        public DeleteModel(DiaryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Diary Diary { get; set; }

        public IActionResult OnGetAsync(long? id)
        {
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Diary = await _context.Diaries.FindAsync(id);

            if (Diary == null || Diary.UserId != this.User.Claims.UserId())
            {
                return NotFound();
            }

            _context.Diaries.Remove(Diary);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index", new { userId = Diary.UserId });
        }
    }
}