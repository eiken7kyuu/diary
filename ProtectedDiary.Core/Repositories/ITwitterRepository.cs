using System.Threading.Tasks;
using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Core.Repositories
{
    public interface ITwitterRepository
    {
        Task<(bool, bool)> GetRelationship(long userId);
        Task<User> GetUser(long userId);
    }
}