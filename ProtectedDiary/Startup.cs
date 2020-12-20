using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtectedDiary.Data;
using ProtectedDiary.Services;
using ProtectedDiary.TwitterAuth;

namespace ProtectedDiary
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Diary");
                options.Conventions.AuthorizePage("/UserDiaries");
                options.Conventions.AuthorizeFolder("/Diaries");
            });

            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
                services.AddDbContext<DiaryContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DiaryContext")));
            }
            else
            {
                var uri = new Uri(Configuration["DATABASE_URL"]);
                var userInfo = uri.UserInfo.Split(":");
                (var user, var password) = (userInfo[0], userInfo[1]);
                var db = Path.GetFileName(uri.AbsolutePath);

                var connStr = $"Host={uri.Host};Port={uri.Port};Database={db};Username={user};Password={password};Enlist=true";
                services.AddDbContext<DiaryContext>(options => options.UseNpgsql(connStr));
            }

            builder.AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/UserDiaries", "/{userId:long}");
                options.Conventions.AddPageRoute("/Diary", "/{userId:long}/diaries/{id:long}");
                options.Conventions.AddPageRoute("/Diaries/Edit", "/diaries/{id:long}/Edit");
                options.Conventions.AddPageRoute("/Diaries/Delete", "/diaries/{id:long}/Delete");
            });

            var twitterConfig = new TwitterConfiguration(Configuration["ConsumerKey"], Configuration["ConsumerSecret"]);
            services.AddSingleton(twitterConfig);
            services.AddTransient<ITwitterApi, TwitterApi>();
            services.AddTransient<IAuthorRequester, AuthorRequester>();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddTwitter(options =>
            {
                options.ConsumerKey = twitterConfig.ConsumerKey;
                options.ConsumerSecret = twitterConfig.ConsumerSecret;
                options.RetrieveUserDetails = true;
                options.Events.OnCreatingTicket = context =>
                {
                    // ユーザーのアクセストークンをClaimに格納
                    var identity = context.Principal.Identity as ClaimsIdentity;
                    identity.AddClaim(new Claim(TwitterClaimTypes.AccessToken, context.AccessToken));
                    identity.AddClaim(new Claim(TwitterClaimTypes.AccessTokenSecret, context.AccessTokenSecret));
                    return Task.CompletedTask;
                };

                options.Events.OnRemoteFailure = context =>
                {
                    var appUrl = $"https://{context.Request.Host}/";
                    context.Response.Redirect(appUrl);
                    context.HandleResponse();
                    return Task.CompletedTask;
                };
            })
            .AddCookie(options =>
            {
                // ログインしてなかったときのリダイレクト先(ログイン先)
                options.LoginPath = "/auth/signin";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/StatusError", "?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
