using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.TwitterAuth;

namespace ProtectedDiary.Web.Pages.Diaries
{
    public class PostedModel : PageModel
    {
        protected readonly IDiaryService _diaryService;

        public Diary Diary { get; set; }

        public PostedModel(IDiaryService diaryService)
        {
            _diaryService = diaryService;
        }

        public async Task<IActionResult> OnGet(long userId, long id)
        {
            if (userId != this.User.Claims.UserId())
            {
                return NotFound();
            }

            Diary = await _diaryService.GetDiaryById(id);

            if (Diary is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}