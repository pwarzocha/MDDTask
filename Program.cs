using System.Security.Cryptography.X509Certificates;
using MDDConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Collections.Specialized;
using MDDConsoleApp.Models;

namespace MDDConsoleApp
{
    internal class Program
    {
        private static IMddBlogService? _mddBlogService;
        static async Task Main(string[] args) {
            Configure();
            var postsUrl = ConfigurationManager.AppSettings.Get("MddJsonPostsUrl");
            var CommentsUrl = ConfigurationManager.AppSettings.Get("MddJsonCommentsUrl");
            var PostsCommented = await _mddBlogService.GetPostsCommented(postsUrl, CommentsUrl);

        }

        

        static void Configure()
        {
            HostApplicationBuilder appBuilder = Host.CreateApplicationBuilder();

            appBuilder.Services.AddHttpClient();
            appBuilder.Services.AddTransient<IMddBlogService, MddBlogService>();
            var serviceProvider = appBuilder.Services.BuildServiceProvider();
            _mddBlogService = serviceProvider.GetService<IMddBlogService>();
        }
    }
}
