using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Web.PageModels
{
    public class PostPageModel : PageModel
    {
        public string TinymceApiKey { get; }
        protected readonly IDiaryService _diaryService;

        [BindProperty]
        public Diary Diary { get; set; }

        public PostPageModel(IDiaryService diaryService, IConfiguration configuration)
        {
            TinymceApiKey = configuration["TinymceApiKey"];
            _diaryService = diaryService;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}