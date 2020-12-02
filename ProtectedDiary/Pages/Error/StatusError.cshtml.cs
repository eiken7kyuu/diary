using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProtectedDiary.Pages.Error
{
    public class StatusErrorModel : PageModel
    {
        public string Summary { get; set; }
        public string Title { get; set; }

        public void OnGet(int? code)
        {
            switch (code)
            {
                case 404:
                    Title = "ページが見つかりません";
                    Summary = "ご指定のページは存在しません。アドレスが正しいかご確認ください。";
                    break;
                default:
                    Title = "エラー";
                    Summary = "しばらく時間をおいてから、アクセスしてください。";
                    break;
            }
        }
    }
}