using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Web.PageModels;

namespace ProtectedDiary.Web.Pages.Diaries
{
    public class DetailModel : MutualFollowPageModel
    {
        private readonly IDiaryService _diaryService;

        public DetailModel(IDiaryService diaryService, ITwitterService twitterService) : base(twitterService)
        {
            _diaryService = diaryService;
        }

        public Diary Diary { get; set; }

        public async Task<IActionResult> OnGetAsync(long userId, long id)
        {
            Diary = await _diaryService.GetDiaryById(id);
            if (Diary is null || Diary.UserId != userId)
            {
                return NotFound();
            }

            Author = await _twitterService.GetUser(Diary.UserId);
            (followedBy, following) = await _twitterService.GetRelationship(Diary.UserId);
            return Page();
        }
    }
}