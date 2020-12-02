using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.PageModels;
using ProtectedDiary.Services;

namespace ProtectedDiary.Pages
{
    public class DiaryModel : AuthorPageModel
    {
        public DiaryModel(DiaryContext context, IAuthorRequester authorRequester) : base(context, authorRequester) { }

        public Diary Diary { get; set; }

        public async Task<IActionResult> OnGetAsync(long? userId, long? id)
        {
            if (userId == null || id == null)
            {
                return NotFound();
            }

            Diary = await _context.Diaries
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (Diary == null)
            {
                return NotFound();
            }

            Author = await _authorRequester.GetAuthor(Diary.UserId, this.User.Claims);
            return Page();
        }
    }
}