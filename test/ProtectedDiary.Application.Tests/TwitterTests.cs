using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using ProtectedDiary.Application.Services;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Repositories;
using Xunit;

namespace ProtectedDiary.Application.Tests
{
    public class TwitterTests
    {
        private Mock<ITwitterRepository> _mockTwitterRepository;
        private Mock<ILogger<TwitterService>> _mockLoggerTwitter;

        private User _testTwitterUser;

        public TwitterTests()
        {
            _mockTwitterRepository = new Mock<ITwitterRepository>();
            _mockLoggerTwitter = new Mock<ILogger<TwitterService>>();
            _testTwitterUser = new User(
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            );
        }

        [Fact]
        public async Task ユーザー取得()
        {
            _mockTwitterRepository
                .Setup(x => x.GetUser(It.IsAny<long>()))
                .ReturnsAsync(_testTwitterUser);
            var twitterService = new TwitterService(_mockTwitterRepository.Object, _mockLoggerTwitter.Object);

            var user = await twitterService.GetUser(It.IsAny<long>());
            _mockTwitterRepository.Verify(x => x.GetUser(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task リレーションシップ取得()
        {
            _mockTwitterRepository
                 .Setup(x => x.GetRelationship(It.IsAny<long>()))
                 .ReturnsAsync((true, true));
            var twitterService = new TwitterService(_mockTwitterRepository.Object, _mockLoggerTwitter.Object);

            var user = await twitterService.GetRelationship(It.IsAny<long>());
            _mockTwitterRepository.Verify(x => x.GetRelationship(It.IsAny<long>()), Times.Once);
        }
    }
}