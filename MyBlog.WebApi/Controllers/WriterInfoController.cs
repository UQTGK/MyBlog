using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebApi.Utility._MD5;
using MyBlog.WebApi.Utility.ApiResult;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _iwriterInfoService;
        public WriterInfoController(IWriterInfoService iwriterInfoService)
        {
            _iwriterInfoService = iwriterInfoService;
        }

        [HttpGet("Create")]
        public async Task<ApiResult> Create(string name, string username, string userpwd)
        {
            WriterInfo writerInfo = new WriterInfo
            {
                Name = name,
                UserName = username,
                UserPwd = MD5Helper.MD5Encrypt32(userpwd)
            };
            //判断用户名是否存在
            var oldWriter = await _iwriterInfoService.FindAsync(c => c.UserName == username);
            if (oldWriter != null)
            {
                return ApiResultHelper.Error("username is exist");
            }
            bool b = await _iwriterInfoService.CreateAsync(writerInfo);
            if (!b)
            {
                return ApiResultHelper.Error("create fail");
            }
            return ApiResultHelper.Success(b);
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            //未完成的方法
            return ApiResultHelper.Error("edit fail");
        }
    }
}
