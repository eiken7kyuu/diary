using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Infrastructure.Helpers;
using Xunit;

namespace ProtectedDiary.Infrastructure.Tests
{
    public class SanitizerTests
    {
        private readonly ISanitizer _sanitizer;

        public SanitizerTests()
        {
            _sanitizer = new Sanitizer();
        }

        [Fact]
        public void 特定のタグ以外サニタイズされる()
        {
            // a, br, strongのみ許可
            var result1 = _sanitizer.Sanitize(@"<a target=""_blank"" href=""javascript:alert(window.opener.document.cookie);"">test</a>");
            var result2 = _sanitizer.Sanitize(@"<br><p>aaaa</p><br><script>alert('a');</script>");
            var result3 = _sanitizer.Sanitize(@"<strong style=""display: none;"">test</strong><input type=""text"" />");
            Assert.Equal(@"<a target=""_blank"">test</a>", result1);
            Assert.Equal(@"<br>aaaa<br>alert('a');", result2);
            Assert.Equal(@"<strong>test</strong>", result3);
        }

        [Fact]
        public void 特定の属性以外サニタイズされる()
        {
            // href, target, rel, titleのみ許可
            var result1 = _sanitizer.Sanitize(@"<a target=""_blank"" style=""font-size: 30px;"" rel=""noopener"" href=""https://google.com"">test</a>");
            var result2 = _sanitizer.Sanitize(@"<strong src=""aaaa"" title=""sample"" max=""1"">test</strong>");
            Assert.Equal(@"<a target=""_blank"" rel=""noopener"" href=""https://google.com"">test</a>", result1);
            Assert.Equal(@"<strong title=""sample"">test</strong>", result2);
        }
    }
}