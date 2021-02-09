using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Application.Services;
using ProtectedDiary.Core.Entities;
using ProtectedDiary.Core.Repositories;
using ProtectedDiary.Core.Specifications;
using Xunit;

namespace ProtectedDiary.Application.Tests
{
    public class DiaryTests
    {
        private Mock<IDiaryRepository> _mockDiaryRepository;
        private Mock<ISanitizer> _mockSanitizer;

        private Diary _testDiary;

        public DiaryTests()
        {
            _mockDiaryRepository = new Mock<IDiaryRepository>();
            _mockSanitizer = new Mock<ISanitizer>();
            _testDiary = new Diary(
                It.IsAny<long>(), It.IsAny<long>(),
                It.IsAny<string>(), "sample content",
                DateTime.Now,
                DateTime.Now
            );
        }

        [Fact]
        public async Task 日記の取得()
        {
            _mockDiaryRepository
                .Setup(x => x.GetAsync(It.IsAny<BaseSpecification<Diary>>()))
                .ReturnsAsync(It.IsAny<IReadOnlyList<Diary>>());
            var diaryService = new DiaryService(_mockDiaryRepository.Object, _mockSanitizer.Object);

            var diaries = await diaryService.GetDiaries(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>());
            _mockDiaryRepository.Verify(x => x.GetAsync(It.IsAny<BaseSpecification<Diary>>()), Times.Once);
        }

        [Fact]
        public async Task 日記の新規投稿()
        {
            _mockDiaryRepository.Setup(x => x.AddAsync(It.IsAny<Diary>())).ReturnsAsync(_testDiary);
            var diaryService = new DiaryService(_mockDiaryRepository.Object, _mockSanitizer.Object);
            var createdDiaryDto = await diaryService.Create(_testDiary, _testDiary.Id);
            _mockDiaryRepository.Verify(x => x.AddAsync(It.IsAny<Diary>()), Times.Once);
        }


        [Fact]
        public async Task 日記の更新()
        {
            _mockDiaryRepository.Setup(x => x.GetByIdAsync(_testDiary.Id)).ReturnsAsync(_testDiary);
            _mockDiaryRepository.Setup(x => x.UpdateAsync(It.IsAny<Diary>()));
            var diaryService = new DiaryService(_mockDiaryRepository.Object, _mockSanitizer.Object);
            await diaryService.Update(_testDiary, _testDiary.Id);
            _mockDiaryRepository.Verify(x => x.UpdateAsync(It.IsAny<Diary>()), Times.Once);
        }

        [Fact]
        public async Task 日記の削除()
        {
            _mockDiaryRepository.Setup(x => x.DeleteAsync(It.IsAny<Diary>()));
            var diaryService = new DiaryService(_mockDiaryRepository.Object, _mockSanitizer.Object);
            await diaryService.Delete(It.IsAny<Diary>());
            _mockDiaryRepository.Verify(x => x.DeleteAsync(It.IsAny<Diary>()), Times.Once);
        }

        [Fact]
        public async Task ユーザーの総日記数()
        {
            long userId = 1001;
            var diaryService = new DiaryService(_mockDiaryRepository.Object, _mockSanitizer.Object);
            await diaryService.GetDiaryCountByUser(userId);
            _mockDiaryRepository.Verify(x => x.CountAsync(x => x.UserId == userId), Times.Once);
        }
    }
}