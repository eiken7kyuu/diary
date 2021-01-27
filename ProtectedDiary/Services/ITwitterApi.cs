using System.Threading.Tasks;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public interface ITwitterApi
    {
        Task<(bool, bool)> GetRelationship(long userId);
        Task<TwitterUser> GetUser(long userId);
    }
}