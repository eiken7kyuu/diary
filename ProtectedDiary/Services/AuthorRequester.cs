using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProtectedDiary.Models;

namespace ProtectedDiary.Services
{
    public class AuthorRequester : IAuthorRequester
    {
        private ITwitterApi _twitterApi;
        private readonly ILogger<AuthorRequester> _logger;


        public AuthorRequester(ITwitterApi twitterApi, ILogger<AuthorRequester> logger)
        {
            _twitterApi = twitterApi;
            _logger = logger;
        }

        public async Task<Author> GetAuthor(long userId)
        {
            try
            {
                var user = await _twitterApi.GetUser(userId);
                (var followedBy, var following) = await _twitterApi.GetRelationship(userId);
                return new Author(user, followedBy, following);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}