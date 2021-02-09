using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Repositories;
using ProtectedDiary.Core.Specifications;

namespace ProtectedDiary.Application.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly ISanitizer _sanitizer;

        private const int PAGE_SIZE = 20;

        public DiaryService(IDiaryRepository diaryRepository, ISanitizer sanitizer)
        {
            _diaryRepository = diaryRepository;
            _sanitizer = sanitizer;
        }


        public async Task<Diary> GetDiaryById(long diaryId)
        {
            return await _diaryRepository.GetByIdAsync(diaryId);
        }

        public async Task<IEnumerable<Diary>> GetDiaries(long userId, int pageIndex, int pageSize)
        {
            var spec = new DiaryFilterPaginatedSpecification(userId, (pageIndex - 1) * pageSize, pageSize);
            return await _diaryRepository.GetAsync(spec);
        }

        public async Task<Diary> Create(Diary diary, long userId)
        {
            var newDiary = new Diary(
                0, userId, diary.Title,
                _sanitizer.Sanitize(diary.Content),
                DateTime.Now, DateTime.Now
            );
            return await _diaryRepository.AddAsync(newDiary);
        }

        public async Task Update(Diary diary, long id)
        {
            var diaryToUpdate = await _diaryRepository.GetByIdAsync(id);
            diaryToUpdate.Title = diary.Title;
            diaryToUpdate.Content = _sanitizer.Sanitize(diary.Content);
            await _diaryRepository.UpdateAsync(diaryToUpdate);
        }

        public async Task Delete(Diary diary)
        {
            await _diaryRepository.DeleteAsync(diary);
        }

        public async Task<int> GetDiaryCountByUser(long userId)
        {
            return await _diaryRepository.CountAsync(d => d.UserId == userId);
        }
    }
}