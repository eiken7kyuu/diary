using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Services
{
    public class RelationshipService : IRelationshipService
    {
        private readonly ITwitterApi _twitterApi;

        public RelationshipService(ITwitterApi twitterApi)
        {
            _twitterApi = twitterApi;
        }

        public async Task<Author> DiaryAuthor(long diaryUserId, IEnumerable<Claim> claims)
        {
            if (diaryUserId == claims.UserId())
            {
                return Author.You;
            }

            return await _twitterApi.IsMutualFollow(diaryUserId, claims)
                ? Author.MutualFollowers
                : Author.NotMutualFollowers;
        }
    }
}