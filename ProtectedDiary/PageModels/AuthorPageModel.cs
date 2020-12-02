using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProtectedDiary.Data;
using ProtectedDiary.Models;
using ProtectedDiary.Services;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.PageModels
{
    public class AuthorPageModel : PageModel
    {
        protected readonly DiaryContext _context;
        protected readonly IAuthorRequester _authorRequester;
        public Author Author { get; set; }

        public AuthorPageModel(DiaryContext context, IAuthorRequester authorRequester)
        {
            _context = context;
            _authorRequester = authorRequester;
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            if (Author == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            // 相互、本人でない場合リダイレクト
            if (!Author.IsMutualFollowers && Author.Id != context.HttpContext.User.Claims.UserId())
            {
                context.Result = new LocalRedirectResult("/error/cantview");
            }
        }
    }
}