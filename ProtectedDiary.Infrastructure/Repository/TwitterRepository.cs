using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Repositories;
using ProtectedDiary.Core.TwitterAuth;
using Tweetinvi;
using Tweetinvi.Models;

namespace ProtectedDiary.Infrastructure.Repository
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly TwitterConfiguration _twitterConfig;
        private readonly TwitterClient _client;
        private readonly long _myUserId;

        public TwitterRepository(TwitterConfiguration twitterConfig, IHttpContextAccessor httpContextAccessor)
        {
            _twitterConfig = twitterConfig;
            var claims = httpContextAccessor.HttpContext.User.Claims;
            _client = CreateTwitterClient(claims);
            _myUserId = claims.UserId();
        }

        private TwitterClient CreateTwitterClient(IEnumerable<Claim> claims)
        {
            var credentials = new TwitterCredentials(
                _twitterConfig.ConsumerKey,
                _twitterConfig.ConsumerSecret,
                claims.AccessToken(),
                claims.AccessTokenSecret()
            );
            return new TwitterClient(credentials);
        }

        public async Task<(bool, bool)> GetRelationship(long targetUserId)
        {
            var relationship = await _client.Users.GetRelationshipBetweenAsync(_myUserId, targetUserId);
            return (relationship.FollowedBy, relationship.Following);
        }

        public async Task<User> GetUser(long userId)
        {
            var user = await _client.Users.GetUserAsync(userId);
            return new User(user.Id, user.ScreenName, user.ProfileImageUrl400x400);
        }
    }
}