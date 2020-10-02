

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public interface ITwitterApi
    {
        Task<bool> IsMutualFollow(long diaryUserId, IEnumerable<Claim> claims);
    }
}