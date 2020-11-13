using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProtectedDiary.Models
{
    public class Diary
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        [Display(Prompt = "タイトル")]
        [Required(ErrorMessage = "必須入力です")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "100文字以内で入力してください")]
        public string Title { get; set; }

        [Display(Prompt = "本文")]
        [Required(ErrorMessage = "必須入力です")]
        public string Content { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime CreatedAt { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime UpdatedAt { get; set; }

        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Diary>().ToTable("Diary");
            builder.Entity<Diary>().Property(d => d.Content).IsRequired();
            builder.Entity<Diary>().Property(d => d.Title).IsRequired();
            builder.Entity<Diary>().Property(d => d.UserId).IsRequired();
            builder.Entity<Diary>().Property(d => d.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd();

            builder.Entity<Diary>().Property(d => d.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd();

            builder.Entity<Diary>(entity =>
                entity.HasCheckConstraint("CK_Diary_UserId", @"""UserId"" > 0"));
        }
    }
}