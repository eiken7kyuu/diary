using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Repositories;
using ProtectedDiary.Infrastructure.Data;
using ProtectedDiary.Infrastructure.Repository.Base;

namespace ProtecteDiary.Infrastructure.Repository
{
    public class DiaryRepository : Repository<Diary>, IDiaryRepository
    {
        public DiaryRepository(DiaryContext diaryContext) : base(diaryContext)
        {
        }
    }
}