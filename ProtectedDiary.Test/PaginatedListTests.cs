using Xunit;
using ProtectedDiary.Helpers;
using System.Threading.Tasks;
using System.Linq;
using MockQueryable.Moq;

namespace ProtectedDiary.Test
{
    public class PaginatedListTests : IAsyncLifetime
    {
        private readonly Moq.Mock<IQueryable<string>> _items;
        private PaginatedList<string> _firstPage;
        private PaginatedList<string> _lastPage;

        public PaginatedListTests()
        {
            _items = new string[]
            {
                "apple",
                "banana",
                "blueberry",
                "cherry",
                "grape",
                "mango",
                "orange",
                "peach",
                "pear",
                "pineapple"
            }
            .AsQueryable()
            .OrderBy(x => x)
            .BuildMock();
        }

        public async Task InitializeAsync()
        {
            _firstPage = await PaginatedList<string>.CreateAsync(_items.Object, 1, 2);
            _lastPage = await PaginatedList<string>.CreateAsync(_items.Object, 5, 2);
        }

        [Fact]
        public async Task 指定した数の要素を持つ()
        {
            var pagiedList = await PaginatedList<string>.CreateAsync(_items.Object, 1, 2);
            var pagiedList2 = await PaginatedList<string>.CreateAsync(_items.Object, 1, 5);

            Assert.Equal(2, pagiedList.Count);
            Assert.Equal(5, pagiedList2.Count);
        }

        [Fact]
        public void 指定したページの要素を返す()
        {
            Assert.Equal("apple", _firstPage[0]);
            Assert.Equal("banana", _firstPage[1]);

            Assert.Equal("pear", _lastPage[0]);
            Assert.Equal("pineapple", _lastPage[1]);
        }

        [Fact]
        public async Task 前後ページの存在有無()
        {
            var pagiedList = await PaginatedList<string>.CreateAsync(_items.Object, 3, 2);

            Assert.True(pagiedList.HasPreviousPage);
            Assert.True(pagiedList.HasNextPage);

            // 最初のページ
            Assert.False(_firstPage.HasPreviousPage);
            Assert.True(_firstPage.HasNextPage);

            // 最終ページ
            Assert.True(_lastPage.HasPreviousPage);
            Assert.False(_lastPage.HasNextPage);
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}