using System;
using System.Collections.Generic;
using System.Linq;
using ProtectedDiary.Models;

namespace ProtectedDiary.Data
{
    public class Seed
    {
        public static void Initialize(DiaryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Diaries.Any())
            {
                return;
            }

            context.Diaries.AddRange(CreateTestDiaries());
            context.SaveChanges();
        }

        public static IEnumerable<Diary> CreateTestDiaries()
        {
            var createdAt = DateTime.Parse("2020-04-01");
            var diaries = new List<Diary>();
            for (var i = 1; i < 100; i++)
            {
                var d = new Diary
                {
                    UserId = 1300782624927961090,
                    Title = $"テスト投稿{i}",
                    Content = $"テスト投稿{i}",
                    CreatedAt = createdAt.AddDays(i),
                    UpdatedAt = createdAt.AddDays(i)
                };

                var d2 = new Diary
                {
                    UserId = 1300783116328382464,
                    Title = $"テスト投稿{i}",
                    Content = $"テスト投稿{i}",
                    CreatedAt = createdAt.AddDays(i),
                    UpdatedAt = createdAt.AddDays(i)
                };
                diaries.Add(d);
                diaries.Add(d2);
            }

            return diaries;
        }
    }
}