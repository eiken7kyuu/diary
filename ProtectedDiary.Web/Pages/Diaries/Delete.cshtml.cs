using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.TwitterAuth;

namespace ProtectedDiary.Web.Pages.Diaries
{
    public class DeleteModel : PageModel
    {
        protected readonly IDiaryService _diaryService;

        public DeleteModel(IDiaryService diaryService)
        {
            _diaryService = diaryService;
        }

        [BindProperty]
        public Diary Diary { get; set; }

        public IActionResult OnGetAsync()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            Diary = await _diaryService.GetDiaryById(id);

            if (Diary is null || Diary.UserId != this.User.Claims.UserId())
            {
                return NotFound();
            }

            await _diaryService.Delete(Diary);
            return RedirectToPage("Index", new { userId = Diary.UserId });
        }
    }
}