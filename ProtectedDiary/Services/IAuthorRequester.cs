using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public interface IAuthorRequester
    {
        Task<Author> GetAuthor(long userId, IEnumerable<Claim> claims);
    }
}