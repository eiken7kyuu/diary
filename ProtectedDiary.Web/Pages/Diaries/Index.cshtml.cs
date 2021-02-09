using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Web.PageModels;

namespace ProtectedDiary.Web.Pages.Diaries
{
    public class IndexModel : MutualFollowPageModel
    {
        private const int PAGE_SIZE = 20;
        private int _totalPages;

        private readonly ILogger<IndexModel> _logger;

        private readonly IDiaryService _diaryService;

        public IEnumerable<Diary> Diaries { get; private set; }


        [BindProperty(Name = "pageIndex", SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < _totalPages;

        public IndexModel(ILogger<IndexModel> logger, IDiaryService diaryService, ITwitterService twitterService) : base(twitterService)
        {
            _logger = logger;
            _diaryService = diaryService;
        }

        public async Task<IActionResult> OnGet(long userId)
        {
            Diaries = await _diaryService.GetDiaries(userId, PageIndex, PAGE_SIZE);
            if (Diaries.Count() == 0)
            {
                return NotFound();
            }

            var count = await _diaryService.GetDiaryCountByUser(userId);
            _totalPages = (int)Math.Ceiling((double)count / PAGE_SIZE);

            Author = await _twitterService.GetUser(userId);
            (followedBy, following) = await _twitterService.GetRelationship(userId);
            return Page();
        }
    }
}