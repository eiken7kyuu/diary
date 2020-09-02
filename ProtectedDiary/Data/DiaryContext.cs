using Microsoft.EntityFrameworkCore;
using ProtectedDiary.Models;

namespace ProtectedDiary.Data
{
    public class DiaryContext : DbContext
    {
        public DiaryContext(DbContextOptions<DiaryContext> options)
            : base(options)
        {
        }

        public DbSet<Diary> Diaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Diary.OnModelCreating(modelBuilder);
        }
    }
}