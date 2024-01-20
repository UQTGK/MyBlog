namespace MyBlog.JWT.Utility.ApiResult
{
    public static class ApiResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "success",
                Total = 0
            };
        }
        public static ApiResult Success(dynamic data,int total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "success",
                Total = total
            };
        }

        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Msg = msg,
                Data = null,
                Total = 0
            };
        }
    }
}
