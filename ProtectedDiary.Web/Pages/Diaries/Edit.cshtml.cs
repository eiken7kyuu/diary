using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.TwitterAuth;
using ProtectedDiary.Web.PageModels;

namespace ProtectedDiary.Web.Pages.Diaries
{
    public class EditModel : PostPageModel
    {
        public EditModel(IDiaryService diaryService, IConfiguration configuration)
            : base(diaryService, configuration)
        {
        }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Diary = await _diaryService.GetDiaryById(id);
            if (Diary is null || Diary.UserId != this.User.Claims.UserId())
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            var diaryToUpdate = await _diaryService.GetDiaryById(id);
            if (diaryToUpdate is null || diaryToUpdate.UserId != this.User.Claims.UserId())
            {
                return NotFound();
            }

            try
            {
                await _diaryService.Update(Diary, id);
                return RedirectToPage("Detail", new { id = diaryToUpdate.Id, userId = diaryToUpdate.UserId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DiaryExists(Diary.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<bool> DiaryExists(long id)
        {
            return await _diaryService.GetDiaryById(id) is not null;
        }
    }
}