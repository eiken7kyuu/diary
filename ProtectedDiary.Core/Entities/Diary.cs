using System;
using System.ComponentModel.DataAnnotations;

namespace ProtectedDiary.Core.Entities
{
    public class Diary
    {
        public long Id { get; private set; }
        public long UserId { get; private set; }

        [Display(Prompt = "タイトル")]
        [Required(ErrorMessage = "必須入力です")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "100文字以内で入力してください")]
        public string Title { get; set; }

        [Display(Prompt = "本文")]
        [Required(ErrorMessage = "必須入力です")]
        public string Content { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime CreatedAt { get; private set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime UpdatedAt { get; private set; }

        public Diary() { }
        public Diary(long id, long userId, string title, string content, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}