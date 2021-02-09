using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.TwitterAuth;

namespace ProtectedDiary.Web.PageModels
{
    public class MutualFollowPageModel : PageModel
    {
        protected readonly ITwitterService _twitterService;
        public User Author { get; set; }

        protected bool followedBy;
        protected bool following;

        public bool IsMutualFollow => followedBy && following;

        public MutualFollowPageModel(ITwitterService twitterService)
        {
            _twitterService = twitterService;
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            if (Author is null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            // 相互、本人でない場合リダイレクト
            if (!IsMutualFollow && Author.Id != context.HttpContext.User.Claims.UserId())
            {
                context.Result = new LocalRedirectResult("/error/cantview");
            }
        }
    }
}