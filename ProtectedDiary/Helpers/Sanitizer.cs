using System.Linq;
using Ganss.XSS;

namespace ProtectedDiary.Helpers
{
    /// <summary>
    /// HTMLタグのサニタイズ
    /// </summary>
    /// <remark>
    /// TinyMCEで入力されたデータはサニタイズされて送信されてくるが、
    /// Httpリクエストから送られてきた場合は生のデータが来るので必要な要素以外全て削除する
    /// </remark>
    public class Sanitizer
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