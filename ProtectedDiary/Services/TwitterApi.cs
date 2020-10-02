using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public async Task<bool> IsMutualFollow(long diaryUserId, IEnumerable<Claim> claims)
        {
            var client = CreateTwitterClient(claims);
            var relationship = await client.Users.GetRelationshipBetweenAsync(claims.UserId(), diaryUserId);
            return relationship.FollowedBy && relationship.Following;
        }

    }
}