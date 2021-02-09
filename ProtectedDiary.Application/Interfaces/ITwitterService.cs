using System.Threading.Tasks;
using ProtectedDiary.Core.Entities;

namespace ProtectedDiary.Application.Interfaces
{
    public interface ITwitterService
    {
        Task<(bool followedBy, bool following)> GetRelationship(long userId);
        Task<User> GetUser(long userId);
    }
}