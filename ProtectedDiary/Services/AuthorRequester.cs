using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary.Services
{
    public class AuthorRequester : IAuthorRequester
    {
        private ITwitterApi _twitterApi;

        public AuthorRequester(ITwitterApi twitterApi)
        {
            _twitterApi = twitterApi;
        }

        public async Task<Author> GetAuthor(long userId, IEnumerable<Claim> claims)
        {
            var author = await _twitterApi.GetUser(userId, claims);
            author.Relationship = await GetAuthorRelationship(userId, claims);
            return author;
        }

        private async Task<AuthorRelationship> GetAuthorRelationship(long diaryUserId, IEnumerable<Claim> claims)
        {
            if (diaryUserId == claims.UserId())
            {
                return AuthorRelationship.You;
            }

            (var followedBy, var following) = await _twitterApi.GetRelationship(diaryUserId, claims);
            return followedBy && following
                ? AuthorRelationship.MutualFollowers
                : AuthorRelationship.NotMutualFollowers;
        }
    }
}