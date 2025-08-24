using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyGameList.Src.Shared.Commons
{
    public class ResponseHandler
    {
        public ActionResult<ApiResponse<T>> Result<T>(HttpStatusCode statusCode, string message, T? data)
        {
            var apiResponse = new ApiResponse<T>((int)statusCode, message, data);
            return new ObjectResult(apiResponse)
            {
                StatusCode = (int)statusCode,
            };
        }

        public ActionResult<ApiResponse> Result(HttpStatusCode statusCode, string message)
        {
            var apiResponse = new ApiResponse((int)statusCode, message);
            return new ObjectResult(apiResponse)
            {
                StatusCode = (int)statusCode,
            };
        }

        public ActionResult<ApiResponse<T>> Result<T>(Response<T> response)
        {
            var apiResponse = new ApiResponse<T>((int)response.HttpStatusCode, response.Message, response.Data);
            return new ObjectResult(apiResponse)
            {
                StatusCode = (int)response.HttpStatusCode,
            };
        }

        public ActionResult<ApiResponse> Result(Response response)
        {
            var apiResponse = new ApiResponse((int)response.HttpStatusCode, response.Message);
            return new ObjectResult(apiResponse)
            {
                StatusCode = (int)response.HttpStatusCode,
            };
        }
    }
}
