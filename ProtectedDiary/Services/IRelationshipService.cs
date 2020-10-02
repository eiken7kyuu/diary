using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public interface IRelationshipService
    {
        Task<Author> DiaryAuthor(long diaryUserId, IEnumerable<Claim> claims);
    }
}