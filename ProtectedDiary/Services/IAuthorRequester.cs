using System.Threading.Tasks;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public interface IAuthorRequester
    {
        Task<Author> GetAuthor(long userId);
    }
}