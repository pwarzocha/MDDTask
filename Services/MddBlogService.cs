using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using MDDConsoleApp.Models;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http;

namespace MDDConsoleApp.Services
{
    internal class MddBlogService : IMddBlogService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MddBlogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<PostCommented>> GetPostsCommented(string postsUrl, string commentsUrl)
        {
            var postsCommented = CombinePostsWithComments(postsUrl, commentsUrl);
            return postsCommented;
        }


        public List<PostCommented> CombinePostsWithComments(string postsUrl, string commentsUrl)
        {
            List<PostCommented> postsCommented = new List<PostCommented>();

            var postsUncommentedResponseJson = GetJsonFromUrl(postsUrl).Result;
            var commentsResponseJson = GetJsonFromUrl(commentsUrl).Result;


            var postsUncommented = DeserializePostsUncommented(postsUncommentedResponseJson);
            var comments = DeserializeComments(commentsResponseJson);

            foreach (var postUncommented in postsUncommented)
            {
                PostCommented postCommented = new PostCommented();
                postCommented.Post = postUncommented;
                postCommented.Comments = new List<Comment>();
                foreach (var comment in comments)
                {
                    if (comment.PostId == postUncommented.Id)
                    {
                        postCommented.Comments.Add(comment);
                    }
                }
                postsCommented.Add(postCommented);
            }

            return postsCommented;
        }

        //TODO GENERIC TYPE METHOD
        private List<PostUncommented> DeserializePostsUncommented(string postsUncommentedResponseJson)
        {
            try
            {
                List<PostUncommented> postsUncommented = JsonConvert.DeserializeObject<IEnumerable<PostUncommented>>(postsUncommentedResponseJson).ToList();
                return postsUncommented;
            } catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return null;
        }

        private List<Comment> DeserializeComments(string commentsResponseJson)
        {
            try
            {
                List<Comment> comments = JsonConvert.DeserializeObject<IEnumerable<Comment>>(commentsResponseJson).ToList();
                return comments;
            } catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return null;
        }

        private async Task<string> GetJsonFromUrl(string url)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;

            } catch (Exception exception) {
                Console.WriteLine(exception);
            }
            return null;
        }
    }
}
