using System.Collections.Generic;
using System.Threading.Tasks;
using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Application.Interfaces
{
    public interface IDiaryService
    {
        Task<IEnumerable<Diary>> GetDiaries(long userId, int pageIndex, int pageSize);
        Task<Diary> GetDiaryById(long diaryId);
        Task<Diary> Create(Diary diary, long userId);
        Task Update(Diary diary, long diaryId);
        Task Delete(Diary diary);

        Task<int> GetDiaryCountByUser(long userId);
    }
}