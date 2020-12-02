using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProtectedDiary.Models;
using ProtectedDiary.TwitterAuth;
using Tweetinvi;
using Tweetinvi.Models;

namespace ProtectedDiary.Services
{
    public class TwitterApi : ITwitterApi
    {
        private readonly TwitterConfiguration _twitterConfig;
        public TwitterApi(TwitterConfiguration twitterConfig)
        {
            _twitterConfig = twitterConfig;
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

        public async Task<(bool, bool)> GetRelationship(long targetUserId, IEnumerable<Claim> claims)
        {
            var client = CreateTwitterClient(claims);
            var relationship = await client.Users.GetRelationshipBetweenAsync(claims.UserId(), targetUserId);
            return (relationship.FollowedBy, relationship.Following);
        }

        public async Task<TwitterUser> GetUser(long userId, IEnumerable<Claim> claims)
        {
            var client = CreateTwitterClient(claims);
            var user = await client.Users.GetUserAsync(userId);
            return new TwitterUser(user.Id, user.ScreenName, user.ProfileImageUrl400x400);
        }
    }
}