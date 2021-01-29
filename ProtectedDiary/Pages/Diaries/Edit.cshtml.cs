using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProtectedDiary.Data;
using ProtectedDiary.Helpers;
using ProtectedDiary.Models;
using ProtectedDiary.PageModels;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Pages.Diaries
{
    public class EditModel : PostPageModel
    {
        public EditModel(DiaryContext context, IConfiguration configuration, Sanitizer sanitizer)
         : base(context, configuration, sanitizer)
        {
        }

        public async Task<IActionResult> OnGetAsync(long? id)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var diaryToUpdate = await _context.Diaries.FindAsync(id);
            if (diaryToUpdate == null || diaryToUpdate.UserId != this.User.Claims.UserId())
            {
                return NotFound();
            }

            Diary.Content = _sanitizer.Sanitize(Diary.Content);
            try
            {
                diaryToUpdate.UpdatedAt = DateTime.Now;
                if (await TryUpdateModelAsync<Diary>(diaryToUpdate, "diary",
                    d => d.Title, d => d.Content, d => d.UpdatedAt))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Detail", new { id = diaryToUpdate.Id, userId = diaryToUpdate.UserId });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryExists(Diary.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index", new { userId = Diary.UserId });
        }

        private bool DiaryExists(long id)
        {
            return _context.Diaries.Any(e => e.Id == id);
        }
    }
}