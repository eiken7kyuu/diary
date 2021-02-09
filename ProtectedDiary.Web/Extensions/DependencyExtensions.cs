using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtectedDiary.Application.Interfaces;
using ProtectedDiary.Application.Services;
using ProtectedDiary.Core.Repositories;
using ProtectedDiary.Infrastructure.Helpers;
using ProtectedDiary.Infrastructure.Repository;
using ProtectedDiary.Core.TwitterAuth;
using ProtecteDiary.Infrastructure.Repository;

namespace ProtectedDiary.Web.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var twitterConfig = new TwitterConfiguration(configuration["ConsumerKey"], configuration["ConsumerSecret"]);
            services.AddSingleton(twitterConfig);
            services.AddHttpContextAccessor();
            services.AddScoped<IDiaryService, DiaryService>();
            services.AddScoped<IDiaryRepository, DiaryRepository>();
            services.AddScoped<ISanitizer, Sanitizer>();
            services.AddScoped<ITwitterRepository, TwitterRepository>();
            services.AddScoped<ITwitterService, TwitterService>();
        }
    }
}