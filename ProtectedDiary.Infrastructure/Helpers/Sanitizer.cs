using Ganss.XSS;
using ProtectedDiary.Application.Interfaces;

namespace ProtectedDiary.Infrastructure.Helpers
{
    /// <summary>
    /// HTMLタグのサニタイズ
    /// </summary>
    public class Sanitizer : ISanitizer
    {
        private readonly HtmlSanitizer _sanitizer;

        public Sanitizer()
        {
            // 以下のタグと属性以外全て拒否
            _sanitizer = new HtmlSanitizer(allowedTags: new string[] { "br", "strong", "a" },
                                           allowedAttributes: new string[] { "href", "title", "target", "rel" });
            _sanitizer.KeepChildNodes = true;
        }

        public string Sanitize(string content) => _sanitizer.Sanitize(content);
    }
}