using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.TwitterAuth;
using ProtectedDiary.Web.PageModels;

namespace ProtectedDiary.Web.Pages.Diaries
{
    public class CreateModel : PostPageModel
    {
        public CreateModel(IDiaryService diaryService, IConfiguration configuration)
            : base(diaryService, configuration)
        {

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = this.User.Claims.UserId();
            var newDiary = await _diaryService.Create(Diary, userId);
            return RedirectToPage("Posted", new { userId = userId, id = newDiary.Id });
        }
    }
}