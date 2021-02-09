using System;
using System.Collections.Generic;
using System.Linq;
using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Infrastructure.Data
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
                var d = new Diary(
                    0,
                    1300782624927961090,
                    $"テスト投稿{i}",
                    $"テスト投稿{i} 本文",
                    createdAt.AddDays(i),
                    createdAt.AddDays(i)
                );

                var d2 = new Diary(
                    0,
                    1300783116328382464,
                    $"テスト投稿{i}",
                    $"テスト投稿{i} 本文",
                    createdAt.AddDays(i),
                    createdAt.AddDays(i)
                );
                diaries.Add(d);
                diaries.Add(d2);
            }

            return diaries;
        }
    }
}