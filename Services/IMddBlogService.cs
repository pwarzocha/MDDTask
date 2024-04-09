using MDDConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDDConsoleApp.Services
{
    public interface IMddBlogService
    {
        List<PostCommented> CombinePostsWithComments(string postsUrl, string commentsUrl);
        Task<List<PostCommented>> GetPostsCommented(string postsUrl, string commentsUrl);
    }
}
