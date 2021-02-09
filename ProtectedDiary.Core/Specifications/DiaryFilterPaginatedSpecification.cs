using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Core.Specifications
{
    public class DiaryFilterPaginatedSpecification : BaseSpecification<Diary>
    {
        public DiaryFilterPaginatedSpecification(long useId, int skip, int take)
            : base(d => d.UserId == useId)
        {
            ApplyOrderByDescending(d => d.CreatedAt);
            ApplyPaging(skip, take);
        }
    }
}