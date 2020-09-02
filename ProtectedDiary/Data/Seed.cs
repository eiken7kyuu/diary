

using System;
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

            var createdAt = DateTime.Parse("2020-08-31");
            var diaries = new Diary[]
            {
                new Diary {
                    Id = 1000001,
                    UserId = 1300782624927961090,
                    Title = "8/31",
                    Text = "テスト投稿1",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt
                },

                new Diary {
                    Id = 1000002,
                    UserId = 1300782624927961090,
                    Title = "9/1",
                    Text = "テスト投稿2",
                    CreatedAt = createdAt.AddDays(1),
                    UpdatedAt = createdAt.AddDays(1)
                },

                new Diary {
                    Id = 1000003,
                    UserId = 1300782624927961090,
                    Title = "9/2",
                    Text = "テスト投稿3",
                    CreatedAt = createdAt.AddDays(2),
                    UpdatedAt = createdAt.AddDays(2)
                },
            };

            context.Diaries.AddRange(diaries);
            context.SaveChanges();
        }
    }
}