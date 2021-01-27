using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ProtectedDiary.Data;
using ProtectedDiary.Helpers;
using ProtectedDiary.Models;

namespace ProtectedDiary.PageModels
{
    public class PostPageModel : PageModel
    {
        protected readonly DiaryContext _context;
        public string TinymceApiKey { get; }
        protected Sanitizer _sanitizer;

        [BindProperty]
        public Diary Diary { get; set; }

        public PostPageModel(DiaryContext context, IConfiguration configuration, Sanitizer sanitizer)
        {
            _context = context;
            TinymceApiKey = configuration["TinymceApiKey"];
            _sanitizer = sanitizer;
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }
    }
}