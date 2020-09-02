using System;
using Microsoft.EntityFrameworkCore;

namespace ProtectedDiary.Models
{
    public class Diary
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Diary>().ToTable("Diary");
            builder.Entity<Diary>().Property(d => d.Text).IsRequired();
            builder.Entity<Diary>().Property(d => d.Title).IsRequired();
            builder.Entity<Diary>().Property(d => d.UserId).IsRequired();
            builder.Entity<Diary>().Property(d => d.UpdatedAt).IsRequired();
            builder.Entity<Diary>().Property(d => d.CreatedAt).IsRequired();
        }
    }
}