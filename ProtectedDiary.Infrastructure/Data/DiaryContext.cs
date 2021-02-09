using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Infrastructure.Data
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
            modelBuilder.Entity<Diary>(ConfigureDiary);
        }

        private void ConfigureDiary(EntityTypeBuilder<Diary> builder)
        {
            builder.ToTable("Diary");
            builder.Property(d => d.Content).IsRequired();
            builder.Property(d => d.Title).IsRequired();
            builder.Property(d => d.UserId).IsRequired();
            builder.Property(d => d.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd();

            builder.Property(d => d.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd();

            builder.HasCheckConstraint("CK_Diary_UserId", @"""UserId"" > 0");
        }
    }
}