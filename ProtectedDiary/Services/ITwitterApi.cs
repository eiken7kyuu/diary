using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public interface ITwitterApi
    {
        Task<(bool, bool)> GetRelationship(long userId, IEnumerable<Claim> claims);
        Task<Author> GetUser(long userId, IEnumerable<Claim> claims);
    }
}