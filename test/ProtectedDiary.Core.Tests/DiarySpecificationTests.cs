using System;
using System.Collections.Generic;
using System.Linq;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Specifications;
using Xunit;

namespace ProtectedDiary.Core.Tests
{
    public class DiarySpecificationTests
    {

        [Fact]
        public void ユーザーの日記がページングされて返ってくる()
        {
            var spec = new DiaryFilterPaginatedSpecification(1001, 5, 5);
            var userDiaries = new List<Diary>();
            var d = DateTime.Now;

            foreach (var i in Enumerable.Range(1, 20))
            {
                userDiaries.Add(new Diary(i, 1001, $"Title {i}", "", d, d));
                d = d.AddDays(1);
            }

            userDiaries.Add(new Diary(21, 1000, $"Title 21", "", d, d));

            // ID: 15〜11
            var result = userDiaries
                .AsQueryable()
                .Where(spec.Criteria)
                .OrderByDescending(spec.OrderByDescending)
                .Skip(spec.Skip)
                .Take(spec.Take);

            Assert.NotNull(result);
            Assert.Equal(result.First(), userDiaries[14]);
            Assert.Equal(result.Last(), userDiaries[10]);
            Assert.True(result.All(x => x.UserId == 1001));
        }
    }
}