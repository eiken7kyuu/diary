using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Repositories;

namespace ProtectedDiary.Application.Services
{
    public class TwitterService : ITwitterService
    {
        private ITwitterRepository _twitterRepository;
        private readonly ILogger<TwitterService> _logger;

        public TwitterService(ITwitterRepository twitterRepository, ILogger<TwitterService> logger)
        {
            _twitterRepository = twitterRepository;
            _logger = logger;
        }

        public async Task<User> GetUser(long userId)
        {
            return await _twitterRepository.GetUser(userId);
        }

        public async Task<(bool followedBy, bool following)> GetRelationship(long userId)
        {
            return await _twitterRepository.GetRelationship(userId);
        }
    }
}