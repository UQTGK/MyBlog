using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebApi.Utility.ApiResult;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;
        public TypeController(ITypeInfoService iTypeInfoService)
        {
            _iTypeInfoService = iTypeInfoService;
        }

        [HttpGet("Types")]
        public async Task<ApiResult> Types()
        {
            var types = await _iTypeInfoService.QueryAsync();
            if (types.Count == 0)
            {
                return ApiResultHelper.Error("no data");
            }
            return ApiResultHelper.Success(types);
        }
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                return ApiResultHelper.Error("name is null");
            }
            TypeInfo typeInfo = new TypeInfo
            {
                Name = name
            };
            bool b = await _iTypeInfoService.CreateAsync(typeInfo);
            if (!b)
            {
                return ApiResultHelper.Error("create fail");
            }
            return ApiResultHelper.Success(b);
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id,string name)
        {
            var type = await _iTypeInfoService.FindAsync(id);
            if(type == null)
            {
                return ApiResultHelper.Error("no data");
            }
            type.Name = name;
            bool b = await _iTypeInfoService.EditAsync(type);
            if (!b)
            {
                return ApiResultHelper.Error("edit fail");
            }
            return ApiResultHelper.Success(type);
        }
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            bool b = await _iTypeInfoService.DeleteAsync(id);
            if (!b)
            {
                return ApiResultHelper.Error("delete fail");
            }
            return ApiResultHelper.Success(b);
        }
    }
}
