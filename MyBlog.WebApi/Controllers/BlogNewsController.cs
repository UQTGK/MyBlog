using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebApi.Utility.ApiResult;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _blogNewsService;
        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            _blogNewsService = blogNewsService;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
           var data = await _blogNewsService.QueryAsync();
           if (data == null)
           {
               return ApiResultHelper.Error("no data");
           }    
           return ApiResultHelper.Success(data);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title, string content,int typeid)
        {
            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeid,
                WriterId = 1
            };
            bool b = await _blogNewsService.CreateAsync(blogNews);
            if (!b)
            {
                return ApiResultHelper.Error("create fail");
            }
            return ApiResultHelper.Success(blogNews);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _blogNewsService.DeleteAsync(id);
            if (!b)
            {
                return ApiResultHelper.Error("delete fail");
            }
            return ApiResultHelper.Success(b);
        }
        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id,string title,string content,int typeid)
        {
            var blogNews = await _blogNewsService.FindAsync(id);
            if (blogNews == null)
            {
                return ApiResultHelper.Error("no data");
            }
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeid;
            bool b = await _blogNewsService.EditAsync(blogNews);
            if (!b)
            {
                return ApiResultHelper.Error("edit fail");
            }
            return ApiResultHelper.Success(blogNews);
        }

    }
}
